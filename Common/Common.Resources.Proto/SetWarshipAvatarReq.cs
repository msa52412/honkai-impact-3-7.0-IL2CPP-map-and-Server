using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SetWarshipAvatarReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1492
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__FirstAvatarId;

	private uint? __pbn__SecondAvatarId;

	[ProtoMember(1, Name = "first_avatar_id")]
	public uint FirstAvatarId
	{
		get
		{
			return __pbn__FirstAvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__FirstAvatarId = value;
		}
	}

	[ProtoMember(2, Name = "second_avatar_id")]
	public uint SecondAvatarId
	{
		get
		{
			return __pbn__SecondAvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__SecondAvatarId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeFirstAvatarId()
	{
		return __pbn__FirstAvatarId.HasValue;
	}

	public void ResetFirstAvatarId()
	{
		__pbn__FirstAvatarId = null;
	}

	public bool ShouldSerializeSecondAvatarId()
	{
		return __pbn__SecondAvatarId.HasValue;
	}

	public void ResetSecondAvatarId()
	{
		__pbn__SecondAvatarId = null;
	}
}
