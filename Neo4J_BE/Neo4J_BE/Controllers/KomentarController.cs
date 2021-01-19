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
    public class KomentarController : ControllerBase
    {

        [HttpGet]
        [Route("vratiSveKomentare")]
        public ActionResult vratiSveKomentare()
        {
            try
            {
                return new JsonResult(DataProvider.vratiSveKomentare());
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }


        [HttpGet]
        [Route("vratiKomSaID/{id}")]
        public ActionResult vratiKomSaId([FromRoute(Name ="id")] int idK)
        {
            try
            {
                return new JsonResult(DataProvider.vratiKomSaId(idK));
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpPost]
        [Route("dodajKomentar")]
        public ActionResult dodajKomentar([FromBody] Komentar kom)
        {
            try
            {
                DataProvider.dodajKomentar(kom);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpPut]
        [Route("izmeniKomentar")]
        public ActionResult izmeniKom([FromBody] Komentar k)
        {
            try
            {
                DataProvider.izmeniKomentar(k);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpDelete]
        [Route("obrisiKomentar/{id}")]
        public ActionResult obrisiKom([FromRoute(Name = "id")] int id)
        {
            try
            {
                DataProvider.obrisiKomentar(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }


    }
}
