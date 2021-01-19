using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Neo4J_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ZaposleniController : ControllerBase
    {
        [HttpGet]
        [Route("vratiSveZaposlene")]
        public IActionResult vratiSveZaposlene()
        {
            try
            {
                return new JsonResult(DataProvider.vratiZaposlene());
            }
            catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        [Route("vratiSveZaposlenePrekoNazivaFirme/{nazivFirme}")]
        public IActionResult vratiSveZaposleneSaNazivomFirme([FromRoute(Name ="nazivFirme")]string nazivFirme)
        {
            try
            {
                return new JsonResult(DataProvider.vratiZaposleneFirme(nazivFirme));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        [Route("vratiSveZaposlenePrekoIdFirme/{idFirme}")]
        public IActionResult vratiSveZaposleneSaIdjemFirme([FromRoute(Name = "idFirme")] int idFirme)
        {
            try
            {
                return new JsonResult(DataProvider.vratiZaposleneFirme(idFirme));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        [Route("vraziZaposlenog/{idZaposlenog}")]
        public IActionResult vratiZaposlenog([FromRoute(Name = "idZaposlenog")] int idZaposlenog)
        {
            try
            {
                return new JsonResult(DataProvider.vratiZaposlenogSaId(idZaposlenog));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        [Route("kreirajZaposlenog")]
        public IActionResult kreirajZaposlenog([FromBody]Zaposleni z)
        {
            try
            {
                DataProvider.dodajZaposlenog(z);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpDelete]
        [Route("obrisiZaposlenog/{idZaposlenog}")]
        public IActionResult obrisiZaposlenog([FromRoute(Name ="idZaposlenog")] int idZaposlenog)
        {
            try
            {
                DataProvider.obrisiZaposlenog(idZaposlenog);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut]
        [Route("azurirajZaposlenog")]
        public IActionResult azurirajZaposlenog([FromBody] Zaposleni z)
        {
            try
            {
                DataProvider.izmeniZaposlenog(z);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
