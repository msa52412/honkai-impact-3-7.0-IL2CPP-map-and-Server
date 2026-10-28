using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class PictureActivityChooseRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4269
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
		[ProtoEnum(Name = "HAS_CHOOSE")]
		HasChoose,
		[ProtoEnum(Name = "SCORE_LACK")]
		ScoreLack,
		[ProtoEnum(Name = "PRE_NOT_CHOOSE")]
		PreNotChoose
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "reward")]
	public RewardData Reward { get; set; }

	[ProtoMember(3, Name = "step")]
	public PictureStep Step { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
