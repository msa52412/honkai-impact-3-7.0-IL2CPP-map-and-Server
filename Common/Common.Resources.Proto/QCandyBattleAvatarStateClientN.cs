using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QCandyBattleAvatarStateClientNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6588
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__State;

	private bool? __pbn__IsMove;

	private uint? __pbn__SubState;

	[ProtoMember(1, Name = "state")]
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

	[ProtoMember(2, Name = "is_move")]
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

	[ProtoMember(3, Name = "sub_state")]
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
