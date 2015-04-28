using System.Collections.Generic;

namespace SchedulerManager.Configuration
{
    public static class Clients
    {
        public static IList<Client> GetClients()
        {
            //Returns 3 clients for tests
            var clientsList = new List<Client>();
            for (var i = 0; i < 250; i++)
            {
                clientsList.Add(new Client
                {
                    Id = i,
                    Name = string.Format("Client Name {0}", i)
                });
            }

            return clientsList;
        }

 
    }
}