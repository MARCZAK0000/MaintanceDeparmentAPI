namespace DUR_Application.Entities
{
    public class MalfunctionRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public decimal? WorkTime { get; set; }

        public decimal? LayoverTime { get; set; }

        public DateTime CreatedTime { get; set; }

        public User? CreatedBy { get; set; }

        public string? UserNumber { get; set; }

        public int? UserId { get; set; }

        public RequestType RequestType { get; set; }

        public int RequestTypeId { get; set; }

        public string LaneNumber { get; set; }

        public Machine Machine { get; set; }

        public int MachineId { get; set; }

        public int RequestStatusId { get; set; }    

        public RequestStatus RequestStatus { get; set; }

        public List<SparePart> SpareParts { get; set; }
    }
}
