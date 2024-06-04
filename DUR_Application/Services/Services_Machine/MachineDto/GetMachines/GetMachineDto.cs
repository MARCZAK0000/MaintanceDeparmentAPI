using Microsoft.Identity.Client;

namespace DUR_Application.Services.Services_Machine.MachineDto.GetMachines
{
    public class GetMachineDto
    {
        public int Id { get; set; } 

        public string MachineName { get; set;}

        public string MachineDescription { get; set;}

        public string LaneNumber { get; set;}
    }
}
