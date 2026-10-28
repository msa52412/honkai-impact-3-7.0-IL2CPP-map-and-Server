using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeDutyRewardReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 288
	}

	private IExtension __pbn__extensionData;

	private DutyType? __pbn__DutyType;

	[ProtoMember(1, Name = "duty_type")]
	[DefaultValue(DutyType.DutyDaily)]
	public DutyType DutyType
	{
		get
		{
			return __pbn__DutyType ?? DutyType.DutyDaily;
		}
		set
		{
			__pbn__DutyType = value;
		}
	}

	[ProtoMember(2, Name = "duty_id_list")]
	public uint[] DutyIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeDutyType()
	{
		return __pbn__DutyType.HasValue;
	}

	public void ResetDutyType()
	{
		__pbn__DutyType = null;
	}
}
