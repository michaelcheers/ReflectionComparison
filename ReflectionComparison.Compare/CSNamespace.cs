using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionComparison.Compare
{
    public class CSNamespace
    {
        public string name;
        public List<CSNamespace> NestedNamespaces = new List<CSNamespace>();
        public List<CSType> NestedClasses = new List<CSType>();

        public void SortAll ()
        {
            NestedNamespaces = NestedNamespaces.OrderBy(v => v.name).ToList();
            NestedClasses = NestedClasses.OrderBy(v => v.Name).ToList();
            NestedNamespaces.ForEach(v => v.SortAll());
        }

        public static CSNamespace FromText(string text)
        {
            CSNamespace result = new CSNamespace();
            bool newType = true;
            CSMemberedType last = null;
            foreach (var _line in text.Split('\n'))
            {
                var line = _line.Replace("\r", "");
                if (string.IsNullOrEmpty(line))
                {
                    newType = true;
                    continue;
                }
                if (newType)
                {
                    newType = false;
                    int minusIndex = line.IndexOf('-');
                    string typeName = line.Substring(0, minusIndex);
                    Attributes attributes = (Attributes)uint.Parse(line.Substring(minusIndex + 1));
                    string[] dotSplitTypeName = typeName.Split('.');
                    var cNamespace = result;
                    for (int n = 0; n < dotSplitTypeName.Length; n++)
                    {
                        var item = dotSplitTypeName[n];
                        if (n == dotSplitTypeName.Length - 1)
                        {
                            cNamespace.NestedClasses.Add(last = new CSMemberedType
                            {
                                Name = item,
                                Attributes = attributes
                            });
                            break;
                        }
                        var firstOrDefault = cNamespace.NestedNamespaces.FirstOrDefault(v => v.name == item);
                        if (firstOrDefault != null)
                        {
                            cNamespace = firstOrDefault;
                        }
                        else
                        {
                            var oldN = cNamespace;
                            oldN.NestedNamespaces.Add(cNamespace = new CSNamespace
                            {
                                name = item
                            });
                        }
                    }
                }
                else
                {
                    newType = false;
                    int pIndex = line.IndexOf('(');
                    int attIndex = line.IndexOf('-');
                    Attributes attributes = (Attributes)uint.Parse(line.Substring(attIndex + 1));
                    CSTypedMember member;
                    if (attributes.HasFlag(Attributes.Field) || attributes.HasFlag(Attributes.Property))
                        member = new CSField();
                    else if (attributes.HasFlag(Attributes.Method))
                        member = new CSMethod();
                    else
                        throw new NotImplementedException();
                    member.Attributes = attributes;
                    string name;
                    CSParameter[] parameters;
                    if (pIndex != -1)
                    {
                        name = line.Substring(0, pIndex);
                        var cPindex = line.IndexOf(')');
                        string args = line.Substring(pIndex + 1, cPindex - pIndex - 1);
                        string[][] argsSplit = Array.ConvertAll(args.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries), v => v.Split(new[] { ' '}, StringSplitOptions.RemoveEmptyEntries));
                        ((CSMethod)member).Parameters = (parameters = Array.ConvertAll(argsSplit, v => new CSParameter
                        {
                            Name = v[1],
                            Type = v[0]
                        })).ToList();
                    }
                    else
                        name = line.Split(' ')[0];
                    member.Name = name;
                    int tIndex = line.IndexOf("=>");
                    if (tIndex != -1)
                        member.Type = line.Substring(tIndex + 3);
                    last.Members.Add(member);
                }
            }
            return result;
        }
    }
}
