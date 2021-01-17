using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Neo4J_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PripadaController : ControllerBase
    {

        [HttpGet]
        [Route("vratiProjekteIFirme")]
        public IActionResult VratiProjekteIFirme()
        {
            try
            {
                var pripadanja = DataLayer.DataProvider.VratiSvePripada();
                return new JsonResult(pripadanja);

            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpGet]
        [Route("vratiProjekteFirme/{idFirme}")]
        public IActionResult VratiProjekteFirme([FromRoute(Name = "idFirme")] int idFirme)
        {
            try
            {
                var projekti = DataLayer.DataProvider.VratiSveProjekteDateFirme(idFirme);

                return new JsonResult(projekti);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpGet]
        [Route("vratiFirmeProjekta/{idProjekta}")]
        public IActionResult VratiFirmeProjekta([FromRoute(Name = "idProjekta")] int idProjekta)
        {
            try
            {
                var projekti = DataLayer.DataProvider.VratiFirmeKojeRadeNaProjektu(idProjekta);

                return new JsonResult(projekti);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpPost]
        [Route("dodajProjekatFirmi/{nazivProjekta}/{nazivFirme}")]
        public IActionResult DodajProjekatFirmi([FromRoute(Name = "nazivProjekta")] string nazivProjekta,
            [FromRoute(Name = "nazivFirme")] string nazivFirme,
            [FromBody] Pripada pripada)
        {
            try
            {
                DataLayer.DataProvider.DodajPripada(nazivFirme, nazivProjekta, pripada);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpPut]
        [Route("izmeniPripada")]
        public IActionResult IzmeniPripada([FromBody] Pripada pripada)
        {
            try
            {
                DataLayer.DataProvider.IzmeniPripada(pripada);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpDelete]
        [Route("obrisiPripadaZaFirmu/{idFirme}")]
        public IActionResult ObrisiPripadaZaFirmu([FromRoute(Name = "idFirme")] int idFirme)
        {
            try
            {
                DataLayer.DataProvider.ObrisiPripadaZaDatuFirmu(idFirme);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpDelete]
        [Route("ObrisiPripadaZaDatiProjekat/{id}")]
        public IActionResult ObrisiPripadaZaDatiProjekat([FromRoute(Name = "id")] int id)
        {
            try
            {
                DataLayer.DataProvider.ObrisiPripadaZaDatiProjekat(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpDelete]
        [Route("obrisiPripada/{id}")]
        public IActionResult ObrisiPripada([FromRoute(Name = "id")] int id)
        {
            try
            {
                DataLayer.DataProvider.ObrisiPripada(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpDelete]
        [Route("obrisiPripadaZaProjekatIFirmu/{idFirme}/{idProjekta}")]
        public IActionResult ObrisiPripadaZaProjekatIFirmu([FromRoute(Name ="idFirme")]int idFirme, [FromRoute(Name ="idProjekta")]int idProjekta)
        {
            try
            {
                DataLayer.DataProvider.ObrisiPripadaZaProjekatIFirmu(idProjekta, idFirme);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

    }
}
