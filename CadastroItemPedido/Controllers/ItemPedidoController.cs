using Microsoft.AspNetCore.Mvc;

namespace CadastroItemPedido.Controllers
{
    public class ItemPedidoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
