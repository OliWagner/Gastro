using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GastroPages.Helpers
{
    public static class FileHelper
    {
        public static byte[] GetBytesFromFile(string fullFilePath)
        {

            FileStream fs = null;

            try
            {
                fs = File.OpenRead(fullFilePath);
                var bytes = new byte[fs.Length];
                fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
                return bytes;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }

        }

        
    }

    
}