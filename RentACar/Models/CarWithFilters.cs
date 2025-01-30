namespace RentACar.Models
{
    public class CarWithFilters
    {
      
            public int MinPrice { get; set; } = 0;
            public int MaxPrice { get; set; } = 0;
            public List<string> SelectedClasses { get; set; } = new List<string>();
            public List<Car> Cars { get; set; } = new List<Car>();
        
    }
}
