using AspNetCore.Reporting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NETCoreRDLC2.Controllers
{
    public class Report : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvirnoment;
        private readonly ApplicationDbContext _context;


        public Report(IWebHostEnvironment webHostEnvirnoment, ApplicationDbContext context)
        {
            this._webHostEnvirnoment = webHostEnvirnoment;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Print()
        {
            string mimtype = "";
            int extension = 1;
            var path = $"{ this._webHostEnvirnoment.WebRootPath}\\Reports\\Report2.rdlc";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            //parameters.Add("rp1", "Welcome to FoxLearn");
            var products = await GetProducts();
            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("DataSet1", products);

            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimtype);




            return File(result.MainStream, "application/pdf");
        }

        public async Task<IEnumerable<Post>> GetProducts()
        {
            return await _context.Posts.ToListAsync();
        }
    }
}
