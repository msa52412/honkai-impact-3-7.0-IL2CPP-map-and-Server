using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MatrixAdvancePath : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "path_from")]
	public MatrixGridPos PathFrom { get; set; }

	[ProtoMember(2, Name = "path_to")]
	public MatrixGridPos PathTo { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
