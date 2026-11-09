using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RogueTowerLevelUpBuffReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4370
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Id;

	private uint? __pbn__BuffId;

	[ProtoMember(1, Name = "id")]
	public uint Id
	{
		get
		{
			return __pbn__Id.GetValueOrDefault();
		}
		set
		{
			__pbn__Id = value;
		}
	}

	[ProtoMember(2, Name = "buff_id")]
	public uint BuffId
	{
		get
		{
			return __pbn__BuffId.GetValueOrDefault();
		}
		set
		{
			__pbn__BuffId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeId()
	{
		return __pbn__Id.HasValue;
	}

	public void ResetId()
	{
		__pbn__Id = null;
	}

	public bool ShouldSerializeBuffId()
	{
		return __pbn__BuffId.HasValue;
	}

	public void ResetBuffId()
	{
		__pbn__BuffId = null;
	}
}
