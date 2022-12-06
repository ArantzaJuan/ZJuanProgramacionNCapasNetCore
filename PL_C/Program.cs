
using ML;

ReadFile();
Console.ReadKey();
static void ReadFile()
{
    string file = @"C:\Users\digis\Documents\Zaida Arantza Juan Alfonso\ZJuanDia1\LayoutUsuario.txt";
    if(File.Exists(file))
    {
        StreamReader Textfile = new StreamReader(file);
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
            usuario.Direccion.Colonia.IdColonia = int.Parse (lines[14]);

            ML.Result result = BL.Usuario.Add(usuario);

            
            if (result.Correct == true)
            {
                Console.WriteLine("Correcto");
                Console.ReadKey();

            }
            else
            {
                string fileError = @"C:\Users\digis\Documents\Zaida Arantza Juan Alfonso\ZJuanDia1\Errores.txt";
                //result.ErrorMessage;
                StreamWriter errorFile = new StreamWriter(fileError);

                errorFile.WriteLine("" + result.ErrorMessage);
                errorFile.Close();  
                //CREAR UN TXT DE ERRORES

            }

        }
        

    }
    
}

