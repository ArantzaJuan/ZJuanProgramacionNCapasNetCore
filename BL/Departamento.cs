using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Departamento
    {
        public static ML.Result GetByIdArea(int IdArea)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ZjuanProgramacionNcapasContext context = new DL.ZjuanProgramacionNcapasContext())
                {
                    var query = context.Departamentos.FromSqlRaw($"DepartamentoGetByIdArea {IdArea}").ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var depar in query)
                        {
                            ML.Departamento departamento = new ML.Departamento();

                            departamento.IdDepartamento = depar.IdDepartamento;
                            departamento.Nombre = depar.Nombre;

                            departamento.Area = new ML.Area();
                            departamento.Area.IdArea = IdArea;

                         
                            result.Objects.Add(departamento);

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


 
