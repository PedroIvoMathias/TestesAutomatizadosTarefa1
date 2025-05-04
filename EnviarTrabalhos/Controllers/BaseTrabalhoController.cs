using Microsoft.AspNetCore.Mvc;

namespace EnviarTrabalhos.Controllers
{
    public class BaseTrabalhoController : Controller
    {
        protected IActionResult ApiOrView(object model, string viewName = null)
        {
            var acceptHeader = Request.Headers["Accept"].ToString();
            if (acceptHeader.Contains("application/json", StringComparison.OrdinalIgnoreCase))
            {
                return Ok(model);
            }

            return viewName == null ? View(model) : View(viewName, model);
        }
    }
}
