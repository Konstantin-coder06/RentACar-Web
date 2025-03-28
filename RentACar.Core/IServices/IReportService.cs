using RentACar.DataAccess.IRepository;
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
        Task<int> ReportCount();
        Task<List<(string title, string description, Customer customer, DateTime CreatedAt)>> GetAllReportsWithCustomersName();
        Task<List<(string title, string description, Customer customer, DateTime CreatedAt)>> GetAllReportsWithCustomersNameWithStartDate(DateTime? startDate);
        Task<List<(string title, string description, Customer customer, DateTime CreatedAt)>> GetAllReportsWithCustomersNameWithEndDate(DateTime? endDate);
        Task<List<(string title, string description, Customer customer, DateTime CreatedAt)>> GetAllReportsWithCustomersNameWithStartEndDate(DateTime? startDate, DateTime? endDate);
        Task<List<Report>> GetReportsByCustomerName(string searchbar);
    }
}
