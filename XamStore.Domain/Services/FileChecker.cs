using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using XamStore.Domain.Entities.Cadastro;

namespace XamStore.Domain.Services
{
    public class FileChecker : Controller
    {
        public void VefifyFileFormat(Slide slide, HttpPostedFile file)
        {
            var imageTypes = new List<string>
            {
                "image/gif",
                "image/jpeg",
                "image/pjpeg",
                "image/png"
            };

            if (slide.File == null || slide.File.ContentLength == 0)
                ModelState.AddModelError("File", "Campo Requerido");
            else if (!imageTypes.Contains(slide.File.ContentType))
                ModelState.AddModelError("File", "Formatos suportados: GIF, JGP ou PNG.");

            if (file == null) return;
            var extensao = "";

            switch (file.ContentType)
            {
                case "image/gif":
                    extensao = ".gif";
                    break;
                case "image/jpeg":
                    extensao = ".jpg";
                    break;
                case "image/pjpeg":
                    extensao = ".jpg";
                    break;
                default:
                    extensao = ".png";
                    break;
            }

            var imageCript = $"{file.FileName}imagem-slide";
            imageCript = string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(imageCript)).Select(s => s.ToString("x2")));

            var fullPath = $"~/Imagens/Produto/{imageCript}{extensao}";
            file.SaveAs(Server.MapPath(fullPath));

            slide.Imagem = $"{imageCript}{extensao}";
        }
    }
}
