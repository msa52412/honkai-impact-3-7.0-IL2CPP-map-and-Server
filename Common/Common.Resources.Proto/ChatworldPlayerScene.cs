using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldPlayerScene : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__LeftHp;

	private uint? __pbn__Score;

	private uint? __pbn__LastLoginTime;

	[ProtoMember(1, Name = "left_hp")]
	public uint LeftHp
	{
		get
		{
			return __pbn__LeftHp.GetValueOrDefault();
		}
		set
		{
			__pbn__LeftHp = value;
		}
	}

	[ProtoMember(2, Name = "score")]
	public uint Score
	{
		get
		{
			return __pbn__Score.GetValueOrDefault();
		}
		set
		{
			__pbn__Score = value;
		}
	}

	[ProtoMember(3, Name = "last_login_time")]
	public uint LastLoginTime
	{
		get
		{
			return __pbn__LastLoginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__LastLoginTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeLeftHp()
	{
		return __pbn__LeftHp.HasValue;
	}

	public void ResetLeftHp()
	{
		__pbn__LeftHp = null;
	}

	public bool ShouldSerializeScore()
	{
		return __pbn__Score.HasValue;
	}

	public void ResetScore()
	{
		__pbn__Score = null;
	}

	public bool ShouldSerializeLastLoginTime()
	{
		return __pbn__LastLoginTime.HasValue;
	}

	public void ResetLastLoginTime()
	{
		__pbn__LastLoginTime = null;
	}
}
