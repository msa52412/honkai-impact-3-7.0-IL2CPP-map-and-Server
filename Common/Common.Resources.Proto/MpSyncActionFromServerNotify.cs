using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MpSyncActionFromServerNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1580
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Uid;

	private MpAction? __pbn__Action;

	[ProtoMember(1, Name = "uid")]
	public uint Uid
	{
		get
		{
			return __pbn__Uid.GetValueOrDefault();
		}
		set
		{
			__pbn__Uid = value;
		}
	}

	[ProtoMember(2, Name = "action")]
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

	public bool ShouldSerializeUid()
	{
		return __pbn__Uid.HasValue;
	}

	public void ResetUid()
	{
		__pbn__Uid = null;
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
