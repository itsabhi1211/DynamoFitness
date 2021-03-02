using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace DynamoFitness.Controllers
{
    public class CommonController : Controller
    {
        // GET: Common
        [HttpPost]
        public JsonResult FileUpload()
        {
            var lstresult = new List<UploadFile>();

            var validextensions = ConfigurationManager.AppSettings["imgextensions"].ToString().Split(',');
            HttpPostedFileBase objhpfb = Request.Files[0] as HttpPostedFileBase;

            if (objhpfb.ContentLength == 0)
            {
                lstresult.Add(new UploadFile()
                {
                    name = "",
                    length = objhpfb.ContentLength,
                    type = objhpfb.ContentType,
                    msg = "Error in file Upload"
                });


            }
            else
            {
                int filesize = objhpfb.ContentLength;
                if (filesize > (1048576 * 10))
                {
                    lstresult.Add(new UploadFile()
                    {
                        name = "",
                        length = objhpfb.ContentLength,
                        type = objhpfb.ContentType,
                        msg = "Maximun size of file should be 10 MB."
                    });
                }
                else
                {
                    if (validextensions.Contains(objhpfb.FileName.Split('.')[objhpfb.FileName.Split('.').Length - 1].ToLower()))
                        {
                        string filename =Guid.NewGuid().ToString()+"."+ objhpfb.FileName.Split('.')[objhpfb.FileName.Split('.').Length - 1].ToString();
                        var savedfilename = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(filename));
                        objhpfb.SaveAs(savedfilename);
                        string url = ConfigurationManager.AppSettings["url"].ToString() + "Images/" + filename;
                        lstresult.Add(new UploadFile()
                        {
                            name = url,
                            length = objhpfb.ContentLength,
                            type = objhpfb.ContentType,
                            msg = "Successfull"
                        });
                    }
                    else
                    {
                        lstresult.Add(new UploadFile()
                        {
                            name = "",
                            length = objhpfb.ContentLength,
                            type = objhpfb.ContentType,
                            msg = "Allowed file extensions are jpg,jpeg,png",
                            
                        });
                    }
                }

            }
            return Json(lstresult, JsonRequestBehavior.AllowGet);

            
        }
    }

    public class UploadFile
    {
        public int length { get; set; }
        public string name { get; set; }

        public string type { get; set; }
        public string msg { get; set; }
    }
}