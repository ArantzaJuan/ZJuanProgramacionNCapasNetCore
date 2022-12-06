using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class VentaProdutoController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public VentaProdutoController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;

        }
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result = new ML.Result();
            ML.Producto producto = new ML.Producto();

            producto.Proveedor = new ML.Proveedor();
            ML.Result resultprove = BL.Proveedor.GetAll();


            result = BL.Producto.Getall(producto);
            if (result.Correct)
            {
                producto.Proveedor.Proveedores = resultprove.Objects;
                producto.Productos = result.Objects;
                return View(producto);
            }
            else
            {
                ViewBag.Message = "Ocurrio un error, no se pueden mostar los registro de productos";
            }
            return View();
        }
        [HttpGet]
        public ActionResult Carrito (int? IdProducto)
        { 
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            if(IdProducto==null)
            {
                if (HttpContext.Session.GetString("producto") == null)
                {
                    ML.VentaProducto ventaProducto = new ML.VentaProducto();
                    
                    ML.Result resultp = BL.Producto.GetById(IdProducto.Value);
                    if (resultp.Correct)
                    {
                        ventaProducto.Cantidad = 1;
                    }
                }
                else
                {
                    var session= HttpContext.Session;
                    result.Objects = new List<object>();
                    bool exis = false;
                    var indice = 0;
                    foreach ( ML.VentaProducto ventaProducto in result.Objects)
                    {
                        exis= true; 
                        indice= result.Objects.IndexOf(indice);
                        if(exis = true)
                        {
                            
                        }
                    }

                }
            }
            return View("Carrito");
        }
    }
}
