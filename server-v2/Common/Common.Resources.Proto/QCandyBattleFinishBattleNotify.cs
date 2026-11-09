using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QCandyBattleFinishBattleNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6591
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Uid;

	private bool? __pbn__IsOver;

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

	[ProtoMember(3, Name = "is_over")]
	public bool IsOver
	{
		get
		{
			return __pbn__IsOver.GetValueOrDefault();
		}
		set
		{
			__pbn__IsOver = value;
		}
	}

	[ProtoMember(4, Name = "settle_info")]
	public QCandyBattleSettleInfo SettleInfo { get; set; }

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

	public bool ShouldSerializeIsOver()
	{
		return __pbn__IsOver.HasValue;
	}

	public void ResetIsOver()
	{
		__pbn__IsOver = null;
	}
}
