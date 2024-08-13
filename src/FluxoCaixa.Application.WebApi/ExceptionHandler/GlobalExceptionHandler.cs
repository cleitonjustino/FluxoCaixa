using FluxoCaixa.Application.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace FluxoCaixa.Application.WebApi.ExceptionHandler
{
    public class GlobalExceptionHandler : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception error)
            {
                var problem = new ProblemDetails
                {
                    Detail = error.InnerException is not null ? error.InnerException.Message : error.Message,
                    Instance = error.StackTrace
                };

                switch (error)
                {
                    case DomainBaseException:
                        problem.Status = (int)HttpStatusCode.BadRequest;
                        problem.Type = $"https://developer.mozilla.org/pt-BR/docs/Web/HTTP/Status/{(int)HttpStatusCode.BadRequest}";
                        problem.Title = "Ocorreu um erro de domínio.";
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case UnauthorizedAccessException:
                        problem.Status = (int)HttpStatusCode.Unauthorized;
                        problem.Type = $"https://developer.mozilla.org/pt-BR/docs/Web/HTTP/Status/{(int)HttpStatusCode.Unauthorized}";
                        problem.Title = "Request não autorizada.";
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException:
                        problem.Status = (int)HttpStatusCode.NotFound;
                        problem.Type = $"https://developer.mozilla.org/pt-BR/docs/Web/HTTP/Status/{(int)HttpStatusCode.NotFound}";
                        problem.Title = "Recurso não encontrado.";
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        problem.Status = (int)HttpStatusCode.InternalServerError;
                        problem.Type = $"https://developer.mozilla.org/pt-BR/docs/Web/HTTP/Status/{(int)HttpStatusCode.InternalServerError}";
                        problem.Title = "Ocorreu um erro inesperado.";
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                context.Response.ContentType = "application/problem+json";

                await context.Response.WriteAsync(JsonConvert.SerializeObject(problem));
            }
        }
    }
}
