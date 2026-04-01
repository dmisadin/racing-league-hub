using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using RacingLeagueHub.Application.Models;

namespace RacingLeagueHub.Api.Configuration.Binders;

public class EncryptedIdModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var value = bindingContext.ValueProvider
            .GetValue(bindingContext.ModelName)
            .FirstValue;

        if (string.IsNullOrEmpty(value))
        {
            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }

        try
        {
            bindingContext.Result = ModelBindingResult.Success(new EncryptedId(value));
        }
        catch (ArgumentException ex)
        {
            bindingContext.ModelState.AddModelError(bindingContext.ModelName, ex.Message);
            bindingContext.Result = ModelBindingResult.Failed();
        }

        return Task.CompletedTask;
    }
}

public class EncryptedIdModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (context.Metadata.ModelType == typeof(EncryptedId))
            return new BinderTypeModelBinder(typeof(EncryptedIdModelBinder));

        return null;
    }
}