using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Estado
    {
        public static ML.Result GetByIdPais(int IdPais)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ZjuanProgramacionNcapasContext context = new DL.ZjuanProgramacionNcapasContext())
                {
                    var query = context.Estados.FromSqlRaw($"EstadoGetByIdPais {IdPais}").ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var estados in query)
                        {
                            ML.Estado estado = new ML.Estado();

                            estado.IdEstado = estados.IdEstado;
                            estado.Nombre = estados.Nombre;

                            estado.Pais = new ML.Pais();
                            estado.Pais.IdPais = IdPais;

                            result.Objects.Add(estado);

                        }

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "los estados no se puden mostar";
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

