using DataLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Neo4J_BE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImaKomentarController : ControllerBase
    {

        [HttpGet]
        [Route("vratiKomSvihFirmi")]
        public IActionResult vrateSveKomSvihF()
        {
            try
            {
                return new JsonResult(DataProvider.vratiKomSvihFirmi());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        [Route("vratiSveFirmeKojeImajuKomentar/{id}")]
        public IActionResult vratiSveFirmeKojeImajuKomentar(int id)
        {
            try
            {
                return new JsonResult(DataProvider.vratiSveFirmeKojeImajuKomentar(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        [Route("vratiSveKomentareOdredjeneFirme/{id}")]
        public IActionResult vratiSveKomentareOdredjeneFirme(int id)
        {
            try
            {
                return new JsonResult(DataProvider.vratiSveKomentareOdredjeneFirme(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        [Route("dodeliKomentarFirmi/{idFirme}/{idKom}")]
        public IActionResult dodeliNagraduFirmi([FromRoute(Name = "idFirme")] int idFirme,
            [FromRoute(Name = "idKom")] int idKom)
        {
            try
            {
                DataProvider.dodeliNagraduFirmi(idFirme, idKom);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpDelete]
        [Route("obrisiKomFirme/{idFirme}/{idKom}")]
        public IActionResult oduzmiNagraduFirmi([FromRoute(Name = "idFirme")] int idFirme,
            [FromRoute(Name = "idKom")] int idKom)
        {
            try
            {
                DataProvider.oduzmiNagraduFirmi(idFirme, idKom);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpDelete]
        [Route("obrisiSveKomFirme/{idFirme}")]
        public IActionResult obrisiSveKomFirme([FromRoute(Name = "idFirme")] int idFirme)
        {
            try
            {
                DataProvider.obrisiSveKomFirme(idFirme);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
