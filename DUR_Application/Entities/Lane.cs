namespace DUR_Application.Entities
{
    public class Lane
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public string Describiton { get; set; }

        public List<Machine> Machines { get; set; }
    }
}
