﻿using RentACar.DataAccess.IRepository;
using RentACar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Core.IServices
{
    public interface IReportService:IRepository<Report>
    {
        Task<IEnumerable<Report>> GetReportFromUser(int customerId);
        
    }
}
