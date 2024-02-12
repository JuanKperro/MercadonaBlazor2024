using System;
using MercadonaBlazor2024.Shared;

namespace MercadonaBlazor2024.Client.Models.Interfaces
{
    public interface IStorageService
    {

        public event EventHandler<Cliente> ClienteRecupIndexedDBEvent;


        void AlmacenamientoDatosCliente(Cliente datoscliente);
        void AlmacenamientoJWT(String jwt);
        Cliente RecuperarDatosCliente();
        String RecuperarJWT();
    }

}
