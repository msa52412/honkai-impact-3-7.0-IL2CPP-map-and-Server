using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class LeaveTeamReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 305
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TeamId;

	private bool? __pbn__IsLogout;

	private bool? __pbn__IsSqueeze;

	[ProtoMember(1, Name = "team_id")]
	public uint TeamId
	{
		get
		{
			return __pbn__TeamId.GetValueOrDefault();
		}
		set
		{
			__pbn__TeamId = value;
		}
	}

	[ProtoMember(2, Name = "is_logout")]
	public bool IsLogout
	{
		get
		{
			return __pbn__IsLogout.GetValueOrDefault();
		}
		set
		{
			__pbn__IsLogout = value;
		}
	}

	[ProtoMember(3, Name = "is_squeeze")]
	public bool IsSqueeze
	{
		get
		{
			return __pbn__IsSqueeze.GetValueOrDefault();
		}
		set
		{
			__pbn__IsSqueeze = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTeamId()
	{
		return __pbn__TeamId.HasValue;
	}

	public void ResetTeamId()
	{
		__pbn__TeamId = null;
	}

	public bool ShouldSerializeIsLogout()
	{
		return __pbn__IsLogout.HasValue;
	}

	public void ResetIsLogout()
	{
		__pbn__IsLogout = null;
	}

	public bool ShouldSerializeIsSqueeze()
	{
		return __pbn__IsSqueeze.HasValue;
	}

	public void ResetIsSqueeze()
	{
		__pbn__IsSqueeze = null;
	}
}
