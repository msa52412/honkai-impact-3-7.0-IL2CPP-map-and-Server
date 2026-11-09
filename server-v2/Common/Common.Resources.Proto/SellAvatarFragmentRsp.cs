using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SellAvatarFragmentRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 103
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "FRAGMENT_NOT_ENOUGH")]
		FragmentNotEnough
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ScoinAdd;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "scoin_add")]
	public uint ScoinAdd
	{
		get
		{
			return __pbn__ScoinAdd.GetValueOrDefault();
		}
		set
		{
			__pbn__ScoinAdd = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeScoinAdd()
	{
		return __pbn__ScoinAdd.HasValue;
	}

	public void ResetScoinAdd()
	{
		__pbn__ScoinAdd = null;
	}
}
