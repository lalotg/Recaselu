using System;
using System.IO;
using McMaster.Extensions.CommandLineUtils;

namespace Recaselu
{
    [Command(Name="Recaselu",Description ="Cambiar de mayúsculas a minúsculas")]
    [HelpOption("-?|--help")]
    class Program
    {
        static void Main(string[] args)
            => CommandLineApplication.Execute<Program>(args);

        [Option("-o|--option",Description ="Opciones: uppercase ó lowercase")]
        string Opcion {get;} = "uppercase";
        [Option("-d|--directory")]
        string Directorio {get;} = Directory.GetCurrentDirectory();

        private void OnExecute(){
            if(Directory.Exists(Directorio))
            {
                DirectoryInfo d = new DirectoryInfo(Directorio);
                FileInfo[] files = d.GetFiles();

                for(int i=0;i<files.Length;i++)
                {
                    string inicial = files[i].Name.Replace(Path.GetExtension(files[i].FullName),"");
                    string fin= string.Empty;

                    //Mayuscula
                    if(Opcion == "lowercase"){
                        fin = inicial.ToLower();
                        Renombrar(files[i],fin);
                    }

                    //Minusculas
                    if(Opcion == "uppercase")
                    {
                        fin = inicial.ToUpper();
                        Renombrar(files[i],fin);
                    }
                }             
            }else{
                Console.WriteLine("El directorio no existe o está mal escrito.");
            }
        }  

        private void Renombrar(FileInfo fileInfo,string fileEndName)
        {
            string final = (fileEndName + Path.GetExtension(fileInfo.FullName));
            System.IO.File.Move(fileInfo.FullName,Directorio + @"\" + final);
            Console.WriteLine($"{fileInfo.Name} => {final}");
        }      
    }
}
