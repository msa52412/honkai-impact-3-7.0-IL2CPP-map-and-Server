using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class JigsawMovePieceInJigsawAreaRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4280
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen,
		[ProtoEnum(Name = "JIGSAW_NOT_BEGIN")]
		JigsawNotBegin,
		[ProtoEnum(Name = "PRE_JIGSAW_NOT_FINISHED")]
		PreJigsawNotFinished,
		[ProtoEnum(Name = "NO_SOURCE_PIECE")]
		NoSourcePiece,
		[ProtoEnum(Name = "OUT_OF_RANGE")]
		OutOfRange,
		[ProtoEnum(Name = "LOCKED")]
		Locked
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__JigsawId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "activity_id")]
	public uint ActivityId
	{
		get
		{
			return __pbn__ActivityId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityId = value;
		}
	}

	[ProtoMember(3, Name = "jigsaw_id")]
	public uint JigsawId
	{
		get
		{
			return __pbn__JigsawId.GetValueOrDefault();
		}
		set
		{
			__pbn__JigsawId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeActivityId()
	{
		return __pbn__ActivityId.HasValue;
	}

	public void ResetActivityId()
	{
		__pbn__ActivityId = null;
	}

	public bool ShouldSerializeJigsawId()
	{
		return __pbn__JigsawId.HasValue;
	}

	public void ResetJigsawId()
	{
		__pbn__JigsawId = null;
	}
}
