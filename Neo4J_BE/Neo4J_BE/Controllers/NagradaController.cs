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
    public class NagradaController : ControllerBase
    {
        [HttpGet]
        [Route("vratiSveNagrade")]
        public ActionResult vratiSveFirme()
        {
            try
            {
                return new JsonResult(DataProvider.vratiSveNagrade());
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpGet]
        [Route("vratiNagradePoImenu/{naziv}")]
        public ActionResult vratiNagradePoNazivu([FromRoute(Name = "naziv")] String naziv)
        {
            try
            {
                return new JsonResult(DataProvider.vratiNagradePoNazivu(naziv));
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpPost]
        [Route("dodajNagradu")]
        public ActionResult dodajNagradu([FromBody] Nagrada nagrada)
        {
            try
            {
                DataProvider.dodajNagradu(nagrada);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpPut]
        [Route("izmeniNagradu")]
        public ActionResult izmeniNagradu([FromBody] Nagrada nagrada)
        {
            try
            {
                DataProvider.izmeniNagradu(nagrada);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpDelete]
        [Route("obrisiNagradu/{id}")]
        public ActionResult obrisiNagradu([FromRoute(Name = "id")] int id)
        {
            try
            {
                DataProvider.obrisiNagradu(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }



    }
}
