using Microsoft.AspNetCore.Mvc;
using PDFReaderStockRenting.Requests;
using PDFReaderStockRenting.Responses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PDFReaderStockRenting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PDFController : ControllerBase
    {

        public PDFReaderStockRenting.Handlers.PDFHandler PDFHandler { get; }

        public PDFController()
        {
            PDFHandler = new PDFReaderStockRenting.Handlers.PDFHandler();
        }

    // POST api/<PDFController>
    [HttpPost("process")]
        public FileResponse Process([FromBody] FileRequest req)
        {
            return PDFHandler.Process(req);
        }

    }
}
