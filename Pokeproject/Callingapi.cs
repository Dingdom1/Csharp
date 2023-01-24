using System.Text.Json;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using CsvHelper;
using System.IO;


using HttpClient client = new();
    
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(
    new MediaTypeWithQualityHeaderValue("applications/json"));
client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

//public class Myclass
//public static async Task ExampleMethodAsync()

    await ProcessRepositoriesAsync(client);
    
    static async Task ProcessRepositoriesAsync(HttpClient client)

{
        var json = await client.GetStringAsync(
        "https://pokeapi.co/api/v2/pokedex/2"); //list all the pokedex entries of 1st gen Pokemon 1-152
        // var records = JsonSerializer.Deserialize<dynamic>(json);
        System.IO.File.WriteAllText(@"pokemon.json", json);
        string json_file = File.ReadAllText("pokemon.json");
        var records = JsonConvert.DeserializeObject<dynamic>(json_file);

        using (var writer = new StreamWriter("data.csv"))
        using (var csv = new CsvWriter(writer))
        {
            csv.WriteRecords(records);
        }

}



