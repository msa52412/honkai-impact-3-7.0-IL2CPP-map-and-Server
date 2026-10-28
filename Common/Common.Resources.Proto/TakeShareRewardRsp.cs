using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeShareRewardRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4238
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "INVALID_SHARE_ID")]
		InvalidShareId,
		[ProtoEnum(Name = "HAVE_TAKEN")]
		HaveTaken
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ShareId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "share_id")]
	public uint ShareId
	{
		get
		{
			return __pbn__ShareId.GetValueOrDefault();
		}
		set
		{
			__pbn__ShareId = value;
		}
	}

	[ProtoMember(3, Name = "reward_list")]
	public List<RewardData> RewardLists { get; } = new List<RewardData>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeShareId()
	{
		return __pbn__ShareId.HasValue;
	}

	public void ResetShareId()
	{
		__pbn__ShareId = null;
	}
}
