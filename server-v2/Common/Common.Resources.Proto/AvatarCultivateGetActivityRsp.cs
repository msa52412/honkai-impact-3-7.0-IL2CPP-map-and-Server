using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AvatarCultivateGetActivityRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5992
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

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "drop_record_list")]
	public List<AvatarCultivateActivityAreaDropRecord> DropRecordLists { get; } = new List<AvatarCultivateActivityAreaDropRecord>();


	[ProtoMember(3, Name = "reward_line_record_list")]
	public List<AvatarCultivateActivityRewardLineRecord> RewardLineRecordLists { get; } = new List<AvatarCultivateActivityRewardLineRecord>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
