using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class JiguangClientTag : IExtensible
{
	private IExtension __pbn__extensionData;

	private JiguangTagType? __pbn__TagType;

	[ProtoMember(1, Name = "tag_type")]
	[DefaultValue(JiguangTagType.TagNone)]
	public JiguangTagType TagType
	{
		get
		{
			return __pbn__TagType.GetValueOrDefault();
		}
		set
		{
			__pbn__TagType = value;
		}
	}

	[ProtoMember(2, Name = "para_list")]
	public uint[] ParaLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTagType()
	{
		return __pbn__TagType.HasValue;
	}

	public void ResetTagType()
	{
		__pbn__TagType = null;
	}
}
