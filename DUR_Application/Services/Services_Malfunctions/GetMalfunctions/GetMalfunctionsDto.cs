using DUR_Application.Services.Services_Magazine.SpareParts;

namespace DUR_Application.Services.Services_Malfunctions.GetMalfunctions
{
    public class GetMalfunctionsDto
    {
        public int Id { get; set; }

        public string Name { get;set; }

        public string Description { get;set; }

        public decimal? WorkTime { get;set; }

        public decimal? LayoverTime { get; set; }

        public DateTime CreatedTime { get; set; }

        public string? UserNumber { get; set; }

        public int RequestTypeId { get; set; }

        public string RequestTypeName { get; set; }

        public string LaneNumber { get; set; }

        public int MachineId { get; set; }

        public string MachineName { get; set; }

        public int RequestStatusId { get; set; }

        public string RequestStatusName { get; set;}

        public List<SparePartsDto> SpareParts { get; set; }
    }
}
