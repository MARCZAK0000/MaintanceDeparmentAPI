namespace DUR_Application.Model.Response
{
    public class ResponseBuilder<T>
    {
        private Response<T> _response = new Response<T>();

        public Response<T> Build()
        {
            return _response;
        }

        public ResponseBuilder<T> SetItems(IEnumerable<T> items) 
        {
            _response.Items = items;

            return this;
        }

        public ResponseBuilder<T> SetItem (T Item)
        {
            _response.Item = Item;
            return this;
        }

        public ResponseBuilder<T> SetId (int Id)
        {
            _response.Id = Id;
            return this;
        }

        public ResponseBuilder<T> SetItemsFrom (int PageSize, int PageNumber)
        {
            _response.ItemsFrom = PageSize*(PageNumber -1)+1;
            return this;
        }

        public ResponseBuilder<T> SetItemsTo (int PageSize, int PageNumber)
        {
            _response.ItemsTo = (PageSize * (PageNumber - 1) + 1) + PageSize - 1;
            return this;
        }

        public ResponseBuilder<T> SetTotalPages (int TotalItemsCount, int PageSize)
        {
            _response.TotalPages = TotalItemsCount % PageSize > 0 ?
                (int)Math.Ceiling((double)(TotalItemsCount / PageSize)) + 1
                :(int)Math.Ceiling((double)(TotalItemsCount / PageSize));

            return this;
        } 

        public ResponseBuilder<T> SetTotalItemsCount (int TotalItemsCount )
        {
            _response.TotalItemsCount = TotalItemsCount;
            return this;
        } 


    }
}
