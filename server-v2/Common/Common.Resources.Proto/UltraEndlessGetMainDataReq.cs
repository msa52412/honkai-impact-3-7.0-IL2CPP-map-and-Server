using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UltraEndlessGetMainDataReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5202
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__PlayerLevel;

	[ProtoMember(1, Name = "player_level")]
	public uint PlayerLevel
	{
		get
		{
			return __pbn__PlayerLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__PlayerLevel = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializePlayerLevel()
	{
		return __pbn__PlayerLevel.HasValue;
	}

	public void ResetPlayerLevel()
	{
		__pbn__PlayerLevel = null;
	}
}
