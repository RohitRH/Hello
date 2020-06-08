using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FirstApi.Models;
using FirstApi.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstApi.Controllers
{
    
    
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UsersInterface uad;
        private IHostingEnvironment hostingEnvironment;

        public UsersController(UsersInterface usersInterface,IHostingEnvironment hostingenvironment)
        {
            uad = usersInterface;
            this.hostingEnvironment = hostingenvironment;
        }

        // GET: api/Users
        [HttpGet("/api/Users")]
        public IEnumerable<UsersData> GetUsers()
        {
            return uad.GetAllData();
        }

        [HttpPost("/api/UploadFile")]
        public IActionResult UploadFile([FromForm]UploadFile DocumentFile)
        {
            Console.WriteLine("Hello1");
            string uniqueFileName = null;
            string ext = Path.GetExtension(DocumentFile.DocumentFile.FileName);
            var allowedExtensions = new[] { ".pdf",".jpg",".doc",".docx"};
            bool allowed = allowedExtensions.Contains(ext);
            if (DocumentFile.DocumentFile.Length <= 0)
            {
                return BadRequest("Length cannot be zero");
            }
            else if (DocumentFile.DocumentFile.FileName == null)
            {
                return BadRequest("Please upload file");
            }
            else if (!allowed)
            {
                return BadRequest("Invalid extension");
            }
            else if (DocumentFile.DocumentFile.Length > 2*1024*1024)
            {
                return BadRequest("Size exceeded max upload is 2MB your file size"+ (DocumentFile.DocumentFile.Length/1024/1024)+"MB");
            }
            if (DocumentFile.DocumentFile != null)
            {
                string uploadsFolder = hostingEnvironment.ContentRootPath;
                uploadsFolder = Path.Combine(uploadsFolder,"Files");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + DocumentFile.DocumentFile.FileName;
                string filepath = Path.Combine(uploadsFolder, uniqueFileName);
                DocumentFile.DocumentFile.CopyTo(new FileStream(filepath, FileMode.Create));
            }
            return Ok();
        }


        [HttpPost("/api/UploadFiles")]
        public IActionResult UploadFiles([FromForm]UploadFile DocumentFile,int id)
        {
            string uploadsFolder = hostingEnvironment.ContentRootPath;
            uploadsFolder = Path.Combine(uploadsFolder, "Files");
            uploadsFolder = uploadsFolder + "\\user" + id;
            Directory.CreateDirectory(uploadsFolder);
            var res = check(DocumentFile.DocumentFile);
            var status = res.GetType().GetProperty("status").GetValue(res, null);
            if (status.ToString().Equals("400"))
            {
                return BadRequest(res);
            }

            foreach (var item in DocumentFile.Files)
            {
                res = check(item);
                status = res.GetType().GetProperty("status").GetValue(res, null);
                if (status.ToString().Equals("400"))
                {
                    return BadRequest(res);
                }
            }

            bool val = SaveFile(DocumentFile.DocumentFile, uploadsFolder);
            if (!val)
            {
                return BadRequest(new { status = 400, message = "Error" });
            }

            foreach (var item in DocumentFile.Files)
            {
                string path = uploadsFolder + "\\Details";
                Directory.CreateDirectory(path);
                val = SaveFile(item,path);
                if (!val)
                {
                    return BadRequest(new { status = 400,message = "Error"});
                }
            }

            

            return Ok(new { status = 200, message = "Uploaded" });
        }


        public static bool SaveFile(IFormFile item, string uploadsFolder)
        {
            if (item != null)
            {
                string uniqueFileName = null;
                
                
                uniqueFileName = Guid.NewGuid().ToString() + "_" + item.FileName;
                string filepath = Path.Combine(uploadsFolder, uniqueFileName);
                item.CopyTo(new FileStream(filepath, FileMode.Create));
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Object check(IFormFile item)
        {
            string ext = Path.GetExtension(item.FileName);
                var allowedExtensions = new[] { ".pdf", ".doc", ".docx" };
                bool allowed = allowedExtensions.Contains(ext);
                if (item.Length <= 0)
                {
                    return new  {status = 400 , message = "Length cannot be zero "+item.FileName };
                }
                else if (item.FileName == null)
                {
                    return new { status = 400, message = "Please upload file All the files" };
                }
                else if (!allowed)
                {
                    return new { status = 400, message = "Invalid extension "+item.FileName+ " allowed are pdf,doc,docx" };
                }
                else if (item.Length > 2 * 1024 * 1024)
                {
                    return new { status = 400, message = "Size exceeded max upload is 2MB your file size " + (item.Length / 1024 / 1024) + "MB " + item.FileName };
                }
            return new { status = 200 , message = "Valid Format"};   
        }

        // GET: api/Users/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Users
        [HttpPost("/api/AddUsers")]
        public int Post([FromBody] UsersData users)
        {
            uad.AddUsers(users);
            return 200;
            
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
