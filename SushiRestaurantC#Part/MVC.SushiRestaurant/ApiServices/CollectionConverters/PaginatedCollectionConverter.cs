using SushiRestaurant.Application.Shared;
using SushiRstaurant.Domain.Models;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text;

namespace MVC.SushiRestaurant.ApiServices.CollectionConverters;

public class PaginatedCollectionConverter<TModel> : JsonConverter<PaginatedCollection<TModel>> where TModel : Model
{
    public override PaginatedCollection<TModel> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Add or modify the options to include camel case naming
        options = options ?? new JsonSerializerOptions();

        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException();
        }

        var models = new List<TModel>();

        // Read the first token in the array
        reader.Read();

        // Deserialize each item in the array
        while (reader.TokenType != JsonTokenType.EndArray)
        {
            var model = JsonSerializer.Deserialize<TModel>(ref reader, options);
            models.Add(model);

            // Read to the next item or the end of the array
            reader.Read();
        }

        // Assuming the total count is not included in the array and is known elsewhere
        int total = models.Count;

        // Print the deserialized models as JSON
        var serializedModels = JsonSerializer.Serialize(models, options);
        Console.WriteLine(serializedModels);

        return new PaginatedCollection<TModel>(models.AsReadOnly(), total);
    }

    public override void Write(Utf8JsonWriter writer, PaginatedCollection<TModel> value, JsonSerializerOptions options)
    {
        // Implement custom serialization logic here
        // You can use writer to write the JSON representation of the PaginatedCollection<TModel>
    }
}