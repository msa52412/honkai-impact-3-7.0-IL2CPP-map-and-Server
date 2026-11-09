using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class CheckDormLikesDropRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1011
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "LEVEL_LIMIT")]
		LevelLimit
	}

	private IExtension __pbn__extensionData;

	private bool? __pbn__HasLikesDrop;

	private uint? __pbn__YesterdayBeLikedNum;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "has_likes_drop")]
	public bool HasLikesDrop
	{
		get
		{
			return __pbn__HasLikesDrop.GetValueOrDefault();
		}
		set
		{
			__pbn__HasLikesDrop = value;
		}
	}

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

	public bool ShouldSerializeHasLikesDrop()
	{
		return __pbn__HasLikesDrop.HasValue;
	}

	public void ResetHasLikesDrop()
	{
		__pbn__HasLikesDrop = null;
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
