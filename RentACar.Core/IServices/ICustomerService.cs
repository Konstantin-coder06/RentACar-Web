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
        Customer GetByUserId(string userId);
        Customer GetByReport(int id);
    }
}
