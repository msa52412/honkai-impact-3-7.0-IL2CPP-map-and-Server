using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldPrayRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2454
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_IN_CHATWORLD")]
		NotInChatworld,
		[ProtoEnum(Name = "PRAYED")]
		Prayed,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__PrayId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "pray_id")]
	public uint PrayId
	{
		get
		{
			return __pbn__PrayId.GetValueOrDefault();
		}
		set
		{
			__pbn__PrayId = value;
		}
	}

	[ProtoMember(3, Name = "reward_data")]
	public RewardData RewardData { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializePrayId()
	{
		return __pbn__PrayId.HasValue;
	}

	public void ResetPrayId()
	{
		__pbn__PrayId = null;
	}
}
