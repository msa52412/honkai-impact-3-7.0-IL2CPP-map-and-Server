using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class DormVisitRecord : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__VisitTime;

	private uint? __pbn__VisitUid;

	private uint? __pbn__VisitAvatar;

	private uint? __pbn__Level;

	private string __pbn__Name;

	[ProtoMember(1, Name = "visit_time")]
	public uint VisitTime
	{
		get
		{
			return __pbn__VisitTime.GetValueOrDefault();
		}
		set
		{
			__pbn__VisitTime = value;
		}
	}

	[ProtoMember(2, Name = "visit_uid")]
	public uint VisitUid
	{
		get
		{
			return __pbn__VisitUid.GetValueOrDefault();
		}
		set
		{
			__pbn__VisitUid = value;
		}
	}

	[ProtoMember(3, Name = "visit_avatar")]
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

	[ProtoMember(5, Name = "name")]
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

	public bool ShouldSerializeVisitTime()
	{
		return __pbn__VisitTime.HasValue;
	}

	public void ResetVisitTime()
	{
		__pbn__VisitTime = null;
	}

	public bool ShouldSerializeVisitUid()
	{
		return __pbn__VisitUid.HasValue;
	}

	public void ResetVisitUid()
	{
		__pbn__VisitUid = null;
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
