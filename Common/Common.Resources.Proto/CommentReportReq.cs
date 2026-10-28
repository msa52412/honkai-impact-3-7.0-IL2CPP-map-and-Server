using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class CommentReportReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 229
	}

	private IExtension __pbn__extensionData;

	private CommentType? __pbn__Comment;

	private uint? __pbn__Times;

	[ProtoMember(1, Name = "comment")]
	[DefaultValue(CommentType.CommentOk)]
	public CommentType Comment
	{
		get
		{
			return __pbn__Comment ?? CommentType.CommentOk;
		}
		set
		{
			__pbn__Comment = value;
		}
	}

	[ProtoMember(2, Name = "times")]
	public uint Times
	{
		get
		{
			return __pbn__Times.GetValueOrDefault();
		}
		set
		{
			__pbn__Times = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeComment()
	{
		return __pbn__Comment.HasValue;
	}

	public void ResetComment()
	{
		__pbn__Comment = null;
	}

	public bool ShouldSerializeTimes()
	{
		return __pbn__Times.HasValue;
	}

	public void ResetTimes()
	{
		__pbn__Times = null;
	}
}
