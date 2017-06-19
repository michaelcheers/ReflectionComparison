using Bridge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionComparison.Bridge_
{
    [External]
    public interface Promise<T>
    {
        /// <summary>
        /// Attaches callbacks for the resolution and/or rejection of the Promise.
        /// </summary><param name="onfulfilled">
        /// The callback to execute when the Promise is resolved.
        /// </param><param name="onrejected">
        /// The callback to execute when the Promise is rejected.
        /// </param><returns>
        /// Attaches callbacks for the resolution and/or rejection of the Promise.
        /// </returns>
        Promise<Union<T, Union<object, Delegate>>> then(Union<Promise_then_Param_onfulfilled_ReturnType_UnionRight<T>, Bridge.Union<UndefinedType, NullType>> onfulfilled = default(Union<Promise_then_Param_onfulfilled_ReturnType_UnionRight<T>, Union<UndefinedType, NullType>>), Bridge.Union<Promise_then_Param_onrejected_ReturnType_UnionRight<T>, Bridge.Union<UndefinedType, NullType>> onrejected = default(Bridge.Union<Promise_then_Param_onrejected_ReturnType_UnionRight<T>, Bridge.Union<UndefinedType, NullType>>));
        /// <summary>
        /// Attaches a callback for only the rejection of the Promise.
        /// </summary><param name="onrejected">
        /// The callback to execute when the Promise is rejected.
        /// </param><returns>
        /// Attaches a callback for only the rejection of the Promise.
        /// </returns>
        [Name("catch")]
        Promise<Union<T, Union<object, Delegate>>> @catch(Union<Promise_catch_Param_onrejected_ReturnType_UnionRight<T>, Union<UndefinedType, NullType>> onrejected = default(Union<Promise_catch_Param_onrejected_ReturnType_UnionRight<T>, Union<UndefinedType, NullType>>));
    }
    [External]
    public delegate Bridge.Union<T, PromiseLike<T>> PromiseLike_then_Param_onfulfilled_ReturnType_UnionRight_2<T>(T value);

    [External]
    public delegate Bridge.Union<Bridge.Union<System.Object, System.Delegate>, PromiseLike<Bridge.Union<System.Object, System.Delegate>>> PromiseLike_then_Param_onrejected_ReturnType_UnionRight_2<T>(System.Object reason);

    [External]
    public delegate Bridge.Union<T, PromiseLike<T>> Promise_then_Param_onfulfilled_ReturnType_UnionRight<T>(T value);

    [External]
    public delegate Bridge.Union<Bridge.Union<System.Object, System.Delegate>, PromiseLike<object>> Promise_then_Param_onrejected_ReturnType_UnionRight<T>(System.Object reason);

    [External]
    public delegate Bridge.Union<Bridge.Union<System.Object, System.Delegate>, PromiseLike<object>> Promise_catch_Param_onrejected_ReturnType_UnionRight<T>(System.Object reason);[External]
    [External]
    public partial interface PromiseLike<T>
    {
        /// <summary>
        /// Attaches callbacks for the resolution and/or rejection of the Promise.
        /// </summary><param name="onfulfilled">
        /// The callback to execute when the Promise is resolved.
        /// </param><param name="onrejected">
        /// The callback to execute when the Promise is rejected.
        /// </param><returns>
        /// Attaches callbacks for the resolution and/or rejection of the Promise.
        /// </returns>
        PromiseLike<Bridge.Union<T, Bridge.Union<System.Object, System.Delegate>>> then(Bridge.Union<PromiseLike_then_Param_onfulfilled_ReturnType_UnionRight_2<T>, Bridge.Union<UndefinedType, NullType>> onfulfilled = default(Bridge.Union<PromiseLike_then_Param_onfulfilled_ReturnType_UnionRight_2<T>, Bridge.Union<UndefinedType, NullType>>), Bridge.Union<PromiseLike_then_Param_onrejected_ReturnType_UnionRight_2<T>, Bridge.Union<UndefinedType, NullType>> onrejected = default(Bridge.Union<PromiseLike_then_Param_onrejected_ReturnType_UnionRight_2<T>, Bridge.Union<UndefinedType, NullType>>));
    }
    static class PromiseExtensions
    {
        public static Task<T> ToTask<T> (this Promise<T> promise)
        {
            TaskCompletionSource<T> source = new TaskCompletionSource<T>();
            Promise_then_Param_onfulfilled_ReturnType_UnionRight<T> func = v =>
            {
                source.SetResult(v);
                Union<T, PromiseLike<T>> Undefined = UndefinedType.Undefined.As<Union<T, PromiseLike<T>>>();
                return Undefined;
            };
            promise.ToDynamic().then(Script.Write<object>("func"));
            return source.Task;
        }
    }
}
