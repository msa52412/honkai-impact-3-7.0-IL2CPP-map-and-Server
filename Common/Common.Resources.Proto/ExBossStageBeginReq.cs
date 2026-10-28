using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ExBossStageBeginReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 529
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__BossId;

	private bool? __pbn__IsTraining;

	private bool? __pbn__IsTurbo;

	[ProtoMember(1, Name = "avatar_id_list")]
	public uint[] AvatarIdLists { get; set; }

	[ProtoMember(2, Name = "boss_id")]
	public uint BossId
	{
		get
		{
			return __pbn__BossId.GetValueOrDefault();
		}
		set
		{
			__pbn__BossId = value;
		}
	}

	[ProtoMember(3, Name = "is_training")]
	public bool IsTraining
	{
		get
		{
			return __pbn__IsTraining.GetValueOrDefault();
		}
		set
		{
			__pbn__IsTraining = value;
		}
	}

	[ProtoMember(4, Name = "elf_id_list")]
	public uint[] ElfIdLists { get; set; }

	[ProtoMember(5, Name = "is_turbo")]
	public bool IsTurbo
	{
		get
		{
			return __pbn__IsTurbo.GetValueOrDefault();
		}
		set
		{
			__pbn__IsTurbo = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBossId()
	{
		return __pbn__BossId.HasValue;
	}

	public void ResetBossId()
	{
		__pbn__BossId = null;
	}

	public bool ShouldSerializeIsTraining()
	{
		return __pbn__IsTraining.HasValue;
	}

	public void ResetIsTraining()
	{
		__pbn__IsTraining = null;
	}

	public bool ShouldSerializeIsTurbo()
	{
		return __pbn__IsTurbo.HasValue;
	}

	public void ResetIsTurbo()
	{
		__pbn__IsTurbo = null;
	}
}
