using RentACar.DataAccess.IRepository;
using RentACar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Core.IServices
{
    public interface ICustomerService:IRepository<Customer>
    {
        Task<Customer> GetByUserId(string userId);
        Task<Customer> GetByReport(int id);
        Task<List<(Customer customer, string email, string phoneNumber)>> GetCustomersWithEmailsAndPhoneNumbers();
        Task<Customer>FindById(int id);

    }
}
