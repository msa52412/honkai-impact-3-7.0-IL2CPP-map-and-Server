using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ExchangeAvatarWeaponReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 135
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__AvatarId1;

	private uint? __pbn__AvatarId2;

	[ProtoMember(1, Name = "avatar_id_1")]
	public uint AvatarId1
	{
		get
		{
			return __pbn__AvatarId1.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarId1 = value;
		}
	}

	[ProtoMember(2, Name = "avatar_id_2")]
	public uint AvatarId2
	{
		get
		{
			return __pbn__AvatarId2.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarId2 = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAvatarId1()
	{
		return __pbn__AvatarId1.HasValue;
	}

	public void ResetAvatarId1()
	{
		__pbn__AvatarId1 = null;
	}

	public bool ShouldSerializeAvatarId2()
	{
		return __pbn__AvatarId2.HasValue;
	}

	public void ResetAvatarId2()
	{
		__pbn__AvatarId2 = null;
	}
}
