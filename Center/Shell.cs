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
            Process p = new Process();
            p.StartInfo = new ProcessStartInfo();
            p.StartInfo.Arguments = path;

            if (!Directory.Exists(p.StartInfo.Arguments))
                Directory.CreateDirectory(p.StartInfo.Arguments);

            p.StartInfo.FileName = "explorer.exe";
            p.Start();
        }
    }
}
