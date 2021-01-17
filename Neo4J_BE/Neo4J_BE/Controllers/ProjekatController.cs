using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace Neo4J_BE.Controllers
{
    
        [Route("[controller]")]
        [ApiController]
        public class ProjekatController : ControllerBase
        {
            [HttpGet]
            [Route("vratiSveProjekte")]
            public IActionResult VratiSveProjekte()
            {
                try
                {
                    return new JsonResult(DataLayer.DataProvider.vratiSveProjekte());
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.ToString());
                }
            }

            [HttpGet]
            [Route("vratiSveGotoveProjekte")]
            public IActionResult VratiSveGotoveProjekte()
            {
                try
                {
                    return new JsonResult(DataLayer.DataProvider.vratiSveGotoveProjekte(true));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.ToString());
                }
            }

            [HttpGet]
            [Route("vratiSveTrenutneProjekte")]
            public IActionResult VratiSveTrenutneProjekte()
            {
                try
                {
                    return new JsonResult(DataLayer.DataProvider.vratiSveGotoveProjekte(false));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.ToString());
                }
            }

            [HttpGet]
            [Route("vratiProjekat/{idProjekta}")]
            public IActionResult VratiProjekat([FromRoute(Name = "idProjekta")] int idProjekta)
            {
                try
                {
                    return new JsonResult(DataLayer.DataProvider.vratiProjekat(idProjekta));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.ToString());
                }
            }

            [HttpPost]
            [Route("dodajProjekat")]
            public IActionResult DodajProjekat([FromBody] Projekat projekat)
            {
                try
                {
                    DataProvider.dodajProjekat(projekat);
                    return Ok();
                }
                catch (Exception e)
                {
                    return BadRequest(e.ToString());
                }
            }

            [HttpPut]
            [Route("izmeniProjekat")]
            public IActionResult IzmeniProjekat([FromBody] Projekat projekat)
            {
                try
                {
                    DataProvider.izmeniProjekat(projekat);
                    return Ok();
                }
                catch (Exception e)
                {
                    return BadRequest(e.ToString());
                }
            }
            [HttpDelete]
            [Route("obrisiProjekat/{idProjekta}")]
            public IActionResult ObrisiProjekat([FromRoute(Name = "idProjekta")] int idProjekta)
            {
                try
                {
                    DataProvider.obrisiProjekat(idProjekta);
                    return Ok();
                }
                catch (Exception e)
                {
                    return BadRequest(e.ToString());
                }
            }
        }
    }

