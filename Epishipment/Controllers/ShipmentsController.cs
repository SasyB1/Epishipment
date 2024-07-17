using Epishipment.Services;
using Microsoft.AspNetCore.Mvc;
using Epishipment.Models;

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

        public IActionResult _GetShipments()
        {
            List<Shipment> allShipments = _shipmentService.GetShipments();
            if (allShipments == null)
            {
                allShipments = new List<Shipment>();
            }
            return PartialView("_GetShipments", allShipments);
        }

       

    }
}
