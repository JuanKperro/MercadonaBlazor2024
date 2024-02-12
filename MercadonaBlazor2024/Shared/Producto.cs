using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MercadonaBlazor2024.Shared
{
    public class Producto
    {
        public String IdProducto { get; set; } //el numero EAN num.identif.unico de cada producto
        public String IdCategoria { get; set; }
        public String NombreProducto { get; set; }
        public Decimal PrecioActual { get; set; }
        public Decimal PrecioUnidad { get; set; }
        public String InformacionDetallada { get; set; }
        public String ImagenProducto { get; set; }

        public Producto()
        {

        }
        public Producto(IDataRecord filaProducto)
        {
            //...constructor de objetos a partir de filas de tabla Productos
            foreach (PropertyInfo prop in this.GetType().GetProperties())
            {
                String nombreprop = prop.Name;
                prop.SetValue(this, filaProducto[nombreprop]);
            }
        }
    }
}

