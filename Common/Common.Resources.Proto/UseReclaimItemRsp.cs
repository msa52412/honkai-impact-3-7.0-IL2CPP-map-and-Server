using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UseReclaimItemRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1182
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "ITEM_ERROR")]
		ItemError,
		[ProtoEnum(Name = "ITEM_LACK")]
		ItemLack
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ExpAdded;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "exp_added")]
	public uint ExpAdded
	{
		get
		{
			return __pbn__ExpAdded.GetValueOrDefault();
		}
		set
		{
			__pbn__ExpAdded = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeExpAdded()
	{
		return __pbn__ExpAdded.HasValue;
	}

	public void ResetExpAdded()
	{
		__pbn__ExpAdded = null;
	}
}
