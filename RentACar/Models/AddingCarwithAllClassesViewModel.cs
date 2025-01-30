using Microsoft.AspNetCore.Mvc.Rendering;

namespace RentACar.Models
{
    public class AddingCarwithAllClassesViewModel
    {
        public int ClassOfCarId {  get; set; }
        public SelectList ClassesOfCars { get; set; }
        public Car Car { get; set; }
    }
}
