using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class DelAvatarEquipSuiteReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1620
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__AvatarId;

	private uint? __pbn__SuiteId;

	[ProtoMember(1, Name = "avatar_id")]
	public uint AvatarId
	{
		get
		{
			return __pbn__AvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarId = value;
		}
	}

	[ProtoMember(2, Name = "suite_id")]
	public uint SuiteId
	{
		get
		{
			return __pbn__SuiteId.GetValueOrDefault();
		}
		set
		{
			__pbn__SuiteId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAvatarId()
	{
		return __pbn__AvatarId.HasValue;
	}

	public void ResetAvatarId()
	{
		__pbn__AvatarId = null;
	}

	public bool ShouldSerializeSuiteId()
	{
		return __pbn__SuiteId.HasValue;
	}

	public void ResetSuiteId()
	{
		__pbn__SuiteId = null;
	}
}
