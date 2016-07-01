using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Shell
    {
        public static void OpenFloder(string path)
        {
            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }
            else
            {
                FileInfo fi = new FileInfo(path);
                    path = Path.GetDirectoryName(path);
            }

            Process p = new Process();
            p.StartInfo = new ProcessStartInfo();
            p.StartInfo.Arguments = path;

            p.StartInfo.FileName = "explorer.exe";
            p.Start();
        }
    }
}
