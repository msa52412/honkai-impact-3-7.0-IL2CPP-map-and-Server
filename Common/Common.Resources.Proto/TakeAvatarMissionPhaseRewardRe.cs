using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeAvatarMissionPhaseRewardReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3006
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Days;

	[ProtoMember(1, Name = "days")]
	public uint Days
	{
		get
		{
			return __pbn__Days.GetValueOrDefault();
		}
		set
		{
			__pbn__Days = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeDays()
	{
		return __pbn__Days.HasValue;
	}

	public void ResetDays()
	{
		__pbn__Days = null;
	}
}
