
namespace VisitorManagement.DataAccess.ViewModel
{
    public class ArrangementViewModel
    {
        public int ArrangementId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? DelegateContactID { get; set; }
        public bool IsSelected { get; set; }
    }
}
