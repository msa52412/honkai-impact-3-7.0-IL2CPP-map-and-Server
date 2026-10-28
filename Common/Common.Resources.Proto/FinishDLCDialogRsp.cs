using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class FinishDLCDialogRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3579
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_SUPPORT_DIALOG")]
		NotSupportDialog,
		[ProtoEnum(Name = "ALREADY_TAKEN")]
		AlreadyTaken
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__DialogId;

	private uint? __pbn__AddSupportPoint;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "dialog_id")]
	public uint DialogId
	{
		get
		{
			return __pbn__DialogId.GetValueOrDefault();
		}
		set
		{
			__pbn__DialogId = value;
		}
	}

	[ProtoMember(3, Name = "add_support_point")]
	public uint AddSupportPoint
	{
		get
		{
			return __pbn__AddSupportPoint.GetValueOrDefault();
		}
		set
		{
			__pbn__AddSupportPoint = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeDialogId()
	{
		return __pbn__DialogId.HasValue;
	}

	public void ResetDialogId()
	{
		__pbn__DialogId = null;
	}

	public bool ShouldSerializeAddSupportPoint()
	{
		return __pbn__AddSupportPoint.HasValue;
	}

	public void ResetAddSupportPoint()
	{
		__pbn__AddSupportPoint = null;
	}
}
