using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tiff2PDF
{
    static class Program
    {
        ///// <summary>
        ///// The main entry point for the application.
        ///// </summary>
        //[STAThread]
        //static void Main()
        //{
        //    Application.EnableVisualStyles();
        //    Application.SetCompatibleTextRenderingDefault(false);
        //    Application.Run(new mainForm());
        //}

        static void Main(string[] args)
        {
            if (args == null || args.Length != 2)
            {
                printHelp();
                return;
            }

            //Write to a PDF format file
            Console.WriteLine("Converting...");
            try
            {
                bool r = Converter.ConvertTiffToPdf(args[0], args[1]);
                Console.WriteLine(r ? "Successful completion." : "Failed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Failed: {0}", ex.ToString()));
            }
        }

        private static void printHelp()
        {
            Console.WriteLine("usage: Txt2Pdf inFilename outFilename");
        }
    }
}
