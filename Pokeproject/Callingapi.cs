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

    [JsonProperty("sprites")]
    public PokemonSprites Sprites { get; set; }

    [JsonProperty("height")]

    public int Height { get; set; }

    [JsonProperty("weight")]

    public int Weight { get; set; }

    [JsonProperty ("abilities")]

    public List<PokemonAbilities> Abilities { get; set; }

    // Convert weight from hectograms to pounds
    [JsonIgnore]
    public double WeightInPounds => ConvertToPounds(Weight);

    private double ConvertToPounds(int weight)
    {
        return Math.Round(weight * 0.220462, 1);
    }

    // Convert height from decimeters to feet
    [JsonIgnore]
    public double HeightInFeet => ConvertToFeet(Height);

    private double ConvertToFeet(int height)
    {
        return Math.Round(height * 0.328084, 1);
    }
}

// Get the abilities from the API - Name should return as a string
public class PokemonAbilities
{
    [JsonProperty("ability")]
    public PokemonAbilityData AbilityData { get; set; }
}
// Get the ability name from the API
public class PokemonAbilityData
{
    [JsonProperty("name")]
    public string Name { get; set; }
}

// Get the sprites from the API - Front and Shiny should return as html links
public class PokemonSprites
{
    [JsonProperty("front_default")]
    public string FrontDefault { get; set; }

    [JsonProperty("front_shiny")]

    public string FrontShiny { get; set;}
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
                csv.WriteField("Height(ft)");
                csv.WriteField("Weight (lbs)");
                csv.WriteField("Abilities");
                csv.WriteField("Front Default");
                csv.WriteField("Front Shiny");
                csv.NextRecord();

            foreach (var pokemon in Pokedex)
            {
                var types = string.Join(", ", pokemon.Types.ConvertAll(t => t.TypeData.Name));
                csv.WriteField(pokemon.Name);
                csv.WriteField(pokemon.Id);
                csv.WriteField(types);
                csv.WriteField(pokemon.HeightInFeet);
                csv.WriteField(pokemon.WeightInPounds);
                csv.WriteField(string.Join(", ", pokemon.Abilities.Select(a => a.AbilityData.Name)));
                csv.WriteField(pokemon.Sprites.FrontDefault);
                csv.WriteField(pokemon.Sprites.FrontShiny);
                csv.NextRecord();
            }
        }
    }
}






