﻿using RentACar.DataAccess.IRepository;
using RentACar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Core.IServices
{
    public interface ICTypeService:IRepository<CType>
    {
        Task<bool> IsThereTypeWithThisName(string typeName, int seatCapacity);
    }
}
