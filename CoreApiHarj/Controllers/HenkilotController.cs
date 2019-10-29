using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreApiHarj.Models;

namespace CoreApiHarj.Controllers

{
    [Route("omareitti/[controller]")]
    [ApiController]
    public class HenkilotController : ControllerBase
    {
        [HttpGet]
        [Route("merkkijono/{nimi}")]
        public string Merkkijono(string nimi)
        {
            return "Päivää, " + nimi;
        }

        //Koska ei ole filtteriä, tähän pääsee kaikilla http-pyynnöillä
        [Route("paivamaara")]
        public DateTime Pvm()
        {
            return DateTime.Now;
        }


        [HttpGet]
        [Route("olio")]
        public Henkilo Olio()
        {
            return new Henkilo()
            {
                Nimi = "Paavo Pesusieni",
                Osoite = "Kotikatu 1",
                Ika = 11
            };
        }

        /* reitiksi tulee: localhost:5001/omareitti/henkilot/oliolista */
        [HttpGet]
        [Route("oliolista")]
        public List<Henkilo> OlioLista()
        {
            List<Henkilo> henkilot = new List<Henkilo>()
            {
                new Henkilo()
                {
                    Nimi = "Paavo Pesusieni",
                    Osoite = "Kotikatu 1",
                    Ika = 11
                },
                new Henkilo()
                {
                    Nimi = "Pia Pakkanen",
                    Osoite = "Koulukatu 1",
                    Ika = 20
                },
                new Henkilo()
                {
                    Nimi = "Tiina Tomera",
                    Osoite = "Kotikatu 1",
                    Ika = 35
                }

            };

            return henkilot; 
        }

    }
}