using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace restaurant
{
    class platoParaLista
    {
        public Bitmap bm ;
        
        public string nombre;
        public UInt32 longitud;
        public platoParaLista(Bitmap bm,string nombre, UInt32 longitud)
        {
            this.bm = bm;
            this.nombre = nombre;
            this.longitud = longitud;
            
        }
        public string getNombre()
        {
            return nombre;
        }
        public UInt32 getLongitud()
        {
            return longitud;
        }
        public Bitmap getBm()
        {
            return bm;
        }
    }
}
