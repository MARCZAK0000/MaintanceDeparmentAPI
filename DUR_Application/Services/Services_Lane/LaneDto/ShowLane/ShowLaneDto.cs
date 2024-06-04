using DUR_Application.Services.Services_Machine.ShowMachine;

namespace DUR_Application.Services.Services_Lane.LaneDto.ShowLane
{
    public class ShowLaneDto
    {
        public string Number { get; set; }

        public string Describiton { get; set; }

        public List<ShowMachineDto> Machines { get; set; }

    }
}
