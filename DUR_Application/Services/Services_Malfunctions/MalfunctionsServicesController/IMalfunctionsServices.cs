using DUR_Application.Services.Services_Malfunctions.ChangeMalfunctions;
using DUR_Application.Services.Services_Malfunctions.CloseMalfunction;
using DUR_Application.Services.Services_Malfunctions.CreateMalfunction;
using DUR_Application.Services.Services_Malfunctions.GetMalfunctions;
using DUR_Application.Services.Services_Malfunctions.GetMalfunctions.GetAllMalfunctionsQuery;
using DUR_Application.Services.Services_Malfunctions.GetMalfunctions.GetClosedMalfunctionsQuery;
using DUR_Application.Services.Services_Malfunctions.UpdateMalfunctions;

namespace DUR_Application.Services.Services_Malfunctions.MalfunctionsServicesController
{
    public interface IMalfunctionsServices
    {
        Task CreateMalfunctions(CreateMalfunctionDto create, string LaneNumber, int MachineId);

        Task UpdateMalfunctions(UpdateMalfunctionsDto update);

        Task CloseMalfucntion (CloseMalfunctioDto close);

        Task<IEnumerable<GetMalfunctionsDto>> GetAllMalfunctions(GetAllMalfunctionsQuery query);

        Task<GetMalfunctionsDto> GetOneMalfunctions (int MalfunctionId);

        Task<IEnumerable<GetMalfunctionsDto>> GetClosedMalfunctionsByDate(GetClosedMalfunctionsQueryDto queryDto);

        Task<GetMalfunctionsDto> ChangeMalfunctions(int MalfunctionId, ChangeMalfunctionsDto changeMalfunctionsDto);

    }
}