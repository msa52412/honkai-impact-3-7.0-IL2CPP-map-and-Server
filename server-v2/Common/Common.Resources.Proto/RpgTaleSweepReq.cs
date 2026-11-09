using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RpgTaleSweepReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2376
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__StageId;

	private uint? __pbn__Times;

	private uint? __pbn__TaleId;

	[ProtoMember(1, Name = "stage_id")]
	public uint StageId
	{
		get
		{
			return __pbn__StageId.GetValueOrDefault();
		}
		set
		{
			__pbn__StageId = value;
		}
	}

	[ProtoMember(2, Name = "times")]
	public uint Times
	{
		get
		{
			return __pbn__Times.GetValueOrDefault();
		}
		set
		{
			__pbn__Times = value;
		}
	}

	[ProtoMember(3, Name = "tale_id")]
	public uint TaleId
	{
		get
		{
			return __pbn__TaleId.GetValueOrDefault();
		}
		set
		{
			__pbn__TaleId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeStageId()
	{
		return __pbn__StageId.HasValue;
	}

	public void ResetStageId()
	{
		__pbn__StageId = null;
	}

	public bool ShouldSerializeTimes()
	{
		return __pbn__Times.HasValue;
	}

	public void ResetTimes()
	{
		__pbn__Times = null;
	}

	public bool ShouldSerializeTaleId()
	{
		return __pbn__TaleId.HasValue;
	}

	public void ResetTaleId()
	{
		__pbn__TaleId = null;
	}
}
