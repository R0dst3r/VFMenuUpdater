using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace VFMenuUpdater
{
    class Program
    {
        public static bool enterAnother = true;
        public static string cr = System.Environment.NewLine;

        static void Main(string[] args)
        {
            Console.WriteLine("Enter the fiscal year you want to update TO as yy|yy (Ex: 18/19):");
            string[] fy = Console.ReadLine().Split('/');
            Console.Clear();

            while (enterAnother)
            {
                Console.WriteLine(cr + cr + "Enter VF menu extension to want to update: (A_P, A_R, EOM, GEN, INV, MFG, MGT, TRK, WO)");
                string fileToUpdate = $@"\\diskstation\versaform\eoyPrep\menus\appmenu.{Console.ReadLine().ToUpper()}";

                FileInfo fi = new FileInfo(fileToUpdate);
                if (!fi.Exists)
                {
                    Console.WriteLine("That file does not exist." + cr);                    
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"Extension accepted. About to modify {fileToUpdate}... press [ENTER] to begin." + cr);
                    Console.ReadKey();
                    ProcessFile(fi);
                    AskForAnother();
                }

            }

            Console.Clear();
            Console.WriteLine("Program exiting... press any key");
            Console.ReadKey();

        }

        private static void ProcessFile(FileInfo fileName)
        {
            // Load the file, iterate through and replace all year strings from most recent pairings to oldest.
            StreamReader sr = new StreamReader(fileName.FullName, true);
            FileStream fs = new FileStream(fileName.DirectoryName + @"\updated\" + fileName.Name, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, sr.CurrentEncoding);

            string examLine;
            while (!sr.EndOfStream)
            {
                examLine = sr.ReadLine();
                Console.WriteLine(examLine);
                foreach (char piece in examLine)
                { sw.Write(piece); }
            }
            sr.Close();
            sw.Close();
            Console.WriteLine(cr + cr + "Process Complete." + cr);
        }

        private static void AskForAnother()
        {
            Console.Write("Do you want to update another menu? (Y/N): ");
            enterAnother = Console.ReadKey().Key.ToString().ToUpper() == "Y" ? true : false;
        }
    }
}
