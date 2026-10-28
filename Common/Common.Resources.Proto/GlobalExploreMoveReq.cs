using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GlobalExploreMoveReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6903
	}

	private IExtension __pbn__extensionData;

	private GlobalExploreMoveType? __pbn__MoveType;

	[ProtoMember(1, Name = "move_pos_list")]
	public List<GlobalExplorePos> MovePosLists { get; } = new List<GlobalExplorePos>();


	[ProtoMember(2, Name = "move_type")]
	[DefaultValue(GlobalExploreMoveType.GlobalExploreMoveWalk)]
	public GlobalExploreMoveType MoveType
	{
		get
		{
			return __pbn__MoveType ?? GlobalExploreMoveType.GlobalExploreMoveWalk;
		}
		set
		{
			__pbn__MoveType = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMoveType()
	{
		return __pbn__MoveType.HasValue;
	}

	public void ResetMoveType()
	{
		__pbn__MoveType = null;
	}
}
