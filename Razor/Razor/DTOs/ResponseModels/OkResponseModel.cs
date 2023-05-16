namespace Razor.DTOs.ResponseModels
{
    public class OkResponseModel<T>
    {
        public int Count { get; set; }
        public List<T> Data { get; set; }
    }
}
