using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class FinishDLCDialogReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3578
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__DialogId;

	[ProtoMember(1, Name = "dialog_id")]
	public uint DialogId
	{
		get
		{
			return __pbn__DialogId.GetValueOrDefault();
		}
		set
		{
			__pbn__DialogId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeDialogId()
	{
		return __pbn__DialogId.HasValue;
	}

	public void ResetDialogId()
	{
		__pbn__DialogId = null;
	}
}
