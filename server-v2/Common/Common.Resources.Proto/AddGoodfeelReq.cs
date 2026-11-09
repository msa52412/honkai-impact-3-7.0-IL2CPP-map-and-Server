using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AddGoodfeelReq : IExtensible
{
	[ProtoContract]
	public enum AddGoodfeelType
	{
		[ProtoEnum(Name = "TOUCH")]
		Touch = 1,
		[ProtoEnum(Name = "STAGE")]
		Stage,
		[ProtoEnum(Name = "DORM")]
		Dorm
	}

	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 154
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__AvatarId;

	private int? __pbn__AddGoodfeel;

	private uint? __pbn__add_goodfeel_type;

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

	[ProtoMember(2, Name = "add_goodfeel")]
	public int AddGoodfeel
	{
		get
		{
			return __pbn__AddGoodfeel.GetValueOrDefault();
		}
		set
		{
			__pbn__AddGoodfeel = value;
		}
	}

	[ProtoMember(3)]
	public uint add_goodfeel_type
	{
		get
		{
			return __pbn__add_goodfeel_type.GetValueOrDefault();
		}
		set
		{
			__pbn__add_goodfeel_type = value;
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

	public bool ShouldSerializeAddGoodfeel()
	{
		return __pbn__AddGoodfeel.HasValue;
	}

	public void ResetAddGoodfeel()
	{
		__pbn__AddGoodfeel = null;
	}

	public bool ShouldSerializeadd_goodfeel_type()
	{
		return __pbn__add_goodfeel_type.HasValue;
	}

	public void Resetadd_goodfeel_type()
	{
		__pbn__add_goodfeel_type = null;
	}
}
