using MercadonaBlazor2024.Shared;
using Microsoft.AspNetCore.Identity;


namespace MercadonaBlazor2024.Server.Models
{
    public class MiClienteIdentity : IdentityUser
    {
        //clase personalizada para añadir sobre las props. de IdentityUser datos propios que me interesan y q Identity no refleja
        #region .... propiedades nuevas q añadimos a clase IdentityUser .....
        public String Nombre { get; set; }
        public String PrimerApellido { get; set; }

        public String SegundoApellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public String Descripcion { get; set; }


        #endregion
    }
}
