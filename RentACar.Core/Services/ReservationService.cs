using Microsoft.EntityFrameworkCore;
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
        public async Task Add(Reservation entity)
        {
          await reservationsRepository.Add(entity);
        }

        public async Task<IEnumerable<Reservation>> AllWithInclude(params Expression<Func<Reservation, object>>[] filters)
        {
            var reservations=await reservationsRepository.AllWithInclude(filters);
            return reservations.ToList();
        }

        public Task<bool> AnyAsync(Expression<Func<Reservation, bool>> predicate)
        {
            return reservationsRepository.AnyAsync(predicate);
        }

        public void Delete(Reservation entity)
        {
            reservationsRepository.Delete(entity);
        }

        public async Task<IEnumerable<Reservation>> FindAll(Expression<Func<Reservation, bool>> predicate)
        {
            var reservations=await reservationsRepository.FindAll(predicate);
            return reservations.ToList();
        }

        public async Task<Reservation> FindOne(Expression<Func<Reservation, bool>> predicate)
        {
            return await reservationsRepository.FindOne(predicate);
        }

        public async Task<IEnumerable<Reservation>> GetAll()
        {
            var reservations=await reservationsRepository.GetAll();
            return reservations.ToList();
        }
        public async Task<bool> HasOverlappingReservation(int carId, DateTime startDate, DateTime endDate)
        {
            return await reservationsRepository
                .AnyAsync(r => r.CarId == carId &&
                               r.StartDate < endDate &&
                               r.EndDate > startDate);
        }
        public async Task Save()
        {
           await reservationsRepository.Save();
        }

        public void Update(Reservation entity)
        {
            reservationsRepository.Update(entity);
        }
    }
}
