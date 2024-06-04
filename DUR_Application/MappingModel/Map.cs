using AutoMapper;
using DUR_Application.Entities;
using DUR_Application.Services.Services_Lane.LaneDto.ShowLane;
using DUR_Application.Services.Services_Machine.MachineDto.AddMachine;
using DUR_Application.Services.Services_Machine.MachineDto.GetMachines;
using DUR_Application.Services.Services_Machine.MachineDto.GetOneMachine;
using DUR_Application.Services.Services_Machine.ShowMachine;
using DUR_Application.Services.Services_Magazine.AddSpareParts;
using DUR_Application.Services.Services_Magazine.ShowSpareParts;
using DUR_Application.Services.Services_Malfunctions.CreateMalfunction;
using DUR_Application.Services.Services_Malfunctions.GetMalfunctions;
using DUR_Application.Services.Services_Malfunctions.UpdateMalfunctions;

namespace DUR_Application.MappingModel
{
    public class Map : Profile
    {
        public Map()
        {
            CreateMap<Lane, ShowLaneDto>(); //Show Lane -> LaneServices
            CreateMap<Machine, ShowMachineDto>(); //Show Machine -> LaneServices

            CreateMap<AddMachineDto, Machine>(); //Add Machine -> MachineServices
            CreateMap<Machine, GetMachineDto>(); //Show Machines -> MachineServices
            CreateMap<Machine, GetOneMachineDto>(); //Show one Machine -> MachineServices

            CreateMap<MalfunctionRequest, ShowMalfuntctionsRequestDto>()
                .ForMember(req=>req.RequestTypeName, res=>res.MapFrom(from=>from.RequestType.Name)); //Show one Machine MalfunctionRequest -> MachineServices


            CreateMap<CreateMalfunctionDto, MalfunctionRequest>(); //Add Malfunctions -> MalfunctionsServies
            CreateMap<UpdateMalfunctionsDto, MalfunctionRequest>(); //Update Malfunctions to Pending -> MalfunctionsServes

            CreateMap<MalfunctionRequest, GetMalfunctionsDto>()
                .ForMember(req=>req.RequestTypeName, res=>res.MapFrom(from=>from.RequestType.Name))
                .ForMember(req=>req.RequestStatusName, res=>res.MapFrom(from=>from.RequestStatus.Status))
                .ForMember(req=>req.MachineName, res=>res.MapFrom(from=>from.Machine.MachineName));


            CreateMap<AddSparePartsDto, SparePart>(); //Add Sprae parts -> MagazineServices (All)


            CreateMap<SparePart, ShowSparePartsDto>(); //Show Spare parts -> MagazineServices 
        }
    }
}
