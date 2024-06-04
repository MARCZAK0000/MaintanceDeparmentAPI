using DUR_Application.Entities;

namespace DUR_Application.Services.Services_Malfunctions.ChangeMalfunctions
{
    public class ChangeMalfunctionsDto
    {
        public string? Description { get; set; }

        public decimal? WorkTime { get; set; }

        public decimal? LayoverTime { get; set; }

        public int? RequestTypeId { get; set; }

        public int RequestStatusId = 3;

        public List<SparePart> SpareParts { get; set; }
    }
}
