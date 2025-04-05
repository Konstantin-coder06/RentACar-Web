using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentACar.Core.IServices;
using RentACar.Models;

namespace RentACar.Controllers
{
    public class FeatureController : Controller
    {
        IFeatureService featureService;
        IClassOfCarService classOfCarService;
        public FeatureController(IFeatureService featureService, IClassOfCarService classOfCarService) 
        {
            this.featureService = featureService;
            this.classOfCarService = classOfCarService;
        }
        [Authorize(Roles ="Admin")]
        public IActionResult Add(string type)
        {
            var viewModel = new AddFeatureOrClassOfCarViewModel();
            ViewBag.AddFeature = type == "feature";
            return View(viewModel);
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> Add(AddFeatureOrClassOfCarViewModel viewModel, string Type)
        {
            if (Type == "feature")
            {
                var isThereAnotherFeatureLikeThis = await featureService.IsThereFeatureWithThisName(viewModel.Name);
                if (isThereAnotherFeatureLikeThis == true)
                {
                    ModelState.AddModelError("Name", "There is a feature with this title");
                }
                if (ModelState.IsValid)
                {

                    Feature feature = new Feature()
                    {
                        NameOfFeatures = viewModel.Name
                    };
                    await featureService.Add(feature);
                    await featureService.Save();
                    return RedirectToAction("Index", "Admin");
                }
            }
            if(Type == "class")
            {
                var isThereAnotherClassLikeThis = await featureService.IsThereFeatureWithThisName(viewModel.Name);
                if (isThereAnotherClassLikeThis == true)
                {
                    ModelState.AddModelError("Name", "There is a class with this title");
                }
                if (ModelState.IsValid)
                {

                    ClassOfCar classOfCar = new ClassOfCar()
                    {
                        Name = viewModel.Name
                    };
                    await classOfCarService.Add(classOfCar);
                    await classOfCarService.Save();
                    return RedirectToAction("Index", "Admin");
                }
            }
            return RedirectToAction("Index", viewModel);
        }
    }
}
