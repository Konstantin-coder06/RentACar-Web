﻿using RentACar.DataAccess.IRepository;
using RentACar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Core.IServices
{
    public interface IReservationService:IRepository<Reservation>
    {
        Task<bool> HasOverlappingReservation(int carId, DateTime startDate, DateTime endDate);
    }
}
