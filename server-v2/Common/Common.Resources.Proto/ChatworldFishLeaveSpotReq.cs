using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldFishLeaveSpotReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2431
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__FishSpotId;

	[ProtoMember(1, Name = "fish_spot_id")]
	public uint FishSpotId
	{
		get
		{
			return __pbn__FishSpotId.GetValueOrDefault();
		}
		set
		{
			__pbn__FishSpotId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeFishSpotId()
	{
		return __pbn__FishSpotId.HasValue;
	}

	public void ResetFishSpotId()
	{
		__pbn__FishSpotId = null;
	}
}
