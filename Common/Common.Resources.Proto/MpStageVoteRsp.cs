using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MpStageVoteRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1031
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_IN_TEAM")]
		NotInTeam,
		[ProtoEnum(Name = "NOT_VOTING")]
		NotVoting
	}

	private IExtension __pbn__extensionData;

	private MpStageVoteOption? __pbn__Option;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "option")]
	[DefaultValue(MpStageVoteOption.MpStageVoteOptionStayInTeam)]
	public MpStageVoteOption Option
	{
		get
		{
			return __pbn__Option.GetValueOrDefault();
		}
		set
		{
			__pbn__Option = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeOption()
	{
		return __pbn__Option.HasValue;
	}

	public void ResetOption()
	{
		__pbn__Option = null;
	}
}
