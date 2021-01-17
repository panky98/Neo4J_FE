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
    public class OsvojilaController : ControllerBase
    {
        [HttpGet]
        [Route("vratiSveOsvojeneNagrade")]
        public IActionResult vrateSveNagrade()
        {
            try
            {
                return new JsonResult(DataProvider.vratiSveOsvojeneNagrade());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        [Route("vratiSveNagradeKojeJeOsvojilaFirma/{id}")]
        public IActionResult vratiSveNagradeKojeJeOsvojilaFirma(int id)
        {
            try
            {
                return new JsonResult(DataProvider.vratiSveNagradeKojeJeOsvojilaFirma(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        [Route("vratiSveFirmeKojeSuOsvojileNagradu/{id}")]
        public IActionResult vratiSveFirmeKojeSuOsvojileNagradu(int id)
        {
            try
            {
                return new JsonResult(DataProvider.vratiSveFirmeKojeSuOsvojileNagradu(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        [Route("dodeliNagraduFirmi/{idFirme}/{idNagrade}")]
        public IActionResult dodeliNagraduFirmi([FromRoute(Name = "idFirme")] int idFirme, 
            [FromRoute(Name = "idNagrade")] int idNagrade)
        {
            try
            {
                DataProvider.dodeliNagraduFirmi(idFirme, idNagrade);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpDelete]
        [Route("oduzmiNagraduFirmi/{idFirme}/{idNagrade}")]
        public IActionResult oduzmiNagraduFirmi([FromRoute(Name = "idFirme")] int idFirme,
            [FromRoute(Name = "idNagrade")] int idNagrade)
        {
            try
            {
                DataProvider.oduzmiNagraduFirmi(idFirme, idNagrade);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpDelete]
        [Route("oduzmiSveNagradeFirmi/{idFirme}")]
        public IActionResult oduzmiSveNagradeFirmi([FromRoute(Name = "idFirme")] int idFirme)
        {
            try
            {
                DataProvider.oduzmiSveNagradeFirmi(idFirme);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


    }
}
