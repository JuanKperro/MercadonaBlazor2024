using MercadonaBlazor2024.Shared;
using static MercadonaBlazor2024.Shared.Cliente;

namespace MercadonaBlazor2024.Client.Models.Interfaces
{
    public interface IRestService
    {
        Task<RestMessage> RegistrarCliente(Cliente nuevoCliente);
        Task<RestMessage> LoginCliente(Credenciales credenciales);

        Task<RestMessage> ActualizarDatosCliente(Cliente cliente);

        Task<List<Provincia>> RecuperarProvincias();
        Task<List<Municipio>> RecuperarMunicipios(String codpro);
    }
}
