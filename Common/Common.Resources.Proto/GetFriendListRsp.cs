using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetFriendListRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 65
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

	private uint? __pbn__AssistantNum;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "friend_list")]
	public List<PlayerFriendBriefData> FriendLists { get; } = new List<PlayerFriendBriefData>();


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

	[ProtoMember(4, Name = "assistant_num")]
	public uint AssistantNum
	{
		get
		{
			return __pbn__AssistantNum.GetValueOrDefault();
		}
		set
		{
			__pbn__AssistantNum = value;
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

	public bool ShouldSerializeAssistantNum()
	{
		return __pbn__AssistantNum.HasValue;
	}

	public void ResetAssistantNum()
	{
		__pbn__AssistantNum = null;
	}
}
