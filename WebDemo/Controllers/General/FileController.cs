using Domain.BaseModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebDemo.Constants;
using WebDemo.Handler;
using static Domain.DBContext.DBContext;

namespace WebDemo.Controllers.General
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : BaseApiController
    {
        private readonly DatabaseContext _dataBaseContext;
        private readonly IWebHostEnvironment _env;
        public FileController(DatabaseContext databaseContext, IWebHostEnvironment environment)
        {
            _dataBaseContext = databaseContext;
            _env = environment; 
        }
        [HttpPost("Images")]
        public async Task<WsResponse> UploadImage(IFormFile file)
        {
            var response = new WsResponse();
            string fileName;
            //var files = Request.Form.Files;           
            try
             {
                 var extn = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                 var filename = file.FileName.Split('.')[file.FileName.Split('.').Length - 2];
                 fileName = filename + DateTime.Now.Ticks;                
                 bool pathBuilt = Directory.Exists("Upload\\files");
                 if (!pathBuilt)
                 {
                     Directory.CreateDirectory("Upload\\files");
                 }
                 var path = Path.Combine("Upload\\files", fileName);
                 using (var stream = new FileStream(path, FileMode.Create))
                 {
                     await file.CopyToAsync(stream);
                 }
                 response.Status = ("Thành công");
                 response.Data = path;
             }
            catch
                {
                    response.Status = ("Request failed: " + "err");
                }
            return response;
        }


        [HttpPost("UploadFolder")]
        public async Task<WsResponse> Index([FromForm] IFormFileCollection files)
        {
            var response = new WsResponse();
            string targetFolder = Path.Combine(_env.WebRootPath, "uploads");
            foreach (IFormFile file in files)
            {
                if (file.Length <= 0) continue;

                //fileName is the the fileName including the relative path
                string path = Path.Combine(targetFolder, file.FileName);

                //check if folder exists, create if not
                var fi = new FileInfo(path);
                fi.Directory?.Create();

                //copy to target
                using var fileStream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(fileStream);
            }
            return response;
        }

    }
}
