using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MatrixEventFinishNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2335
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__EventId;

	private MatrixEventType? __pbn__DisplayType;

	private uint? __pbn__DisplayId;

	[ProtoMember(1, Name = "pos")]
	public MatrixGridPos Pos { get; set; }

	[ProtoMember(2, Name = "event_id")]
	public uint EventId
	{
		get
		{
			return __pbn__EventId.GetValueOrDefault();
		}
		set
		{
			__pbn__EventId = value;
		}
	}

	[ProtoMember(3, Name = "reward_data")]
	public RewardData RewardData { get; set; }

	[ProtoMember(4, Name = "display_type")]
	[DefaultValue(MatrixEventType.MatrixEventInvalid)]
	public MatrixEventType DisplayType
	{
		get
		{
			return __pbn__DisplayType.GetValueOrDefault();
		}
		set
		{
			__pbn__DisplayType = value;
		}
	}

	[ProtoMember(5, Name = "display_id")]
	public uint DisplayId
	{
		get
		{
			return __pbn__DisplayId.GetValueOrDefault();
		}
		set
		{
			__pbn__DisplayId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeEventId()
	{
		return __pbn__EventId.HasValue;
	}

	public void ResetEventId()
	{
		__pbn__EventId = null;
	}

	public bool ShouldSerializeDisplayType()
	{
		return __pbn__DisplayType.HasValue;
	}

	public void ResetDisplayType()
	{
		__pbn__DisplayType = null;
	}

	public bool ShouldSerializeDisplayId()
	{
		return __pbn__DisplayId.HasValue;
	}

	public void ResetDisplayId()
	{
		__pbn__DisplayId = null;
	}
}
