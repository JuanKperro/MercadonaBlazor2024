using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;


namespace MercadonaBlazor2024.Shared
{
    public class Cliente
    {
        #region ...propiedades clase modelo Cliente....

        [Required(ErrorMessage = "* Nombre obligatorio")]
        [MaxLength(50, ErrorMessage = "El nombre no puede exceder de 50 caracteres")]
        public String Nombre { get; set; }



        [Required(ErrorMessage = "* Primer Apellido obligatorio")]
        [MaxLength(100, ErrorMessage = "El primer apellido no puede exceder de 100 caracteres")]
        public String PrimerApellido { get; set; }



        [Required(ErrorMessage = "* Segundo Apellido obligatorio")]
        [MaxLength(100, ErrorMessage = "El segundo apellido no puede exceder de 100 caracteres")]
        public String SegundoApellido { get; set; }



        [Required(ErrorMessage = "* Fecha de nacimiento requerida")]
        public DateTime FechaNacimiento { get; set; }


        public String IdCliente { get; set; } = Guid.NewGuid().ToString();

        public TipoIdentificacion TipoIdentificacionCliente { get; set; }

        //....falta validacion de telefonos.....
        public Dictionary<string, bool> TelefonosContacto { get; set; }

        //...falta validacion de Direcciones
        public Dictionary<String, Direccion> Direcciones { get; set; }


        public Credenciales CredencialesCliente { get; set; }


        public Pedido PedidoActual { get; set; }

        public Dictionary<String, Pedido> HistoricoPedidos { get; set; }

        #endregion



        #region ...metodos clase modelo Cliente....

        public Cliente()
        {
            //incializo props.de tipo Objeto(credenciales, lista direcciones, lista pedidos...)
            this.CredencialesCliente = new Credenciales();
            this.TipoIdentificacionCliente = new TipoIdentificacion();
            this.Direcciones = new Dictionary<String, Direccion>();
            this.TelefonosContacto = new Dictionary<String, bool>();
            this.PedidoActual = new Pedido
            {
                FechaPedido = DateTime.Now,
                EstadoPedido = "en curso"
            };
            this.HistoricoPedidos = new Dictionary<String, Pedido>();
        }
        #endregion

        #region //-------------- subclases  -------------------
        public class Credenciales
        {

            [Required(ErrorMessage = "* Email obligatorio")]
            [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Formato de Email invalido")]
            public String Email { get; set; }

            [Required(ErrorMessage = "* Password obligatoria")]
            [MinLength(4, ErrorMessage = "Se requieren al menos 4 caracteres MIN")]
            [MaxLength(20, ErrorMessage = "la Password no debe tener mas de 20 caracteres")]
            [RegularExpression("^(?=.*[A-Z].*[A-Z])(?=.*[!@#$&*])(?=.*[0-9].*[0-9])(?=.*[a-z].*[a-z].*[a-z]).{4,}$", ErrorMessage = "la password debe tener al menos una letra min, letra MAYS, numero y simbolo")]
            public String? Password { get; set; }

            public String? HashPasword { get; set; }




        }

        public class TipoIdentificacion
        {


            [Required(ErrorMessage = "* Tipo Identificacion requerida")]
            public String TipoId { get; set; } = "NIF";

            //falta validacion del numero identif en funcion del tipo de identificacion....
            [Required(ErrorMessage = "*Numero de Identificacion requerido")]
            public String NumeroId { get; set; }

        }




        #endregion
    }

}

