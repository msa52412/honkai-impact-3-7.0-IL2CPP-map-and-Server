using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QCandyBattleAvatarStateServerNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6589
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__EntityId;

	private uint? __pbn__State;

	private bool? __pbn__IsMove;

	private uint? __pbn__SubState;

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

	[ProtoMember(2, Name = "state")]
	public uint State
	{
		get
		{
			return __pbn__State.GetValueOrDefault();
		}
		set
		{
			__pbn__State = value;
		}
	}

	[ProtoMember(3, Name = "is_move")]
	public bool IsMove
	{
		get
		{
			return __pbn__IsMove.GetValueOrDefault();
		}
		set
		{
			__pbn__IsMove = value;
		}
	}

	[ProtoMember(4, Name = "sub_state")]
	public uint SubState
	{
		get
		{
			return __pbn__SubState.GetValueOrDefault();
		}
		set
		{
			__pbn__SubState = value;
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

	public bool ShouldSerializeState()
	{
		return __pbn__State.HasValue;
	}

	public void ResetState()
	{
		__pbn__State = null;
	}

	public bool ShouldSerializeIsMove()
	{
		return __pbn__IsMove.HasValue;
	}

	public void ResetIsMove()
	{
		__pbn__IsMove = null;
	}

	public bool ShouldSerializeSubState()
	{
		return __pbn__SubState.HasValue;
	}

	public void ResetSubState()
	{
		__pbn__SubState = null;
	}
}
