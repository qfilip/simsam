using Google.Protobuf.Collections;

namespace Grpc.Server.Models;

class CocktailModel
{
    public string? Name { get; set; }
    public IEnumerable<string>? Ingredients { get; set; }

    public static CocktailDto Map(CocktailModel model) =>
        new CocktailDto()
}
