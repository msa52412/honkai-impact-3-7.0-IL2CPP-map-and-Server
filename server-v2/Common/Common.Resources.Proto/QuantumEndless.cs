using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QuantumEndless : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__CurMonsterGroup;

	private uint? __pbn__MonsterProgress;

	private uint? __pbn__FinishMonsterNum;

	[ProtoMember(1, Name = "cur_monster_group")]
	public uint CurMonsterGroup
	{
		get
		{
			return __pbn__CurMonsterGroup.GetValueOrDefault();
		}
		set
		{
			__pbn__CurMonsterGroup = value;
		}
	}

	[ProtoMember(2, Name = "monster_progress")]
	public uint MonsterProgress
	{
		get
		{
			return __pbn__MonsterProgress.GetValueOrDefault();
		}
		set
		{
			__pbn__MonsterProgress = value;
		}
	}

	[ProtoMember(3, Name = "finish_monster_num")]
	public uint FinishMonsterNum
	{
		get
		{
			return __pbn__FinishMonsterNum.GetValueOrDefault();
		}
		set
		{
			__pbn__FinishMonsterNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeCurMonsterGroup()
	{
		return __pbn__CurMonsterGroup.HasValue;
	}

	public void ResetCurMonsterGroup()
	{
		__pbn__CurMonsterGroup = null;
	}

	public bool ShouldSerializeMonsterProgress()
	{
		return __pbn__MonsterProgress.HasValue;
	}

	public void ResetMonsterProgress()
	{
		__pbn__MonsterProgress = null;
	}

	public bool ShouldSerializeFinishMonsterNum()
	{
		return __pbn__FinishMonsterNum.HasValue;
	}

	public void ResetFinishMonsterNum()
	{
		__pbn__FinishMonsterNum = null;
	}
}
