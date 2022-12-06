using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace BL
{
    public class Proveedor
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ZjuanProgramacionNcapasContext context = new DL.ZjuanProgramacionNcapasContext())
                {
                    var proveedores = context.Proveedors.FromSqlRaw("ProveedorGetAll").ToList();

                    result.Objects = new List<object>();
                    if (proveedores != null)
                    {
                        foreach (var obj in proveedores)
                        {

                            ML.Proveedor proveedor = new ML.Proveedor();

                            proveedor.IdProveedor = obj.IdProveedor;
                            proveedor.Telefono = obj.Telefono;
                            proveedor.Nombre = obj.Nombre;

                            result.Objects.Add(proveedor);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "los rols no se puden mostar";
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