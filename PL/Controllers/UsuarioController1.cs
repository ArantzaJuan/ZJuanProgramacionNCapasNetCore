using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace PL.Controllers
{
    public class UsuarioController1 : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public UsuarioController1(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result = new ML.Result();
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol =new ML.Rol();  
           
            //ML.Result result = BL.Usuario.GetAll(usuario);
            ML.Result resultrol = BL.Rol.GetAll();
           // Conexion del SL al PL
            try
            {
                string urlAPi = _configuration["UrlAPI"];
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlAPi);

                    var responseTask = client.GetAsync("Usuario/GetAll");
                    //result = bl.Usuario.GetAll();

                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        result.Objects = new List<object>();

                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.Usuario resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                            result.Objects.Add(resultItemList);
                        }

                        result.Correct = true;
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            if (result.Correct)
            {
                usuario.Rol.Roles = resultrol.Objects;
                usuario.Usuarios = result.Objects;
                return View(usuario);
            }
            else
            {
                ViewBag.Message = "Ocurrio un error, no se pueden mostar los registro ";
                return View();
            }

        }
        [HttpPost]
        public ActionResult GetAll(ML.Usuario usuario)
        {
            //ML.Result result = BL.Usuario.GetAll(usuario);
            ML.Result result=new ML.Result();   
            usuario.Rol = new ML.Rol();
            ML.Result resultrol = BL.Rol.GetAll();

            try
            {
                string urlAPi = _configuration["UrlAPI"];
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlAPi);

                    var responseTask = client.PostAsJsonAsync<ML.Usuario>("Usuario/GetAll",usuario);
                    //result = bl.Usuario.GetAll();

                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        result.Objects = new List<object>();

                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.Usuario resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                            result.Objects.Add(resultItemList);
                        }

                        result.Correct = true;
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            if (result.Correct)
            {
                usuario.Rol.Roles = resultrol.Objects;
                usuario.Usuarios = result.Objects;
                return View(usuario);
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error al consultar los alumnos";
                return View();
            }
        }



        // Vistas para el formulario 
        [HttpGet]
        public ActionResult Form(int? IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result resulrol = new ML.Result();
            usuario.Rol = new ML.Rol();
            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();

            ML.Result resultpais = BL.Pais.GetAll();
            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

            resulrol = BL.Rol.GetAll();



            if (IdUsuario == null)
            {

                usuario.Rol.Roles = resulrol.Objects;
                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultpais.Objects;
                return View(usuario);
            }
            else
            {
                ML.Result result = new ML.Result();
                    //BL.Usuario.GetById(IdUsuario.Value);
                try
                {
                    string urlAPi = _configuration["UrlAPI"];
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(urlAPi);

                        var responseTask = client.GetAsync("Usuario/GetById/"+IdUsuario);
                        //result = bl.Usuario.GetAll();

                        responseTask.Wait();

                        var resultServicio = responseTask.Result;

                        if (resultServicio.IsSuccessStatusCode)
                        {
                            var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                            readTask.Wait();

                            result.Correct = true;
                        }
                    }

                }
                catch (Exception ex)
                {
                    result.Correct = false;
                    result.ErrorMessage = ex.Message;
                }
                if (result.Correct)
                {
                    usuario = (ML.Usuario)result.Object;
                    usuario.Rol.Roles = resulrol.Objects;
                    usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultpais.Objects;
                    //L.Result resultGrupos = BL.Grupo.GetByIdPlantel(alumno.Horario.Grupo.IdGrupo);
                    //alumno.Horario.Grupo.Grupos = resultGrupos.Objects; SE LLENA LA LISTA
                    ML.Result resultestado = BL.Estado.GetByIdPais(usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais);
                    usuario.Direccion.Colonia.Municipio.Estado.Estados = resultestado.Objects;
                    ML.Result resultmuni = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                    usuario.Direccion.Colonia.Municipio.Municipios = resultmuni.Objects;
                    ML.Result resultcolo = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                    usuario.Direccion.Colonia.Colonias = resultcolo.Objects;
                    ML.Result resultDireccion = BL.Direccion.GetByIdColonia(usuario.Direccion.Colonia.IdColonia);
                    usuario.Direccion.Direcciones = resultDireccion.Objects;


                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al consultar al usuario seleccionado";
                }
                return View(usuario);
            }
        }
        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            IFormFile imagen = Request.Form.Files["IFImage"];

            if (imagen != null)
            {
                //llamar al metodo que esta en la parte de hasta abajo que convierte a bytes la imagen
                byte[] ImagenBytes = ConvertToBytes(imagen);
                //convierto a base 64 la imagen y la guardo en la propiedad de imagen en el objeto alumno
                usuario.Imagen = Convert.ToBase64String(ImagenBytes);
            }

            if (!ModelState.IsValid)
            {


                if (usuario.IdUsuario == 0)
                {
                    try
                    { 
                        string urlAPi = _configuration["UrlAPI"];
                        using (var client = new HttpClient())
                        {
                           client.BaseAddress = new Uri(urlAPi);

                            var responseTask = client.PostAsJsonAsync<ML.Usuario>("Usuario/Add",usuario);
                            //result = bl.Usuario.GetAll();

                            responseTask.Wait();

                            var resultServicio = responseTask.Result;

                            if (resultServicio.IsSuccessStatusCode)
                            {
                                result.Correct = true;
                            }
                        }

                    }
                         catch (Exception ex)
                    {
                        result.Correct = false;
                        result.ErrorMessage = ex.Message;
                    }
                //add
                //ML.Result result = BL.Usuario.Add(usuario);

                    if (result.Correct)
                    {
                        usuario = (ML.Usuario)result.Object;
                        ViewBag.Mensaje = "Usuario guardado exitosamente";

                    }
                    else
                    {
                        ViewBag.Mensaje = "Ocurrio un error al agregar al usuario";
                    }
                }
                else
                {
                    //update
                    /*ML.Result */ result = BL.Usuario.Update(usuario);

                    if (result.Correct)
                    {
                        usuario = (ML.Usuario)result.Object;
                        ViewBag.Mensaje = "Usuario Actualizado Correctamente";
                    }
                    else
                    {
                        ViewBag.Mensaje = "Ocurrio un error El usuario no se pudo actualizar";
                    }
                }

            }
            else
            {
              
                ML.Result resulrol = new ML.Result();
                usuario.Rol = new ML.Rol();
                usuario.Direccion = new ML.Direccion();
                usuario.Direccion.Colonia = new ML.Colonia();
                usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();

                ML.Result resultpais = BL.Pais.GetAll();
                usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

                resulrol = BL.Rol.GetAll();

                    usuario.Rol.Roles = resulrol.Objects;
                    usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultpais.Objects;
                    return View(usuario);
              
            }

            return PartialView("Modal");

        }
        //Boton de borrar 
        public ActionResult Delete(int IdUsuario)
        {

            ML.Result result = new ML.Result();
            result = BL.Usuario.DeleteEF(IdUsuario);

            if (result.Correct)
            {
                ViewBag.Mensaje = "Se elimino el registro";
            }
            return PartialView("Modal");
        }

        //Creacion del login 
      
        public ActionResult Login () 
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Login (string username, string password) 
        {
            ML.Result result = BL.Usuario.GetByUserName(username);
            if (result.Correct)
            {
                ML.Usuario usuario = new ML.Usuario();
                if (usuario.Password == password)
                {


                }
            }

            return View();
        }


        //Json para llamar a los drop down list
        public JsonResult GetEstado(int IdPais)
        {
            var result = BL.Estado.GetByIdPais(IdPais);
            return Json(result.Objects);
        }
        public JsonResult GetMunicipio(int IdEstado)
        {
            var result = BL.Municipio.GetByIdEstado(IdEstado);
            return Json(result.Objects);
        }
        public JsonResult GetColonia(int IdMunicipio)
        {
            var result = BL.Colonia.GetByIdMunicipio(IdMunicipio);
            return Json(result.Objects);
        }
        //MEtodo que convierte la imagen a bytes
        public static byte[] ConvertToBytes(IFormFile Imagen)
        {
            using var fileStream = Imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }

        //Metodo para cambiar los status del usuario 
        public JsonResult CambiarStatus (int IdUsuario, bool Status)
        {
            var result = BL.Usuario.ChangeStatus(IdUsuario, Status);
            return Json(result);
        }



    }
}
