using DataLayer.Models;
using Neo4jClient;
using Neo4jClient.Cypher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer
{
    
    public static class DataProvider
    {
        private static int getMaxId()
        {
            var query = new Neo4jClient.Cypher.CypherQuery("start n=node(*) where exists(n.id) return max(n.id)",
                                                    new Dictionary<string, object>(), CypherResultMode.Set);
            int maxId = ((IRawGraphClient)Session.Client).ExecuteGetCypherResults<int>(query).ToList().FirstOrDefault();

            return maxId;
        }

        #region Zaposleni

        #region GetMethods

        public static IList<Zaposleni> vratiZaposlene()
        {
            var query = new Neo4jClient.Cypher.CypherQuery("MATCH (n:Zaposleni) RETURN n",
                                               new Dictionary<string, object>(), CypherResultMode.Set);

            List<Zaposleni> zaposleni = ((IRawGraphClient)Session.Client).ExecuteGetCypherResults<Zaposleni>(query).ToList();

            //Dusan:
            foreach(var z in zaposleni)
            {
                z.Projekti = DataProvider.VratiSveProjekteZaposlenog(z.id);
            }

            return zaposleni;
        }


        public static IList<Zaposleni> vratiZaposleneFirme(String imeFirme)
        {
            string nazivFirme = ".*" + imeFirme + ".*";

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("nazivFirme", nazivFirme);


            var query = new Neo4jClient.Cypher.CypherQuery("MATCH (n:Zaposleni)-[r:ZAPOSLEN]-(m:Firma) WHERE exists(m.naziv) and m.naziv=~ {nazivFirme} RETURN n",
                                               queryDict, CypherResultMode.Set);

            List<Zaposleni> zaposleni = ((IRawGraphClient)Session.Client).ExecuteGetCypherResults<Zaposleni>(query).ToList();

            foreach (var z in zaposleni)
            {
                z.Projekti = DataProvider.VratiSveProjekteZaposlenog(z.id);
            }

            return zaposleni;
        }

        public static IList<Zaposleni> vratiZaposleneFirme(int idFirme)
        {

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("idFirme", idFirme);


            var query = new Neo4jClient.Cypher.CypherQuery("MATCH (n:Zaposleni)-[r:ZAPOSLEN]-(m:Firma) WHERE exists(m.id) and m.id={idFirme} RETURN n",
                                               queryDict, CypherResultMode.Set);

            List<Zaposleni> zaposleni = ((IRawGraphClient)Session.Client).ExecuteGetCypherResults<Zaposleni>(query).ToList();
            foreach (var z in zaposleni)
            {
                z.Projekti = DataProvider.VratiSveProjekteZaposlenog(z.id);
            }

            return zaposleni;
        }
        public static Zaposleni vratiZaposlenogSaId(int idZaposlenog)
        {

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("idZaposlenog", idZaposlenog);
            var query = new Neo4jClient.Cypher.CypherQuery("MATCH (n:Zaposleni) WHERE exists(n.id) and n.id={idZaposlenog} RETURN n",
                                               queryDict, CypherResultMode.Set);

            Zaposleni zaposleni = ((IRawGraphClient)Session.Client).ExecuteGetCypherResults<Zaposleni>(query).SingleOrDefault();

            zaposleni.Projekti = DataProvider.VratiSveProjekteZaposlenog(zaposleni.id);

            return zaposleni;
        }
        #endregion

        public static void dodajZaposlenog(Zaposleni zap)
        {
            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("id", DataProvider.getMaxId() + 1);
            queryDict.Add("ime", zap.ime);
            queryDict.Add("prezime", zap.prezime);
            queryDict.Add("starost", zap.starost);
            queryDict.Add("pol", zap.pol);

            var query = new Neo4jClient.Cypher.CypherQuery("CREATE (n:Zaposleni {id:{id},ime: {ime}, prezime:{prezime},starost:{starost},pol:{pol}}) return n",
                                                            queryDict, CypherResultMode.Set);

            ((IRawGraphClient)Session.Client).ExecuteGetCypherResults<Zaposleni>(query);
        }

        public static void obrisiZaposlenog(int idZaposlenog)
        {
            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("idZaposlenog", idZaposlenog);

            //obrisi prvo sve veze zaposlenog

            //RADI_NA
            DataProvider.ObrisiRadiNaZaZaposlenog(idZaposlenog);

            //

            var query = new Neo4jClient.Cypher.CypherQuery("MATCH(n: Zaposleni) WHERE n.id= {idZaposlenog} DELETE n",
                                                            queryDict, CypherResultMode.Set);

            ((IRawGraphClient)Session.Client).ExecuteCypher(query);
        }

        public static void izmeniZaposlenog(Zaposleni zap)
        {
            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("id", zap.id);
            queryDict.Add("ime", zap.ime);
            queryDict.Add("prezime", zap.prezime);
            queryDict.Add("starost", zap.starost);
            queryDict.Add("pol", zap.pol);

            var query = new Neo4jClient.Cypher.CypherQuery("MATCH (n:Zaposleni {id:{id}}) SET n={id:{id},ime: {ime}, prezime:{prezime},starost:{starost},pol:{pol}} return n",
                                                queryDict, CypherResultMode.Set);
            ((IRawGraphClient)Session.Client).ExecuteCypher(query);
        }

        #endregion

        #region Firma

        public static IList<Firma> vratiSveFirme()
        {
            var query = new Neo4jClient.Cypher.CypherQuery("MATCH (n:Firma) RETURN n",
                                   new Dictionary<string, object>(), CypherResultMode.Set);

            List<Firma> firme = ((IRawGraphClient)Session.Client).ExecuteGetCypherResults<Firma>(query).ToList();


            //Dusan:
            foreach(var f in firme)
            {
                f.Projekti=DataLayer.DataProvider.VratiSveProjekteDateFirme(f.id);
            }

            return firme;
        }

        public static IList<Firma> vratiFirmuSaId(int idFirme)
        {
            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("id", idFirme);
            var query = new Neo4jClient.Cypher.CypherQuery("MATCH (n:Firma)  WHERE n.id={id} RETURN n",
                                   queryDict, CypherResultMode.Set);

            List<Firma> firme = ((IRawGraphClient)Session.Client).ExecuteGetCypherResults<Firma>(query).ToList();


            //Dusan:
            foreach (var f in firme)
            {
                f.Projekti = DataLayer.DataProvider.VratiSveProjekteDateFirme(f.id);
            }

            return firme;
        }

        public static void dodajFirmu(Firma fir)
        {
            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("id", DataProvider.getMaxId() + 1);
            queryDict.Add("naziv", fir.naziv);
            queryDict.Add("adresa", fir.adresa);
            queryDict.Add("pib", fir.PIB);

            var query = new Neo4jClient.Cypher.CypherQuery("CREATE (n:Firma {id:{id},naziv: {naziv}, adresa:{adresa},PIB:{pib}}) return n",
                                                            queryDict, CypherResultMode.Set);

            ((IRawGraphClient)Session.Client).ExecuteGetCypherResults<Firma>(query);
        }



        public static void obrisiFirmu(int idFirme)
        {
            Dictionary<string, object> queryDict = new Dictionary<string, object>();

            //Dusan:Prvo je potrebno obrisati sve potege date firme...brisem samo PRIPADA za projekat

            DataProvider.ObrisiPripadaZaDatuFirmu(idFirme);

            //

            queryDict.Add("idFirme", idFirme);

            var query = new Neo4jClient.Cypher.CypherQuery("MATCH(n: Firma) WHERE n.id= {idFirme} DELETE n",
                                                            queryDict, CypherResultMode.Set);

            ((IRawGraphClient)Session.Client).ExecuteCypher(query);
        }

        public static void izmeniFirmu(Firma fir)
        {
            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("id", fir.id);
            queryDict.Add("naziv", fir.naziv);
            queryDict.Add("adresa", fir.adresa);
            queryDict.Add("pib", fir.PIB);

            var query = new Neo4jClient.Cypher.CypherQuery("MATCH (n:Firma {id:{id}}) SET n={id:{id},naziv: {naziv}, adresa:{adresa},PIB:{pib}} return n",
                                                queryDict, CypherResultMode.Set);
            ((IRawGraphClient)Session.Client).ExecuteCypher(query);
        }

        //LINQ
        public static Firma vratiFirmuLINQID(int idFirme)
        {
            //Dusan:

           /* return Session.Client.Cypher.Match("(n:Firma)")
                .Where((Firma n) => n.id == idFirme)
                .Return(n => n.As<Firma>())
                .Results.FirstOrDefault();*/

            Firma firma= Session.Client.Cypher.Match("(n:Firma)")
                .Where((Firma n) => n.id == idFirme)
                .Return(n => n.As<Firma>())
                .Results.FirstOrDefault();

            firma.Projekti = DataProvider.VratiSveProjekteDateFirme(firma.id);

            return firma;
        }
        #endregion

        #region Projekat

        public static IList<Projekat> vratiSveProjekte()
        {
            var query = new Neo4jClient.Cypher
                .CypherQuery("MATCH (p:Projekat) return p;",
                new Dictionary<string, object>(), CypherResultMode.Set);

            IList<Projekat> sviProjekti = ((IRawGraphClient)Session.Client)
                .ExecuteGetCypherResults<Projekat>(query)
                .ToList();

            foreach(var projekat in sviProjekti)
            {
                projekat.FirmeNaProjektu = DataProvider.VratiFirmeKojeRadeNaProjektu(projekat.id);
                projekat.ZaposleniNaProjektu = DataProvider.VratiSveZaposleneNaProjektu(projekat.id);
            }

            return sviProjekti;
        }

        public static IList<Projekat> vratiSveGotoveProjekte(bool gotovi)
        {
            CypherQuery query;
            if(gotovi==true)
            {
                query = new Neo4jClient.Cypher
                .CypherQuery("match (p:Projekat) WHERE exists(p.datum_do) return p;",
                new Dictionary<string, object>(), CypherResultMode.Set);
            }
            else
            {
                query = new Neo4jClient.Cypher
               .CypherQuery("match (p:Projekat) WHERE not exists(p.datum_do) return p;",
               new Dictionary<string, object>(), CypherResultMode.Set);
            }
            IList<Projekat> sviProjekti = ((IRawGraphClient)Session.Client)
               .ExecuteGetCypherResults<Projekat>(query)
               .ToList();

            foreach (var projekat in sviProjekti)
            {
                projekat.FirmeNaProjektu = DataProvider.VratiFirmeKojeRadeNaProjektu(projekat.id);
                projekat.ZaposleniNaProjektu = DataProvider.VratiSveZaposleneNaProjektu(projekat.id);
            }

            return sviProjekti;

        }

        public static Projekat vratiProjekat(int idProjekta)
        {
            Projekat projekat= Session.Client.Cypher.Match("(p:Projekat)")
                .Where((Projekat p) => p.id == idProjekta)
                .Return(p => p.As<Projekat>())
                .Results.FirstOrDefault();

            projekat.FirmeNaProjektu = DataProvider.VratiFirmeKojeRadeNaProjektu(projekat.id);
            projekat.ZaposleniNaProjektu = DataProvider.VratiSveZaposleneNaProjektu(projekat.id);

            return projekat;

        }

        public static void dodajProjekat(Projekat projekat)
        {
            Dictionary<string, object> queryDictionary = new Dictionary<string, object>();

            queryDictionary.Add("id", DataProvider.getMaxId() + 1);
            queryDictionary.Add("naziv", projekat.naziv);
            queryDictionary.Add("opis", projekat.opis);
            queryDictionary.Add("datum_od", projekat.datum_od);

            CypherQuery query;
            if (projekat.datum_do != DateTime.MinValue)
            {
                queryDictionary.Add("datum_do", projekat.datum_do);

                query = new Neo4jClient.Cypher.CypherQuery("CREATE (p:Projekat {id:{id},naziv: {naziv}, opis:{opis},datum_od:{datum_od}, datum_do:{datum_do}}) return p",
                                                           queryDictionary, CypherResultMode.Set);
            }
            else
                query = new Neo4jClient.Cypher.CypherQuery("CREATE (p:Projekat {id:{id},naziv: {naziv}, opis:{opis},datum_od:{datum_od}}) return p",
                                                           queryDictionary, CypherResultMode.Set);


            ((IRawGraphClient)Session.Client).ExecuteGetCypherResults<Projekat>(query);
        }

        public static void izmeniProjekat(Projekat projekat)
        {
            Dictionary<string, object> queryDictionary = new Dictionary<string, object>();

            queryDictionary.Add("id", DataProvider.getMaxId() + 1);
            queryDictionary.Add("naziv", projekat.naziv);
            queryDictionary.Add("opis", projekat.opis);
            queryDictionary.Add("datum_od", projekat.datum_od);

            CypherQuery query;

            if (projekat.datum_do != DateTime.MinValue)
            {
                queryDictionary.Add("datum_do", projekat.datum_do);

                query = new Neo4jClient.Cypher.CypherQuery("MATCH (p:Projekat {id:{id}}) SET p={id:{id},naziv: {naziv}, opis:{opis},datum_od:{datum_od}, datum_do:{datum_do}} return p",
                                                           queryDictionary, CypherResultMode.Set);
            }
            else
                query = new Neo4jClient.Cypher.CypherQuery("MATCH (p:Projekat {id:{id}}) SET p={id:{id},naziv: {naziv}, opis:{opis},datum_od:{datum_od}} return p",
                                                           queryDictionary, CypherResultMode.Set);
            
            ((IRawGraphClient)Session.Client).ExecuteGetCypherResults<Projekat>(query);

            
        }

        public static void obrisiProjekat(int id)
        {
            Dictionary<string, object> queryDict = new Dictionary<string, object>();

            //prvo obrisati sve veze sa projektom...trenutno ima samo PRIPADA...

            DataProvider.ObrisiPripadaZaDatiProjekat(id);
            DataProvider.ObrisiRadiNaZaDatiProjekat(id);

            //DODATI BRISANJE OSTALIH VEZA!!!!

            queryDict.Add("id", id);

            var query = new Neo4jClient.Cypher.CypherQuery("MATCH(p: Projekat) WHERE p.id= {id} DELETE p",
                                                            queryDict, CypherResultMode.Set);

            ((IRawGraphClient)Session.Client).ExecuteCypher(query);
        }



        #endregion

        #region Pripada


        public static object VratiSvePripada()
        {

            var obj = Session.Client.Cypher
                .Match("(f:Firma)-[r:PRIPADA]->(p:Projekat)")
                .Return((f, p, r) => new
                {
                    Firma = f.As<Firma>(),
                   Projekat = p.As<Projekat>(),
                    Pripada = r.As<Pripada>()
                })
                .Results.ToList();


            return obj;
        }

        public static IList<Projekat> VratiSveProjekteDateFirme(int idFirme)
        {
            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("id", idFirme);
            var query = new Neo4jClient.Cypher.CypherQuery("MATCH (n:Firma {id:{id}})-[pr:PRIPADA]-(p:Projekat) return p;",
                                   queryDict, CypherResultMode.Set);

            List<Projekat> projekti = ((IRawGraphClient)Session.Client).ExecuteGetCypherResults<Projekat>(query).ToList();
            return projekti;
        }

        public static IList<Firma> VratiFirmeKojeRadeNaProjektu(int idProjekta)
        {
            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("id", idProjekta);
            var query = new Neo4jClient.Cypher.CypherQuery("MATCH (f:Firma )-[pr:PRIPADA]-(p:Projekat {id:{id}}) return f;",
                                   queryDict, CypherResultMode.Set);

            List<Firma> firme = ((IRawGraphClient)Session.Client).ExecuteGetCypherResults<Firma>(query).ToList();
            return firme;
        }

        public static void DodajPripada(string nazivFirme, string nazivProjekta, Pripada pripada)
        {
            pripada.id = DataProvider.getMaxId() + 1;
            var query =Session.Client.Cypher
                        .Match("(f:Firma)", "(p:Projekat)")
                        .Where((Firma f) => f.naziv == nazivFirme)
                        .AndWhere((Projekat p) => p.naziv == nazivProjekta)
                        .Create("(f)-[r:PRIPADA {pripada}]->(p)")
                        .WithParam("pripada", pripada);

            query.ExecuteWithoutResults();
        }

        public static void IzmeniPripada(Pripada pripada)
        {

            Dictionary<string, object> queryDictionary = new Dictionary<string, object>();

            queryDictionary.Add("id", pripada.id);

            queryDictionary.Add("datum_od", pripada.datum_od);

            CypherQuery query;

            if (pripada.datum_do != DateTime.MinValue)
            {
                queryDictionary.Add("datum_do", pripada.datum_do);

                query = new Neo4jClient.Cypher.CypherQuery("match (f:Firma)-[r:PRIPADA {id:{id}}]-(p:Projekat) SET r={id:{id}, datum_od:{datum_od}, datum_do:{datum_do}} return r",
                                                           queryDictionary, CypherResultMode.Set);
            }
            else
                query = new Neo4jClient.Cypher.CypherQuery("match (f:Firma)-[r:PRIPADA {id:{id}}]-(p:Projekat) SET r={id:{id}, datum_od:{datum_od}} return r",
                                                           queryDictionary, CypherResultMode.Set);

            ((IRawGraphClient)Session.Client).ExecuteGetCypherResults<Projekat>(query);
            //match (f:Firma)-[r:PRIPADA {id:0}]-(p:Projekat) SET r.datum_od="01.01.1999" return r;
        }

        public static void ObrisiPripadaZaDatuFirmu(int idFirme)
        {

            //match (f:Firma {id:1})-[r:PRIPADA]-(p:Projekat) delete r;
            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("id", idFirme);

            var query = new Neo4jClient.Cypher.CypherQuery("match (f:Firma {id:{id}})-[r:PRIPADA]-(p:Projekat) delete r",
                                                            queryDict, CypherResultMode.Set);

            ((IRawGraphClient)Session.Client).ExecuteCypher(query);
        }

        public static void ObrisiPripadaZaDatiProjekat(int id)
        {

            //match (f:Firma {id:1})-[r:PRIPADA]-(p:Projekat) delete r;
            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("id", id);

            var query = new Neo4jClient.Cypher.CypherQuery("match (f:Firma)-[r:PRIPADA]-(p:Projekat {id:{id}}) delete r",
                                                            queryDict, CypherResultMode.Set);

            ((IRawGraphClient)Session.Client).ExecuteCypher(query);
        }

        public static void ObrisiPripada(int id)
        {

            //match (f:Firma {id:1})-[r:PRIPADA]-(p:Projekat) delete r;
            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("id", id);

            var query = new Neo4jClient.Cypher.CypherQuery("match (f:Firma)-[r:PRIPADA {id:{id}}]-(p:Projekat) delete r",
                                                            queryDict, CypherResultMode.Set);

            ((IRawGraphClient)Session.Client).ExecuteCypher(query);
        }


        #endregion

        #region RadiNa

        public static object VratiSveRadiNa()
        {
            //match (z:Zaposleni)-[r:RADI_NA]-(p:Projekat) return z, r, p;
            var obj = Session.Client.Cypher
                .Match("(z:Zaposleni)-[r:RADI_NA]-(p:Projekat)")
                .Return((z,r, p) => new
                {
                    Zaposleni=z.As<Zaposleni>(),
                    RadiNa=r.As<RadiNa>(),
                    Projekat=p.As<Projekat>()
                    
                })
                .Results.ToList();


            return obj;
        }

        public static IList<Zaposleni> VratiSveZaposleneNaProjektu(int idProjekta)
        {
            //match (z:Zaposleni)-[r:RADI_NA]-(p:Projekat {id:0}) return z;

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("id", idProjekta);
            var query = new Neo4jClient.Cypher.CypherQuery("match (z:Zaposleni)-[r:RADI_NA]-(p:Projekat {id:{id}}) return z",
                queryDict, CypherResultMode.Set);

           
            List<Zaposleni> zaposleni = ((IRawGraphClient)Session.Client).ExecuteGetCypherResults<Zaposleni>(query).ToList();
            return zaposleni;
        }

        public static IList<Projekat> VratiSveProjekteZaposlenog(int idZaposlenog)
        {
            //match (z:Zaposleni)-[r:RADI_NA]-(p:Projekat {id:0}) return z;

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("id", idZaposlenog);
            var query = new Neo4jClient.Cypher.CypherQuery("match (z:Zaposleni {id:{id}})-[r:RADI_NA]-(p:Projekat ) return p",
                queryDict, CypherResultMode.Set);


            List<Projekat> projekti = ((IRawGraphClient)Session.Client).ExecuteGetCypherResults<Projekat>(query).ToList();
            return projekti;
        }

        public static void DodajProjekatZaposlenom(int idProjekta, int idZaposlenog, RadiNa radiNa)
        {
            /*MATCH (a:Zaposleni),(b:Projekat)
                WHERE a.id = 0 AND b.id = 0
                CREATE (a)-[r:RADI_NA { datum_od: "10.12.2020.",datum_do:"10.10.2021."}]->(b)*/

            //moze i ovako, ali ne umem onda da ne uzmem u obzir datum_do
            /* radiNa.id = DataProvider.getMaxId() + 1;
             var query = Session.Client.Cypher
                         .Match("(z:Zaposleni)", "(p:Projekat)")
                         .Where((Zaposleni z) => z.id == idZaposlenog)
                         .AndWhere((Projekat p) => p.id == idProjekta)
                         .Create("(z)-[r:RADI_NA {radiNa}]->(p)")
                         .WithParam("radiNa", radiNa);

             query.ExecuteWithoutResults();*/

            Dictionary<string, object> queryDictionary = new Dictionary<string, object>();

            queryDictionary.Add("id", DataProvider.getMaxId() + 1);
            queryDictionary.Add("idProjekta", idProjekta);
            queryDictionary.Add("idZaposlenog", idZaposlenog);
            queryDictionary.Add("datum_od", radiNa.datum_od);

            CypherQuery query;
            if (radiNa.datum_do != DateTime.MinValue)
            {
                queryDictionary.Add("datum_do", radiNa.datum_do);

                query = new Neo4jClient.Cypher
                    .CypherQuery("MATCH (a:Zaposleni),(b:Projekat) "+
                                 "WHERE a.id = {idZaposlenog} AND b.id = {idProjekta} "+
                                "CREATE(a) -[r: RADI_NA {id:{id}, datum_od: {datum_od}, datum_do:{datum_do} }]->(b)",
                                                           queryDictionary, CypherResultMode.Set);
            }
            else
                query = new Neo4jClient.Cypher
                    .CypherQuery("MATCH (a:Zaposleni),(b:Projekat) " +
                                 "WHERE a.id = {idZaposlenog} AND b.id = {idProjekta} " +
                                "CREATE(a) -[r: RADI_NA {id:{id}, datum_od: {datum_od} }]->(b)",
                                                           queryDictionary, CypherResultMode.Set);


            ((IRawGraphClient)Session.Client).ExecuteCypher(query);
            //query.ExecuteWithoutResults(); 
        }

        public static void IzmeniRadiNa(RadiNa radiNa)
        {
            Dictionary<string, object> queryDictionary = new Dictionary<string, object>();

            queryDictionary.Add("id", radiNa.id);
            queryDictionary.Add("datum_od", radiNa.datum_od);

            CypherQuery query;

            if (radiNa.datum_do != DateTime.MinValue)
            {
                queryDictionary.Add("datum_do", radiNa.datum_do);

                query = new Neo4jClient.Cypher.CypherQuery("match (z:Zaposleni)-[r:RADI_NA {id:{id}}]-(p:Projekat) SET r={id:{id}, datum_od:{datum_od}, datum_do:{datum_do}}",
                                                           queryDictionary, CypherResultMode.Set);
            }
            else
                query = new Neo4jClient.Cypher.CypherQuery("match (z:Zaposleni)-[r:RADI_NA {id:{id}}]-(p:Projekat) SET r={id:{id}, datum_od:{datum_od}}",
                                                           queryDictionary, CypherResultMode.Set);
            ((IRawGraphClient)Session.Client).ExecuteCypher(query);
        }
         

        public static void ObrisiRadiNaZaZaposlenog(int idZaposlenog)
        {

            //match (f:Firma {id:1})-[r:PRIPADA]-(p:Projekat) delete r;
            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("id", idZaposlenog);

            var query = new Neo4jClient.Cypher.CypherQuery("match (z:Zaposleni {id:{id}})-[r:RADI_NA]-(p:Projekat ) delete r",
                                                            queryDict, CypherResultMode.Set);

            ((IRawGraphClient)Session.Client).ExecuteCypher(query);
        }

        public static void ObrisiRadiNaZaDatiProjekat(int id)
        {

           
            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("id", id);

            var query = new Neo4jClient.Cypher.CypherQuery("match (z:Zaposleni)-[r:RADI_NA]-(p:Projekat {id:{id}}) delete r",
                                                            queryDict, CypherResultMode.Set);

            ((IRawGraphClient)Session.Client).ExecuteCypher(query);
        }

        public static void ObrisiRadiNa(int id)
        {

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("id", id);

            var query = new Neo4jClient.Cypher.CypherQuery("match (z:Zaposleni)-[r:RADI_NA {id:{id}}]-(p:Projekat) delete r",
                                                            queryDict, CypherResultMode.Set);

            ((IRawGraphClient)Session.Client).ExecuteCypher(query);
        }

        #endregion
    }
}
