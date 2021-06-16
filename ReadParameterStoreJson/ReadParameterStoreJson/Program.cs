using Newtonsoft.Json;
using System;
using System.IO;

namespace ReadParameterStoreJson
{
    public class Program
    {
        static void Main(string[] args)
        {
            var path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())
                .Parent
                .Parent
                .FullName, 
                "parameters.json");

            using var reader = new StreamReader(path);            
            var json = reader.ReadToEnd();
            var parameters = JsonConvert.DeserializeObject<ParameterJsonRoot>(json);
            
            foreach(var parameter in parameters.Parameters)
            {
                Console.WriteLine($"aws ssm put-parameter --name {parameter.Name} --value {parameter.Value} --type String");
            }

            Console.ReadKey();
        }
    }
}
