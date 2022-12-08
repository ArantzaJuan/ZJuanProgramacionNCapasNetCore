
using System.Data;
using System.Data.OleDb;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Usuario
    {
        /* este getall es sin like*/
        //public static ML.Result GetAll ()
        //{
        //    ML.Result result = new ML.Result ();
        //    try
        //    {
        //        using(DL.ZjuanProgramacionNcapasContext context = new DL.ZjuanProgramacionNcapasContext())
        //        {
        //           var usuarios = context.Usuarios.FromSqlRaw("UsuarioGetAll").ToList();
        //            result.Objects = new List<object>();
        //            if (usuarios != null)
        //            {
        //                foreach (var obj in usuarios)
        //                {
        //                    ML.Usuario usuario = new ML.Usuario();

        //                    usuario.IdUsuario = obj.IdUsuario;
        //                    usuario.Nombre = obj.Nombre;
        //                    usuario.ApellidoPaterno = obj.ApellidoPaterno;
        //                    usuario.ApellidoMaterno = obj.ApellidoMaterno;
        //                    usuario.Sexo = obj.Sexo;
        //                    usuario.Telefono = obj.Telefono;
        //                    usuario.Email = obj.Email;
        //                    usuario.FechaDeNacimiento = obj.FechaDeNacimiento.Value.ToString("dd-MM-yyyy");
        //                    usuario.Password = obj.Password;
        //                    usuario.Celular = obj.Celular;
        //                    usuario.CURP = obj.Curp;
        //                    usuario.UserName = obj.UserName; 


        //                    usuario.Rol = new ML.Rol();
        //                    usuario.Rol.IdRol = obj.IdRol.Value;
        //                    usuario.Rol.NombreRol = obj.NombreRol;

        //                    usuario.Imagen= obj.Imagen;

        //                    usuario.Direccion = new ML.Direccion();
        //                    usuario.Direccion.Calle = obj.Calle;
        //                    usuario.Direccion.NumeroInterior = obj.NumeroInterior;
        //                    usuario.Direccion.NumeroExterior = obj.NumeroExterior;

        //                    usuario.Direccion.Colonia = new ML.Colonia();

        //                    usuario.Direccion.Colonia.Nombre = obj.NombreColonia;

        //                    usuario.Direccion.Colonia.Municipio = new ML.Municipio();
        //                    usuario.Direccion.Colonia.Municipio.Nombre = obj.NombreMunicipio;

        //                    usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
        //                    usuario.Direccion.Colonia.Municipio.Estado.Nombre = obj.NombreEstado;

        //                    usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
        //                    usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = obj.NombrePais;


        //                    result.Objects.Add(usuario);
        //                }
        //                result.Correct = true;
        //            }
        //            else
        //            {
        //                result.Correct = false;
        //                result.ErrorMessage = "los usuarios no se pudo mostar";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Correct = false;
        //        result.ErrorMessage = ex.Message;
        //        result.Ex = ex;

        //    }
        //    return result;
        //}
        public static ML.Result GetAll(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ZjuanProgramacionNcapasContext context = new DL.ZjuanProgramacionNcapasContext())
                {
                    usuario.Rol.IdRol=(usuario.Rol.IdRol == null)? 0: usuario.Rol.IdRol;
                    var usuarios = context.Usuarios.FromSqlRaw($"UsuarioGetall '{usuario.Nombre}','{usuario.ApellidoPaterno}',{usuario.Rol.IdRol}").ToList();
                    result.Objects = new List<object>();
                    if (usuarios != null)
                    {
                        foreach (var obj in usuarios)
                        {
                            ML.Usuario usuarioobj = new ML.Usuario();

                            usuarioobj.IdUsuario = obj.IdUsuario;
                            usuarioobj.Nombre = obj.Nombre;
                            usuarioobj.ApellidoPaterno = obj.ApellidoPaterno;
                            usuarioobj.ApellidoMaterno = obj.ApellidoMaterno;
                            usuarioobj.Sexo = obj.Sexo;
                            usuarioobj.Telefono = obj.Telefono;
                            usuarioobj.Email = obj.Email;
                            usuarioobj.FechaDeNacimiento = obj.FechaDeNacimiento.Value. ToString("dd-MM-yyyy");
                            usuarioobj.Password = obj.Password;
                            usuarioobj.Celular = obj.Celular;
                            usuarioobj.CURP = obj.Curp;
                            usuarioobj.UserName = obj.UserName;


                            usuarioobj.Rol = new ML.Rol();
                            usuarioobj.Rol.IdRol = obj.IdRol.Value;
                            usuarioobj.Rol.NombreRol = obj.NombreRol;

                            usuarioobj.Imagen = null;
                            usuarioobj.Status= obj.Status.Value; 

                            usuarioobj.Direccion = new ML.Direccion();
                            usuarioobj.Direccion.Calle = obj.Calle;
                            usuarioobj.Direccion.NumeroInterior = obj.NumeroInterior;
                            usuarioobj.Direccion.NumeroExterior = obj.NumeroExterior;

                            usuarioobj.Direccion.Colonia = new ML.Colonia();

                            usuarioobj.Direccion.Colonia.Nombre = obj.NombreColonia;

                            usuarioobj.Direccion.Colonia.Municipio = new ML.Municipio();
                            usuarioobj.Direccion.Colonia.Municipio.Nombre = obj.NombreMunicipio;

                            usuarioobj.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                            usuarioobj.Direccion.Colonia.Municipio.Estado.Nombre = obj.NombreEstado;

                            usuarioobj.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                            usuarioobj.Direccion.Colonia.Municipio.Estado.Pais.Nombre = obj.NombrePais;


                            result.Objects.Add(usuarioobj);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "los usuarios no se pudo mostar";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;

            }
            return result;
        }
        public static ML.Result Add (ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ZjuanProgramacionNcapasContext context = new DL.ZjuanProgramacionNcapasContext())
                {
                    var usuarios = context.Database.ExecuteSqlRaw($"UsuarioAdd'{usuario.Nombre}','{usuario.ApellidoPaterno}','{usuario.ApellidoMaterno}','{usuario.Sexo}','{usuario.Telefono}','{usuario.Email}','{usuario.FechaDeNacimiento}','{usuario.Password}','{usuario.Celular}','{usuario.CURP}','{usuario.UserName}',{usuario.Rol.IdRol},'{usuario.Imagen}',{usuario.Status},'{usuario.Direccion.Calle}','{usuario.Direccion.NumeroInterior}','{usuario.Direccion.NumeroExterior}',{usuario.Direccion.Colonia.IdColonia}");
                    if (usuarios > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se ha podido realizar la consulta";
                    }

                } 
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;

            }
            return result;
        }
        public static ML.Result GetById(int IdUsuario)
        {

            ML.Result result = new ML.Result();
            try
            {
                using (DL.ZjuanProgramacionNcapasContext context = new DL.ZjuanProgramacionNcapasContext())
                {
                    var usuarios = context.Usuarios.FromSqlRaw($"UsuarioGetById {IdUsuario}").AsEnumerable().FirstOrDefault();
                    
                    if (usuarios != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.Rol = new ML.Rol();

                        usuario.IdUsuario = usuarios.IdUsuario;
                        usuario.Nombre = usuarios.Nombre;
                        usuario.ApellidoPaterno = usuarios.ApellidoPaterno;
                        usuario.ApellidoMaterno = usuarios.ApellidoMaterno;
                        usuario.Sexo = usuarios.Sexo;
                        usuario.Telefono = usuarios.Telefono;
                        usuario.Email = usuarios.Email;
                        usuario.FechaDeNacimiento = usuarios.FechaDeNacimiento.ToString();
                        usuario.Password = usuarios.Password;
                        usuario.Celular = usuarios.Celular;
                        usuario.CURP = usuarios.Curp;
                        usuario.UserName = usuarios.UserName;
                        usuario.Rol.IdRol = usuarios.IdRol.Value;
                        usuario.Rol.NombreRol = usuarios.NombreRol;
                        usuario.Imagen = usuarios.Imagen;

                        usuario.Direccion = new ML.Direccion();
                        
                        usuario.Direccion.Calle = usuarios.Calle;
                        usuario.Direccion.NumeroInterior = usuarios.NumeroInterior;
                        usuario.Direccion.NumeroExterior = usuarios.NumeroExterior;

                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.IdColonia = usuarios.IdColonia;
                        usuario.Direccion.Colonia.Nombre = usuarios.NombreColonia;

                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.IdMunicipio = usuarios.IdMunicipio;
                        usuario.Direccion.Colonia.Municipio.Nombre = usuarios.NombreMunicipio;

                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        usuario.Direccion.Colonia.Municipio.Estado.IdEstado = usuarios.IdEstado;
                        usuario.Direccion.Colonia.Municipio.Estado.Nombre = usuarios.NombreEstado;


                        usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = usuarios.IdPais;
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = usuarios.NombrePais;
                        result.Object = usuario;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "El usuario no se pudo mostrar";
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ZjuanProgramacionNcapasContext context = new DL.ZjuanProgramacionNcapasContext())
                {
                    var usuarios = context.Database.ExecuteSqlRaw($"UsuarioUpdate'{usuario.Nombre}','{usuario.ApellidoPaterno}','{usuario.ApellidoMaterno}','{usuario.Sexo}','{usuario.Telefono}','{usuario.Email}','{usuario.FechaDeNacimiento}','{usuario.Password}','{usuario.Celular}','{usuario.CURP}','{usuario.UserName}',{usuario.Rol.IdRol},'{usuario.Imagen}','{usuario.Direccion.Calle}','{usuario.Direccion.NumeroInterior}','{usuario.Direccion.NumeroExterior}',{usuario.Direccion.Colonia.IdColonia},{usuario.IdUsuario}");
                    if (usuarios > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se ingreso el registro";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result DeleteEF(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ZjuanProgramacionNcapasContext context = new DL.ZjuanProgramacionNcapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UsuarioDelete {IdUsuario}");
                    if(query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = !false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;

        }



        //Metodo para convertir el excel a datatable
        public static ML.Result ConvertirExceltoDataTable(string connString)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (OleDbConnection context = new OleDbConnection(connString))
                {
                    string query = " SELECT * FROM [Hoja1$]";
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;

                        OleDbDataAdapter adpte = new OleDbDataAdapter();
                        adpte.SelectCommand = cmd;
                        //se debe establecer antes de llamar al método Fill de DataAdapter.
                        DataTable tableUsuario = new DataTable();
                        adpte.Fill(tableUsuario);
                        if (tableUsuario.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();

                            foreach (DataRow row in tableUsuario.Rows)
                            {
                                ML.Usuario usuario = new ML.Usuario();
                                usuario.Nombre = row[0].ToString();
                                usuario.ApellidoPaterno = row[1].ToString();
                                usuario.ApellidoMaterno = row[2].ToString();
                                usuario.Sexo = row[3].ToString();
                                usuario.Telefono = row[4].ToString();
                                usuario.Email = row[5].ToString();
                                usuario.FechaDeNacimiento = row[6].ToString();
                                usuario.Password = row[7].ToString();
                                usuario.Celular = row[8].ToString();
                                usuario.CURP = row[9].ToString();
                                usuario.UserName = row[10].ToString();

                                usuario.Rol = new ML.Rol();
                                usuario.Rol.IdRol = int.Parse(row[11].ToString());

                                usuario.Imagen = null;

                                usuario.Direccion = new ML.Direccion();
                                usuario.Direccion.Calle = row[12].ToString();
                                usuario.Direccion.NumeroInterior = row[13].ToString();
                                usuario.Direccion.NumeroExterior = row[14].ToString();

                                usuario.Direccion.Colonia = new ML.Colonia();
                                usuario.Direccion.Colonia.IdColonia = int.Parse(row[14].ToString());

                                result.Objects.Add(usuario);
                            }
                            result.Correct = true;
                        }
                            if (tableUsuario.Rows.Count > 1)
                            {
                                result.Correct = true;
                            }
                            else
                            {
                                result.Correct = false;
                                result.ErrorMessage = "No existen registros en el excel";
                            }

                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result ValidarExcel(List<object> Object)
        {
            ML.Result result = new ML.Result();

            try
            {
                result.Objects = new List<object>();
                //DataTable  //Rows //Columns
                int i = 1;
                foreach (ML.Usuario usuario in Object)
                {
                    ML.ErrorExcel error = new ML.ErrorExcel();
                    error.IdRegistro = i++;


                    usuario.Nombre = (usuario.Nombre == "") ? error.Mensaje += "Ingresar el nombre  " : usuario.Nombre; //operador ternario
                    //Esta es otra opcopn para validar el registro que esta en el documento de excel
                    if (usuario.ApellidoPaterno == "")
                    {
                        error.Mensaje += "Ingresar el Apellido Paterno ";
                    }
                    usuario.ApellidoMaterno = (usuario.ApellidoMaterno == "") ? error.Mensaje += " \nIngrese el aprllido Marerno" : usuario.ApellidoMaterno;
                    usuario.Sexo = (usuario.Sexo == "") ? error.Mensaje += "\n Ingrese el sexo" : usuario.Sexo;
                    usuario.Telefono = (usuario.Telefono == "") ? error.Mensaje += "\nIngrese el numero telefonico" : usuario.Telefono;
                    usuario.Email = (usuario.Email == "") ? error.Mensaje += "\nIngrese el Correo electronico" : usuario.Email;
                    usuario.FechaDeNacimiento = (usuario.FechaDeNacimiento == "") ? error.Mensaje += "\nIngrese la fecha de nacimiento" : usuario.FechaDeNacimiento;
                    usuario.Password = (usuario.Password == "") ? error.Mensaje += "\nIngrese la contraseña" : usuario.Password;
                    usuario.Celular = (usuario.Celular == "") ? error.Mensaje += "\nIngrese el numero celular" : usuario.Celular;
                    usuario.CURP = (usuario.CURP == "") ? error.Mensaje += "\nIngrese el curp" : usuario.CURP;
                    usuario.UserName = (usuario.UserName == "") ? error.Mensaje += "\nIngrese el nombre de usuario" : usuario.UserName;
                    usuario.Direccion.Calle = (usuario.Direccion.Calle == "") ? error.Mensaje += "\nIngrese la calle" : usuario.Direccion.Calle;
                    usuario.Direccion.NumeroInterior = (usuario.Direccion.NumeroInterior == "") ? error.Mensaje += "\nIngrese el numero interior" : usuario.Direccion.NumeroInterior;
                    usuario.Direccion.NumeroExterior = (usuario.Direccion.NumeroExterior == "") ? error.Mensaje += "\nIngrese el numero exterios" : usuario.Direccion.NumeroExterior;
                    if (usuario.Direccion.Colonia.IdColonia.ToString() == "")
                    {
                        error.Mensaje += "\nIngresar el id colonia ";
                    }
                    if (error.Mensaje != null)
                    {
                        result.Objects.Add(error);
                    }


                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }

            return result;
        }


        //Metodo para hacer el combio de estatus de 1(true) a 0(false) o al contrario 
        public static ML.Result ChangeStatus(int IdUsuario, bool Status)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ZjuanProgramacionNcapasContext context = new DL.ZjuanProgramacionNcapasContext())
                {
                    var usuarios = context.Database.ExecuteSqlRaw($"UsuarioChangeStatus {IdUsuario},{Status}");
                    if (usuarios > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se ingreso el registro";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        // Metodo para hacer el username en el login 

        public static ML.Result GetByUserName(string UserName)
        {

            ML.Result result = new ML.Result();
            try
            {
                using (DL.ZjuanProgramacionNcapasContext context = new DL.ZjuanProgramacionNcapasContext())
                {
                    var usuarios = context.Usuarios.FromSqlRaw($"UsuarioGetByUsername {UserName}").AsEnumerable().FirstOrDefault();

                    if (usuarios != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.Rol = new ML.Rol();

                        usuario.IdUsuario = usuarios.IdUsuario;
                        usuario.Nombre = usuarios.Nombre;
                        usuario.ApellidoPaterno = usuarios.ApellidoPaterno;
                        usuario.ApellidoMaterno = usuarios.ApellidoMaterno;
                        usuario.Sexo = usuarios.Sexo;
                        usuario.Telefono = usuarios.Telefono;
                        usuario.Email = usuarios.Email;
                        usuario.FechaDeNacimiento = usuarios.FechaDeNacimiento.ToString();
                        usuario.Password = usuarios.Password;
                        usuario.Celular = usuarios.Celular;
                        usuario.CURP = usuarios.Curp;
                        usuario.UserName = usuarios.UserName;
                        usuario.Rol.IdRol = usuarios.IdRol.Value;
                        usuario.Rol.NombreRol = usuarios.NombreRol;
                        usuario.Imagen = usuarios.Imagen;

                        usuario.Direccion = new ML.Direccion();

                        usuario.Direccion.Calle = usuarios.Calle;
                        usuario.Direccion.NumeroInterior = usuarios.NumeroInterior;
                        usuario.Direccion.NumeroExterior = usuarios.NumeroExterior;

                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.IdColonia = usuarios.IdColonia;
                        usuario.Direccion.Colonia.Nombre = usuarios.NombreColonia;

                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.IdMunicipio = usuarios.IdMunicipio;
                        usuario.Direccion.Colonia.Municipio.Nombre = usuarios.NombreMunicipio;

                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        usuario.Direccion.Colonia.Municipio.Estado.IdEstado = usuarios.IdEstado;
                        usuario.Direccion.Colonia.Municipio.Estado.Nombre = usuarios.NombreEstado;


                        usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = usuarios.IdPais;
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = usuarios.NombrePais;
                        result.Object = usuario;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "El usuario no se pudo mostrar";
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }


    }
}
