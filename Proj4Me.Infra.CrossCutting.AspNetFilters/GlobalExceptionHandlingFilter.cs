using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;

namespace Proj4Me.Infra.CrossCutting.AspNetFilters
{
    public class GlobalExceptionHandlingFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionHandlingFilter> _logger;
        private readonly IHostingEnvironment _hostingEnviroment;

        public GlobalExceptionHandlingFilter(ILogger<GlobalExceptionHandlingFilter> logger, 
                                             IHostingEnvironment hostingEnviroment)
        {
            _logger = logger;
            _hostingEnviroment = hostingEnviroment;
        }

        public void OnException(ExceptionContext context)
        {
            // verifica se é ambiente de producao ou hml
            if (_hostingEnviroment.IsProduction())
            {
                _logger.LogError(1, context.Exception, context.Exception.Message);
            }

            var result = new ViewResult {ViewName = "Error"}; // envia para  a view de erro
            var modelData = new EmptyModelMetadataProvider();
            result.ViewData = new ViewDataDictionary(modelData, context.ModelState)
            {
                {"MensagemErro", context.Exception.Message}
            };

            context.ExceptionHandled = true;// É preciso colocar como true pra que o servidor o iis entenda que a excecao foi tratada
            context.Result = result;
        }
    }
}