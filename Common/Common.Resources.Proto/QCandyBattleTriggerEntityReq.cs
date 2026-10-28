using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QCandyBattleTriggerEntityReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6580
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__EntityId;

	private uint? __pbn__CurState;

	[ProtoMember(1, Name = "entity_id")]
	public uint EntityId
	{
		get
		{
			return __pbn__EntityId.GetValueOrDefault();
		}
		set
		{
			__pbn__EntityId = value;
		}
	}

	[ProtoMember(2, Name = "cur_state")]
	public uint CurState
	{
		get
		{
			return __pbn__CurState.GetValueOrDefault();
		}
		set
		{
			__pbn__CurState = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeEntityId()
	{
		return __pbn__EntityId.HasValue;
	}

	public void ResetEntityId()
	{
		__pbn__EntityId = null;
	}

	public bool ShouldSerializeCurState()
	{
		return __pbn__CurState.HasValue;
	}

	public void ResetCurState()
	{
		__pbn__CurState = null;
	}
}
