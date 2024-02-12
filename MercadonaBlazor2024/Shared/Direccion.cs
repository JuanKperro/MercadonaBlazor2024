using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MercadonaBlazor2024.Shared
{
    public class Direccion
    {
        #region ...propiedades de la clase modelo Direccion....

        public String TipoVia { get; set; }

        [Required(ErrorMessage = "* Nombre via obligatorio")]
        public String NombreVia { get; set; }
        public String NumeroVia { get; set; } = "";
        public String Piso { get; set; } = "";
        public String Puerta { get; set; } = "";
        public String Bloque { get; set; } = "";
        public String Escalera { get; set; } = "";
        public String Urbanizacion { get; set; } = "";
        public String Observaciones { get; set; } = "";

        [Required(ErrorMessage = "* CP obligatorio")]
        public int CP { get; set; }
        public Provincia Provincia { get; set; }
        public Municipio Municipio { get; set; }
        public bool EsPrincipal { get; set; }
        public String IdCliente { get; set; }
        public String IdDireccion { get; set; } = Guid.NewGuid().ToString();

        #endregion


        #region ...metodos de la clase modelo Direccion....

        #endregion
    }
}
