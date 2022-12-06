using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace BL
{
    public class Producto
    {
        //public static ML.Result Getall()
        //{
        //    ML.Result result = new ML.Result();
        //    try
        //    {
        //        using (DL.ZjuanProgramacionNcapasContext context = new DL.ZjuanProgramacionNcapasContext())
        //        {
        //            var productos = context.Productos.FromSqlRaw("ProductoGetall").ToList();
        //            result.Objects = new List<object>();
        //            if (productos != null)
        //            {
        //                foreach (var obj in productos)
        //                {

        //                    ML.Producto producto = new ML.Producto();
        //                    producto.Proveedor = new ML.Proveedor();
        //                    producto.Departamento = new ML.Departamento();
        //                    producto.IdProducto = obj.IdProducto;
        //                    producto.Nombre = obj.Nombre;
        //                    producto.PrecioUnitario = obj.PrecioUnitario.Value;
        //                    producto.Stock = obj.Stock.Value;
        //                    producto.Proveedor.IdProveedor = obj.IdProveedor.Value;
        //                    producto.Proveedor.Nombre = obj.NombreProveedor;
        //                    producto.Departamento.IdDepartamento = obj.IdDepartamento.Value;
        //                    producto.Departamento.Nombre = obj.NombreDepartamento;
        //                    producto.Descripcion = obj.Descripcion;
        //                    producto.Imagen = obj.Imagen;
                            

        //                    result.Objects.Add(producto);
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
        public static ML.Result Getall(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ZjuanProgramacionNcapasContext context = new DL.ZjuanProgramacionNcapasContext())
                {
                    producto.Proveedor.IdProveedor = (producto.Proveedor.IdProveedor == null) ? 0 : producto.Proveedor.IdProveedor;
                    var productos = context.Productos.FromSqlRaw($"ProductoGetall '{producto.Nombre}',{producto.Proveedor.IdProveedor}").ToList();
                    result.Objects = new List<object>();
                    if (productos != null)
                    {
                        foreach (var obj in productos)
                        {

                            ML.Producto productoobju = new ML.Producto();
                            productoobju.Proveedor = new ML.Proveedor();
                            productoobju.Departamento = new ML.Departamento();
                            productoobju.IdProducto = obj.IdProducto;
                            productoobju.Nombre = obj.Nombre;
                            productoobju.PrecioUnitario = obj.PrecioUnitario.Value;
                            productoobju.Stock = obj.Stock.Value;
                            productoobju.Proveedor.IdProveedor = obj.IdProveedor.Value;
                            productoobju.Proveedor.Nombre = obj.NombreProveedor;
                            productoobju.Departamento.IdDepartamento = obj.IdDepartamento.Value;
                 
                            productoobju.Departamento.Nombre = obj.NombreDepartamento;
                            productoobju.Descripcion = obj.Descripcion;
                            productoobju.Imagen = obj.Imagen;


                            result.Objects.Add(productoobju);
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
        public static ML.Result Add(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ZjuanProgramacionNcapasContext context = new DL.ZjuanProgramacionNcapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"ProductoAdd'{producto.Nombre}', {producto.PrecioUnitario}, {producto.Stock}, {producto.Proveedor.IdProveedor},{producto.Departamento.Area.IdArea}, {producto.Departamento.IdDepartamento}, '{ producto.Descripcion}'");
                    if (query > 0)
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
        public static ML.Result GetById(int IdProducto)
        {

            ML.Result result = new ML.Result();
            try
            {
                using (DL.ZjuanProgramacionNcapasContext context = new DL.ZjuanProgramacionNcapasContext())
                {
                    var productos = context.Productos.FromSqlRaw($"ProductoGetById {IdProducto}").AsEnumerable().FirstOrDefault();
                    
                    if (productos != null)
                    {

                        ML.Producto producto = new ML.Producto();
                       
                        

                        producto.IdProducto = productos.IdProducto;
                        producto.Nombre = productos.Nombre;
                        producto.PrecioUnitario = productos.PrecioUnitario.Value;
                        producto.Stock = productos.Stock.Value;

                        producto.Proveedor = new ML.Proveedor();
                        producto.Proveedor.IdProveedor = productos.IdProveedor.Value;
                        producto.Proveedor.Nombre = productos.NombreProveedor;

                        producto.Departamento = new ML.Departamento();
                        producto.Departamento.IdDepartamento = productos.IdDepartamento.Value;
                        producto.Departamento.Nombre = productos.NombreDepartamento;

                        producto.Departamento.Area = new ML.Area();
                        producto.Departamento.Area.IdArea = productos.IdArea;
                        producto.Departamento.Area.Nombre = productos.NombreArea;

                        producto.Descripcion = productos.Descripcion;
                        producto.Imagen = null;
                        result.Object = producto;
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
    
        public static ML.Result Update(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ZjuanProgramacionNcapasContext context = new DL.ZjuanProgramacionNcapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"ProductoUpDate '{producto.Nombre}', {producto.PrecioUnitario}, {producto.Stock}, {producto.Proveedor.IdProveedor},{producto.Departamento.IdDepartamento}, '{producto.Descripcion}','{producto.Imagen}', {producto.IdProducto}");
                    if (query > 0)
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
        public static ML.Result Delete(int IdProducto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ZjuanProgramacionNcapasContext context = new DL.ZjuanProgramacionNcapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"ProductoDelete {IdProducto}");
                    if (query > 0)
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

        
        
    }
}
