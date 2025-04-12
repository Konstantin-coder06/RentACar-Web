using RentACar.Core.IServices;
using RentACar.DataAccess.IRepository;
using RentACar.DataAccess.IRepository.Repository;
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
        ICustomerService customerService;
        public ReportService(IRepository<Report> reports, ICustomerService customerService)
        {
            this.reports = reports;
            this.customerService = customerService;
        }
        public async Task Add(Report entity)
        {
          await reports.Add(entity);
        }

        public IQueryable<Report> AllWithInclude(params Expression<Func<Report, object>>[] filters)
        {
            return reports.AllWithInclude(filters);
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
        public async Task<bool> AnyAsync(Expression<Func<Report, bool>> predicate)
        {
            return await reports.AnyAsync(predicate);
        }
        public void Update(Report entity)
        {
            reports.Update(entity);
        }

        public async Task<int> ReportCount()
        {
            return await reports.Count();
        }

        public async Task<int> CountAsync(Expression<Func<Report, bool>> predicate)
        {
            return await reports.CountAsync(predicate);
        }

        public async Task<IEnumerable<Report>> GetAllOrderBy(Expression<Func<Report, object>> predicate)
        {
          return await reports.GetAllOrderBy(predicate);
        }

        public async Task<int> Count()
        {
           return await reports.Count();
        }

        public async Task<IEnumerable<Report>> FindAllLimited(Expression<Func<Report, bool>> predicate, int limit)
        {
            return await reports.FindAllLimited(predicate, limit);  
        }

        public async Task<List<(string title, string description, Customer customer, DateTime CreatedAt)>> GetAllReportsWithCustomersName()
        {
            var reportsList = await reports.GetAll();
            var result = new List<(string, string, Customer, DateTime)>();
            foreach (var report in reportsList)
            {
                var title = report.Title;
                var description = report.Description;
                var customer = await customerService.GetByReport(report.CustomerId);
                var createdAt = report.CreateAt;
                result.Add((title, description, customer, createdAt));
            }
            result= result.OrderBy(x => x.Item4).ToList();
            return result;
        }

        public async Task<List<(string title, string description, Customer customer, DateTime CreatedAt)>> GetAllReportsWithCustomersNameWithStartDate(DateTime? startDate)
        {
            var reportsList = await reports.FindAll(x=>x.CreateAt>=startDate);
            var result = new List<(string, string, Customer, DateTime)>();
            foreach (var report in reportsList)
            {
                var title = report.Title;
                var description = report.Description;
                var customer = await customerService.GetByReport(report.CustomerId);
                var createdAt = report.CreateAt;
                result.Add((title, description, customer, createdAt));
            }
            result = result.OrderBy(x => x.Item4).ToList();
            return result;

        }

        public async Task<List<(string title, string description, Customer customer, DateTime CreatedAt)>> GetAllReportsWithCustomersNameWithEndDate(DateTime? endDate)
        {
            var reportsList = await reports.FindAll(x => x.CreateAt <= endDate);
            var result = new List<(string, string, Customer, DateTime)>();
            foreach (var report in reportsList)
            {
                var title = report.Title;
                var description = report.Description;
                var customer = await customerService.GetByReport(report.CustomerId);
                var createdAt = report.CreateAt;
                result.Add((title, description, customer, createdAt));
            }
            result = result.OrderBy(x => x.Item4).ToList();
            return result;
        }

        public async Task<List<(string title, string description, Customer customer, DateTime CreatedAt)>> GetAllReportsWithCustomersNameWithStartEndDate(DateTime? startDate, DateTime? endDate)
        {
            var reportsList = await reports.FindAll(x => x.CreateAt >= startDate && x.CreateAt<=endDate);
            var result = new List<(string, string, Customer, DateTime)>();
            foreach (var report in reportsList)
            {
                var title = report.Title;
                var description = report.Description;
                var customer = await customerService.GetByReport(report.CustomerId);
                var createdAt = report.CreateAt;
                result.Add((title, description, customer, createdAt));
            }
            result = result.OrderBy(x => x.Item4).ToList();
            return result;
        }
        public async Task<List<Report>> GetReportsByCustomerName(string searchbar)
        {
            var customers = await customerService.FindAll(x => x.Name.ToUpper().Contains(searchbar.ToUpper()));
            var reports = new List<Report>();

            foreach (var customer in customers)
            {
                var customerReports = await GetReportFromUser(customer.Id);
                reports.AddRange(customerReports);
            }

            return reports;
        }

        public List<Report> GetReportsByTitle(string title)
        {
            var query = reports.AllWithInclude(r => r.Customer);
            if (title.ToLower() == "others")
            {
                query = query.Where(x => x.Title != "Booking Inquiry" && x.Title != "Customer Support" && x.Title != "Feedback");
            }
            else
            {
                query = query.Where(x => x.Title.ToUpper() == title.ToUpper());
            }
            return query.ToList();
        }
    }
}
