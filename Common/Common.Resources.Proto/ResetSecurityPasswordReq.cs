using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ResetSecurityPasswordReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5031
	}

	private IExtension __pbn__extensionData;

	private bool? __pbn__IsWithdraw;

	[ProtoMember(1, Name = "is_withdraw")]
	public bool IsWithdraw
	{
		get
		{
			return __pbn__IsWithdraw.GetValueOrDefault();
		}
		set
		{
			__pbn__IsWithdraw = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeIsWithdraw()
	{
		return __pbn__IsWithdraw.HasValue;
	}

	public void ResetIsWithdraw()
	{
		__pbn__IsWithdraw = null;
	}
}
