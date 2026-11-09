using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GratuityPlayerInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private string __pbn__Nickname;

	private uint? __pbn__Level;

	private uint? __pbn__FrameId;

	private uint? __pbn__CustomHeadId;

	[ProtoMember(1, Name = "nickname")]
	[DefaultValue("")]
	public string Nickname
	{
		get
		{
			return __pbn__Nickname ?? "";
		}
		set
		{
			__pbn__Nickname = value;
		}
	}

	[ProtoMember(2, Name = "level")]
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

	[ProtoMember(3, Name = "frame_id")]
	public uint FrameId
	{
		get
		{
			return __pbn__FrameId.GetValueOrDefault();
		}
		set
		{
			__pbn__FrameId = value;
		}
	}

	[ProtoMember(4, Name = "custom_head_id")]
	public uint CustomHeadId
	{
		get
		{
			return __pbn__CustomHeadId.GetValueOrDefault();
		}
		set
		{
			__pbn__CustomHeadId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeNickname()
	{
		return __pbn__Nickname != null;
	}

	public void ResetNickname()
	{
		__pbn__Nickname = null;
	}

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}

	public bool ShouldSerializeFrameId()
	{
		return __pbn__FrameId.HasValue;
	}

	public void ResetFrameId()
	{
		__pbn__FrameId = null;
	}

	public bool ShouldSerializeCustomHeadId()
	{
		return __pbn__CustomHeadId.HasValue;
	}

	public void ResetCustomHeadId()
	{
		__pbn__CustomHeadId = null;
	}
}
