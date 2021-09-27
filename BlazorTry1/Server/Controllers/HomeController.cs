using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AccettazioneMateriale.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        readonly IConfiguration _configuration;

        public HomeController(IConfiguration iconfiguration)
        {
            _configuration = iconfiguration;
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpPost]
        public IActionResult Login(string user, string pass)
        {
            SqlConnection nSqlConn = new SqlConnection(_configuration["dbConnectionString"]);
            
            SqlParameter sqlProcedureResult = new SqlParameter();

            using (nSqlConn)
            {

                try
                {
                    nSqlConn.Open();
                    string verifyUser = "VERIFY_LOGIN";
                    var verifyUserCmd = new SqlCommand(verifyUser, nSqlConn);
                    verifyUserCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    verifyUserCmd.Parameters.Add("@USERNAME", System.Data.SqlDbType.NVarChar).Value = user.Trim().ToUpper();
                    verifyUserCmd.Parameters.Add("@PASSWORD", System.Data.SqlDbType.NVarChar).Value = pass;
                    var returnParameter = verifyUserCmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;

                    verifyUserCmd.ExecuteNonQuery();

                    sqlProcedureResult.Value = returnParameter.Value;

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            if (sqlProcedureResult.Value.ToString() == "1")
            {
                return RedirectToAction("Index", "GestMain");
            }
            else
            {
                return View("LoginError");
            }
        }
    }
}
