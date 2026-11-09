using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class VirtualTrainItemDropNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3524
	}

	[ProtoContract]
	public enum DropSrcType
	{
		[ProtoEnum(Name = "DROP_SRC_NONE")]
		DropSrcNone = 0,
		[ProtoEnum(Name = "DROP_SRC_RPG_STAGE")]
		DropSrcRpgStage = 1,
		[ProtoEnum(Name = "DROP_SRC_RPG_MISSION")]
		DropSrcRpgMission = 2,
		[ProtoEnum(Name = "DROP_SRC_RPG_QAVATAR")]
		DropSrcRpgQavatar = 3,
		[ProtoEnum(Name = "DROP_SRC_GM")]
		DropSrcGm = 10
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__GroupId;

	private DropSrcType? __pbn__drop_src_type;

	private uint? __pbn__StageId;

	[ProtoMember(1, Name = "group_id")]
	public uint GroupId
	{
		get
		{
			return __pbn__GroupId.GetValueOrDefault();
		}
		set
		{
			__pbn__GroupId = value;
		}
	}

	[ProtoMember(2)]
	[DefaultValue(DropSrcType.DropSrcNone)]
	public DropSrcType drop_src_type
	{
		get
		{
			return __pbn__drop_src_type.GetValueOrDefault();
		}
		set
		{
			__pbn__drop_src_type = value;
		}
	}

	[ProtoMember(3, Name = "item_drop_list")]
	public List<VirtualTrainItemDrop> ItemDropLists { get; } = new List<VirtualTrainItemDrop>();


	[ProtoMember(4, Name = "stage_id")]
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeGroupId()
	{
		return __pbn__GroupId.HasValue;
	}

	public void ResetGroupId()
	{
		__pbn__GroupId = null;
	}

	public bool ShouldSerializedrop_src_type()
	{
		return __pbn__drop_src_type.HasValue;
	}

	public void Resetdrop_src_type()
	{
		__pbn__drop_src_type = null;
	}

	public bool ShouldSerializeStageId()
	{
		return __pbn__StageId.HasValue;
	}

	public void ResetStageId()
	{
		__pbn__StageId = null;
	}
}
