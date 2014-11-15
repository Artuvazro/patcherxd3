using System;
using System.IO;
using System.Windows.Forms;



//Rutina para comprobar que realmente has abierto la ROM que toca.
//Dump desde posición 0x120 hasta 0x12E (14 bytes)
//Contenido que se espera: "SHINING FORCE 2" (5348494E494E4720464F5243452032)

namespace FileCheck
{
    class CheckFile
    {
        public static bool Checker(string fileName)
        {
            //MessageBox.Show(fileName); //Debug
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            int offset = 288;       //Empieza aquí SHINING FORCE II (0x120)
            fs.Seek(offset, 0);
            int hexIn;
            String hex;
            hex = "";
            hexIn = fs.ReadByte();

            for (int i = 0; i < 15; i++)
            {
                fs.Seek(offset + i, 0);
                hexIn = fs.ReadByte();
                hex = hex + string.Format("{0:X2}", hexIn);
            }

            if (hex != "5348494E494E4720464F5243452032") //SHINING FORCE II
            {
                MessageBox.Show("Esta ROM no es compatible con el parche.\rRecuerda que necesitas la ROM SHINING FORCE II (U).", "ROM no compatible", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
                return true;
        }


    }
}