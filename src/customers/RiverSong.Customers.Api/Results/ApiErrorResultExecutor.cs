using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Options;

namespace RiverSong.Customers.Api.Results;

public class ApiErrorResultExecutor : IActionResultExecutor<ApiErrorResult>
{
    public ApiErrorResultExecutor(
        OutputFormatterSelector formatterSelector,
        IHttpResponseStreamWriterFactory writerFactory,
        ILoggerFactory loggerFactory,
        IOptions<MvcOptions> mvcOptions)
    {
        if (writerFactory == null)
        {
            throw new ArgumentNullException(nameof(writerFactory));
        }

        if (loggerFactory == null)
        {
            throw new ArgumentNullException(nameof(loggerFactory));
        }

        FormatterSelector = formatterSelector ?? throw new ArgumentNullException(nameof(formatterSelector));
        WriterFactory = writerFactory.CreateWriter;
        Logger = loggerFactory.CreateLogger<ApiErrorResultExecutor>();
    }

    /// <summary>
    /// Gets the <see cref="ILogger"/>.
    /// </summary>
    protected ILogger Logger { get; }

    /// <summary>
    /// Gets the <see cref="OutputFormatterSelector"/>.
    /// </summary>
    protected OutputFormatterSelector FormatterSelector { get; }

    /// <summary>
    /// Gets the writer factory delegate.
    /// </summary>
    protected Func<Stream, Encoding, TextWriter> WriterFactory { get; }

    public Task ExecuteAsync(ActionContext context, ApiErrorResult result)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (result == null)
        {
            throw new ArgumentNullException(nameof(result));
        }

        var objectType = result.Errors?.GetType();
        var value = result.Errors;

        return ExecuteAsyncCore(context, result, objectType, value);
    }

    private Task ExecuteAsyncCore(ActionContext context, ApiErrorResult result, Type? objectType, object? value)
    {
        var formatterContext = new OutputFormatterWriteContext(
            context.HttpContext,
            WriterFactory,
            objectType,
            value);

        var selectedFormatter = FormatterSelector.SelectFormatter(
            formatterContext,
            Array.Empty<IOutputFormatter>(),
            result.ContentTypes);
        if (selectedFormatter == null)
        {
            // No formatter supports this.
            Logger.LogWarning("No output formatter was found for content types '{ContentTypes}' to write the response.", result.ContentTypes);

            context.HttpContext.Response.StatusCode = StatusCodes.Status406NotAcceptable;
            return Task.CompletedTask;
        }

        Logger.LogInformation("Executing {ObjectResultType}, writing value of type '{Type}'.", result.GetType(), objectType);

        context.HttpContext.Response.StatusCode = result.StatusCode ?? StatusCodes.Status400BadRequest;
        context.HttpContext.Response.Headers.ContentType = result.ContentTypes.ToArray();
        return selectedFormatter.WriteAsync(formatterContext);
    }
}