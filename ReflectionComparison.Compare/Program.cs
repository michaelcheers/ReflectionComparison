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
        }
    }
}
