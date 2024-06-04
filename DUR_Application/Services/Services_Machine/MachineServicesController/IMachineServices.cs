using DUR_Application.Model;
using DUR_Application.Model.Response;
using DUR_Application.Services.Services_Machine.MachineDto.AddMachine;
using DUR_Application.Services.Services_Machine.MachineDto.GetMachines;
using DUR_Application.Services.Services_Machine.MachineDto.GetOneMachine;
using DUR_Application.Services.Services_Machine.ShowMachine;

namespace DUR_Application.Services.Services_Machine.MachineServicesController
{
    public interface IMachineServices
    {
        Task<Response<ShowMachineDto>> AddMachine(AddMachineDto add, string LaneNumber);

        Task<IEnumerable<GetMachineDto>> GetAllMachines();

        Task<IEnumerable<GetMachineDto>> GetMachinesByLane(string LaneNumber);

        Task UpdateLaneMachines(string LaneNumber, string NewMachineNumber);

        Task<GetOneMachineDto> GetOneMachine (string  LaneNumber, string MachineName);

    }
}
