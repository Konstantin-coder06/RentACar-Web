using RentACar.Core.IServices;
using RentACar.DataAccess.IRepository;
using RentACar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Core.Services
{
    public class ReservationService : IReservationService
    {
        IRepository<Reservation> reservationsRepository;
        public ReservationService(IRepository<Reservation> reservationsRepository)
        {
            this.reservationsRepository = reservationsRepository;
        }
        public void Add(Reservation entity)
        {
           reservationsRepository.Add(entity);
        }

        public IEnumerable<Reservation> AllWithInclude(params Expression<Func<Reservation, object>>[] filters)
        {
            return reservationsRepository.AllWithInclude(filters).ToList();
        }

        public void Delete(Reservation entity)
        {
            reservationsRepository.Delete(entity);
        }

        public IEnumerable<Reservation> FindAll(Expression<Func<Reservation, bool>> predicate)
        {
            return reservationsRepository.FindAll(predicate).ToList();
        }

        public Reservation FindOne(Expression<Func<Reservation, bool>> predicate)
        {
            return reservationsRepository.FindOne(predicate);
        }

        public IEnumerable<Reservation> GetAll()
        {
            return reservationsRepository.GetAll().ToList();
        }

        public void Update(Reservation entity)
        {
            reservationsRepository.Update(entity);
        }
    }
}
