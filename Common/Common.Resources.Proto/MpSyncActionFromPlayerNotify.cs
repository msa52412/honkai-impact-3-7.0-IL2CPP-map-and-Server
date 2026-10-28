using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MpSyncActionFromPlayerNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1579
	}

	private IExtension __pbn__extensionData;

	private MpAction? __pbn__Action;

	[ProtoMember(1, Name = "action")]
	[DefaultValue(MpAction.MpActionError)]
	public MpAction Action
	{
		get
		{
			return __pbn__Action.GetValueOrDefault();
		}
		set
		{
			__pbn__Action = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAction()
	{
		return __pbn__Action.HasValue;
	}

	public void ResetAction()
	{
		__pbn__Action = null;
	}
}
