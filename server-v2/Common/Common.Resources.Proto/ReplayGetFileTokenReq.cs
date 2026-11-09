using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ReplayGetFileTokenReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5410
	}

	private IExtension __pbn__extensionData;

	private ReplayOp? __pbn__Op;

	[ProtoMember(1, Name = "op")]
	[DefaultValue(ReplayOp.ReplayUpload)]
	public ReplayOp Op
	{
		get
		{
			return __pbn__Op ?? ReplayOp.ReplayUpload;
		}
		set
		{
			__pbn__Op = value;
		}
	}

	[ProtoMember(2, Name = "replay")]
	public Replay Replay { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeOp()
	{
		return __pbn__Op.HasValue;
	}

	public void ResetOp()
	{
		__pbn__Op = null;
	}
}
