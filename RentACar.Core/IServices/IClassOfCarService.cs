using RentACar.DataAccess.IRepository;
using RentACar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RentACar.Core.IServices
{
    public interface IClassOfCarService:IRepository<ClassOfCar>
    {
        Task<List<ClassOfCar>> GetClassOptionsAsync();
        Task<List<int>> GetAllClassSelectedIds(List<string> selectedCategories);
        Task<bool> IsThereClassWithThisName(string className);
        Task<ClassOfCar>GetClassOfCarById(int id);
        Task<int> GetStandardId();
        Task<int> GetLuxuryId();
        Task<int> GetEconomyId();
        Task<int> GetBusinessId();
        Task<int> GetElectricId();
        Task<int> GetSportId();
    }
}
