namespace DUR_Application.Entities
{
    public class RequestType
    {
        public int Id { get; set; } 

        public string Name { get; set; }

        public List<MalfunctionRequest> MalfunctionRequests { get; set; }
    }
}
