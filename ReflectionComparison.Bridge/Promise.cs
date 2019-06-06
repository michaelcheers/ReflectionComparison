using Bridge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionComparison.Bridge_
{
    static class PromiseExtensions
    {
        public static Task<T> ToTask<T> (this Retyped.es5.Promise<T> promise)
        {
            TaskCompletionSource<T> source = new TaskCompletionSource<T>();
            Func<T, object> func = v =>
            {
                source.SetResult(v);
                return Script.Write<object>("undefined");
            };
            promise.ToDynamic().then(func);
            return source.Task;
        }
    }
}
