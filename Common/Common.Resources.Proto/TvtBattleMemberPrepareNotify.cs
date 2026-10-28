using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TvtBattleMemberPrepareNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3351
	}

	private IExtension __pbn__extensionData;

	private bool? __pbn__IsCancelPrepare;

	[ProtoMember(1, Name = "is_cancel_prepare")]
	public bool IsCancelPrepare
	{
		get
		{
			return __pbn__IsCancelPrepare.GetValueOrDefault();
		}
		set
		{
			__pbn__IsCancelPrepare = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeIsCancelPrepare()
	{
		return __pbn__IsCancelPrepare.HasValue;
	}

	public void ResetIsCancelPrepare()
	{
		__pbn__IsCancelPrepare = null;
	}
}
