using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class FinishedExBossInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__BossId;

	private uint? __pbn__Score;

	[ProtoMember(1, Name = "boss_id")]
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

	[ProtoMember(2, Name = "score")]
	public uint Score
	{
		get
		{
			return __pbn__Score.GetValueOrDefault();
		}
		set
		{
			__pbn__Score = value;
		}
	}

	[ProtoMember(3, Name = "avatar_info_list")]
	public List<AvatarBriefInfo> AvatarInfoLists { get; } = new List<AvatarBriefInfo>();


	[ProtoMember(4, Name = "elf_info_list")]
	public List<ElfBriefInfo> ElfInfoLists { get; } = new List<ElfBriefInfo>();


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

	public bool ShouldSerializeScore()
	{
		return __pbn__Score.HasValue;
	}

	public void ResetScore()
	{
		__pbn__Score = null;
	}
}
