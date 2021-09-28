using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QTEMP.Models;
using QTEMP.Providers;

namespace QTEMP.Controllers
{
    [Route("Record")]
    public class RecordController : Controller
    {
        private static readonly ApiProvider ApiProvider = new ApiProvider();
        private Task<Response> _response;

        [HttpGet]
        [Route("Index")]
        public IActionResult Index()
        {
            return PartialView("Index");
        }

        [HttpGet]
        [Route("PendingInformation")]
        public IActionResult PendingInformation()
        {
            return PartialView("_Pending");
        }

        [HttpPost]
        [Route("ThermoCheck")]
        public IActionResult ThermoCheck(string qrCode, string status)
        {
            var temperature = new Temperature
            {
                QrCode = qrCode,
                Status = status
            };
            _response = ApiProvider.PostHealth(temperature);
            if (string.IsNullOrEmpty(_response.Result.ProfileImageUrl))
            {
                _response.Result.ProfileImageUrl = "/assets/icon/profile.png";
            }
            return _response.Result.IsSuccess ? PartialView("_Information", _response.Result) : PartialView("_ErrorPage");
        }
        
        [HttpGet]
        [Route("ErrorPage")]
        public IActionResult ErrorPage()
        {
            return PartialView("_ErrorPage");
        }
    }
}