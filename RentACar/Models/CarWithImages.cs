using System.ComponentModel.DataAnnotations;

namespace RentACar.Models
{
    public class CarWithImages
    {
        public Car Car { get; set; }=new Car();
        public List<Image> Images { get; set; }= new List<Image>();
      
    }
}
