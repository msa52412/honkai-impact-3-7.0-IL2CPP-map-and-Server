using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeDormLikesDropRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1009
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "LEVEL_LIMIT")]
		LevelLimit,
		[ProtoEnum(Name = "NO_DROP")]
		NoDrop,
		[ProtoEnum(Name = "HAVE_TAKEN")]
		HaveTaken
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__YesterdayBeLikedNum;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "drop_item_list")]
	public List<DropItem> DropItemLists { get; } = new List<DropItem>();


	[ProtoMember(3, Name = "yesterday_be_liked_num")]
	public uint YesterdayBeLikedNum
	{
		get
		{
			return __pbn__YesterdayBeLikedNum.GetValueOrDefault();
		}
		set
		{
			__pbn__YesterdayBeLikedNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeYesterdayBeLikedNum()
	{
		return __pbn__YesterdayBeLikedNum.HasValue;
	}

	public void ResetYesterdayBeLikedNum()
	{
		__pbn__YesterdayBeLikedNum = null;
	}
}
