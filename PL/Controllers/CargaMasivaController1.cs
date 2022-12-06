using Microsoft.AspNetCore.Mvc;
using ML;
using Newtonsoft.Json.Linq;
using System.IO;

namespace PL.Controllers
{
    public class CargaMasivaController1 : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public CargaMasivaController1(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;

        }

        [HttpGet]
            public ActionResult UsuarioCargaMasiva()
        {
            ML.Result result = new Result();
            return View(result);
        }
        [HttpPost]
        public ActionResult CargaTXT ()
        {

            IFormFile filetext = Request.Form.Files["archivoTXT"];
            if (filetext != null)
            {
                StreamReader Textfile = new StreamReader(filetext.OpenReadStream());
                string line;
                line = Textfile.ReadLine();
                while ((line = Textfile.ReadLine()) != null)
                {
                    string[] lines = line.Split('|');

                    ML.Usuario usuario = new ML.Usuario();
                    usuario.Nombre = lines[0];
                    usuario.ApellidoPaterno = lines[1];
                    usuario.ApellidoMaterno = lines[2];
                    usuario.Sexo = lines[3];
                    usuario.Telefono = lines[4];
                    usuario.Email = lines[5];
                    usuario.FechaDeNacimiento = lines[6];
                    usuario.Password = lines[7];
                    usuario.Celular = lines[8];
                    usuario.CURP = lines[9];
                    usuario.UserName = lines[10];

                    usuario.Rol = new ML.Rol();
                    usuario.Rol.IdRol = int.Parse(lines[11]);

                    usuario.Imagen = null;

                    usuario.Direccion = new ML.Direccion();
                    usuario.Direccion.Calle = lines[12];
                    usuario.Direccion.NumeroInterior = lines[13];
                    usuario.Direccion.NumeroExterior = lines[14];

                    usuario.Direccion.Colonia = new ML.Colonia();
                    usuario.Direccion.Colonia.IdColonia = int.Parse(lines[14]);

                    ML.Result result = BL.Usuario.Add(usuario);


                    if (result.Correct == true)
                    {
                        ViewBag.Mensaj = "Se agregaron Correctamente los usuarios";
                    }
                    else
                    {
                        string fileError = @"C:\Users\digis\Documents\Zaida Arantza Juan Alfonso\ZJuanDia1\Errores.txt";
                        //result.ErrorMessage;
                        StreamWriter errorFile = new StreamWriter(fileError);
                        ViewBag.Mensaj = "Fallo la accion, favor de descargar el archivo de errores";
                        errorFile.WriteLine("ccgf" + result.ErrorMessage);
                        errorFile.Close();
                        //CREAR UN TXT DE ERRORES
                        ViewBag.Mensaj = "Fallo la accion, favor de descargar el archivo de errores";
                       
                    }
                    
                }
                return PartialView("Modal");
            }
            return PartialView("Modal");
        }


        [HttpPost]
        public ActionResult UsuarioCargaMasiva(ML.Usuario usuario)
        {

            IFormFile excelCargaMasiva = Request.Form.Files["FileExcel"];
            //Session 

            if (HttpContext.Session.GetString("PathArchivo") == null) //sesion nula o que no exista 
            {
                //validar el excel

                if (excelCargaMasiva.Length > 0)
                {
                    //que sea .xlsx
                    string fileName = Path.GetFileName(excelCargaMasiva.FileName);
                    string folderPath = _configuration["PathFolder:value"];
                    string extensionArchivo = Path.GetExtension(excelCargaMasiva.FileName).ToLower();
                    string extensionModulo = _configuration["TipoExcel"];

                    if (extensionArchivo == extensionModulo)
                    {
                        //crear copia
                        string filePath = Path.Combine(_hostingEnvironment.ContentRootPath, folderPath, Path.GetFileNameWithoutExtension(fileName)) + '-' + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                        if (!System.IO.File.Exists(filePath))
                        {
                            using (FileStream stream = new FileStream(filePath, FileMode.Create))
                            {
                                excelCargaMasiva.CopyTo(stream);
                            }
                            //leer
                            string connectionString = _configuration["ConnectionStringExcel:value"] + filePath;
                            

                            ML.Result resultConvertExcel = BL.Usuario.ConvertirExceltoDataTable(connectionString);
                            if (resultConvertExcel.Correct)
                            {
                                ML.Result resultValidacion = BL.Usuario.ValidarExcel(resultConvertExcel.Objects);
                                if (resultValidacion.Objects.Count == 0)
                                {
                                    resultValidacion.Correct = true;
                                    HttpContext.Session.SetString("PathArchivo", filePath);
                                }

                                return View("UsuarioCargaMasiva", resultValidacion);
                            }
                            else
                            {
                                //error al leer el archivo
                                ViewBag.Mensaj = "Ocurrio un error al leer el arhivo";
                                return View("Modal");
                            }
                        }
                    }

                }



                //verificar que no tenga errores 
                //le avisamos al usuario que el excel esta mal ML.ErrorExcel 
                //crea la sesion 
            }
            else
            {
                string rutaArchivoExcel = HttpContext.Session.GetString("PathArchivo");
                string connectionString = _configuration["ConnectionStringExcel:value"] + rutaArchivoExcel;

                ML.Result resultData = BL.Usuario.ConvertirExceltoDataTable(connectionString);
                if (resultData.Correct)
                {
                    ML.Result resultErrores = new ML.Result();
                    resultErrores.Objects = new List<object>();

                    foreach (ML.Usuario usuarioItem in resultData.Objects)
                    {

                        ML.Result resultAdd = BL.Usuario.Add(usuarioItem);
                        if (!resultAdd.Correct)
                        {
                            resultErrores.Objects.Add("No se insertó el Usuario con nombre: " + usuarioItem.Nombre + " Error: " + resultAdd.ErrorMessage);
                        }
                    }
                    if (resultErrores.Objects.Count > 0)
                    {

                        string fileError = _configuration["PathError:value"];
                        using (StreamWriter writer = new StreamWriter(fileError))
                        {
                            foreach (string ln in resultErrores.Objects)
                            {
                                writer.WriteLine(ln);
                            }
                        }
                        ViewBag.Message = "Los usuarios No han sido registrados correctamente";
                    }
                    else
                    {
                        ViewBag.Mensaj = "Los usuarios han sido registrados correctamente";

                    }

                }

            }
        
            return PartialView("Modal");
        }
    }
}
