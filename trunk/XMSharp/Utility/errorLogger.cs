using System;
using System.Collections.Generic;
using System.Text;

namespace XMSharp.Utility
{
    class errorLog
    {
        public static void addException(string functionName, string exceptionString)
        {
            // Write the string to a file.
            System.IO.StreamWriter logFile = new System.IO.StreamWriter("c:\\errorLogXMS.txt", true);
            logFile.WriteLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + "::" + functionName + "::" + exceptionString);

            logFile.Close();            
        }
    }
}
