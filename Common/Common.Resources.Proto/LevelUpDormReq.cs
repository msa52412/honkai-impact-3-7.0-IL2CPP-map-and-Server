using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class LevelUpDormReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 613
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__HouseId;

	private uint? __pbn__TargetLevel;

	[ProtoMember(1, Name = "house_id")]
	public uint HouseId
	{
		get
		{
			return __pbn__HouseId.GetValueOrDefault();
		}
		set
		{
			__pbn__HouseId = value;
		}
	}

	[ProtoMember(2, Name = "target_level")]
	public uint TargetLevel
	{
		get
		{
			return __pbn__TargetLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__TargetLevel = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeHouseId()
	{
		return __pbn__HouseId.HasValue;
	}

	public void ResetHouseId()
	{
		__pbn__HouseId = null;
	}

	public bool ShouldSerializeTargetLevel()
	{
		return __pbn__TargetLevel.HasValue;
	}

	public void ResetTargetLevel()
	{
		__pbn__TargetLevel = null;
	}
}
