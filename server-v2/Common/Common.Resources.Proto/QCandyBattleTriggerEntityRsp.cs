using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QCandyBattleTriggerEntityRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6581
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "ENTITY_NOT_EXIST")]
		EntityNotExist,
		[ProtoEnum(Name = "STATE_ERROR")]
		StateError
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__EntityId;

	private uint? __pbn__CurState;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "entity_id")]
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

	[ProtoMember(3, Name = "cur_state")]
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
