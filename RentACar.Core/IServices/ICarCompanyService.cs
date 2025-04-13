using RentACar.DataAccess.IRepository;
using RentACar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Core.IServices
{
    public interface ICarCompanyService:IRepository<CarCompany>
    {
        Task<CarCompany> GetByUserId(string id);
        Task<CarCompany>GetById(int? id);
        Task<string> GetNameById(int id);
        Task<string> GetAddressById(int id);
        Task<string> GetCompanyEmail(int id);
    }
}
