using System;
using System.Net.Http;
using Newtonsoft.Json;
using CsvHelper;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

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
    static async Task Main(string[] args)
    {
        var client = new HttpClient();
        var response = await client.GetAsync("https://pokeapi.co/api/v2/pokemon/1");
        var json = await response.Content.ReadAsStringAsync();
        var pokemon = JsonConvert.DeserializeObject<Pokemon>(json);
        Console.WriteLine($"Name: {pokemon.Name}, Id: {pokemon.Id}");
    }
}