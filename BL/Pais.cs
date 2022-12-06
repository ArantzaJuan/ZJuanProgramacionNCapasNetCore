using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace BL
{
    public class Pais
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ZjuanProgramacionNcapasContext context = new DL.ZjuanProgramacionNcapasContext())
                {
                    var paises = context.Pais.FromSqlRaw("PaisGetAll").ToList();
                    result.Objects = new List<object>();
                    if (paises != null)
                    { 
                        foreach (var obj in paises)
                        {

                            ML.Pais pais = new ML.Pais();

                            pais.IdPais = obj.IdPais;
                            pais.Nombre  = obj.Nombre;

                            result.Objects.Add(pais);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "los Paises no se puden mostar";
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
