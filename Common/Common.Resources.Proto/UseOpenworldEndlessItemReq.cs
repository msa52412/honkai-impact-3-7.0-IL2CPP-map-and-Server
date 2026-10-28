using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UseOpenworldEndlessItemReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3422
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ItemId;

	private uint? __pbn__TargetUid;

	private uint? __pbn__ItemNum;

	[ProtoMember(1, Name = "item_id")]
	public uint ItemId
	{
		get
		{
			return __pbn__ItemId.GetValueOrDefault();
		}
		set
		{
			__pbn__ItemId = value;
		}
	}

	[ProtoMember(2, Name = "target_uid")]
	public uint TargetUid
	{
		get
		{
			return __pbn__TargetUid.GetValueOrDefault();
		}
		set
		{
			__pbn__TargetUid = value;
		}
	}

	[ProtoMember(3, Name = "invasion")]
	public OpenworldInvasionItem Invasion { get; set; }

	[ProtoMember(4, Name = "item_num")]
	public uint ItemNum
	{
		get
		{
			return __pbn__ItemNum.GetValueOrDefault();
		}
		set
		{
			__pbn__ItemNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeItemId()
	{
		return __pbn__ItemId.HasValue;
	}

	public void ResetItemId()
	{
		__pbn__ItemId = null;
	}

	public bool ShouldSerializeTargetUid()
	{
		return __pbn__TargetUid.HasValue;
	}

	public void ResetTargetUid()
	{
		__pbn__TargetUid = null;
	}

	public bool ShouldSerializeItemNum()
	{
		return __pbn__ItemNum.HasValue;
	}

	public void ResetItemNum()
	{
		__pbn__ItemNum = null;
	}
}
