using DAL;
using Jean_P2_AP1.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Jean_P2_AP1.BLL
{
    public class TiposTareaBLL
    {
        public static List<TiposTarea> ObtenerLista(Expression<Func<TiposTarea, bool>> criterio)
        {
            List<TiposTarea> lista = new List<TiposTarea>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.TiposTarea.Where(criterio).AsNoTracking().ToList();
            }
            catch
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return lista;
        }
    }
}
