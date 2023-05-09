using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using CsvHelper;
using Newtonsoft.Json;

 public class Pokemon
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("types")]
    public List<PokemonType> Types { get; set; }
}


public class PokemonType
{
    [JsonProperty("type")]
    public PokemonTypeData TypeData { get; set; }
}

public class PokemonTypeData
{
    [JsonProperty("name")]
    public string Name { get; set; }
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
                csv.WriteField("Name");
                csv.WriteField("Id");
                csv.WriteField("Types");
                csv.NextRecord();

            foreach (var pokemon in Pokedex)
            {
                var types = string.Join(", ", pokemon.Types.ConvertAll(t => t.TypeData.Name));
                csv.WriteField(pokemon.Name);
                csv.WriteField(pokemon.Id);
                csv.WriteField(types);
                csv.NextRecord();
            }
        }
    }
}






