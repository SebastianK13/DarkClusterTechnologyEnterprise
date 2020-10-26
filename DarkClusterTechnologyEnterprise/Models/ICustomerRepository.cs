using DarkClusterTechnologyEnterprise.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkClusterTechnologyEnterprise.Models
{
    public interface ICustomerRepository
    {
        List<CustomerInfo> Customer(string type);
        void CreateCustomer(Customer customer, CustomerLocation location, string responsibleEmployee);
        Task<bool> CreateInvoice(InvoiceReceive invoice, string responsibleEmployee);
        List<Invoice> GetInvoices(string responsibleEmployee);
        Invoice GetInvoice(string invID);
        List<Service> GetServices(string invID);
    }
}
