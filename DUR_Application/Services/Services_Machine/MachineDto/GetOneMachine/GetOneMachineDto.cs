namespace DUR_Application.Services.Services_Machine.MachineDto.GetOneMachine
{
    public class GetOneMachineDto
    {
        public string MachineName { get; set; }

        public string MachineDescription { get; set; }

        public string LaneNumber { get; set; }

        public List<ShowMalfuntctionsRequestDto> Malfunctions { get;set; }
    }
}
