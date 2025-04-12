namespace RentACar.Models
{
    public class ListOfReportsViewModel
    {
        public List<ReportViewModel> ReportViewModels {  get; set; }=new List<ReportViewModel>();
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string TitleFilter {  get; set; }
    }
}
