using System;
using System.Collections.Generic;
using System.Text;
using VisitorManagement.DataAccess.Models;
using VisitorManagement.DataAccess.ViewModel;

namespace VisitorManagement.DataAccess
{
    public interface IVisitorRepository
    {
        //VisitViewModel GetAllVisitors();

        //IEnumerable<VisitDetail> GetAllVisitors();

        List<ListVisitModel> GetVisits();
        ViewModel.ListVisitModel GetVisitorById(int id);

        VisitViewModel AddVisitor(VisitViewModel model);
        VisitViewModel UpdateVisitor(VisitViewModel updatedModel);
        VisitViewModel CreateGet();
        VisitViewModel EditGet(int id);

        VisitViewModel EditGetByName(string Name);

        int UpdateSigninSignOut(int Visitdetailid,string singIn, string signOut,string photo);

        bool DeleteVisitor(int id);

        HashSet<ListVisitModel> UserVisits(int id);
    }
}
