using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using XamStore.Domain.Entities.Cadastro;

namespace XamStore.Application.Controllers
{
    public class ContatoController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Menus = Context.Menu;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Enviar(Contato contato)
        {
            ViewBag.Menus = Context.Menu;
            if (!ModelState.IsValid)
                return View("Index");

            try
            {
                const string body = "<p>XamStore Contato</p><p>Nome: {0} ({1})</p><p>Mensagem:</p><p>{2}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress(contato.Email));
                message.From = new MailAddress("rodrigojbm@hotmail.com");
                message.Subject = "XamStore - Contato";
                message.Body = string.Format(body, contato.Nome, contato.Email, contato.Mensagem);
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "rodrigojbm@hotmail.com",
                        Password = "6352jbm"
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp-mail.outlook.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;

                    smtp.Send(message);

                    var routeValue = new RouteValueDictionary { {"mensagem", "Sua mensagem foi enviada, entraremos em contato."} };

                    return RedirectToAction("Index", routeValue);
                }
            }
            catch (Exception)
            {
                var routeValue = new RouteValueDictionary { {"mensagem", "Erro ao enviar mensagem, tente novamente mais tarde."} };
                return RedirectToAction("Index", routeValue);
            }
        }
    }
}