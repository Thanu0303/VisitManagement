using Main.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using VisitorManagement.DataAccess.Models;
using VisitorManagement.DataAccess.ViewModel;

namespace Main.Controllers
{
    public class HomeController : Controller
    {
        private VisitorManagementContext _context;

        public HomeController(VisitorManagementContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Dashboard()
        {
            DashBoardViewModel dashboard = new DashBoardViewModel();

            dashboard.visits_count = _context.VisitDetail.Where(x => x.IsDeleted == false).Count();
            dashboard.employees_count = _context.User.Count();
            dashboard.upComingVisits_count = _context.VisitDetail.Where(x => x.StartTime > DateTime.Now).Count();
            dashboard.completedVisits_count = _context.VisitDetail.Where(x => x.StartTime < DateTime.Now && x.IsDeleted == false).Count();
            dashboard.primaryParticipant_count = _context.VisitParticipant.Where(x => x.IsPrimary == true).Count();
            dashboard.secondaryParticipants_count = _context.VisitParticipant.Where(x => x.IsPrimary == false).Count();

            Dictionary<DateTime, DateTime> daterange = GetWeekStartDate();

            dashboard.perweekvisits_count = _context.VisitDetail.Where(x => x.StartTime >= daterange.ElementAt(0).Key && x.StartTime <= daterange.ElementAt(0).Value && x.IsDeleted == false).Count();
            return View(dashboard);
        }

        private Dictionary<DateTime, DateTime> GetWeekStartDate()
        {
            Dictionary<DateTime, DateTime> result = new Dictionary<DateTime, DateTime>();
            string week = DateTime.Now.DayOfWeek.ToString();
            int reducedate = 0;
            int increasedate = 0;
            switch (week)
            {
                case "Sunday":
                    reducedate = 6; increasedate = 0;
                    break;
                case "Saturday":
                    reducedate = 5; increasedate = 1;
                    break;
                case "Friday":
                    reducedate = 4; increasedate = 2;
                    break;
                case "Thursday":
                    reducedate = 3; increasedate = 3;
                    break;
                case "Wednesday":
                    reducedate = 2; increasedate = 4;
                    break;
                case "Tuesday":
                    reducedate = 1; increasedate = 5;
                    break;
                case "Monday":
                    reducedate = 0; increasedate = 6;
                    break;
            }
            DateTime startdate = DateTime.Now.AddDays(-reducedate);
            DateTime enddate = DateTime.Now.AddDays(increasedate);
            result.Add(startdate, enddate);
            return result;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
