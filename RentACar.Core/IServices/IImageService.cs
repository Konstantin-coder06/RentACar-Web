using RentACar.DataAccess.IRepository;
using RentACar.DataAccess.IRepository.Repository;
using RentACar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Core.IServices
{
    public interface IImageService: DataAccess.IRepository.IRepository<Image>
    {
        IEnumerable<Image> GetImagesByCarId(int carid);
    }
}
