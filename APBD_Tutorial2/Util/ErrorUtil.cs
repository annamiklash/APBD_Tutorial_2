using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace APBD_Tutorial2
{
    public class ErrorUtil
    {

        public static void GenerateError(int line, string message)
        {
            using TextWriter textWriter = new StreamWriter(Constants.logFile, true);
            textWriter.WriteLine("Error at line " + line.ToString() + ": " + message);

        }
    }
}
