using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Championship_of_box.Models;
using System.Security.Cryptography.X509Certificates;
using Championship_of_box.DataModel;
using AutoMapper;

namespace Championship_of_box.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public string GetData()
        {
            BattleRepository repo = new BattleRepository();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Rating, RatingViewModel>());
            var mapper = new Mapper(config);
            List<RatingViewModel> ratingsList = mapper.Map<List<RatingViewModel>>(repo.GetAllBoxerRating());
            return JsonConvert.SerializeObject(ratingsList);
        }

        public ActionResult GetBattlePage()
        {
            ViewBag.Message = "Your application description page.";

            return View("~/Views/Home/PageOfChampionship.cshtml");
        }

        public string GetBattleTableData()
        {
            IRepository<Battle> repo = new BattleRepository();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Battle, BattleViewModel>());
            var mapper = new Mapper(config);
            List<BattleViewModel> battlesViewList = mapper.Map<List<BattleViewModel>>(repo.GetAll());
            return JsonConvert.SerializeObject(battlesViewList);
        }

        [HttpGet]
        public ActionResult CreateBattle()
        {
            return View("~/Views/Home/Battle.cshtml", new BattleViewModel());
        }

        [HttpPost]
        public ActionResult CreateBattle(BattleViewModel model)
        {
            IRepository<Battle> repo = new BattleRepository();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<BattleViewModel, Battle>());
            var mapper = new Mapper(config);
            Battle battle = mapper.Map<BattleViewModel, Battle>(model);
            repo.Create(battle);
            repo.Save();
            return RedirectToAction("GetBattlePage");
        }

        [HttpGet]
        public ActionResult EditBattle(int? id)
        {
            IRepository<Battle> repo = new BattleRepository();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Battle, BattleViewModel>());
            var mapper = new Mapper(config);
            BattleViewModel battleView = mapper.Map<Battle, BattleViewModel>(repo.Get(id.Value));
            return View("~/Views/Home/Battle.cshtml", battleView);
        }
        [HttpPost]
        public ActionResult EditBattle(BattleViewModel model)
        {
            IRepository<Battle> repo = new BattleRepository();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<BattleViewModel, Battle>());
            var mapper = new Mapper(config);
            Battle battle = mapper.Map<BattleViewModel, Battle>(model);
            repo.Update(battle);
            repo.Save();
            return RedirectToAction("GetBattlePage");
        }
    }
}