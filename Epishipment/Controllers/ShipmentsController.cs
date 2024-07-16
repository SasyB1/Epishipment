using Epishipment.Services;
using Microsoft.AspNetCore.Mvc;

namespace Epishipment.Controllers
{
    public class ShipmentsController : Controller
    {

        private readonly ShipmentService _shipmentService;

        public ShipmentsController(ShipmentService shipmentService)
        {
            _shipmentService = shipmentService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult BackOffice()
        {
            return View();
        }
    }
}
