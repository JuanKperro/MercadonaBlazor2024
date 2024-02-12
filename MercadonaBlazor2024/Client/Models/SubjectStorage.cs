
using MercadonaBlazor2024.Client.Models.Interfaces;
using MercadonaBlazor2024.Shared;
using System.Reactive.Subjects;

namespace MercadonaBlazor2024.Client.Models
{
    public class SubjectStorage : IStorageService
    {

        public event EventHandler<Cliente> ClienteRecupIndexedDBEvent;

        private BehaviorSubject<Cliente> _clienteSubject = new BehaviorSubject<Cliente>(null);
        private BehaviorSubject<String> _jwtSubject = new BehaviorSubject<string>("");


        private Cliente _datosCliente = new Cliente(); //<----variable para almacenar datos del subject Cliente
        private String _datosJWT = ""; //<--------------------variable para almacenar datos del subject String

        public SubjectStorage()
        {
            IDisposable _subscripClienteSubject = this._clienteSubject
                                                .Subscribe<Cliente>(
                                                    (Cliente datosObs) => this._datosCliente = datosObs
                                                    );

            IDisposable _jwtSubject = this._jwtSubject
                                        .Subscribe<String>(
                                            (String datosJWT) => this._datosJWT = datosJWT
                                        );

        }


        public void AlmacenamientoDatosCliente(Cliente datoscliente)
        {
            this._clienteSubject.OnNext(datoscliente); //<---- actualizo datos en observable Cliente
            this.ClienteRecupIndexedDBEvent.Invoke(this, datoscliente); //<----disparo evento de actualizacion de datos del cliente por si alguien escucha el evento....
        }

        public void AlmacenamientoJWT(string jwt)
        {
            this._jwtSubject.OnNext(jwt); //<------ actualizo datos en observable String...
        }

        public Cliente RecuperarDatosCliente()
        {
            return this._datosCliente;
        }
        public string RecuperarJWT()
        {
            return this._datosJWT;
        }
    }
}
