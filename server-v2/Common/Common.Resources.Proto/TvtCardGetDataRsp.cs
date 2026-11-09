using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TvtCardGetDataRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3310
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "LOCKED")]
		Locked
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__SelectedSuiteIdx;

	private bool? __pbn__IsTakeSeasonReturn;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "card_list")]
	public List<TvtCard> CardLists { get; } = new List<TvtCard>();


	[ProtoMember(3, Name = "suite_list")]
	public List<TvtCardSuite> SuiteLists { get; } = new List<TvtCardSuite>();


	[ProtoMember(4, Name = "selected_suite_idx")]
	public uint SelectedSuiteIdx
	{
		get
		{
			return __pbn__SelectedSuiteIdx.GetValueOrDefault();
		}
		set
		{
			__pbn__SelectedSuiteIdx = value;
		}
	}

	[ProtoMember(5, Name = "is_take_season_return")]
	public bool IsTakeSeasonReturn
	{
		get
		{
			return __pbn__IsTakeSeasonReturn.GetValueOrDefault();
		}
		set
		{
			__pbn__IsTakeSeasonReturn = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSelectedSuiteIdx()
	{
		return __pbn__SelectedSuiteIdx.HasValue;
	}

	public void ResetSelectedSuiteIdx()
	{
		__pbn__SelectedSuiteIdx = null;
	}

	public bool ShouldSerializeIsTakeSeasonReturn()
	{
		return __pbn__IsTakeSeasonReturn.HasValue;
	}

	public void ResetIsTakeSeasonReturn()
	{
		__pbn__IsTakeSeasonReturn = null;
	}
}
