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
    public class ProyectosBLL
    {
        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool found = false;

            try
            {
                found = contexto.Proyectos.Any(p => p.ProyectoId == id);
            }
            catch
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return found;
        }

        public static Proyectos Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Proyectos proyecto;

            try
            {
                proyecto = contexto.Proyectos
                    .Include(p => p.Detalle)
                    .Where(p => p.ProyectoId == id)
                    .SingleOrDefault();
            }
            catch
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return proyecto;
        }

        public static bool Guardar(Proyectos proyecto)
        {
            if (!Existe(proyecto.ProyectoId))
                return Insertar(proyecto);
            else
                return Modificar(proyecto);
        }

        private static bool Insertar(Proyectos proyecto)
        {
            Contexto contexto = new Contexto();
            bool found = false;

            try
            {
                contexto.Proyectos.Add(proyecto);
                found = contexto.SaveChanges() > 0;
            }
            catch
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return found;
        }

        public static bool Modificar(Proyectos proyecto)
        {
            Contexto contexto = new Contexto();
            bool found = false;

            try
            {
                contexto.Database.ExecuteSqlRaw($"delete from ProyectosDetalle where ProyectoId = {proyecto.ProyectoId}");
                foreach (var anterior in proyecto.Detalle)
                {
                    contexto.Entry(anterior).State = EntityState.Added;
                }

                contexto.Entry(proyecto).State = EntityState.Modified;
                found = contexto.SaveChanges() > 0;
            }
            catch
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return found;
        }

        public static bool Eliminar(int id)
        {
            Contexto contexto = new Contexto();
            bool found = false;

            try
            {
                Proyectos proyecto = Buscar(id);

                if (proyecto != null)
                {
                    contexto.Proyectos.Remove(proyecto);
                    found = contexto.SaveChanges() > 0;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return found;
        }

        public static List<Proyectos> ObtenerLista(Expression<Func<Proyectos, bool>> criterio)
        {
            List<Proyectos> lista = new List<Proyectos>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.Proyectos.Where(criterio).AsNoTracking().ToList();
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
