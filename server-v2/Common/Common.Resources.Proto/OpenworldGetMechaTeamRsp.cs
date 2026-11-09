using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldGetMechaTeamRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4509
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__CurTeamIndex;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "mecha_team_list")]
	public List<OpenworldMechaTeam> MechaTeamLists { get; } = new List<OpenworldMechaTeam>();


	[ProtoMember(3, Name = "cur_team_index")]
	public uint CurTeamIndex
	{
		get
		{
			return __pbn__CurTeamIndex.GetValueOrDefault();
		}
		set
		{
			__pbn__CurTeamIndex = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeCurTeamIndex()
	{
		return __pbn__CurTeamIndex.HasValue;
	}

	public void ResetCurTeamIndex()
	{
		__pbn__CurTeamIndex = null;
	}
}
