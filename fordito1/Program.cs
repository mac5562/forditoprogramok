using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace fordito1
{
    class Program
    {
        static void Main(string[] args)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            SourceHandler SH = new SourceHandler(@currentDirectory+"/filename.txt", @"filename2.txt");
            SH.OpenFileToRead();
            Console.WriteLine(SH.content);
            SH.ReplaceFirst();
            //SH.ReplaceContent("new text");
            Console.WriteLine(SH.content);
            SH.OpenFileToWrite();
            Console.ReadKey();

            //    StreamReader SR = new StreamReader(File.OpenRead("path1"));
            //    string s = SR.ReadToEnd();

            //    //while (SR.Peek()>-1)
            //    {
            //    //    s = SR.ReadLine();
            //    }

            //    SR.Close();
            //    StreamWriter SW = new StreamWriter(File.Open("path2",FileMode.Create));
            //    SW.WriteLine("some text");
            //    SW.Flush();
            //    SW.Close();
        }
    }
}

