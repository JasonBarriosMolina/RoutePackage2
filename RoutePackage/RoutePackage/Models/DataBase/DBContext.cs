using SQLite;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutePackage.Models.Database
{
    public class DBContext
    {
        public string RutaConexion { get; set; }

        public Func<SQLiteConnection> NuevaConexion { get; set; }

        public void Configurar<TClass>() where TClass : class
        {
            using (var conexion = NuevaConexion())
            {
                conexion.CreateTable<TClass>();
            }
        }

        public void Guardar<TClass>(TClass[] elementos) where TClass : class
        {
            using (var conexion = NuevaConexion())
            {
                conexion.RunInTransaction(() => {

                    foreach (var item in elementos)
                    {
                        conexion.Insert(item);
                    }
                });
            }
        }

        public IEnumerable<TClass> Seleccionar<TClass>() where TClass : class
        {
            using (var conexion = NuevaConexion())
            {
                var query = conexion.Table<TClass>().ToArray();

                return query;
            }

        }
    }
}