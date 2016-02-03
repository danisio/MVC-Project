namespace MySurveys.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Data.Repository;
    using MySurveys.Models;
    using Ninject;
    using ViewModels;

    public class HomeController : Controller
    {
        [Inject]
        public IRepository<Question> Questions { get; set; }

        public ActionResult Index()
        {
            var all = this.Questions.All().ProjectTo<QuesionViewModel>().ToList();
            return this.View(all);
        }

        public ActionResult Error()
        {
            return this.View();
        }
    }
}