using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ActiveRaidRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1625
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen,
		[ProtoEnum(Name = "ALREADY_ACTIVED")]
		AlreadyActived,
		[ProtoEnum(Name = "TYPE_ERROR")]
		TypeError
	}

	private IExtension __pbn__extensionData;

	private RaidActiveType? __pbn__ActiveType;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "active_type")]
	[DefaultValue(RaidActiveType.RaidActiveNone)]
	public RaidActiveType ActiveType
	{
		get
		{
			return __pbn__ActiveType.GetValueOrDefault();
		}
		set
		{
			__pbn__ActiveType = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeActiveType()
	{
		return __pbn__ActiveType.HasValue;
	}

	public void ResetActiveType()
	{
		__pbn__ActiveType = null;
	}
}
