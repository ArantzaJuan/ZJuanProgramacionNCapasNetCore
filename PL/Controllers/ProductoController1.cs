using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class ProductoController1 : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result = new ML.Result();
            ML.Producto producto = new ML.Producto();

            producto.Proveedor = new ML.Proveedor();
            ML.Result resultprove =BL.Proveedor.GetAll();


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

        

        [HttpPost]
        public ActionResult GetAll(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            result = BL.Producto.Getall(producto);
            producto.Proveedor = new ML.Proveedor();
            ML.Result resultprove = BL.Proveedor.GetAll();

            if (result.Correct)
            {
                producto.Proveedor.Proveedores = resultprove.Objects;
                producto.Productos = result.Objects;
                return View(producto);
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error al consultar los alumnos";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Form(int? IdProducto)
        {

            ML.Producto producto = new ML.Producto();
            ML.Result resultprovee = new ML.Result();
            producto.Proveedor = new ML.Proveedor();
            producto.Departamento = new ML.Departamento();
            producto.Departamento.Area = new ML.Area();

            resultprovee = BL.Proveedor.GetAll();
            ML.Result resultarea = BL.Area.GetAll();
            producto.Departamento.Area = new ML.Area();


            if (IdProducto == null)
            {
                producto.Proveedor.Proveedores = resultprovee.Objects;
                producto.Departamento.Area.Areas = resultarea.Objects;

                return View(producto);
            }
            else
            {

                ML.Result result = BL.Producto.GetById(IdProducto.Value);

                if (result.Correct)
                {
                    producto = (ML.Producto)result.Object;
                    producto.Proveedor.Proveedores = resultprovee.Objects;
                    producto.Departamento.Area.Areas = resultarea.Objects;

                    ML.Result resuldepa = BL.Departamento.GetByIdArea(producto.Departamento.Area.IdArea);
                    producto.Departamento.Departamentos = resuldepa.Objects;

                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al consultar el producto seleccionado";
                }
            }
            return View(producto);
        }
            [HttpPost]
        public ActionResult Form(ML.Producto producto)
        {
            IFormFile imagen = Request.Form.Files["IFImage"];

            if (imagen != null)
            {
                //llamar al metodo que esta en la parte de hasta abajo que convierte a bytes la imagen
                byte[] ImagenBytes = ConvertToBytes(imagen);
                //convierto a base 64 la imagen y la guardo en la propiedad de imagen en el objeto alumno
                producto.Imagen = Convert.ToBase64String(ImagenBytes);
            }



            if (producto.IdProducto == 0)
            {
                //add
                ML.Result result = BL.Producto.Add(producto);

                if (result.Correct)
                {
                    producto = (ML.Producto)result.Object;
                    ViewBag.Mensaje = "producto guardado exitosamente";

                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error al agregar al usuario";
                }
            }
            else
            {
                ML.Result result = BL.Producto.Update(producto);

                if (result.Correct)
                {
                    producto = (ML.Producto)result.Object;
                    ViewBag.Mensaje = "Producto Actualizado Correctamente";
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error, el producto no se pudo actualizar";
                }

            }

            return PartialView("Modal");

        }


        public ActionResult Delete(int IdProducto)
        {

            ML.Result result = new ML.Result();
            result = BL.Producto.Delete(IdProducto);

            if (result.Correct)
            {
                ViewBag.Mensaje = "Se elimino el producto";
            }
            else
            {
                ViewBag.Mensaje = "no se pudo eliminar el producto";
            }
            return PartialView("Modal");
        }


        public JsonResult GetDepartamento(int IdArea)
        {
            var result = BL.Departamento.GetByIdArea(IdArea);
            return Json(result.Objects);
        }

        public static byte[] ConvertToBytes(IFormFile Imagen)
        {
            using var fileStream = Imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }
    }
}
