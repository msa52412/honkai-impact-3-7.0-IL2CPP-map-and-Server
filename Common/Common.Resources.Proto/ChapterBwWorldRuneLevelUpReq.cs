using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChapterBwWorldRuneLevelUpReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 7654
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ChapterId;

	private uint? __pbn__MainRuneUniqueId;

	private uint? __pbn__TargetLevel;

	private uint? __pbn__TargetExp;

	[ProtoMember(1, Name = "chapter_id")]
	public uint ChapterId
	{
		get
		{
			return __pbn__ChapterId.GetValueOrDefault();
		}
		set
		{
			__pbn__ChapterId = value;
		}
	}

	[ProtoMember(2, Name = "main_rune_unique_id")]
	public uint MainRuneUniqueId
	{
		get
		{
			return __pbn__MainRuneUniqueId.GetValueOrDefault();
		}
		set
		{
			__pbn__MainRuneUniqueId = value;
		}
	}

	[ProtoMember(3, Name = "target_level")]
	public uint TargetLevel
	{
		get
		{
			return __pbn__TargetLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__TargetLevel = value;
		}
	}

	[ProtoMember(4, Name = "target_exp")]
	public uint TargetExp
	{
		get
		{
			return __pbn__TargetExp.GetValueOrDefault();
		}
		set
		{
			__pbn__TargetExp = value;
		}
	}

	[ProtoMember(5, Name = "exp_material_list")]
	public List<Material> ExpMaterialLists { get; } = new List<Material>();


	[ProtoMember(6, Name = "assist_rune_unique_id_list")]
	public uint[] AssistRuneUniqueIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeChapterId()
	{
		return __pbn__ChapterId.HasValue;
	}

	public void ResetChapterId()
	{
		__pbn__ChapterId = null;
	}

	public bool ShouldSerializeMainRuneUniqueId()
	{
		return __pbn__MainRuneUniqueId.HasValue;
	}

	public void ResetMainRuneUniqueId()
	{
		__pbn__MainRuneUniqueId = null;
	}

	public bool ShouldSerializeTargetLevel()
	{
		return __pbn__TargetLevel.HasValue;
	}

	public void ResetTargetLevel()
	{
		__pbn__TargetLevel = null;
	}

	public bool ShouldSerializeTargetExp()
	{
		return __pbn__TargetExp.HasValue;
	}

	public void ResetTargetExp()
	{
		__pbn__TargetExp = null;
	}
}
