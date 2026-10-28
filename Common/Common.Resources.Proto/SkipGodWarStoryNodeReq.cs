using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SkipGodWarStoryNodeReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6207
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__GodWarId;

	private uint? __pbn__NodeId;

	[ProtoMember(1, Name = "god_war_id")]
	public uint GodWarId
	{
		get
		{
			return __pbn__GodWarId.GetValueOrDefault();
		}
		set
		{
			__pbn__GodWarId = value;
		}
	}

	[ProtoMember(2, Name = "node_id")]
	public uint NodeId
	{
		get
		{
			return __pbn__NodeId.GetValueOrDefault();
		}
		set
		{
			__pbn__NodeId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeGodWarId()
	{
		return __pbn__GodWarId.HasValue;
	}

	public void ResetGodWarId()
	{
		__pbn__GodWarId = null;
	}

	public bool ShouldSerializeNodeId()
	{
		return __pbn__NodeId.HasValue;
	}

	public void ResetNodeId()
	{
		__pbn__NodeId = null;
	}
}
