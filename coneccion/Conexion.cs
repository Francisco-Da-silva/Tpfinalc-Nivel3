using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coneccion
{
 
        public static class Conexion
        {
            public static string Cadena
            {
                get
                {
                    return ConfigurationManager
                        .ConnectionStrings["DB"]
                        .ConnectionString;
                }
            }
        }
    }