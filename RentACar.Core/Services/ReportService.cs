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
    public class ReportService : IReportService
    {
        IRepository<Report> reports;
        public ReportService(IRepository<Report> reports)
        {
            this.reports = reports;
        }
        public async Task Add(Report entity)
        {
          await reports.Add(entity);
        }

        public async Task<IEnumerable<Report>> AllWithInclude(params Expression<Func<Report, object>>[] filters)
        {
            var report= await reports.AllWithInclude(filters);
            return report.ToList();
        }

        public void Delete(Report entity)
        {
            reports.Delete(entity);
        }

        public async Task<IEnumerable<Report>> FindAll(Expression<Func<Report, bool>> predicate)
        {
            var report = await reports.FindAll(predicate);
            return report.ToList();
        }

        public async Task<Report> FindOne(Expression<Func<Report, bool>> predicate)
        {
            return await reports.FindOne(predicate);
        }

        public async Task<IEnumerable<Report>> GetAll()
        {
            var report = await reports.GetAll();
            return report.ToList();
        }

        public async Task<IEnumerable<Report>> GetReportFromUser(int customerId)
        {
           var report= await reports.FindAll(x=>x.CustomerId == customerId);
            return report.ToList();
        }

        public async Task Save()
        {
           await reports.Save();
        }

        public void Update(Report entity)
        {
            reports.Update(entity);
        }
    }
}
