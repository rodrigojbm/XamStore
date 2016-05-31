using System.Collections.Generic;
using XamStore.Domain.Entities.Cadastro;

namespace XamStore.Resource.Usuario
{
    public class UsuarioNivelResource
    {
        public static IEnumerable<UsuarioNivel> ListUsuarioNivel()
        {
            var usuarioNivelList = new List<UsuarioNivel>
            {
                new UsuarioNivel
                {
                    Nome = "Administrador"
                },
                new UsuarioNivel
                {
                    Nome = "Técnico"
                }
            };

            return usuarioNivelList;
        }
    }
}
