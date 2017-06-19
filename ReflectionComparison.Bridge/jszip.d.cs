using Bridge;
using Bridge.Html5;
using ReflectionComparison.Bridge_;
//using number = System.Double;
//using any = Bridge.Union<System.Delegate, object>;
//using boolean = System.Boolean;
#pragma warning disable CS0626
#pragma warning disable CS0824
//[assembly: Convention(Notation.LowerCamelCase)]
[External]
[Name("Bridge.global")]
public static partial class Global
{
	[External]
	public partial interface JSZip_Type
	{
		JSZip prototype { get; set; }
		JSZipSupport support { get; set; }
		/// <summary>
		/// Create JSZip instance
		/// </summary>
		JSZip Invoke ();
		/// <summary>
		/// Create JSZip instance
		/// If no parameters given an empty zip archive will be created
		/// </summary><param name="data">
		/// Serialized zip archive
		/// </param><param name="options">
		/// Description of the serialized zip archive
		/// </param>
		JSZip Invoke (System.Object data, JSZipLoadOptions options = default(JSZipLoadOptions));
		/// <summary>
		/// Create JSZip instance
		/// </summary>
		[Template("new {this}()")]
		JSZip @new ();
		/// <summary>
		/// Create JSZip instance
		/// If no parameters given an empty zip archive will be created
		/// </summary><param name="data">
		/// Serialized zip archive
		/// </param><param name="options">
		/// Description of the serialized zip archive
		/// </param>
		[Template("new {this}({data}, {options})")]
		JSZip @new (System.Object data, JSZipLoadOptions options = default(JSZipLoadOptions));
	}
	
	public extern static JSZip_Type JSZip { get; set; }
}

[External]
public partial interface JSZip
{
	JSZip_files_Type files { get; set; }
	/// <summary>
	/// Get a file from the archive
	/// </summary><param name="Path">
	/// relative path to file
	/// </param><returns>
	/// Get a file from the archive
	/// </returns>
	JSZipObject file (System.String path);
	/// <summary>
	/// Get files matching a RegExp from archive
	/// </summary><param name="path">
	/// RegExp to match
	/// </param><returns>
	/// Get files matching a RegExp from archive
	/// </returns>
	JSZipObject[] file (RegExp path);
	/// <summary>
	/// Add a file to the archive
	/// </summary><param name="path">
	/// Relative path to file
	/// </param><param name="content">
	/// Content of the file
	/// </param><param name="options">
	/// Optional information about the file
	/// </param><returns>
	/// Add a file to the archive
	/// </returns>
	JSZip file (System.String path, System.Object data, JSZipFileOptions options = default(JSZipFileOptions));
	/// <summary>
	/// Return an new JSZip instance with the given folder as root
	/// </summary><param name="name">
	/// Name of the folder
	/// </param><returns>
	/// Return an new JSZip instance with the given folder as root
	/// </returns>
	JSZip folder (System.String name);
	/// <summary>
	/// Returns new JSZip instances with the matching folders as root
	/// </summary><param name="name">
	/// RegExp to match
	/// </param><returns>
	/// Returns new JSZip instances with the matching folders as root
	/// </returns>
	JSZipObject[] folder (RegExp name);
	/// <summary>
	/// Call a callback function for each entry at this folder level.
	/// </summary><param name="callback">
	/// function
	/// </param>
	void forEach (JSZip_forEach_Param_callback_ReturnType callback);
	/// <summary>
	/// Get all files wchich match the given filter function
	/// </summary><param name="predicate">
	/// Filter function
	/// </param><returns>
	/// Get all files wchich match the given filter function
	/// </returns>
	JSZipObject[] filter (JSZip_filter_Param_predicate_ReturnType predicate);
	/// <summary>
	/// Removes the file or folder from the archive
	/// </summary><param name="path">
	/// Relative path of file or folder
	/// </param><returns>
	/// Removes the file or folder from the archive
	/// </returns>
	JSZip remove (System.String path);
	/// <summary>
	/// 
	/// </summary>
	System.Object generate (JSZipGeneratorOptions options = default(JSZipGeneratorOptions));
	/// <summary>
	/// Generates a new archive asynchronously
	/// </summary><param name="options">
	/// Optional options for the generator
	/// </param><returns>
	/// Generates a new archive asynchronously
	/// </returns>
	Promise<System.Object> generateAsync (JSZipGeneratorOptions options = default(JSZipGeneratorOptions), Function onUpdate = default(Function));
	/// <summary>
	/// 
	/// </summary>
	void load ();
	/// <summary>
	/// Deserialize zip file asynchronously
	/// </summary><param name="data">
	/// Serialized zip file
	/// </param><param name="options">
	/// Options for deserializing
	/// </param><returns>
	/// Deserialize zip file asynchronously
	/// </returns>
	Promise<JSZip> loadAsync (System.Object data, JSZipLoadOptions options = default(JSZipLoadOptions));
}

[External, Enum(Emit.StringNamePreserveCase)]
public enum Serialization_string
{
	[Name("string")]
	@string,
}

[External, Enum(Emit.StringNamePreserveCase)]
public enum Serialization_UnionRight_text
{
	text,
}

[External, Enum(Emit.StringNamePreserveCase)]
public enum Serialization_UnionRight_UnionRight_base64
{
	base64,
}

[External, Enum(Emit.StringNamePreserveCase)]
public enum Serialization_UnionRight_UnionRight_UnionRight_binarystring
{
	binarystring,
}

[External, Enum(Emit.StringNamePreserveCase)]
public enum Serialization_UnionRight_UnionRight_UnionRight_UnionRight_uint8array
{
	uint8array,
}

[External, Enum(Emit.StringNamePreserveCase)]
public enum Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_arraybuffer
{
	arraybuffer,
}

[External, Enum(Emit.StringNamePreserveCase)]
public enum Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_blob
{
	blob,
}

[External, Enum(Emit.StringNamePreserveCase)]
public enum Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_nodebuffer
{
	nodebuffer,
}

[External]
public partial interface JSZipObject
{
	System.String name { get; set; }
	System.Boolean dir { get; set; }
	Date date { get; set; }
	System.String comment { get; set; }
	JSZipObjectOptions options { get; set; }
	/// <summary>
	/// Prepare the content in the asked type.
	/// </summary><param name="type">
	/// the type of the result.
	/// </param><param name="onUpdate">
	/// a function to call on each internal update.
	/// </param><returns>
	/// Prepare the content in the asked type.
	/// </returns>
	Promise<System.Object> async (___4<Serialization_string, ___3<Serialization_UnionRight_text, ___2<Serialization_UnionRight_UnionRight_base64, ___1<Serialization_UnionRight_UnionRight_UnionRight_binarystring, ___0<Serialization_UnionRight_UnionRight_UnionRight_UnionRight_uint8array, Union_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_arraybuffer_Union_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_blob_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_nodebuffer_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_blob__Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_nodebuffer_<Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_arraybuffer, Union_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_blob_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_nodebuffer<Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_blob, Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_nodebuffer>>>>>>> type, Function onUpdate = default(Function));
	/// <summary>
	/// 
	/// </summary>
	void asText ();
	/// <summary>
	/// 
	/// </summary>
	void asBinary ();
	/// <summary>
	/// 
	/// </summary>
	void asArrayBuffer ();
	/// <summary>
	/// 
	/// </summary>
	void asUint8Array ();
}

[External]
public partial interface JSZipFileOptions
{
	System.Boolean base64 { get; set; }
	System.Boolean binary { get; set; }
	Date date { get; set; }
	System.String compression { get; set; }
	System.String comment { get; set; }
	System.Boolean optimizedBinaryString { get; set; }
	System.Boolean createFolders { get; set; }
	System.Boolean dir { get; set; }
}

[External]
public partial interface JSZipObjectOptions
{
	/// <summary>
	/// deprecated
	/// </summary>
	System.Boolean base64 { get; set; }
	/// <summary>
	/// deprecated
	/// </summary>
	System.Boolean binary { get; set; }
	/// <summary>
	/// deprecated
	/// </summary>
	System.Boolean dir { get; set; }
	/// <summary>
	/// deprecated
	/// </summary>
	Date date { get; set; }
	System.String compression { get; set; }
}

[External]
public partial interface JSZipGeneratorOptions
{
	/// <summary>
	/// deprecated
	/// </summary>
	System.Boolean base64 { get; set; }
	/// <summary>
	/// DEFLATE or STORE
	/// </summary>
	System.String compression { get; set; }
	/// <summary>
	/// base64 (default), string, uint8array, arraybuffer, blob
	/// </summary>
	System.String type { get; set; }
	System.String comment { get; set; }
	/// <summary>
	/// mime-type for the generated file.
	/// Useful when you need to generate a file with a different extension, ie: “.ods”.
	/// </summary>
	System.String mimeType { get; set; }
	/// <summary>
	/// streaming uses less memory
	/// </summary>
	System.Boolean streamFiles { get; set; }
	/// <summary>
	/// DOS (default) or UNIX
	/// </summary>
	System.String platform { get; set; }
}

[External]
public partial interface JSZipLoadOptions
{
	System.Boolean base64 { get; set; }
	System.Boolean checkCRC32 { get; set; }
	System.Boolean optimizedBinaryString { get; set; }
	System.Boolean createFolders { get; set; }
}

[External]
public partial interface JSZipSupport
{
	System.Boolean arraybuffer { get; set; }
	System.Boolean uint8array { get; set; }
	System.Boolean blob { get; set; }
	System.Boolean nodebuffer { get; set; }
}

[External]
public partial class NullType
{
	extern NullType();
}

[External]
public partial class UndefinedType
{
	extern UndefinedType();
	public extern static UndefinedType Undefined
	{
		[Template("undefined")]
		get;
	}
}

[External]
public partial class VoidType : UndefinedType
{
	extern VoidType();
}

[External]
public partial class Symbol
{
	public extern Symbol (System.String value);
	public extern Symbol ();
}

[External]
public partial class Union_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_blob_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_nodebuffer<A, B> : Bridge.Union<Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_blob, Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_nodebuffer>
{
	extern Union_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_blob_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_nodebuffer();
	public static extern implicit operator Union_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_blob_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_nodebuffer<A, B>(A t);
	public static extern implicit operator Union_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_blob_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_nodebuffer<A, B>(B t);
	
}

[External]
[Name("Object")]
public partial class Union_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_arraybuffer_Union_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_blob_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_nodebuffer_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_blob__Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_nodebuffer_<A, B> : Bridge.Union<Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_arraybuffer, Union_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_blob_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_nodebuffer<Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_blob, Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_nodebuffer>>
{
	extern Union_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_arraybuffer_Union_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_blob_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_nodebuffer_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_blob__Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_nodebuffer_();
	public static extern implicit operator Union_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_arraybuffer_Union_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_blob_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_nodebuffer_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_blob__Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_nodebuffer_<A, B>(A t);
	public static extern implicit operator Union_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_arraybuffer_Union_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_blob_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_nodebuffer_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_blob__Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_nodebuffer_<A, B>(B t);
	
}

[External]
[Name("Object")]
public partial class ___0<A, B> : Bridge.Union<Serialization_UnionRight_UnionRight_UnionRight_UnionRight_uint8array, Union_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_arraybuffer_Union_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_blob_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_nodebuffer_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_blob__Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_nodebuffer_<Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_arraybuffer, Union_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_blob_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_nodebuffer<Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_blob, Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_nodebuffer>>>
{
	extern ___0();
	public static extern implicit operator ___0<A, B>(A t);
	public static extern implicit operator ___0<A, B>(B t);
	
}

[External]
[Name("Object")]
public partial class ___1<A, B> : Bridge.Union<Serialization_UnionRight_UnionRight_UnionRight_binarystring, ___0<Serialization_UnionRight_UnionRight_UnionRight_UnionRight_uint8array, Union_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_arraybuffer_Union_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_blob_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_nodebuffer_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_blob__Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_nodebuffer_<Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_arraybuffer, Union_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_blob_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_nodebuffer<Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_blob, Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_nodebuffer>>>>
{
	extern ___1();
	public static extern implicit operator ___1<A, B>(A t);
	public static extern implicit operator ___1<A, B>(B t);
	
}

[External]
[Name("Object")]
public partial class ___2<A, B> : Bridge.Union<Serialization_UnionRight_UnionRight_base64, ___1<Serialization_UnionRight_UnionRight_UnionRight_binarystring, ___0<Serialization_UnionRight_UnionRight_UnionRight_UnionRight_uint8array, Union_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_arraybuffer_Union_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_blob_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_nodebuffer_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_blob__Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_nodebuffer_<Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_arraybuffer, Union_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_blob_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_nodebuffer<Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_blob, Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_nodebuffer>>>>>
{
	extern ___2();
	public static extern implicit operator ___2<A, B>(A t);
	public static extern implicit operator ___2<A, B>(B t);
	
}

[External]
[Name("Object")]
public partial class ___3<A, B> : Bridge.Union<Serialization_UnionRight_text, ___2<Serialization_UnionRight_UnionRight_base64, ___1<Serialization_UnionRight_UnionRight_UnionRight_binarystring, ___0<Serialization_UnionRight_UnionRight_UnionRight_UnionRight_uint8array, Union_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_arraybuffer_Union_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_blob_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_nodebuffer_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_blob__Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_nodebuffer_<Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_arraybuffer, Union_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_blob_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_nodebuffer<Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_blob, Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_nodebuffer>>>>>>
{
	extern ___3();
	public static extern implicit operator ___3<A, B>(A t);
	public static extern implicit operator ___3<A, B>(B t);
	
}

[External]
[Name("Object")]
public partial class ___4<A, B> : Bridge.Union<Serialization_string, ___3<Serialization_UnionRight_text, ___2<Serialization_UnionRight_UnionRight_base64, ___1<Serialization_UnionRight_UnionRight_UnionRight_binarystring, ___0<Serialization_UnionRight_UnionRight_UnionRight_UnionRight_uint8array, Union_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_arraybuffer_Union_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_blob_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_nodebuffer_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_blob__Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_nodebuffer_<Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_arraybuffer, Union_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_blob_Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_nodebuffer<Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_blob, Serialization_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_UnionRight_nodebuffer>>>>>>>
{
	extern ___4();
	public static extern implicit operator ___4<A, B>(A t);
	public static extern implicit operator ___4<A, B>(B t);
	
}

[External]
public partial interface JSZip_files_Type
{
	JSZipObject this [System.String key]
	{
		get;
		set;
	}
}

[External]
public delegate void JSZip_forEach_Param_callback_ReturnType (System.String relativePath, JSZipObject file);

[External]
public delegate System.Boolean JSZip_filter_Param_predicate_ReturnType (System.String relativePath, JSZipObject file);

