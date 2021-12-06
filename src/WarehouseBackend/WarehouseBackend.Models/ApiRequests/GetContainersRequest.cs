namespace WarehouseBackend.Models.ApiRequests
{
    public class GetContainersRequest
    {
        public GetContainersRequest()
        {
            Page = 0;
            PageSize = 50;
        }

        public int Page { get; set; }

        public int PageSize { get; set; }
    }
}
