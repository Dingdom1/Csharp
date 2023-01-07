using System.IO;
using System.Text;

namespace Pokemon

{
public class pokedex
{
    public int id { get; set; }
    public string name { get; set; }
    public boolean is_main_series { get; set; }
    public list descriptions { get; set; }
    public list names { get; set; }
    public list pokemon_entries { get; set; }
    public boolean is_main_series { get; set; }
    public string region { get; set; }
    public list version_groups { get; set; }
    public int entry_number { get; set; }
    public string pokemon_species { get; set; }
}

class program
{
    static void Main(string[]args)
//Create New CSV file and add headings
{
    string file = @"C:\Users\44743\OneDrive\Desktop\Output.csv";
    string seperator = ":";
    stringbuilder output = new stringbuilder();
    
    foreach (Catergory catergory in catergories)
    {
        String[] newLine = {catergory.id.Tostring(), }
    }
}
}
}
