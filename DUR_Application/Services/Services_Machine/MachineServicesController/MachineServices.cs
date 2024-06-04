using AutoMapper;
using DUR_Application.Entities;
using DUR_Application.Exceptions;
using DUR_Application.Helper;
using DUR_Application.Model;
using DUR_Application.Model.Response;
using DUR_Application.Services.Services_Lane.LaneDto.ShowLane;
using DUR_Application.Services.Services_Machine.MachineDto.AddMachine;
using DUR_Application.Services.Services_Machine.MachineDto.GetMachines;
using DUR_Application.Services.Services_Machine.MachineDto.GetOneMachine;
using DUR_Application.Services.Services_Machine.ShowMachine;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace DUR_Application.Services.Services_Machine.MachineServicesController
{
    public class MachineServices : IMachineServices
    {
        private readonly DatabaseContext _databaseContext;

        private readonly IMapper _mapper;

        private readonly ILogger<MachineServices> _logger;

        public MachineServices(DatabaseContext databaseContext, IMapper mapper, ILogger<MachineServices> logger)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<ShowMachineDto>> AddMachine(AddMachineDto add, string LaneNumber)
        {
            var getLane = _databaseContext
                .Lanes
                .Include(pr=>pr.Machines)
                .First(pr=>pr.Number== LaneNumber);

            if(getLane is null)
            {
                throw new NotFoundException($"Create Lane: Can't find lane with {LaneNumber}");
            }

            add.LaneID = getLane.Id;
            var result = _mapper.Map<Machine>(add);
            await _databaseContext.Machines.AddAsync(result);
            await _databaseContext.SaveChangesAsync();


            var resultCreated = await _databaseContext.Machines.OrderBy(pr => pr.Id).LastOrDefaultAsync();
            var resultCreatedMapId = resultCreated.Id;
            var resultCreatdMap = _mapper.Map<ShowMachineDto>(resultCreated);

            _logger.LogInformation($"Add Machine: New Machine : {add.MachineName}, lane: {LaneNumber}");

            var responseBuilder = new ResponseBuilder<ShowMachineDto>();

            var response = responseBuilder
                 .SetItem(resultCreatdMap)
                 .SetId(resultCreatedMapId)
                 .SetTotalItemsCount(getLane.Machines.Count)
                 .Build();

            return response;
        }

        public async Task<IEnumerable<GetMachineDto>> GetAllMachines()
        {
            var getMachines = await _databaseContext.Machines.ToListAsync();

            var result = _mapper.Map<List<GetMachineDto>>(getMachines);

            return result;
        }



        public async Task<IEnumerable<GetMachineDto>> GetMachinesByLane(string LaneNumber)
        {
            var getMachines = await _databaseContext.Machines.Where(pr=>pr.LaneNumber==LaneNumber).ToListAsync();

            if(!getMachines.Any())
            {
                throw new NotFoundException($"Show Machine by lane: There is no machine at {LaneNumber}");
            }

            var result = _mapper.Map<List<GetMachineDto>>(getMachines);

            return result;
        }


        public async Task<GetOneMachineDto> GetOneMachine(string LaneNumber, string MachineName)
        {
            var getMachine = await _databaseContext.Machines
                .Include(pr=>pr.Malfunctions)
                .Where(pr=>pr.LaneNumber== LaneNumber)
                .FirstOrDefaultAsync(pr=>pr.MachineName.ToLower() == MachineName.ToLower());

            if(getMachine is null)
            {
                throw new NotFoundException($"Machine: We cannot find {MachineName} at {LaneNumber}");
            }

            var result = _mapper.Map<GetOneMachineDto>(getMachine);
            return result;
        }

        public async Task UpdateLaneMachines(string LaneNumber, string NewMachineNumber)
        {
            var getLane = await _databaseContext.Lanes
                .Include(pr=>pr.Machines)
                .Where(pr=>pr.Number==LaneNumber)
                .ToListAsync();

            foreach(var item in  getLane) 
            {
                item.Machines.ForEach(item =>
                {
                    item.LaneNumber = NewMachineNumber;
                });
            }

            await _databaseContext.SaveChangesAsync();
        }

    }
}
