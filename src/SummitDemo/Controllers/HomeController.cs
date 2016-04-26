using FlugDemo.Data;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SummitDemo.Controllers
{
    public class HomeController: Controller
    {
        // private IFlugRepository repo = new FlugEfRepository();
        //private IFlugRepository repo = new FlugDemoRepository();

        private IFlugRepository repo;

        public HomeController(IFlugRepository repo)
        {
            this.repo = repo;
        }

        // /Home/Index
        public ActionResult Index() {
            var all = this.repo.FindAll();
            return View("List", all); 
        }

        // /Home/Detail?id=1
        // /Home/Detail/1
        public ActionResult Detail(int id) {
            var flug = this.repo.FindById(id);
            return View("Detail", flug);
        }

    }
}
