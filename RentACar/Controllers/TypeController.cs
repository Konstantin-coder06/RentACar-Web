using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentACar.Core.IServices;
using RentACar.Models;

namespace RentACar.Controllers
{
    public class TypeController : Controller
    {
        ICTypeService typeService;
        public TypeController(ICTypeService typeService)
        {
            this.typeService = typeService;
        }
        [Authorize(Roles = "Admin")]

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "User")]

        public async Task<IActionResult> Add(AddTypeOfCarViewModel model)
        {
            var isThereAnotherTypeLikeThis=await typeService.IsThereTypeWithThisName(model.Name,model.SeatCapacity);
            if(isThereAnotherTypeLikeThis==true)
            {
                ModelState.AddModelError("Name", "There is type with this name and seat capacity");
            }
            if (ModelState.IsValid)
            {
                CType cType = new CType()
                {
                    Name = model.Name,
                    SeatCapacity = model.SeatCapacity,
                };
                await typeService.Add(cType);
                await typeService.Save();
                return RedirectToAction("Index", "Admin");
            }
            return View(model);
        }
    }
}
