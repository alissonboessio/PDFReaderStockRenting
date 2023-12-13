namespace PDFReaderStockRenting.Responses
{
    public class FileResponse : BaseResponse
    {
        public Object? file { get; set; }
        public decimal totalLiquido { get; set; }
    }
}
