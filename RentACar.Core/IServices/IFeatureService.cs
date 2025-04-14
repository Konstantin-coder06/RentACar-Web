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
        Task<bool> IsThereFeatureWithThisName(string featureName);
        Task<IEnumerable<string>> GetAllSelectedNames();
        Task<IEnumerable<Feature>> GetAllSelectedFeatures(List<string>selectedFeatures);
        Task<List<string>> GetAllFeaturesNames();
        Task<List<string>> GetAllFeaturesNamesOfSelectedFeatures(IEnumerable<Feature> features);
        Task<IEnumerable<Feature>> GetFeatureToDelete(IEnumerable<Feature> features, List<string> newFeatureNames);
        Task<IEnumerable<string>> GetFeatureToAdd(List<string> features,List<string> newFeatureNames);
        Task<Feature>GetFirst(IEnumerable<Feature> features,string featureName);
    }
}
