namespace DUR_Application.Model.Response
{
    public interface IResponse<T>
    {
        IEnumerable<T> Items { get; set; }

        T Item { get; set; }

        int Id { get; set; }

        int TotalPages { get; set; }

        int ItemsFrom { get; set; }

        int ItemsTo { get; set; }

        int TotalItemsCount { get; set; }


    }



    public class Response<T> : IResponse<T>
    {
        public IEnumerable<T> Items { get; set; }

        public T Item { get; set; }

        public int Id { get; set; }

        public int TotalPages { get; set; }

        public int ItemsFrom { get; set; }

        public int ItemsTo { get; set; }

        public int TotalItemsCount { get; set; }

    }
}
