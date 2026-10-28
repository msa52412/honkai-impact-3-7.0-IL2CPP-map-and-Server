using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetBlackListRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1595
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

	private bool? __pbn__IsWholeData;

	private bool? __pbn__IsOnlyUid;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "black_list")]
	public List<PlayerFriendBriefData> BlackLists { get; } = new List<PlayerFriendBriefData>();


	[ProtoMember(3, Name = "is_whole_data")]
	public bool IsWholeData
	{
		get
		{
			return __pbn__IsWholeData.GetValueOrDefault();
		}
		set
		{
			__pbn__IsWholeData = value;
		}
	}

	[ProtoMember(4, Name = "is_only_uid")]
	public bool IsOnlyUid
	{
		get
		{
			return __pbn__IsOnlyUid.GetValueOrDefault();
		}
		set
		{
			__pbn__IsOnlyUid = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeIsWholeData()
	{
		return __pbn__IsWholeData.HasValue;
	}

	public void ResetIsWholeData()
	{
		__pbn__IsWholeData = null;
	}

	public bool ShouldSerializeIsOnlyUid()
	{
		return __pbn__IsOnlyUid.HasValue;
	}

	public void ResetIsOnlyUid()
	{
		__pbn__IsOnlyUid = null;
	}
}
