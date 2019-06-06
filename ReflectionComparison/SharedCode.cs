using System;
#if !COMPARE
using System.Collections.Generic;
#if BRIDGE
using Retyped;
using Bridge;
using ReflectionComparison.Bridge_;
using static Retyped.jszip;
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
        SetProperty = GetProperty * 2,
        Delegate = SetProperty * 2
}
#if !COMPARE
static class SharedCode
    {
        static string FullName_(this Type type) => type?.FullName?.Replace(" ", "").Replace(',', '&');
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
                if (type.FullName.StartsWith("<PrivateImplementationDetails>"))
                    continue;
                result.AppendLine();
                result.Append(type.FullName_());
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
                        result.Append(string.Join(", ", Array.ConvertAll(method.GetParameters(), v => $"{v.ParameterType?.FullName_() ?? "Unknown"} {v.Name}")));
                        result.Append(")");
                        attributes |= Attributes.Method;
                        if (method is MethodInfo)
                        {
                            result.Append(" => ");
                            result.Append(((MethodInfo)method).ReturnType?.FullName_() ?? "Unknown");
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
                            result.Append(field.FieldType.FullName_());
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
                            result.Append(prop.PropertyType.FullName_());
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
                            result.Append(event1.AddMethod.GetParameters()[0].ParameterType.FullName_());
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
        static void Main()
        {
            Script.Write(@"/*
* FileSaver.js
* A saveAs() FileSaver implementation.
*
* By Eli Grey, http://eligrey.com
*
* License : https://github.com/eligrey/FileSaver.js/blob/master/LICENSE.md (MIT)
* source  : http://purl.eligrey.com/github/FileSaver.js
*/

// The one and only way of getting global scope in all environments
// https://stackoverflow.com/q/3277182/1008999
var _global = typeof window === 'object' && window.window === window
  ? window : typeof self === 'object' && self.self === self
  ? self : typeof global === 'object' && global.global === global
  ? global
  : this

function bom (blob, opts) {
  if (typeof opts === 'undefined') opts = { autoBom: false }
  else if (typeof opts !== 'object') {
    console.warn('Deprecated: Expected third argument to be a object')
    opts = { autoBom: !opts }
  }

  // prepend BOM for UTF-8 XML and text/* types (including HTML)
  // note: your browser will automatically convert UTF-16 U+FEFF to EF BB BF
  if (opts.autoBom && /^\s*(?:text\/\S*|application\/xml|\S*\/\S*\+xml)\s*;.*charset\s*=\s*utf-8/i.test(blob.type)) {
    return new Blob([String.fromCharCode(0xFEFF), blob], { type: blob.type })
  }
  return blob
}

function download (url, name, opts) {
  var xhr = new XMLHttpRequest()
  xhr.open('GET', url)
  xhr.responseType = 'blob'
  xhr.onload = function () {
    saveAs(xhr.response, name, opts)
  }
  xhr.onerror = function () {
    console.error('could not download file')
  }
  xhr.send()
}

function corsEnabled (url) {
  var xhr = new XMLHttpRequest()
  // use sync to avoid popup blocker
  xhr.open('HEAD', url, false)
  try {
    xhr.send()
  } catch (e) {}
  return xhr.status >= 200 && xhr.status <= 299
}

// `a.click()` doesn't work for all browsers (#465)
function click (node) {
  try {
    node.dispatchEvent(new MouseEvent('click'))
  } catch (e) {
    var evt = document.createEvent('MouseEvents')
    evt.initMouseEvent('click', true, true, window, 0, 0, 0, 80,
                          20, false, false, false, false, 0, null)
    node.dispatchEvent(evt)
  }
}

var saveAs = _global.saveAs || (
  // probably in some web worker
  (typeof window !== 'object' || window !== _global)
    ? function saveAs () { /* noop */ }

  // Use download attribute first if possible (#193 Lumia mobile)
  : 'download' in HTMLAnchorElement.prototype
  ? function saveAs (blob, name, opts) {
    var URL = _global.URL || _global.webkitURL
    var a = document.createElement('a')
    name = name || blob.name || 'download'

    a.download = name
    a.rel = 'noopener' // tabnabbing

    // TODO: detect chrome extensions & packaged apps
    // a.target = '_blank'

    if (typeof blob === 'string') {
      // Support regular links
      a.href = blob
      if (a.origin !== location.origin) {
        corsEnabled(a.href)
          ? download(blob, name, opts)
          : click(a, a.target = '_blank')
      } else {
        click(a)
      }
    } else {
      // Support blobs
      a.href = URL.createObjectURL(blob)
      setTimeout(function () { URL.revokeObjectURL(a.href) }, 4E4) // 40s
      setTimeout(function () { click(a) }, 0)
    }
  }

  // Use msSaveOrOpenBlob as a second approach
  : 'msSaveOrOpenBlob' in navigator
  ? function saveAs (blob, name, opts) {
    name = name || blob.name || 'download'

    if (typeof blob === 'string') {
      if (corsEnabled(blob)) {
        download(blob, name, opts)
      } else {
        var a = document.createElement('a')
        a.href = blob
        a.target = '_blank'
        setTimeout(function () { click(a) })
      }
    } else {
      navigator.msSaveOrOpenBlob(bom(blob, opts), name)
    }
  }

  // Fallback to using FileReader and a popup
  : function saveAs (blob, name, opts, popup) {
    // Open a popup immediately do go around popup blocker
    // Mostly only available on user interaction and the fileReader is async so...
    popup = popup || open('', '_blank')
    if (popup) {
      popup.document.title =
      popup.document.body.innerText = 'downloading...'
    }

    if (typeof blob === 'string') return download(blob, name, opts)

    var force = blob.type === 'application/octet-stream'
    var isSafari = /constructor/i.test(_global.HTMLElement) || _global.safari
    var isChromeIOS = /CriOS\/[\d]+/.test(navigator.userAgent)

    if ((isChromeIOS || (force && isSafari)) && typeof FileReader === 'object') {
      // Safari doesn't allow downloading of blob URLs
      var reader = new FileReader()
      reader.onloadend = function () {
        var url = reader.result
        url = isChromeIOS ? url : url.replace(/^data:[^;]*;/, 'data:attachment/file;')
        if (popup) popup.location.href = url
        else location = url
        popup = null // reverse-tabnabbing #460
      }
      reader.readAsDataURL(blob)
    } else {
      var URL = _global.URL || _global.webkitURL
      var url = URL.createObjectURL(blob)
      if (popup) popup.location = url
      else location.href = url
      popup = null // reverse-tabnabbing #460
      setTimeout(function () { URL.revokeObjectURL(url) }, 4E4) // 40s
    }
  }
)

_global.saveAs = saveAs.saveAs = saveAs

if (typeof module !== 'undefined') {
  module.exports = saveAs;
}");
            Bridge.Html5.HTMLScriptElement script = Bridge.Html5.Document.Head.AppendChild(new Bridge.Html5.HTMLScriptElement()).As<Bridge.Html5.HTMLScriptElement>();
            script.Src = "https://cdnjs.cloudflare.com/ajax/libs/jszip/3.2.0/jszip.min.js";
            script.OnLoad = async e =>
            {
                JSZip zipFile = Script.Write<JSZip>("new JSZip()");
                zipFile.file("result.txt", Go().ToString());
                JSZipGeneratorOptions options = new object().As<JSZipGeneratorOptions>();
                options.type = "blob";
                var content = await zipFile.generateAsync(options).ToTask();
                Script.Write("window.saveAs(content, \"result.zip\")");
            };
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