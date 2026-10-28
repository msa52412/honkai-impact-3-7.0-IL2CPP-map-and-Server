using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetLotteryActivityRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4178
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__CurGroupId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "cur_group_id")]
	public uint CurGroupId
	{
		get
		{
			return __pbn__CurGroupId.GetValueOrDefault();
		}
		set
		{
			__pbn__CurGroupId = value;
		}
	}

	[ProtoMember(3, Name = "lottery_data_list")]
	public List<LotteryData> LotteryDataLists { get; } = new List<LotteryData>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeCurGroupId()
	{
		return __pbn__CurGroupId.HasValue;
	}

	public void ResetCurGroupId()
	{
		__pbn__CurGroupId = null;
	}
}
