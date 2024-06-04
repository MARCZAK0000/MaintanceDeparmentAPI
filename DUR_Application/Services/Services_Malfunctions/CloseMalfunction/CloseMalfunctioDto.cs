using DUR_Application.Services.Services_Magazine.SpareParts;

namespace DUR_Application.Services.Services_Malfunctions.CloseMalfunction
{
    public class CloseMalfunctioDto
    {
        public int Id { get; set; } 

        public string? Describiton { get; set; }
        
        public decimal WorkTime { get; set; }

        public decimal LayoverTime { get; set; }

        public int RequestedTypeId { get; set; }

        public int RequestStatusId { get; } = 3;

        public List<SparePartsDto> SpareParts { get; set; }
    }
}
