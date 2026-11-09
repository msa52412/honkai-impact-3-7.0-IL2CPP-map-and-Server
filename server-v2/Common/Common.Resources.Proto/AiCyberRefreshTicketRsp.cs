using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AiCyberRefreshTicketRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6659
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "ACTIVITY_NOT_OPEN")]
		ActivityNotOpen
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TicketMaterialId;

	private uint? __pbn__AddNum;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "ticket_material_id")]
	public uint TicketMaterialId
	{
		get
		{
			return __pbn__TicketMaterialId.GetValueOrDefault();
		}
		set
		{
			__pbn__TicketMaterialId = value;
		}
	}

	[ProtoMember(3, Name = "add_num")]
	public uint AddNum
	{
		get
		{
			return __pbn__AddNum.GetValueOrDefault();
		}
		set
		{
			__pbn__AddNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTicketMaterialId()
	{
		return __pbn__TicketMaterialId.HasValue;
	}

	public void ResetTicketMaterialId()
	{
		__pbn__TicketMaterialId = null;
	}

	public bool ShouldSerializeAddNum()
	{
		return __pbn__AddNum.HasValue;
	}

	public void ResetAddNum()
	{
		__pbn__AddNum = null;
	}
}
