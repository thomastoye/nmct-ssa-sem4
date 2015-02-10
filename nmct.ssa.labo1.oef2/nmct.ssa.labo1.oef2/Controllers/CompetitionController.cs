using nmct.ssa.labo1.oef2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nmct.ssa.labo1.oef2.Controllers
{
    public class CompetitionController : Controller
    {

        public ActionResult Index()
        {
            CompetitionRepository repo = new CompetitionRepository();
            List<Competition> competitions = repo.GetCompetitions();
            return View(competitions);
        }
    }
}