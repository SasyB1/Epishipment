using Epishipment.Models.Dto;
using Epishipment.Services;
using Microsoft.AspNetCore.Mvc;

namespace Epishipment.Controllers
{
    public class CustomerController : Controller
    {
        private readonly AuthService _authSvc;
        private readonly CustomerService _customerSvc;

        public CustomerController(AuthService authSvc, CustomerService customerSvc)
        {
            _authSvc = authSvc;
            _customerSvc = customerSvc;
        }
        public IActionResult AddCustomer()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCustomer([Bind("CustomerName,CustomerSurname,CustomerBusinessName,CustomerTaxIdCode,CustomerVATNumber,CustomerType")] CustomerDto model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Errore nei dati inseriti";
                return View();
            }

            var userId = _authSvc.GetCurrentUserId();
            if (userId == null)
            {
                TempData["Error"] = "Utente non autenticato";
                return View();
            }
            model.UserId = userId.Value;

            var customer = _customerSvc.AddCustomer(model);
            if (customer == null)
            {
                TempData["Error"] = "Errore nella registrazione";
                return View();
            }
            TempData["Success"] = "Registrazione effettuata con successo";
            return RedirectToAction("Index", "Home");
        }
    }
}
