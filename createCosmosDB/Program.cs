using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace createCosmosDB
{
    class Program
    {
        private static readonly string _endpointUri = "https://slynchcosmos.documents.azure.com:443/";
        private static readonly string _primaryKey = "Ysm3B1Qt2OcvQJPVQiuwCXCX2nQ7DOz8WJuwD2GN3ZWdJPU3SSEtyl0dQmh8LOlNyGAt4e9d8jhYZ5HMFTTIrg==";
        public static async Task Main(string[] args)
        {
            using (CosmosClient client = new CosmosClient(_endpointUri, _primaryKey))
            {
                DatabaseResponse databaseResponse = await client.CreateDatabaseIfNotExistsAsync("Products");
                Database targetDatabase = databaseResponse.Database;
                await Console.Out.WriteLineAsync($"Database Id:\t{targetDatabase.Id}");

                IndexingPolicy indexingPolicy = new IndexingPolicy
                {
                    IndexingMode = IndexingMode.Consistent,
                    Automatic = true,
                    IncludedPaths =
                    {
                        new IncludedPath
                        {
                            Path = "/*"
                        }
                    }

                };

                var containerProperties = new ContainerProperties("Clothing", "/productID")
                {
                    IndexingPolicy = indexingPolicy
                };

                var containerResponse = await targetDatabase.CreateContainerIfNotExistsAsync(containerProperties, 10000);
                var customContainer = containerResponse.Container;
                await Console.Out.WriteLineAsync($"Custom Container ID:\t{customContainer.Id}");

            }
        }
    }
}
