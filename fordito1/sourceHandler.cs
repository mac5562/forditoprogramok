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
        Dictionary<string, string> FromTo = new Dictionary<string, string>();
        List<string> SymbolTable = new List<string>();
        int SymbolIndex = 0;
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
            string BlockComment = @"/[*][\w\d\s]+[*]/";
            string LineComment= @"//.*?\n";
            content = Regex.Replace(content,BlockComment, " ");
            content = Regex.Replace(content, LineComment, " ");
            




            FromTo.Add("  ", " ");
            FromTo.Add("   ", " ");
            FromTo.Add("\n"," ");
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

        
        

        public void ReplaceCharchter()
        {

            FromTo.Add("IF", " 100 ");
            FromTo.Add("FOR", " 102 ");
            FromTo.Add("WHILE", " 103 ");
            FromTo.Add("SWITCH", " 104 ");
            FromTo.Add("CASE", " 105 ");
            FromTo.Add("ELSE", " 106 ");
            FromTo.Add("("," 200 ");
            FromTo.Add(")"," 201 ");
            FromTo.Add("{"," 202 ");
            FromTo.Add("}"," 203 ");
            FromTo.Add("["," 204 ");
            FromTo.Add("]"," 205 ");
            FromTo.Add("(\""," 206 ");
            FromTo.Add("\'"," 207 ");
            FromTo.Add("\\"," 208 ");
            FromTo.Add("/"," 209 ");
            FromTo.Add("|"," 210 ");
            FromTo.Add("&"," 211 ");
            FromTo.Add("!="," 212 ");
            FromTo.Add("!"," 213 ");
            FromTo.Add("=="," 214 ");
            FromTo.Add("="," 215 ");
            FromTo.Add(">"," 216 ");
            FromTo.Add("<"," 217 ");
            FromTo.Add("<="," 218 ");
            FromTo.Add(">="," 219 ");
            FromTo.Add("."," 220 ");
            FromTo.Add(";"," 221 ");
            FromTo.Add(":"," 222 ");
            FromTo.Add("%"," 223 ");
            FromTo.Add("++"," 224 ");
            FromTo.Add("--"," 225 ");
            FromTo.Add("-"," 226 ");
            FromTo.Add("@"," 227 ");
            FromTo.Add("*"," 228 ");

            foreach (KeyValuePair<string, string> item in FromTo)
            {
                ReplaceText(item.Key, item.Value);
            }
        }

        public void ReplaceContent()
        {
            string fromNum = @"([0-9]+)";
            string fromVar = @"([a-z-_]+)";

            content = Regex.Replace(content, fromNum, changeVariableAndConstants("$1"));
            content = Regex.Replace(content, fromVar, changeVariableAndConstants("$1"));

        }

        private string changeVariableAndConstants(string v)
        {
            SymbolTable.Add(v);
            SymbolIndex += 1;
            string result = "00" + SymbolIndex.ToString();
            return result.Substring(result.Length - 3);
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
