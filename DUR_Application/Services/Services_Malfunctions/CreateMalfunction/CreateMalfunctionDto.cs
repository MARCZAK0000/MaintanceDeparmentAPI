namespace DUR_Application.Services.Services_Malfunctions.CreateMalfunction
{
    public class CreateMalfunctionDto
    {

        public string Name { get; set; }

        public DateTime CreatedTime { get; set; }

        public int RequestStatusId { get;  } = 1;

        public int RequestTypeId { get; set; }

    }
}
