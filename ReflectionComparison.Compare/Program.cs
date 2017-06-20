using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionComparison.Compare
{
    class Program
    {
        static string RQuotes (string value)
        {
            if (value[0] == '"')
                return value.Substring(1, value.Length - 2);
            else
                return value;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Bridge result?");
            string bridgeResult = File.ReadAllText(args.Length == 2 ? args[0] : RQuotes(Console.ReadLine()));
            Console.WriteLine(".NET result?");
            string netResult = File.ReadAllText(args.Length == 2 ? args[1] : RQuotes(Console.ReadLine()));
            CSNamespace bridgeNamespace = CSNamespace.FromText(bridgeResult);
            CSNamespace netNamespace = CSNamespace.FromText(netResult);
            bridgeNamespace.SortAll();
            netNamespace.SortAll();
            using (writer = new StreamWriter("result.html"))
            {
                #region Start
                writer.Write(@"<!DOCTYPE html>
<html>
<head>
  <meta charset=""utf-8"">
  <title>jsTree test</title>
  <!-- 2 load the theme CSS file -->
  <link rel=""stylesheet"" href=""dist/themes/default/style.min.css"" />
</head>
<body><div id=""main"">");
#endregion
                DiffNamespaces(bridgeNamespace, netNamespace);
#region End
                writer.Write(@"</div>
<!-- 4 include the jQuery library -->
  <script src=""dist/libs/jquery.min.js""></script>
  <script src = ""dist/jstree.min.js"" ></script>
<script>
$(function(){
$('#main').jstree();
});
</script>
</body>
</html>");
#endregion
            }
        }

        static StreamWriter writer;
        
        static void DiffNamespaces (CSNamespace bridgeNamespace, CSNamespace netNamespace)
        {
            writer.Write("<ul>");
            HashSet<string> combinedTypeStrings = new HashSet<string>();
            HashSet<string> combinedNamespaceStrings = new HashSet<string>();
            (Dictionary<string, CSType> classes, Dictionary<string, CSNamespace> namespaces) CreateFrom(CSNamespace @namespace)
            {
                Dictionary<string, CSType> classes = new Dictionary<string, CSType>();
                Dictionary<string, CSNamespace> namespaces = new Dictionary<string, CSNamespace>();
                foreach (var @class in @namespace.NestedClasses)
                {
                    combinedTypeStrings.Add(@class.Name);
                    classes.Add(@class.Name, @class);
                }
                foreach (var nestedNamespace in @namespace.NestedNamespaces)
                {
                    namespaces.Add(nestedNamespace.name, nestedNamespace);
                    combinedNamespaceStrings.Add(nestedNamespace.name);
                }
                return (classes, namespaces);
            }
            (Dictionary<string, CSType> bridgeTypes, Dictionary<string, CSNamespace> bridgeNamespaces) = CreateFrom(bridgeNamespace);
            (Dictionary<string, CSType> netTypes, Dictionary<string, CSNamespace> netNamespaces) = CreateFrom(netNamespace);
            foreach (var @string in combinedTypeStrings)
            {
                bool inBridge = bridgeTypes.ContainsKey(@string);
                bool inNet = netTypes.ContainsKey(@string);
                string color;
                if (inBridge && inNet)
                    color = "black";
                else if (inBridge)
                    color = "blue";
                else if (inNet)
                    color = "red";
                else
                    throw new Exception();
                writer.Write($"<li data-jstree='{"{"}\"icon\":\"dist/images/class.png\"{"}"}'><span style=\"color:{color}\">{@string}</span></li>");
                // TODO: Add to tree.
            }
            foreach (var @string in combinedNamespaceStrings)
            {
                bool inBridge = bridgeNamespaces.ContainsKey(@string);
                bool inNet = netNamespaces.ContainsKey(@string);
                string color;
                if (inBridge && inNet)
                    color = "black";
                else if (inBridge)
                    color = "blue";
                else if (inNet)
                    color = "red";
                else
                    throw new Exception();
                writer.Write($"<li data-jstree='{"{"}\"icon\":\"dist/images/namespace.png\"{"}"}'><span style=\"color:{color}\">{@string}</span>");
                if (inBridge && inNet)
                    DiffNamespaces(bridgeNamespaces[@string], netNamespaces[@string]);
                writer.Write("</li>");
                // TODO: Add to tree.
            }
            writer.Write("</ul>");
        }
    }
}
