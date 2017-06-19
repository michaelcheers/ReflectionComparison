using System;
#if !COMPARE
using System.Collections.Generic;
#if BRIDGE
using Bridge;
using ReflectionComparison.Bridge_;
#else
using System.IO;
#endif
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionComparison
{
#endif
[Flags]
    public enum Attributes : uint
    {
        Class = 1,
        Struct = Class * 2,
        Sealed = Struct * 2,
        Abstract = Sealed * 2,
        Virtual = Abstract * 2,
        Const = Virtual * 2,
        Enum = Const * 2,
        Event = Enum * 2,
        Interface = Event * 2,
        Static = Interface * 2,
        Field = Static * 2,
        Method = Field * 2,
        Constructor = Method * 2,
        Property = Constructor * 2,
        GetProperty = Property * 2,
        SetProperty = GetProperty * 2
}
#if !COMPARE
class SharedCode
    {
        static readonly Type[] distypes =
                {
                    typeof(System.Threading.Tasks.Task<>),
                    typeof(System.Net.WebSockets.ClientWebSocket),
                    typeof(System.Net.WebSockets.WebSocketReceiveResult)
                };
        static StringBuilder Go()
        {
            var assemblies =
                new List<Assembly>
                {
                    typeof(string).Assembly
                };
            var types = new List<Type>();
            foreach (var assembly in assemblies)
                types.AddRange(assembly.GetTypes());
            var typesLength = types.Count;
            StringBuilder result = new StringBuilder();
            int lastPercent = 0;
            for (int n = 1; n <= typesLength; n++)
            {
                Attributes attributes = 0;
                var type = types[n - 1];
                if (distypes.Contains(type) || type.IsNotPublic)
                    continue;
                result.AppendLine();
                result.Append(type.FullName);
                if (type.IsClass)
                    attributes |= Attributes.Class;
                else if (type.IsInterface)
                    attributes |= Attributes.Interface;
                else if (type.IsEnum)
                    attributes |= Attributes.Enum;
                else
                    attributes |= Attributes.Struct;
                result.Append("-");
                result.Append((uint)attributes);
                result.AppendLine();
                foreach (var member in type.GetMembers())
                {
                    if (member.MemberType.HasFlag(MemberTypes.NestedType))
                        continue;
                    attributes = 0;
                    if (!((member as FieldInfo)?.IsPublic ?? true))
                        continue;
                    var @event = member as EventInfo;
                    if (@event != null)
                        if (!(@event.AddMethod?.IsPublic ?? true)
#if !BRIDGE
                                && !(@event.RaiseMethod?.IsPublic ?? true)
#endif
                                && !(@event.RemoveMethod?.IsPublic ?? true))
                            continue;
                    if (!((member as MethodBase)?.IsPublic ?? true))
                        continue;
                    var property = member as PropertyInfo;
                    if (property != null)
                        if (!((property.GetMethod?.IsPublic ?? true) || (property.SetMethod?.IsPublic ?? true)))
                            continue;
                    result.Append(member.Name);
                    if (member.MemberType.HasFlag(MemberTypes.Constructor) || member.MemberType.HasFlag(MemberTypes.Method))
                    {
                        var method = (MethodBase)member;
                        result.Append("(");
                        result.Append(string.Join(", ", Array.ConvertAll(method.GetParameters(), v => $"{v.ParameterType?.FullName ?? "Unknown"} {v.Name}")));
                        result.Append(")");
                        attributes |= Attributes.Method;
                        if (method is MethodInfo)
                        {
                            result.Append(" => ");
                            result.Append(((MethodInfo)method).ReturnType?.FullName ?? "Unknown");
                            if (method.IsVirtual)
                                attributes |= Attributes.Virtual;
                            if (method.IsAbstract)
                                attributes |= Attributes.Abstract;
                        }
                        else if (method is ConstructorInfo)
                            attributes |= Attributes.Constructor;
                        if (method.IsStatic)
                            attributes |= Attributes.Static;
                    }
                    else if (member.MemberType.HasFlag(MemberTypes.Field) || member.MemberType.HasFlag(MemberTypes.Event) || member.MemberType.HasFlag(MemberTypes.Property))
                    {
                        result.Append(" => ");
                        if (member is FieldInfo)
                        {
                            var field = (FieldInfo)member;
                            attributes |= Attributes.Field;
                            if (field.IsStatic)
                                attributes |= Attributes.Static;
                            result.Append(field.FieldType.FullName);
                        }
                        else if (member is PropertyInfo)
                        {
                            attributes |= Attributes.Property;
                            var prop = ((PropertyInfo)member);
                            if ((prop.GetMethod?.IsVirtual ?? false) || (prop.SetMethod?.IsVirtual ?? false))
                                attributes |= Attributes.Virtual;
                            if ((prop.GetMethod?.IsStatic ?? false) || (prop.SetMethod?.IsStatic ?? false))
                                attributes |= Attributes.Static;
                            if ((prop.GetMethod?.IsAbstract ?? false) || (prop.SetMethod?.IsAbstract ?? false))
                                attributes |= Attributes.Abstract;
                            result.Append(prop.PropertyType.FullName);
                            if (prop.GetMethod?.IsPublic ?? false)
                                attributes |= Attributes.GetProperty;
                            if (prop.SetMethod?.IsPublic ?? false)
                                attributes |= Attributes.SetProperty;
                        }
                        else if (member is EventInfo)
                        {
                            var event1 = (EventInfo)member;
                            attributes |= Attributes.Event;
                            attributes |= Attributes.Field;
                            if (event1.AddMethod.IsStatic)
                                attributes |= Attributes.Static;
                            result.Append(event1.AddMethod.GetParameters()[0].ParameterType.FullName);
                        }
                    }
                    result.Append("-");
                    result.Append((uint)attributes);
                    result.AppendLine();
                }
                if (typesLength / n > lastPercent)
                {
                    lastPercent = typesLength / n;
                    Console.WriteLine($"{lastPercent}%");
                }
            }
            return result;
        }
#if BRIDGE
        static async void Main()
        {
            JSZip zipFile = Global.JSZip.@new();
            zipFile.file("result.txt", Go().ToString());
            JSZipGeneratorOptions options = new object().As<JSZipGeneratorOptions>();
            options.type = "blob";
            var content = await zipFile.generateAsync(options).ToTask();
            Script.Write("window.saveAs(content, \"result.zip\")");
        }
#else
        static void Main(string[] args)
        {
            StreamWriter g = new StreamWriter("result.txt");
            g.Write(Go().ToString());
            g.Flush();
            g.Close();
        }
#endif
    }
}
#endif