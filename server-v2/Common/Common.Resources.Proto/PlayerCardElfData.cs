using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class PlayerCardElfData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Index;

	[ProtoMember(1, Name = "index")]
	public uint Index
	{
		get
		{
			return __pbn__Index.GetValueOrDefault();
		}
		set
		{
			__pbn__Index = value;
		}
	}

	[ProtoMember(2, Name = "elf")]
	public ElfDetailData Elf { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeIndex()
	{
		return __pbn__Index.HasValue;
	}

	public void ResetIndex()
	{
		__pbn__Index = null;
	}
}
