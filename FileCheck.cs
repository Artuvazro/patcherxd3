using System;
using System.IO;
using System.Windows.Forms;



//Rutina para comprobar que realmente has abierto la ROM que toca.

namespace FileCheck
{
    class CheckFile
    {
        public static bool Checker(string fileName, int checkoffset, string datacheck, string ROM)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            int offset = checkoffset;
            fs.Seek(offset, 0);
            int hexIn;
            String hex;
            hex = "";
            hexIn = fs.ReadByte();

            for (int i = 0; i < datacheck.Length/2; i++)
            {
                fs.Seek(offset + i, 0);
                hexIn = fs.ReadByte();
                hex = hex + string.Format("{0:X2}", hexIn);
            }

            if (hex != datacheck)
            {
                MessageBox.Show("Esta ROM no es compatible con el parche.\rRecuerda que necesitas la ROM\r" + ROM + ".", "ROM no compatible", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
                return true;
        }


    }
}