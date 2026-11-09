using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QCandyBattleAvatarLifeTimesChangeNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6582
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__LifeTimes;

	[ProtoMember(1, Name = "life_times")]
	public uint LifeTimes
	{
		get
		{
			return __pbn__LifeTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__LifeTimes = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeLifeTimes()
	{
		return __pbn__LifeTimes.HasValue;
	}

	public void ResetLifeTimes()
	{
		__pbn__LifeTimes = null;
	}
}
