namespace DUR_Application.Entities
{
    public class SparePart
    {
        public int Id { get; set; }

        public string Name { get; set; }    

        public string Description { get; set; }

        public string Type { get; set; }    

        public decimal Price { get; set; }

        public int Count { get; set; }

        public int MagazineId { get; set; }

        public string? PartNumber { get; set; }

        public Magazine Magazine { get; set; }
    }
}
