using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class BingoActivityResetBingoReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4161
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__BingoId;

	[ProtoMember(1, Name = "activity_id")]
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

	[ProtoMember(2, Name = "bingo_id")]
	public uint BingoId
	{
		get
		{
			return __pbn__BingoId.GetValueOrDefault();
		}
		set
		{
			__pbn__BingoId = value;
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

	public bool ShouldSerializeBingoId()
	{
		return __pbn__BingoId.HasValue;
	}

	public void ResetBingoId()
	{
		__pbn__BingoId = null;
	}
}
