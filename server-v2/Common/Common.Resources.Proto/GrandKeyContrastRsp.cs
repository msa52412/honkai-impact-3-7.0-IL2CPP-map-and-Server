using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GrandKeyContrastRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 762
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__UniqueId;

	private uint? __pbn__UniqueIdAfter;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "unique_id")]
	public uint UniqueId
	{
		get
		{
			return __pbn__UniqueId.GetValueOrDefault();
		}
		set
		{
			__pbn__UniqueId = value;
		}
	}

	[ProtoMember(3, Name = "unique_id_after")]
	public uint UniqueIdAfter
	{
		get
		{
			return __pbn__UniqueIdAfter.GetValueOrDefault();
		}
		set
		{
			__pbn__UniqueIdAfter = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeUniqueId()
	{
		return __pbn__UniqueId.HasValue;
	}

	public void ResetUniqueId()
	{
		__pbn__UniqueId = null;
	}

	public bool ShouldSerializeUniqueIdAfter()
	{
		return __pbn__UniqueIdAfter.HasValue;
	}

	public void ResetUniqueIdAfter()
	{
		__pbn__UniqueIdAfter = null;
	}
}
