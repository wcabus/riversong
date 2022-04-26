using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace RiverSong.Customers.Api.Results;

public class ApiErrorResult : ActionResult, IStatusCodeActionResult
{
    private readonly MediaTypeCollection _contentTypes;

    public ApiErrorResult(IReadOnlyCollection<string>? errors)
    {
        Errors = errors;
        _contentTypes = new MediaTypeCollection
        {
            Api.ContentTypes.Errors
        };
    }

    public IReadOnlyCollection<string>? Errors { get; }

    public int? StatusCode => StatusCodes.Status422UnprocessableEntity;
    public MediaTypeCollection ContentTypes => _contentTypes;

    public override Task ExecuteResultAsync(ActionContext context)
    {
        var executor = context.HttpContext.RequestServices.GetRequiredService<IActionResultExecutor<ApiErrorResult>>();
        return executor.ExecuteAsync(context, this);
    }
}