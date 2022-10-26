using Grpc.Core;
using Grpc.Server.Models;
using Microsoft.Extensions.ObjectPool;

namespace Grpc.Server.Services;

public class CocktailService : Server.Cocktail.CocktailBase
{
    public override async Task<CocktailDto> GetAll(GetByNameRequest request, ServerCallContext context)
    {
        var models = GetModels();
        request.

    }

    private IEnumerable<CocktailModel> GetModels()
    {
        Func<string, string[], CocktailModel> create = (name, ings) =>
            new CocktailModel { Name = name, Ingredients = ings };

        return new List<CocktailModel>
        {
            create("Negroni", new string[] {"Gin", "Vermuth Roso", "Campari" }),
            create("Dark n Stormy", new string[] {"Rum", "Ginger Ale", "Mint" }),
            create("Basil Bramble", new string[] {"Gin", "sweet & sour mix", "Basil leaves", "Creme de Mure" })
        };
    }
}

