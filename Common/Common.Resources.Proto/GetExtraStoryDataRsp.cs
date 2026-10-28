using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetExtraStoryDataRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 232
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail
	}

	private IExtension __pbn__extensionData;

	private bool? __pbn__IsDynamicHardLv;

	private bool? __pbn__IsOpenEnhance;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "chapter_list")]
	public List<ExtraStoryChapter> ChapterLists { get; } = new List<ExtraStoryChapter>();


	[ProtoMember(3, Name = "is_dynamic_hard_lv")]
	public bool IsDynamicHardLv
	{
		get
		{
			return __pbn__IsDynamicHardLv.GetValueOrDefault();
		}
		set
		{
			__pbn__IsDynamicHardLv = value;
		}
	}

	[ProtoMember(4, Name = "chapter_key_list")]
	public uint[] ChapterKeyLists { get; set; }

	[ProtoMember(5, Name = "taken_reward_line_id_list")]
	public uint[] TakenRewardLineIdLists { get; set; }

	[ProtoMember(6, Name = "is_open_enhance")]
	public bool IsOpenEnhance
	{
		get
		{
			return __pbn__IsOpenEnhance.GetValueOrDefault();
		}
		set
		{
			__pbn__IsOpenEnhance = value;
		}
	}

	[ProtoMember(7, Name = "taken_enhance_reward_act_id_list")]
	public uint[] TakenEnhanceRewardActIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeIsDynamicHardLv()
	{
		return __pbn__IsDynamicHardLv.HasValue;
	}

	public void ResetIsDynamicHardLv()
	{
		__pbn__IsDynamicHardLv = null;
	}

	public bool ShouldSerializeIsOpenEnhance()
	{
		return __pbn__IsOpenEnhance.HasValue;
	}

	public void ResetIsOpenEnhance()
	{
		__pbn__IsOpenEnhance = null;
	}
}
