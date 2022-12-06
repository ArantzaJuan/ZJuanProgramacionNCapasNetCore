using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Municipio
    {
        public static ML.Result GetByIdEstado(int IdEstado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ZjuanProgramacionNcapasContext context = new DL.ZjuanProgramacionNcapasContext())
                {
                    var municipios = context.Municipios.FromSqlRaw($"MunicipioGetByIdEstado {IdEstado}").ToList();
                    result.Objects = new List<object>();
                    if (municipios != null)
                    {
                        foreach (var obj in municipios)
                        {
                            ML.Municipio municipio = new ML.Municipio();

                            municipio.IdMunicipio = obj.IdMunicipio;
                            municipio.Nombre = obj.Nombre;

                            municipio.Estado = new ML.Estado();
                            municipio.Estado.IdEstado = IdEstado;

                            result.Objects.Add(municipio);
                        }
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "los municipios no se puden mostar";
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
