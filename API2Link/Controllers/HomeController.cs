using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace API2Link.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            StringBuilder oSb = new StringBuilder();
            oSb.AppendLine($"");
            oSb.AppendLine($"<h1>{Assembly.GetExecutingAssembly().GetName().Name} v{Assembly.GetEntryAssembly().GetName().Version}</h1>");
            //oSb.AppendLine($"<a href='{Startup.tC_VirtualPath}/swagger'>Swagger</a>");
            //oSb.AppendLine($"<p><b>Environment Infomation</b></p>");
            //oSb.AppendLine($"<table>");
            //string[] atEnvKeys = new string[Environment.GetEnvironmentVariables().Count];
            ////DictionaryEntry
            //Environment.GetEnvironmentVariables().Keys.CopyTo(atEnvKeys, 0);
            //Array.Sort(atEnvKeys);
            //foreach (string key in atEnvKeys)
            //{
            //    oSb.AppendLine($"<tr><td>{key}</td><td>{Environment.GetEnvironmentVariable(key)}</td></tr>");
            //}
            //oSb.AppendLine($"</table>");
            return new ContentResult
            {
                ContentType = "text/html",
                Content = oSb.ToString()
            };
        }
    }
}
