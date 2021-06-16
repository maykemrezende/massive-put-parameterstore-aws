using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace ReadParameterStoreJson
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var credential = new SessionAWSCredentials("accessKey", "secretAccess", "sessionToken");

            var ssmClient = new AmazonSimpleSystemsManagementClient(credential);

            var path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())
                .Parent
                .Parent
                .FullName,
                "parameters.json");

            using var reader = new StreamReader(path);
            var json = reader.ReadToEnd();
            var parameters = JsonConvert.DeserializeObject<ParameterJsonRoot>(json);

            var result = new Dictionary<string, string>();

            foreach (var parameter in parameters.Parameters)
            {
                var inputParameter = new PutParameterRequest() { Name = parameter.Name, Value = parameter.Value, Type = ParameterType.String };
                var putResponse = await ssmClient.PutParameterAsync(inputParameter);
                result.Add(parameter.Name, putResponse.HttpStatusCode.ToString());
            }

            Console.WriteLine($"Resultado: \n {JsonConvert.SerializeObject(result)}");
            Console.ReadKey();
        }
    }
}
