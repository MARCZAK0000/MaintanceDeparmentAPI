namespace DUR_Application.Entities
{
    public class Machine
    {

        public int Id { get; set; }

        public string MachineName { get; set; }

        public string MachineDescription { get; set; }

        public int LaneID { get; set; }

        public string LaneNumber { get; set; }

        public Lane Lane { get; set; }

        public List<MalfunctionRequest> Malfunctions { get; set; }
    }
}
