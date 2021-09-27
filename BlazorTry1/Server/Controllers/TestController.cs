using System;
using BlazorTry1.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorTry1.Models;

namespace BlazorTry1.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : Controller
    {
        [HttpGet]
        [Route("[action]")]
        [ActionName("Action4")]
        public async Task<JsonResult> myStringResult1()
        {
            Notes myNotes = new Notes();
            myNotes.note1 = "YouHitTheGetRequest";
            var yourdata = myNotes;
            return Json(new { data = yourdata });
        }
    }
}