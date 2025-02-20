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
        public void Add(Report entity)
        {
           reports.Add(entity);
        }

        public IEnumerable<Report> AllWithInclude(params Expression<Func<Report, object>>[] filters)
        {
            return reports.AllWithInclude(filters).ToList();
        }

        public void Delete(Report entity)
        {
            reports.Delete(entity);
        }

        public IEnumerable<Report> FindAll(Expression<Func<Report, bool>> predicate)
        {
            return reports.FindAll(predicate).ToList();
        }

        public Report FindOne(Expression<Func<Report, bool>> predicate)
        {
            return reports.FindOne(predicate);
        }

        public IEnumerable<Report> GetAll()
        {
            return reports.GetAll().ToList();
        }

        public IEnumerable<Report> GetReportFromUser(int customerId)
        {
           return reports.FindAll(x=>x.CustomerId == customerId).ToList();
        }

        public void Save()
        {
            reports.Save();
        }

        public void Update(Report entity)
        {
            reports.Update(entity);
        }
    }
}
