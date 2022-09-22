using Microsoft.AspNetCore.Mvc;
using static Domain.DBContext.DBContext;

namespace WebDemo.Handler
{

    //public interface IImageHandler
    //{
    //    Task<IActionResult> UploadImage(IFormFile file);        
    //}
   
    //public class ImageHandler : IImageHandler
    //{
    //    private readonly DatabaseContext _dataBaseContext;
    //    public ImageHandler(DatabaseContext dataBaseContext)
    //    {
    //        _dataBaseContext = dataBaseContext;
    //    }
        
    //    public async Task<IActionResult> UploadImage(IFormFile file)
    //    {
    //        var upload_path = System.Configuration.ConfigurationManager.AppSettings["FilePath"];
    //        var success_file = new List<string>();
    //        var date_now = DateTime.Now.ToString("dd-MM-yyyy");
    //        var folder_virtual_path = $"/UploadFiles/Images/Temp/{date_now}";
    //        var folder_physic_path = $"{upload_path}{folder_virtual_path}";
    //        bool exists = System.IO.Directory.Exists(folder_physic_path);
    //        if (!exists)
    //            System.IO.Directory.CreateDirectory(folder_physic_path);
            
    //        return true;
    //    }
    //}
}
