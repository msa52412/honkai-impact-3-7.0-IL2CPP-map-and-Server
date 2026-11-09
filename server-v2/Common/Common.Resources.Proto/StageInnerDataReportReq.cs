using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class StageInnerDataReportReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 131
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__RotateCameraTimes;

	private uint? __pbn__StageId;

	[ProtoMember(1, Name = "avatar_list")]
	public List<StageInnerAvatarData> AvatarLists { get; } = new List<StageInnerAvatarData>();


	[ProtoMember(2, Name = "monster_list")]
	public List<StageInnerMonsterData> MonsterLists { get; } = new List<StageInnerMonsterData>();


	[ProtoMember(3, Name = "rotate_camera_times")]
	public uint RotateCameraTimes
	{
		get
		{
			return __pbn__RotateCameraTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__RotateCameraTimes = value;
		}
	}

	[ProtoMember(4, Name = "elf_list")]
	public List<StageInnerElfData> ElfLists { get; } = new List<StageInnerElfData>();


	[ProtoMember(5, Name = "stage_id")]
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

	public bool ShouldSerializeRotateCameraTimes()
	{
		return __pbn__RotateCameraTimes.HasValue;
	}

	public void ResetRotateCameraTimes()
	{
		__pbn__RotateCameraTimes = null;
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
