using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class StageBuff : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__StageId;

	private bool? __pbn__IsNew;

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

	[ProtoMember(2, Name = "gen_buff_id_list")]
	public uint[] GenBuffIdLists { get; set; }

	[ProtoMember(3, Name = "hold_buff_id_list")]
	public uint[] HoldBuffIdLists { get; set; }

	[ProtoMember(4, Name = "is_new")]
	public bool IsNew
	{
		get
		{
			return __pbn__IsNew.GetValueOrDefault();
		}
		set
		{
			__pbn__IsNew = value;
		}
	}

	[ProtoMember(5, Name = "carry_buff_id_list")]
	public uint[] CarryBuffIdLists { get; set; }

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

	public bool ShouldSerializeIsNew()
	{
		return __pbn__IsNew.HasValue;
	}

	public void ResetIsNew()
	{
		__pbn__IsNew = null;
	}
}
