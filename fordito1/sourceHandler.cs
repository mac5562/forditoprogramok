using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace fordito1
{
    class SourceHandler
    {
        private string path1, path2 = "";

        public string content = "";


        public string Path1
        {
            get { return path1; }
            set { path1 = value; }
        }

        public string Path2
        {
            get { return path2; }
            set { path2 = value; }
        }
       

        public string Content
        {
            get { return content; }
            set { content = value; }
        }


        public SourceHandler(string path1, string path2)
        {
            this.path1 = path1;
            this.path2 = path2;
        }
        public void OpenFileToRead()
        {
            try
            {
                StreamReader SR = new StreamReader(File.OpenRead(this.path1));
                content  = SR.ReadToEnd();

                //while (SR.Peek()>-1)
                //{
                //    content = SR.ReadLine();
                //}

                SR.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void ReplaceContent(string s)
        {
            try
            {
                this.content = s;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void OpenFileToWrite()
        {
            try
            {
                StreamWriter SW = new StreamWriter(File.Open(path2, FileMode.Create));
                SW.WriteLine(content);
                SW.Flush();
                SW.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void ReplaceFirst()
        {
            Dictionary<string, string> FromTo = new Dictionary<string, string>();
            content = Regex.Replace(content, @"//(.*?)\r?\n", "");
            content = Regex.Replace(content, @"/\*(.*?)\*/", "");
            content = Regex.Replace(content, @"/\*[\w\w]*\*/", "");

            


            FromTo.Add("   ", "  ");
            FromTo.Add("\r\n"," ");
            FromTo.Add("    ", " ");    //Tab to 1 space
            FromTo.Add(" {", "{");
            FromTo.Add(" }", "}");
            FromTo.Add("{ ", "{");
            FromTo.Add("} ", "}");
            FromTo.Add(" (", "(");
            FromTo.Add(" )", ")");
            FromTo.Add("( ", "(");
            FromTo.Add(") ", ")");
            FromTo.Add(" ;", ";");
            FromTo.Add("; ", ";");
            FromTo.Add(" =", "=");
            FromTo.Add("= ", "=");

            foreach (KeyValuePair<string,string> item in FromTo)
            {
                ReplaceText(item.Key, item.Value);
            }

        }

        public void ReplaceText(string from,string to)
        {
            while (content.Contains(from))
            {
                content = content.Replace(from, to);
            }
        }
    }
}
