using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Builder : BoolObject
    {
        public string Name;
        public string Complier;
        public string Linker;
        public string Debugger;

        public void Build()
        {
            if (string.IsNullOrEmpty(Complier))
                return;

            string floder = Center.Option.Solution.LastSolutionPath;
            if (string.IsNullOrEmpty(floder))
                return;

            string ccp_entry = Path.Combine(floder, "main.cpp");
            string out_file = Path.Combine(floder, "bin") + "/bin.exe";
            string args = string.Format("{0} -o {1} -g", ccp_entry, out_file);

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = Complier;
            startInfo.Arguments = args;
            startInfo.CreateNoWindow = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.UseShellExecute = false;
            startInfo.WorkingDirectory = Path.GetDirectoryName(Complier);

            Process process = new Process();
            process.EnableRaisingEvents = true;
            process.StartInfo = startInfo;
            process.ErrorDataReceived += process_ErrorDataReceived;
            process.OutputDataReceived += process_ErrorDataReceived;
            process.Exited += process_Exited;

            if (process.Start())
            {
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                PortHub.OnConsoleClear.Trigger();
                PortHub.OnConsole.Value = "=>Complier Args:" + startInfo.Arguments;
                PortHub.OnConsole.Trigger();
                PortHub.OnBeginBuild.Trigger();
            }
        }

        void process_Exited(object sender, EventArgs e)
        {
            Process process = (Process)sender;
            PortHub.OnConsole.Value= process.ExitCode == 0 ? "=>Complier Sucessfully!" : string.Empty;
            PortHub.OnEndBuild.Trigger();
        }

        void process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
                PortHub.OnConsole.Value = e.Data;
        }
    }
}
