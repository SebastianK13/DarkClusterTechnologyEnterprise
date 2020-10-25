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
        void CreateCustomer(Customer customer, CustomerLocation location);
        Task<bool> CreateInvoice(InvoiceReceive invoice);
       
    }
}
