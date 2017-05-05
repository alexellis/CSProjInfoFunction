using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Text;
using System.Xml;
using System.Linq;

namespace Function
{

    class Program
    {
        private static string getStdin()
        {
            StringBuilder buffer = new StringBuilder();
            string s;
            while ((s = Console.ReadLine()) != null)
            {
                //              Console.WriteLine(s);

                buffer.AppendLine(s);
            }
            return buffer.ToString();
        }

        static void Main(string[] args)
        {
            string buffer = getStdin();
            //            Console.WriteLine("Read: " + buffer);

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(buffer);
            XmlNamespaceManager ns = new XmlNamespaceManager(doc.NameTable);
            ns.AddNamespace("msbld", "http://schemas.microsoft.com/developer/msbuild/2003");

            StringBuilder builder = new StringBuilder("");

            var projectTypes = doc.SelectNodes(@"/msbld:Project/msbld:PropertyGroup/msbld:OutputType", ns);
            if (projectTypes.Count > 0)
            {
                foreach(XmlElement element in projectTypes)
                {
                    builder.AppendLine("Detected project type: " + element.InnerText);
                }
                builder.AppendLine();
            }

            var nodes = doc.SelectNodes(@"/msbld:Project/msbld:ItemGroup/msbld:Reference", ns);

            builder.AppendLine("References:");
            foreach (XmlElement node in nodes)
            {
                var included = node.GetAttribute("Include");
                if (included != null)
                {
                    builder.AppendLine("- " + included);
                }
            }
            Console.WriteLine(builder.ToString());
            Environment.Exit(0);
        }
    }
}