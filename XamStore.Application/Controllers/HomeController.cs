using System;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using XamStore.Domain.Entities.Cadastro;
using XamStore.Domain.Enums;
using XamStore.Infrastructure.Context;

namespace XamStore.Application.Controllers
{
    public class HomeController : BaseController
    {
        private readonly Context _db = new Context();

        [Route("Inicio")]
        public ActionResult Index()
        {
            ViewBag.Menus = _db.Menu;
            ViewBag.Produtos = _db.Slide.Include(x => x.Produto);

            return View();
        }

        public string CadastrarNews(string email)
        {
            try
            {
                var news = new Newsletter
                {
                    Email = email,
                    Data = DateTime.Now,
                    Status = StatusEnum.Ativo
                };

                var newsExists = _db.Newsletter.FirstOrDefault(x => x.Email == email);

                if (newsExists != null)
                    return "0";

                _db.Newsletter.Add(news);
                _db.SaveChanges();

                return "Email cadastrado com sucesso!";
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}