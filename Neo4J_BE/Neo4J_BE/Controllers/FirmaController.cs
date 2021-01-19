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
    public class FirmaController : ControllerBase
    {
        [HttpGet]
        [Route("vratiSveFirme")]
        public IActionResult vratiSveFirme()
        {
            try
            {
                return new JsonResult(DataProvider.vratiSveFirme());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        [Route("vratiFirmuPrekoId/{idFirme}")]
        public IActionResult vratiFirmuPrekoId([FromRoute(Name = "idFirme")] int idFirme)
        {
            try
            {
                return new JsonResult(DataProvider.vratiFirmuLINQID(idFirme));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [HttpPost]
        [Route("kreirajFirmu")]
        public IActionResult kreirajFirmu([FromBody] Firma f)
        {
            try
            {
                DataProvider.dodajFirmu(f);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpDelete]
        [Route("obrisiFirmu/{idFirme}")]
        public IActionResult obrisiFirmu([FromRoute(Name = "idFirme")] int idFirme)
        {
            try
            {
                DataProvider.obrisiFirmu(idFirme);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut]
        [Route("azurirajFirmu")]
        public IActionResult azurirajFirmu([FromBody] Firma f)
        {
            try
            {
                DataProvider.izmeniFirmu(f);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
