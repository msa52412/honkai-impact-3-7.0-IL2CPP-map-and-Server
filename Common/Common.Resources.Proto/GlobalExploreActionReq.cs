using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GlobalExploreActionReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6907
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__EntityId;

	private uint? __pbn__ActionId;

	private uint? __pbn__ActionParam;

	[ProtoMember(1, Name = "pos")]
	public GlobalExplorePos Pos { get; set; }

	[ProtoMember(2, Name = "entity_id")]
	public uint EntityId
	{
		get
		{
			return __pbn__EntityId.GetValueOrDefault();
		}
		set
		{
			__pbn__EntityId = value;
		}
	}

	[ProtoMember(3, Name = "action_id")]
	public uint ActionId
	{
		get
		{
			return __pbn__ActionId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActionId = value;
		}
	}

	[ProtoMember(4, Name = "action_param")]
	public uint ActionParam
	{
		get
		{
			return __pbn__ActionParam.GetValueOrDefault();
		}
		set
		{
			__pbn__ActionParam = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeEntityId()
	{
		return __pbn__EntityId.HasValue;
	}

	public void ResetEntityId()
	{
		__pbn__EntityId = null;
	}

	public bool ShouldSerializeActionId()
	{
		return __pbn__ActionId.HasValue;
	}

	public void ResetActionId()
	{
		__pbn__ActionId = null;
	}

	public bool ShouldSerializeActionParam()
	{
		return __pbn__ActionParam.HasValue;
	}

	public void ResetActionParam()
	{
		__pbn__ActionParam = null;
	}
}
