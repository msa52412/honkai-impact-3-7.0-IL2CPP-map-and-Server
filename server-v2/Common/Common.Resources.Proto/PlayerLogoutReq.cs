using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class PlayerLogoutReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 8
	}

	private IExtension __pbn__extensionData;

	private bool? __pbn__IsKick;

	[ProtoMember(1, Name = "is_kick")]
	public bool IsKick
	{
		get
		{
			return __pbn__IsKick.GetValueOrDefault();
		}
		set
		{
			__pbn__IsKick = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeIsKick()
	{
		return __pbn__IsKick.HasValue;
	}

	public void ResetIsKick()
	{
		__pbn__IsKick = null;
	}
}
