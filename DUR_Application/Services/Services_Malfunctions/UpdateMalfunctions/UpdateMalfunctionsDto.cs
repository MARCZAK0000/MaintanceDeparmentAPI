using DUR_Application.Entities;
using DUR_Application.Services.Services_Magazine.SpareParts;

namespace DUR_Application.Services.Services_Malfunctions.UpdateMalfunctions
{
    public class UpdateMalfunctionsDto
    {
        public int Id { get; set; }

        public string Describiton { get; set; }

        public int UserId { get; set; }

        public string UserNumber { get; set; }

        public int RequestTypeId { get; set; }

        public int RequestedStatusId = 2;

        public List<SparePartsDto>? SpareParts { get; set; }
    }
}
