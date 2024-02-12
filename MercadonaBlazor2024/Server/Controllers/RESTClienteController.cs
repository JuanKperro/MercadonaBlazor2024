using MercadonaBlazor2024.Shared;
using Microsoft.AspNetCore.Mvc;
using MercadonaBlazor2024.Server.Models;

namespace MercadonaBlazor2024.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RESTClienteController : ControllerBase
    {
        private AaplicacionDBContext _dbContext;

        [HttpGet]
        public List<Provincia> RecuperarProvincias()
        {
            try
            {
                return this._dbContext.Provincias.AsEnumerable<Provincia>().OrderBy((Provincia p) => p.CodPro).ToList<Provincia>();
            }
            catch (Exception ex)
            {

                return new List<Provincia>();
            }
        }

        [HttpGet]
        public List<Municipio> RecuperarMunicipios([FromQuery] int codpro)
        {
            try
            {
                return this._dbContext.Municipios.Where((Municipio muni) => muni.CodPro == codpro).ToList<Municipio>();
            }
            catch (Exception ex)
            {

                return new List<Municipio>();
            }
        }

    }
}
