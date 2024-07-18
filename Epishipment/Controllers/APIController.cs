using Epishipment.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Epishipment.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class APIController : ControllerBase
    {
        private readonly ShipmentService _shipmentService;

        public APIController(ShipmentService shipmentService)
        {
            _shipmentService = shipmentService;
        }

        [HttpGet("shipment")]
        public IActionResult GetShipments()
        {
            var shipments = _shipmentService.GetShipments();
            return Ok(shipments);
        }
    }
}
