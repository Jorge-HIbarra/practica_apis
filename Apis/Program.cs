using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

public record Pokemon(int Id, string Name, int Height, int Weight, int BaseExperience);

class Program
{
    static async Task Main()
    {
        using HttpClient cliente = new();

        string url = "https://pokeapi.co/api/v2/pokemon/181";

        Console.WriteLine($"Consultando la API: {url}...\n");

        HttpResponseMessage respuesta = await cliente.GetAsync(url);

        if (respuesta.IsSuccessStatusCode)
        {
            Pokemon? pokemon = await respuesta.Content.ReadFromJsonAsync<Pokemon>();
            
            if (pokemon != null)
            {
                Console.WriteLine("--- Datos del Pokémon ---");
                Console.WriteLine($"ID: {pokemon.Id}");
                Console.WriteLine($"Nombre: {pokemon.Name}");
                Console.WriteLine($"Altura: {pokemon.Height}");
                Console.WriteLine($"Peso: {pokemon.Weight}");
                Console.WriteLine($"Experiencia Base: {pokemon.BaseExperience}");
            }
        }
        else
        {
            Console.WriteLine($"Error {(int)respuesta.StatusCode}: {respuesta.ReasonPhrase}");
        }

        Console.WriteLine("\n----------------------------------------\n");

        try
        {
            Console.WriteLine("Probando el GET abreviado...");
            Pokemon? pokemonAbreviado = await cliente.GetFromJsonAsync<Pokemon>(url);
            Console.WriteLine($"El Pokémon abreviado es: {pokemonAbreviado?.Name}");
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error en GET abreviado: {ex.Message}");
        }
    }
}