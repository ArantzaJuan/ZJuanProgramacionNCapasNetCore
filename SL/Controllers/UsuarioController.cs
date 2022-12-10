using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        // GET: api/<UsuarioController>

        [HttpGet("GetAll")]
        
        public IActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            ML.Result result = BL.Usuario.GetAll(usuario);
            ML.Result resultrol = BL.Rol.GetAll();
            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
                usuario.Rol.Roles= resultrol.Objects;
                return Ok(result);
            }
            else
            {
                return NoContent();
            }
        }
        
        [HttpPost("GetAll")]
        public IActionResult GetAll(int? idRol, string? nombre,string? apellido )
        {
            ML.Usuario usuario =new ML.Usuario();   
            usuario.Rol = new ML.Rol();
            usuario.Nombre = (nombre == null)? "" : nombre;
            usuario.ApellidoPaterno= (usuario.ApellidoPaterno == null)? "" : usuario.ApellidoPaterno;
            
            usuario.Rol.IdRol= (idRol == null) ? 0 : idRol.Value;
            

            ML.Result result = BL.Usuario.GetAll(usuario);
            ML.Result restrol = BL.Rol.GetAll();
            if (result.Correct)
            {
                usuario.Usuarios= result.Objects;   
                usuario.Rol.Roles = restrol.Objects;
                return Ok(result);
            }
            else
            {
                return NoContent();
            }
        }
        // GET api/<UsuarioController>/5

        
        [HttpGet("GetById/{idUsuario}")]

        public IActionResult GetById(int idUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            ML.Result result = BL.Usuario.GetById(idUsuario);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NoContent();
            }
        }

        // POST api/<UsuarioController>
       
        [HttpPost("Add")]
        public IActionResult Post([FromBody]ML.Usuario usuario)
        {
           
            ML.Result result = BL.Usuario.Add(usuario);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("Update")]
        public IActionResult Update( [FromBody]ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            usuario.Rol = new ML.Rol();
            result = BL.Usuario.Update(usuario);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NoContent();
            }
        }

        // DELETE api/<UsuarioController>/5
        
        [HttpDelete("Delete/{idUsuario}")]
        public IActionResult Delete(int idUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            ML.Result result = BL.Usuario.DeleteEF(idUsuario);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NoContent();
            }
        }
    }
}
