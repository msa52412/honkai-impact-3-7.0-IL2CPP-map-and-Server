using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class VirtualGachaRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3509
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "WAIT_CHOOSE")]
		WaitChoose,
		[ProtoEnum(Name = "MATERIAL_LACK")]
		MaterialLack,
		[ProtoEnum(Name = "EMPTY")]
		Empty
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ItemId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "item_id")]
	public uint ItemId
	{
		get
		{
			return __pbn__ItemId.GetValueOrDefault();
		}
		set
		{
			__pbn__ItemId = value;
		}
	}

	[ProtoMember(3, Name = "candidate_item_id_list")]
	public uint[] CandidateItemIdLists { get; set; }

	[ProtoMember(4, Name = "gacha_status")]
	public VirtualGacha GachaStatus { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeItemId()
	{
		return __pbn__ItemId.HasValue;
	}

	public void ResetItemId()
	{
		__pbn__ItemId = null;
	}
}
