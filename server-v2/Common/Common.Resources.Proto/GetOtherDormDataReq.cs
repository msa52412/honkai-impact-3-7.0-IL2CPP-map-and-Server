using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetOtherDormDataReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 633
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TargetUid;

	private uint? __pbn__VisitAvatar;

	private uint? __pbn__Level;

	private string __pbn__Name;

	[ProtoMember(1, Name = "target_uid")]
	public uint TargetUid
	{
		get
		{
			return __pbn__TargetUid.GetValueOrDefault();
		}
		set
		{
			__pbn__TargetUid = value;
		}
	}

	[ProtoMember(2, Name = "visit_avatar")]
	public uint VisitAvatar
	{
		get
		{
			return __pbn__VisitAvatar.GetValueOrDefault();
		}
		set
		{
			__pbn__VisitAvatar = value;
		}
	}

	[ProtoMember(3, Name = "level")]
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

	[ProtoMember(4, Name = "name")]
	[DefaultValue("")]
	public string Name
	{
		get
		{
			return __pbn__Name ?? "";
		}
		set
		{
			__pbn__Name = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTargetUid()
	{
		return __pbn__TargetUid.HasValue;
	}

	public void ResetTargetUid()
	{
		__pbn__TargetUid = null;
	}

	public bool ShouldSerializeVisitAvatar()
	{
		return __pbn__VisitAvatar.HasValue;
	}

	public void ResetVisitAvatar()
	{
		__pbn__VisitAvatar = null;
	}

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}

	public bool ShouldSerializeName()
	{
		return __pbn__Name != null;
	}

	public void ResetName()
	{
		__pbn__Name = null;
	}
}
