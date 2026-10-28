using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class JigsawMovePieceInJigsawAreaReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4279
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__JigsawId;

	private uint? __pbn__SourceX;

	private uint? __pbn__SourceY;

	private uint? __pbn__TargetX;

	private uint? __pbn__TargetY;

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

	[ProtoMember(2, Name = "jigsaw_id")]
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

	[ProtoMember(3, Name = "source_x")]
	public uint SourceX
	{
		get
		{
			return __pbn__SourceX.GetValueOrDefault();
		}
		set
		{
			__pbn__SourceX = value;
		}
	}

	[ProtoMember(4, Name = "source_y")]
	public uint SourceY
	{
		get
		{
			return __pbn__SourceY.GetValueOrDefault();
		}
		set
		{
			__pbn__SourceY = value;
		}
	}

	[ProtoMember(5, Name = "target_x")]
	public uint TargetX
	{
		get
		{
			return __pbn__TargetX.GetValueOrDefault();
		}
		set
		{
			__pbn__TargetX = value;
		}
	}

	[ProtoMember(6, Name = "target_y")]
	public uint TargetY
	{
		get
		{
			return __pbn__TargetY.GetValueOrDefault();
		}
		set
		{
			__pbn__TargetY = value;
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

	public bool ShouldSerializeSourceX()
	{
		return __pbn__SourceX.HasValue;
	}

	public void ResetSourceX()
	{
		__pbn__SourceX = null;
	}

	public bool ShouldSerializeSourceY()
	{
		return __pbn__SourceY.HasValue;
	}

	public void ResetSourceY()
	{
		__pbn__SourceY = null;
	}

	public bool ShouldSerializeTargetX()
	{
		return __pbn__TargetX.HasValue;
	}

	public void ResetTargetX()
	{
		__pbn__TargetX = null;
	}

	public bool ShouldSerializeTargetY()
	{
		return __pbn__TargetY.HasValue;
	}

	public void ResetTargetY()
	{
		__pbn__TargetY = null;
	}
}
