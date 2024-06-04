namespace DUR_Application.Services.Services_Machine.MachineDto.GetOneMachine
{
    public class ShowMalfuntctionsRequestDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal WorkTime { get; set; }

        public decimal LayoverTime { get; set; }

        public DateTime CreatedTime { get; set; } 
        
        public string RequestTypeName { get; set; }


    }
}
