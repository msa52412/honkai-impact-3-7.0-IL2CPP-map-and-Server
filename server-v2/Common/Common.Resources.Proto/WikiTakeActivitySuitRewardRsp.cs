using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class WikiTakeActivitySuitRewardRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1593
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "HAS_TAKE")]
		HasTake,
		[ProtoEnum(Name = "NOT_GET")]
		NotGet
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__SuitId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "suit_id")]
	public uint SuitId
	{
		get
		{
			return __pbn__SuitId.GetValueOrDefault();
		}
		set
		{
			__pbn__SuitId = value;
		}
	}

	[ProtoMember(3, Name = "reward")]
	public RewardData Reward { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSuitId()
	{
		return __pbn__SuitId.HasValue;
	}

	public void ResetSuitId()
	{
		__pbn__SuitId = null;
	}
}
