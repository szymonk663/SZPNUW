using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SZPNUW.Data;
using SZPNUW.DBService;

namespace SZPNUW.WebAPI.Sections.Controllers
{
    [Route("[controller]/[action]")]
    public class ReportController : Controller
    {
        private Service service = new Service();
        [HttpGet("{id}")]
        public IActionResult GetSectionReports(int id)
        {
            List<ReportModel> reports = service.GetReportsBySectionId(id);
            return Json(reports);
        }
        [HttpGet("{id}")]
        public IActionResult DownloadReport(int id)
        {
            string errorMessage = string.Empty;
            FileModel file = service.GetReport(id, ref errorMessage);
            if(file != null)
            {
                FileContentResult result = new FileContentResult(file.Content, "application/pdf")
                {
                    FileDownloadName = file.FileName
                };
                HttpContext.Response.ContentType = "application/pdf";
                return result;
            }
            return new JsonResult(errorMessage);
        }
        [HttpPost]
        public IActionResult Upload(IFormFile file, [FromQuery]int sectionId)
        {
            if (file == null)
                return Json(new Result("Brak pliku."));
            if (file.Length == 0)
                return Json(new Result("Plik pusty."));
            if (!file.FileName.Contains(".pdf") && !file.FileName.Contains(".zip"))
                return Json(new Result("Plik o niewłaściwym rozszerzeniu."));
            byte[] fileContent = ReadFileContext(file);
            service.SaveReport(new FileModel { Content = fileContent, FileName = file.FileName, SectionId = sectionId });
            return Json(new Result(true));
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteReport(int id)
        {
            service.DeleteReport(id);
            return Json(new Result(true));
        }

        private byte[] ReadFileContext(IFormFile file)
        {
            using (Stream stream = file.OpenReadStream())
            {
                using (var binaryReader = new BinaryReader(stream))
                {
                    var fileContent = binaryReader.ReadBytes((int)file.Length);
                    return fileContent;
                }
            }
        }
    }
}
