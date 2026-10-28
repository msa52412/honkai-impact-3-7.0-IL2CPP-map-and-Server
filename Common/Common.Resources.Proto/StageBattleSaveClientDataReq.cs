using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class StageBattleSaveClientDataReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 829
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__StageId;

	private uint? __pbn__ActId;

	private string __pbn__ClientData;

	[ProtoMember(1, Name = "stage_id")]
	public uint StageId
	{
		get
		{
			return __pbn__StageId.GetValueOrDefault();
		}
		set
		{
			__pbn__StageId = value;
		}
	}

	[ProtoMember(2, Name = "act_id")]
	public uint ActId
	{
		get
		{
			return __pbn__ActId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActId = value;
		}
	}

	[ProtoMember(3, Name = "client_data")]
	[DefaultValue("")]
	public string ClientData
	{
		get
		{
			return __pbn__ClientData ?? "";
		}
		set
		{
			__pbn__ClientData = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeStageId()
	{
		return __pbn__StageId.HasValue;
	}

	public void ResetStageId()
	{
		__pbn__StageId = null;
	}

	public bool ShouldSerializeActId()
	{
		return __pbn__ActId.HasValue;
	}

	public void ResetActId()
	{
		__pbn__ActId = null;
	}

	public bool ShouldSerializeClientData()
	{
		return __pbn__ClientData != null;
	}

	public void ResetClientData()
	{
		__pbn__ClientData = null;
	}
}
