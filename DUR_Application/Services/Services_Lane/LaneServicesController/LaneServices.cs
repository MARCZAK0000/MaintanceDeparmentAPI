using AutoMapper;
using DUR_Application.Entities;
using DUR_Application.Exceptions;
using DUR_Application.Model;
using DUR_Application.Model.Response;
using DUR_Application.Services.Services_Lane.LaneDto.CreateLane;
using DUR_Application.Services.Services_Lane.LaneDto.ShowLane;
using DUR_Application.Services.Services_Lane.LaneDto.UpdateLane;
using Microsoft.EntityFrameworkCore;

namespace DUR_Application.Services.Services_Lane.LaneServicesController
{
    public class LaneServices : ILaneServices
    {
        private readonly DatabaseContext _dbContext;
        private readonly ILogger<LaneServices> _logger;
        private readonly IMapper _mapper;
        public LaneServices(DatabaseContext dbContext, ILogger<LaneServices> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<ShowLaneDto>> CreateLane(CreateLaneDto createLane)
        {
            if (createLane is null) throw new NotFoundException("Update: There is no informations in Request");

            var newLane = new Lane()
            {
                Number = createLane.Number,
                Describiton = createLane.Describtion
            };

            await _dbContext.Lanes.AddAsync(newLane);
            await _dbContext.SaveChangesAsync();

            var created = await _dbContext.Lanes.OrderBy(pr=>pr.Id).LastOrDefaultAsync();
            var createdDto = _mapper.Map<ShowLaneDto>(created);
            var createdId = created.Id;

            var responseBuilder = new ResponseBuilder<ShowLaneDto>();

            var result = responseBuilder
                .SetItem(createdDto)
                .SetId(createdId)
                .Build();

            _logger.LogInformation($"Admin add lane: {newLane.Number}");

            return result;

        }

        public async Task<IEnumerable<ShowLaneDto>> GetAllLanes()
        {
            var getAllLanes = await _dbContext.Lanes
                .Include(r=>r.Machines)
                .ToListAsync();

            var result = _mapper.Map<IEnumerable<ShowLaneDto>>(getAllLanes); 

            return result;

        }

        public async Task<ShowLaneDto> GetLaneById(string laneID)
        {
            var getLane = await _dbContext.Lanes
                .Include(r=>r.Machines)
                .FirstAsync(pr=>pr.Number == laneID);

            if(getLane is null)
            {
                throw new NotFoundException($"{laneID} is not a lane");
            }

            var result = _mapper.Map<ShowLaneDto>(getLane);

            return result;
        }



        public async Task UpdateLane(UpdateLaneDto update, string LaneID)
        {
            var findLane = await _dbContext.Lanes.FirstAsync(pr => pr.Number == LaneID) ?? throw new NotFoundException("Update: Lane Not Found") ;

            if(update is null)
            {
                throw new NotFoundException("Update: Lane body request is null");
            }

            if(update.Describtion != string.Empty) findLane.Describiton = update.Describtion;

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Updated lane: {LaneID}");
        }






    }
}
