﻿namespace RentACar.Models
{
    public class ReportViewModel
    {
        public string Title {  get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public Customer Customer { get; set; }
    }
}
