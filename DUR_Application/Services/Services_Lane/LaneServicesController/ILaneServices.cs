using DUR_Application.Model;
using DUR_Application.Model.Response;
using DUR_Application.Services.Services_Lane.LaneDto.CreateLane;
using DUR_Application.Services.Services_Lane.LaneDto.ShowLane;
using DUR_Application.Services.Services_Lane.LaneDto.UpdateLane;

namespace DUR_Application.Services.Services_Lane.LaneServicesController
{
    public interface ILaneServices
    {
        Task<Response<ShowLaneDto>> CreateLane(CreateLaneDto createLane);

        Task UpdateLane(UpdateLaneDto update, string LaneID);

        Task<IEnumerable<ShowLaneDto>> GetAllLanes();

        Task<ShowLaneDto> GetLaneById(string laneID);
        
    }
}
