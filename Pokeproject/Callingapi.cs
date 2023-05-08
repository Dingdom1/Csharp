using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using CsvHelper;
using Newtonsoft.Json;

 public class Pokemon
{
    public string Name { get; set; }
    public int Id { get; set; }
    public List<NameUrlPair> Types { get; set; }
}

public class NameUrlPair
{
    public string Name { get; set; }
    public string Url { get; set; }
}

class Program
{
    public static List<Pokemon> Pokedex = new List<Pokemon>();

    public static object Pokemonentries { get; private set; }

    static async System.Threading.Tasks.Task Main(string[] args)
    {
        var client = new HttpClient();

               for (int i = 1; i <= 151; i++)
        {
            var response = await client.GetAsync($"https://pokeapi.co/api/v2/pokemon/{i}/");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var pokemon = JsonConvert.DeserializeObject<Pokemon>(json);
                Pokedex.Add(pokemon);
            }
            else
            {
                Console.WriteLine($"Failed to get Pokemon data for ID {i}. Status code: {response.StatusCode}");
            }
        }
            using (var writer = new StreamWriter("pokemon.csv"))
        using (var csv = new CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(Pokedex);
        }

    }
}






