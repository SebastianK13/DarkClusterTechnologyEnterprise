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
        private int PageSize;

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
            string responsibleEmployee = HttpContext.User.Identity.Name;
            bool result = await repository.CreateInvoice(postData, responsibleEmployee);

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
                string responsibleEmployee = HttpContext.User.Identity.Name;
                repository.CreateCustomer(customer, location, responsibleEmployee);
                return View();
            }
            else
            {
                CustomerInfo ci = new CustomerInfo(customer, location);

                return View(ci);
            }

            
        }
        public IActionResult MyInvoices(int page = 1)
        {
            string? responsibleEmployee = HttpContext.User.Identity.Name;
            var model = repository.GetInvoices(responsibleEmployee);
            PageSize = 10;

            InvoiceDetail invD = new InvoiceDetail(model);

            PageInfo pages = new PageInfo
            {
                CurrentPage = page,
                ItemsPerPage = PageSize,
                TotalItems = model.Count()
            };

            InvoicesViewModel iVm = new InvoicesViewModel
            {
                Invoices = invD.Invoices.Skip((page - 1) * PageSize).Take(PageSize),
                PagesInfo = pages
            };

            return View(iVm);
        }
        public IActionResult DisplayInvoice(string invID)
        {
            var invoice = repository.GetInvoice(invID);
            InvoiceView iv = new InvoiceView(invoice);
            iv.Services = repository.GetServices(invID);

            return View(iv);
        }

    }
}