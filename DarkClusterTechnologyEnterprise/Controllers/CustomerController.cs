using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DarkClusterTechnologyEnterprise.Models;
using DarkClusterTechnologyEnterprise.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DarkClusterTechnologyEnterprise.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomerRepository repository;
        public CustomerController(ICustomerRepository repository)
        {
            this.repository = repository;
        }
        public IActionResult Index(string type)
        {
            return Ok(repository.Customer(type));
        }
        public IActionResult Invoice()
        {


            return View();
        }
        [HttpPost]
        public async Task<bool> Invoice([FromBody]InvoiceReceive postData)
        {
            bool result = await repository.CreateInvoice(postData);

            return result;
        }
        public IActionResult CreateCustomer()
        {

            return View();
        }
        [HttpPost]
        public IActionResult CreateCustomer(Customer customer, CustomerLocation location)
        {
            if (ModelState.IsValid)
            {
                repository.CreateCustomer(customer, location);
                return View();
            }
            else
            {
                CustomerInfo ci = new CustomerInfo(customer, location);

                return View(ci);
            }

            
        }
        public IActionResult MyInvoices()
        {

            return View();
        }
    }
}