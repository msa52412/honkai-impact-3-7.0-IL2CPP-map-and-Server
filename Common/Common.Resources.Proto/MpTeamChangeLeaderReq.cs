using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MpTeamChangeLeaderReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 362
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__NewLeaderUid;

	[ProtoMember(1, Name = "new_leader_uid")]
	public uint NewLeaderUid
	{
		get
		{
			return __pbn__NewLeaderUid.GetValueOrDefault();
		}
		set
		{
			__pbn__NewLeaderUid = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeNewLeaderUid()
	{
		return __pbn__NewLeaderUid.HasValue;
	}

	public void ResetNewLeaderUid()
	{
		__pbn__NewLeaderUid = null;
	}
}
