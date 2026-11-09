using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MassiveWarGetRankRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4805
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

	private uint? __pbn__BattleUniqId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "battle_uniq_id")]
	public uint BattleUniqId
	{
		get
		{
			return __pbn__BattleUniqId.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleUniqId = value;
		}
	}

	[ProtoMember(3, Name = "member_list")]
	public List<MassiveWarBattleMember> MemberLists { get; } = new List<MassiveWarBattleMember>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBattleUniqId()
	{
		return __pbn__BattleUniqId.HasValue;
	}

	public void ResetBattleUniqId()
	{
		__pbn__BattleUniqId = null;
	}
}
