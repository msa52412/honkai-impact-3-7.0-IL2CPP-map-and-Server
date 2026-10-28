using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetTeamListReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1215
	}

	private IExtension __pbn__extensionData;

	private bool? __pbn__IsSelectInOrder;

	private uint? __pbn__Level;

	[ProtoMember(1, Name = "stage_id_list")]
	public uint[] StageIdLists { get; set; }

	[ProtoMember(2, Name = "team_id_list")]
	public uint[] TeamIdLists { get; set; }

	[ProtoMember(3, Name = "is_select_in_order")]
	public bool IsSelectInOrder
	{
		get
		{
			return __pbn__IsSelectInOrder.GetValueOrDefault();
		}
		set
		{
			__pbn__IsSelectInOrder = value;
		}
	}

	[ProtoMember(4, Name = "level")]
	public uint Level
	{
		get
		{
			return __pbn__Level.GetValueOrDefault();
		}
		set
		{
			__pbn__Level = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeIsSelectInOrder()
	{
		return __pbn__IsSelectInOrder.HasValue;
	}

	public void ResetIsSelectInOrder()
	{
		__pbn__IsSelectInOrder = null;
	}

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}
}
