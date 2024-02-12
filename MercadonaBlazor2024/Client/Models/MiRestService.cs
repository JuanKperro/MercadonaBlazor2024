
using MercadonaBlazor2024.Client.Models.Interfaces;
using MercadonaBlazor2024.Shared;
using System.Net.Http.Json;
using static MercadonaBlazor2024.Shared.Cliente;

namespace MercadonaBlazor2024.Client.Models
{
    public class MiRestService : IRestService
    {
        private HttpClient _httpClient;

        public MiRestService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<List<Provincia>> RecuperarProvincias()
        {
            return await this._httpClient.GetFromJsonAsync<List<Provincia>>("/api/RESTTienda/RecuperarProvincias") ?? new List<Provincia>();

        }

        public async Task<List<Municipio>> RecuperarMunicipios(string codpro)
        {
            return await this._httpClient.GetFromJsonAsync<List<Municipio>>($"/api/RESTTienda/RecuperarMunicipios?codpro={codpro}") ?? new List<Municipio>();

        }

        public Task<RestMessage> RegistrarCliente(Cliente nuevoCliente)
        {
            throw new NotImplementedException();
        }

        public Task<RestMessage> LoginCliente(Credenciales credenciales)
        {
            throw new NotImplementedException();
        }

        public Task<RestMessage> ActualizarDatosCliente(Cliente cliente)
        {
            throw new NotImplementedException();
        }
    }
}
