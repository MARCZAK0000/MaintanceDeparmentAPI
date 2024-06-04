using AutoMapper;
using DUR_Application.Entities;
using DUR_Application.Exceptions;
using DUR_Application.Helper;
using DUR_Application.Model;
using DUR_Application.Services.Services_Malfunctions.ChangeMalfunctions;
using DUR_Application.Services.Services_Malfunctions.CloseMalfunction;
using DUR_Application.Services.Services_Malfunctions.CreateMalfunction;
using DUR_Application.Services.Services_Malfunctions.GetMalfunctions;
using DUR_Application.Services.Services_Malfunctions.GetMalfunctions.GetAllMalfunctionsQuery;
using DUR_Application.Services.Services_Malfunctions.GetMalfunctions.GetClosedMalfunctionsQuery;
using DUR_Application.Services.Services_Malfunctions.UpdateMalfunctions;
using DUR_Application.Services.Services_User.UserContexServices;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DUR_Application.Services.Services_Malfunctions.MalfunctionsServicesController
{
    public class MalfunctionService : IMalfunctionsServices
    {
        private readonly DatabaseContext _databaseContext;

        private readonly IMapper _mapper;

        private readonly ILogger<MalfunctionService> _logger;

        private readonly IUserContextServices _userContextServices;

        public MalfunctionService(DatabaseContext databaseContext, IMapper mapper, ILogger<MalfunctionService> logger, IUserContextServices userContextServices)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
            _logger = logger;
            _userContextServices = userContextServices;
        }

        public async Task CreateMalfunctions(CreateMalfunctionDto create, string LaneNumber, int MachineId)
        {
            var getMachine = await _databaseContext.Machines
                .Where(pr=>pr.LaneNumber==LaneNumber)
                .FirstOrDefaultAsync(pr=>pr.Id==MachineId);

            if(getMachine is null) 
            {
                throw new NotFoundException($"Create Malfunction: Cannon't find {MachineId} on {LaneNumber}");
            }

            var newMalfunctions = new MalfunctionRequest()
            {
                Name = create.Name,
                LaneNumber = LaneNumber,
                CreatedTime = create.CreatedTime,
                MachineId = MachineId,
                RequestTypeId = create.RequestTypeId,
                RequestStatusId = create.RequestStatusId
                

            };

            await _databaseContext.MalfunctionRequests.AddAsync(newMalfunctions);
            await _databaseContext.SaveChangesAsync();



        }

        public async Task UpdateMalfunctions(UpdateMalfunctionsDto update)
        {
            var getMalfunctions = await _databaseContext.MalfunctionRequests
                .FirstOrDefaultAsync(pr=>pr.Id ==update.Id);


            var getUser = await _databaseContext.Users.FirstAsync(pr => pr.Id == _userContextServices.GetUserId);

            if(getMalfunctions is null)
            {
                throw new NotFoundException("Update malfunctions: we cannot find malfunctions with that id");
            }

            getMalfunctions.Description = update.Describiton;
            getMalfunctions.UserId = getUser.Id;
            getMalfunctions.UserNumber = getUser.UserCode;
            getMalfunctions.RequestStatusId= update.RequestedStatusId;
            getMalfunctions.RequestTypeId = update.RequestTypeId;

            if(update.SpareParts.Any())
            {
                var listPart = new List<SparePart>();
                foreach(var part in update.SpareParts)
                {
                    listPart.Add(new SparePart
                    {
                        Id = part.Id,
                        Name = part.Name,
                        Type = part.Type,
                    });
                }
                getMalfunctions.SpareParts.AddRange(listPart);
            }
            await _databaseContext.SaveChangesAsync();
        }


        public async Task CloseMalfucntion(CloseMalfunctioDto close)
        {
            var getMalfunctions = await _databaseContext.MalfunctionRequests
                .FirstOrDefaultAsync(pr=>pr.Id==close.Id);

            var getUser = await _databaseContext.Users.FirstAsync(pr => pr.Id == _userContextServices.GetUserId);

            if (getMalfunctions is null)
            {
                throw new NotFoundException("Close Malfunctions: we cannot find Malfcuntions with that id");
            }

            if(close.Describiton is  not null) getMalfunctions.Description = close.Describiton;
            getMalfunctions.WorkTime = close.WorkTime;
            getMalfunctions.LayoverTime = close.LayoverTime;
            getMalfunctions.RequestTypeId = close.RequestedTypeId;
            getMalfunctions.RequestStatusId = close.RequestStatusId;
            getMalfunctions.UserNumber = getUser.UserCode;
            getMalfunctions.UserId = getUser.Id;

            if (close.SpareParts.Any())
            {
                var listPart = new List<SparePart>();
                foreach (var part in close.SpareParts)
                {
                    listPart.Add(new SparePart
                    {
                        Id = part.Id,
                        Name = part.Name,
                        Type = part.Type,
                    });
                }
                getMalfunctions.SpareParts.AddRange(listPart);
            }

            await _databaseContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<GetMalfunctionsDto>> GetAllMalfunctions(GetAllMalfunctionsQuery query)
        {
            var RequestStatus = query.StatusOfRequest.ToString();
            var baseQuery = _databaseContext.MalfunctionRequests
                .Include(pr => pr.RequestStatus)
                .Include(pr => pr.Machine)
                .Include(pr => pr.RequestType)
                .Include(pr => pr.SpareParts)
                .Where(pr => pr.CreatedTime >= ConvertDate.ChangeToDateTime(query.DateTimeStart)
                            && pr.CreatedTime < ConvertDate.ChangeToDateTime(query.DateTimeEnd)
                                && (query.LaneNumber == null || pr.LaneNumber.ToLower().Contains(query.LaneNumber))
                                    && pr.RequestStatus.Status == RequestStatus);
                

            if(!string.IsNullOrEmpty(query.SortBy))
            {
                var columnSelector = new Dictionary<string, Expression<Func<MalfunctionRequest, object>>>{
                    { nameof(MalfunctionRequest.CreatedTime), pr=>pr.CreatedTime },
                    { nameof(MalfunctionRequest.RequestType.Name), pr=>pr.RequestType.Name }
                };

                var selectedColumn = columnSelector[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.ASC ?
                    baseQuery.OrderBy(selectedColumn)
                    :baseQuery.OrderByDescending(selectedColumn);
            }
            

            var resultDto = await baseQuery.ToListAsync();
            var result = _mapper.Map<List<GetMalfunctionsDto>>(resultDto);

            return result;
        }

        public async Task<GetMalfunctionsDto> GetOneMalfunctions(int MalfunctionId)
        {
            var getMalfunction = await _databaseContext.MalfunctionRequests
                .Include(pr=>pr.RequestStatus)
                .Include(pr=>pr.Machine)
                .Include(pr=>pr.RequestType)
                .Include(pr => pr.SpareParts)
                .FirstOrDefaultAsync(pr=>pr.Id == MalfunctionId);


            if(getMalfunction is null) 
            {
                throw new NotFoundException($"GetOneMalfunction: We cannon find malfunctions with that id");
            }

            var result = _mapper.Map<GetMalfunctionsDto>(getMalfunction);
            return result;
        }

        public async Task<IEnumerable<GetMalfunctionsDto>> GetClosedMalfunctionsByDate(GetClosedMalfunctionsQueryDto queryDto)
        {
            
            
            var baseQuery = _databaseContext.MalfunctionRequests
                .Include(pr => pr.RequestStatus)
                .Include(pr => pr.Machine)
                .Include(pr => pr.RequestType)
                .Include(pr => pr.SpareParts)
                .Where(pr => (pr.CreatedTime >= ConvertDate.ChangeToDateTime(queryDto.DateTimeStart)
                                && pr.CreatedTime < ConvertDate.ChangeToDateTime(queryDto.DateTimeEnd))
                                        && pr.RequestStatus.Status == StatusOfRequest.Closed.ToString()
                                            && (queryDto.LaneNumber ==null || pr.LaneNumber.ToLower().Contains(queryDto.LaneNumber)));

            if (!string.IsNullOrEmpty(queryDto.SortBy))
            {
                var columnSelector = new Dictionary<string, Expression<Func<MalfunctionRequest, object>>>
                {
                    { nameof(MalfunctionRequest.CreatedTime) , r=>r.CreatedTime },
                    { nameof(MalfunctionRequest.LayoverTime), r=>r.LayoverTime},
                };

                var selectedColumn = columnSelector[queryDto.SortBy];

                baseQuery = queryDto.SortDirection == Model.SortDirection.ASC?
                    baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var resulQuery = await baseQuery.ToListAsync();


            if(!resulQuery.Any()) 
            {
                throw new NotFoundException($"{nameof(GetClosedMalfunctionsByDate)}: Wrong Data");
            }
            var resultDto = _mapper.Map<List<GetMalfunctionsDto>>(resulQuery);

            return resultDto;
        }

        public async Task<GetMalfunctionsDto> ChangeMalfunctions(int MalfunctionId, ChangeMalfunctionsDto changeMalfunctionsDto)
        {
            var getMalfunctions = await _databaseContext.MalfunctionRequests
                .Include(pr=> pr.RequestStatus)
                .Include(pr=>pr.Machine)
                .Include(pr=>pr.RequestType)
                .FirstOrDefaultAsync(pr=>pr.Id == MalfunctionId);

            if(getMalfunctions is null)
            {
                throw new NotFoundException($"ChangeMalfuncitons: There is no malfuncitons with that id: {MalfunctionId}");
            }

            if(changeMalfunctionsDto.Description is not null) getMalfunctions.Description = changeMalfunctionsDto.Description;
            if(changeMalfunctionsDto.WorkTime is not null) getMalfunctions.WorkTime = changeMalfunctionsDto.WorkTime;
            if(changeMalfunctionsDto.LayoverTime is not null) getMalfunctions.LayoverTime = changeMalfunctionsDto.LayoverTime;
            if(changeMalfunctionsDto.RequestTypeId is not null) getMalfunctions.RequestTypeId = (int)changeMalfunctionsDto.RequestTypeId;
            getMalfunctions.RequestStatusId = changeMalfunctionsDto.RequestStatusId;

            await _databaseContext.SaveChangesAsync();

            getMalfunctions = await _databaseContext.MalfunctionRequests
                .Include(pr => pr.RequestStatus)
                .Include(pr => pr.Machine)
                .Include(pr => pr.RequestType)
                .FirstOrDefaultAsync(pr => pr.Id == MalfunctionId);


            var reuslt = _mapper.Map<GetMalfunctionsDto>(getMalfunctions);

            return reuslt;
        }

    }
}
