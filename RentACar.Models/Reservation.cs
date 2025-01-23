using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsSelfPick {  get; set; }
        public string PaidDeliveryPlace {  get; set; }
        public bool IsReturnBackAtSamePlace {  get; set; }
        public int CarId {  get; set; }
        public Car Car { get; set; }
        public int CustomerId {  get; set; }
        public Customer Customer { get; set; }


    
    }
}
