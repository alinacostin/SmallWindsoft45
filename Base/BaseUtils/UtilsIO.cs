using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Base.BaseUtils
{
    public class UtilsIO
    {

        public static void CopyDirectory(string Src, string Dst, out string strError)
        {

            strError = String.Empty;

            String[] Files;

            if (Dst[Dst.Length - 1] != Path.DirectorySeparatorChar)
                Dst += Path.DirectorySeparatorChar;
            if (!Directory.Exists(Dst)) Directory.CreateDirectory(Dst);
            try
            {
                Files = Directory.GetFileSystemEntries(Src);
                foreach (string Element in Files)
                {
                    // Sub directories
                    if (Directory.Exists(Element))
                    {
                        CopyDirectory(Element, Dst + Path.GetFileName(Element), out strError);
                        if (strError != String.Empty)
                            throw new Exception(strError);
                    }
                    // Files in directory
                    else
                        File.Copy(Element, Dst + Path.GetFileName(Element), true);
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
        }

    }
}
