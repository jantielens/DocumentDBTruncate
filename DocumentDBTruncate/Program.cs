using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jtnsTruncateDocumentDB
{
    class Program
    {
        static string uri = "https://<change>.documents.azure.com:443/";
        static string key = "<change>";
        static string databaseid = "<change>";
        static string collectionid = "<change>";

        static string query = "select * from c"; //optionally change

        static void Main(string[] args)
        {
            Console.WriteLine("Starting to delete ...");
            DoWork();
            Console.ReadLine();
        }

        private static async Task DoWork()
        {
            bool cont = true;
            while (cont)
            {
                DocumentClient docClient = new DocumentClient(new Uri(uri), key);

                var response =
                    await docClient.ExecuteStoredProcedureAsync<Document>(UriFactory.CreateStoredProcedureUri(
                        databaseid, collectionid, "truncate"), query);

                cont = response.Response.GetPropertyValue<bool>("continuation");
                int deleted = response.Response.GetPropertyValue<int>("deleted");

                Console.Write($"{deleted} documents deleted, ");
                if (cont)
                    Console.WriteLine("contuing ...");
            }
            Console.WriteLine("done!");
        }
    }
}