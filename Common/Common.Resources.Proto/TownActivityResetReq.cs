using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TownActivityResetReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6476
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TownId;

	[ProtoMember(1, Name = "town_id")]
	public uint TownId
	{
		get
		{
			return __pbn__TownId.GetValueOrDefault();
		}
		set
		{
			__pbn__TownId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTownId()
	{
		return __pbn__TownId.HasValue;
	}

	public void ResetTownId()
	{
		__pbn__TownId = null;
	}
}
