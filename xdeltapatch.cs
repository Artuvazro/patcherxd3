using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

//Rutina para aplicar el parche xdelta.

class XdeltaPatch {


    public string extension;
    public string fileName2;

    public void Patch(string fileName, string fileNameS, string directoryPath, string defnamespace, string patch, string label)
    {
        //Aislar y eliminar la extensión.
        extension = System.IO.Path.GetExtension(fileName);
        fileName2 = fileName.Substring(0, fileName.Length - extension.Length);
        string fileNameS2 = fileNameS.Substring(0, fileNameS.Length - extension.Length);

        Environment.CurrentDirectory = Application.StartupPath;

        //Crear un directorio de archivos temporales para extraer los recursos.
        Directory.CreateDirectory("./temporal");

        //Extaer xdelta3
        Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(defnamespace +".Resources.xdelta3.exe");
        FileStream fileStream = new FileStream("./temporal/xdelta3.exe", FileMode.CreateNew);
        for (int i = 0; i < stream.Length; i++)
            fileStream.WriteByte((byte)stream.ReadByte());
        fileStream.Close();

        //Extraer parche
        Stream stream2 = Assembly.GetExecutingAssembly().GetManifestResourceStream(defnamespace +".Resources."+ patch +".xd3");
        FileStream fileStream2 = new FileStream("./temporal/"+ patch +".xd3", FileMode.CreateNew);
        for (int i = 0; i < stream2.Length; i++)
            fileStream2.WriteByte((byte)stream2.ReadByte());
        fileStream2.Close();

        
        //Ejecutar xdelta3
        System.Diagnostics.Process process = new System.Diagnostics.Process();
        System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
        startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
        startInfo.FileName = Environment.CurrentDirectory + "/temporal/xdelta3.exe";
        startInfo.Arguments = "-dfs \"" + fileName + "\" ./temporal/"+ patch +".xd3 " + "\"" + directoryPath + "\\" + fileNameS2 + " " + label + extension + "\"";
        process.StartInfo = startInfo;
        process.Start();



        //Borrar todo.
        process.WaitForExit();
        File.Delete("./temporal/"+ patch +".xd3");
        File.Delete("./temporal/xdelta3.exe");
        Directory.Delete("./temporal");
    }

}