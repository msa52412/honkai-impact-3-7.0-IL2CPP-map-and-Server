using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AdventureStorySweepReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3912
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__SweepId;

	[ProtoMember(1, Name = "sweep_id")]
	public uint SweepId
	{
		get
		{
			return __pbn__SweepId.GetValueOrDefault();
		}
		set
		{
			__pbn__SweepId = value;
		}
	}

	[ProtoMember(2, Name = "avatar_id_list")]
	public uint[] AvatarIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSweepId()
	{
		return __pbn__SweepId.HasValue;
	}

	public void ResetSweepId()
	{
		__pbn__SweepId = null;
	}
}
