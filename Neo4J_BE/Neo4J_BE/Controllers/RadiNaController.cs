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
    public class RadiNaController : ControllerBase
    {
        [HttpGet]
        [Route("vratiSveRadiNa")]
        public IActionResult VratiSveRadiNa()
        {
            try
            {
                return new JsonResult(DataLayer.DataProvider.VratiSveRadiNa());

            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }

        }

        [HttpGet]
        [Route("vratiSveZaposleneNaProjektu/{idProjekta}")]
        public IActionResult VratiSveRadiNa([FromRoute(Name = "idProjekta")] int idProjekta)
        {
            try
            {
                return new JsonResult(DataLayer.DataProvider.VratiSveZaposleneNaProjektu(idProjekta));

            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }

        }

        [HttpGet]
        [Route("vratiSveProjekteZaposlenog/{idZaposlenog}")]
        public IActionResult vratiSveProjekteZaposlenog([FromRoute(Name = "idZaposlenog")] int idZaposlenog)
        {
            try
            {
                return new JsonResult(DataLayer.DataProvider.VratiSveProjekteZaposlenog(idZaposlenog));

            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }

        }

        [HttpPost]
        [Route("dodajProjekatZaposlenom/{idProjekta}/{idZaposlenog}")]
        public IActionResult DodajProjekatZaposlenom([FromRoute(Name = "idProjekta")] int idProjekta,
            [FromRoute(Name = "idZaposlenog")] int idZaposlenog,
            [FromBody] RadiNa radiNa)
        {
            try
            {
                DataLayer.DataProvider.DodajProjekatZaposlenom(idProjekta, idZaposlenog, radiNa);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpPut]
        [Route("izmeniRadiNa")]
        public IActionResult IzmeniRadiNa([FromBody] RadiNa radiNa)
        {
            try
            {
                DataLayer.DataProvider.IzmeniRadiNa(radiNa);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpDelete]
        [Route("obrisiRadiNaZaZaposlenog/{idZaposlenog}")]
        public IActionResult ObrisiRadiNaZaZaposlenog([FromRoute(Name = "idZaposlenog")] int idZaposlenog)
        {
            try
            {
                DataLayer.DataProvider.ObrisiRadiNaZaZaposlenog(idZaposlenog);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpDelete]
        [Route("obrisiRadiNaZaDatiProjekat/{idProjekta}")]
        public IActionResult ObrisiRadiNaZaDatiProjekat([FromRoute(Name = "idProjekta")] int idProjekta)
        {
            try
            {
                DataLayer.DataProvider.ObrisiRadiNaZaDatiProjekat(idProjekta);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpDelete]
        [Route("obrisiRadiNa/{id}")]
        public IActionResult ObrisiRadiNa([FromRoute(Name = "id")] int id)
        {
            try
            {
                DataLayer.DataProvider.ObrisiRadiNa(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
    }
}
