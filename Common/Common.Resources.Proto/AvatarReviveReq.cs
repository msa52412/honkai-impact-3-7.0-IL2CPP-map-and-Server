using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AvatarReviveReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 106
	}

	private IExtension __pbn__extensionData;

	private bool? __pbn__IsRetry;

	private uint? __pbn__ReviveUseId;

	[ProtoMember(1, Name = "is_retry")]
	public bool IsRetry
	{
		get
		{
			return __pbn__IsRetry.GetValueOrDefault();
		}
		set
		{
			__pbn__IsRetry = value;
		}
	}

	[ProtoMember(2, Name = "revive_use_id")]
	public uint ReviveUseId
	{
		get
		{
			return __pbn__ReviveUseId.GetValueOrDefault();
		}
		set
		{
			__pbn__ReviveUseId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeIsRetry()
	{
		return __pbn__IsRetry.HasValue;
	}

	public void ResetIsRetry()
	{
		__pbn__IsRetry = null;
	}

	public bool ShouldSerializeReviveUseId()
	{
		return __pbn__ReviveUseId.HasValue;
	}

	public void ResetReviveUseId()
	{
		__pbn__ReviveUseId = null;
	}
}
