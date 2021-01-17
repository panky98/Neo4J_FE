using Neo4jClient;
using System;

namespace DataLayer
{
    public static class Session
    {
        private static GraphClient _client;
        public static  GraphClient Client 
        {
            get 
            {
                if (_client == null)
                {
                    _client = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "Petar@Pan1*2");
                   
                }
                _client.Connect();
                return _client;

            }
            set { _client = value; }
        }

        private static bool Connect()
        {
            try
            {
                Client.Connect();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
