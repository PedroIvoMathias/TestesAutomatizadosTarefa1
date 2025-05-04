//using System.Net;
//using System.Text.Json;
//using Microsoft.EntityFrameworkCore;
//using System.Data.Common;

//namespace EnviarTrabalhos.Exceptions
//{
//    public class ExceptionMiddleware
//    {
//        private readonly RequestDelegate _next;

//        public ExceptionMiddleware(RequestDelegate next)
//        {
//            _next = next;
//        }

//        public async Task InvokeAsync(HttpContext httpContext)
//        {
//            try
//            {
//                await _next(httpContext);
//            }
//            catch (BusinessException ex)
//            {
//                await HandleExceptionAsync(httpContext, HttpStatusCode.BadRequest, ex.Message);
//            }
//            catch (Exception ex)
//            {
//                await HandleExceptionAsync(httpContext, HttpStatusCode.InternalServerError, "Ocorreu um erro inesperado.");
//            }
//            catch (DbUpdateException ex)
//            {
//                // Captura erros relacionados ao banco de dados (como falhas de atualização)
//                await HandleExceptionAsync(httpContext, HttpStatusCode.InternalServerError, "Erro ao acessar o banco de dados.");
//            }
//            catch (DbException ex)
//            {
//                // Captura exceções gerais relacionadas ao banco de dados (MySQL, SQL Server, etc)
//                await HandleExceptionAsync(httpContext, HttpStatusCode.InternalServerError, "Erro de banco de dados. Verifique a conexão ou a consulta.");
//            }
//        }

//        private static Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message)
//        {
//            context.Response.ContentType = "application/json";
//            context.Response.StatusCode = (int)statusCode;

//            var result = JsonSerializer.Serialize(new { error = message });
//            return context.Response.WriteAsync(result);
//        }
//    }
//}

using System.Net;
using System.Text.Json;
using System.Data;
using System.Data.Common;

namespace EnviarTrabalhos.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (BusinessException ex)
            {
                // Exceção de negócio (validação ou regra de negócio)
                await HandleExceptionAsync(httpContext, HttpStatusCode.BadRequest, ex.Message);
            }
            catch (DbException ex)
            {
                // Exceção genérica de banco de dados (erro de conexão ou consulta)
                await HandleExceptionAsync(httpContext, HttpStatusCode.InternalServerError, "Erro ao acessar o banco de dados. Verifique a conexão ou a consulta.");
            }
            catch (TimeoutException ex)
            {
                // Exceção de timeout (exemplo: erro de rede ou conexão com o banco)
                await HandleExceptionAsync(httpContext, HttpStatusCode.RequestTimeout, "O tempo de conexão com o banco de dados ou servidor expirou.");
            }
            catch (InvalidOperationException ex)
            {
                // Exceção para operações inválidas (ex: operação não permitida)
                await HandleExceptionAsync(httpContext, HttpStatusCode.BadRequest, "Operação inválida. Verifique os dados fornecidos.");
            }
            catch (Exception ex)
            {
                // Exceção genérica para erros inesperados
                await HandleExceptionAsync(httpContext, HttpStatusCode.InternalServerError, "Ocorreu um erro inesperado.");
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message)
        {
            var acceptHeader = context.Request.Headers["Accept"].FirstOrDefault() ?? "";
            if (acceptHeader.Contains("text/html") || acceptHeader.Contains("*/*"))
            {
                context.Response.Redirect($"/Trabalho/EnviarTrabalho?erro={WebUtility.UrlEncode(message)}");
                return Task.CompletedTask;
            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var result = JsonSerializer.Serialize(new { error = message });
            return context.Response.WriteAsync(result);
        }
    }
}

