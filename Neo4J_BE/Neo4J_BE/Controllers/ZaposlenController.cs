using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Neo4J_BE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ZaposlenController : ControllerBase
    {
        [HttpGet]
        [Route("vratiSveZaposlen")]
        public IActionResult VratiSveZaposlen()
        {
            try
            {
                return new JsonResult(DataLayer.DataProvider.VratiSveZaposlen());

            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }

        }


        [HttpGet]
        [Route("vratiSveDosadasnjeZaposleneDateFirme/{idFirme}")]
        public IActionResult VratiSveZaposlene([FromRoute(Name = "idFirme")] int idFirme)
        {
            try
            {
                return new JsonResult(DataLayer.DataProvider.VratiSveDosadasnjeZaposleneDateFirme(idFirme));

            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }

        }

        [HttpGet]
        [Route("vratiSveFirmeZaposlenog/{idZaposlenog}")]
        public IActionResult vratiSveFirmeZaposlenog([FromRoute(Name = "idZaposlenog")] int idZaposlenog)
        {
            try
            {
                return new JsonResult(DataLayer.DataProvider.VratiSveFirmeZaposlenog(idZaposlenog));

            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }

        }

        [HttpPost]
        [Route("dodajZaposlenogFirmi/{nazivFirme}/{idZaposlenog}")]
        public IActionResult DodajZapFirmi([FromRoute(Name = "nazivFirme")] string nazivFirme,
           [FromRoute(Name = "idZaposlenog")] int idZaposlenog,
           [FromBody] Zaposlen zaposlen)
        {
            try
            {
                DataLayer.DataProvider.DodajZaposlen(nazivFirme, idZaposlenog, zaposlen);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpPut]
        [Route("izmeniZaposlen")]
        public IActionResult IzmeniZaposlen([FromBody] Zaposlen zaposlen)
        {
            try
            {
                DataLayer.DataProvider.izmeniZaposlen(zaposlen);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpDelete]
        [Route("obrisiZaposlen/{id}")]
        public IActionResult ObrisiZaposlen([FromRoute(Name = "id")] int id)
        {
            try
            {
                DataLayer.DataProvider.ObrisiZaposlen(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpDelete]
        [Route("obrisiZaposlenZaFirmu/{idFirme}")]
        public IActionResult ObrisiZaposlenZaFirmu([FromRoute(Name = "idFirme")] int idFirme)
        {
            try
            {
                DataLayer.DataProvider.ObrisiZaposlenZaDatuFirmu(idFirme);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpDelete]
        [Route("ObrisiZaposlenZaDatogZaposlenog/{id}")]
        public IActionResult ObrisiZaposlenZaZaposl([FromRoute(Name = "id")] int id)
        {
            try
            {
                DataLayer.DataProvider.ObrisiZaposlenZaDatogZaposlenog(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

    }
}
