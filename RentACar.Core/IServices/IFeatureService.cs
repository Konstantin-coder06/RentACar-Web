using RentACar.DataAccess.IRepository;
using RentACar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Core.IServices
{
    public interface IFeatureService:IRepository<Feature>
    {
        Task<Feature> GetById(int id);
        Task<List<Feature>>GetAllFeaturesByIds(IEnumerable<int> ids);
    }
}
