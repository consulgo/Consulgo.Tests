using System.Diagnostics;

namespace Consulgo.Test.ReactiveAutomation.SampleRunner
{
    public class ProcessHelper
    {
        public static Process StartUglyApplication()
        {
            var fi = new System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var path = System.IO.Path.Combine(
                @"..\..\..\Ugly.Yaua",
                fi.Directory.Parent.Name,
                fi.Directory.Name,
                @"Ugly.Yaua.exe");
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(path
                //, "--TryError"
               );
            p.Start();
            return p;
        }

        public static bool TryToKill(Process p)
        {
            try
            {
                p.Kill();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
