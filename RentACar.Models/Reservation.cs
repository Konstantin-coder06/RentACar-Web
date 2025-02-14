using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentACar;

namespace RentACar.Models
{
    public class Reservation
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public bool IsSelfPick { get; set; }

        public string? PaidDeliveryPlace { get; set; }
        [Required]
        public bool IsReturnBackAtSamePlace { get; set; }
        [Required]
        public int CarId { get; set; }
        public Car Car { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Required]
        public double TotalPrice { get; set; }
        [Required]
        public DateTime CreateTime { get; set; }
    }
}
