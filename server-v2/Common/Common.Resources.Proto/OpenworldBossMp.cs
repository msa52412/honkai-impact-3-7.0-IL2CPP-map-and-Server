using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldBossMp : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__BossId;

	private uint? __pbn__LeftHp;

	private uint? __pbn__MpLeftHp;

	[ProtoMember(1, Name = "boss_id")]
	public uint BossId
	{
		get
		{
			return __pbn__BossId.GetValueOrDefault();
		}
		set
		{
			__pbn__BossId = value;
		}
	}

	[ProtoMember(2, Name = "left_hp")]
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

	[ProtoMember(3, Name = "mp_left_hp")]
	public uint MpLeftHp
	{
		get
		{
			return __pbn__MpLeftHp.GetValueOrDefault();
		}
		set
		{
			__pbn__MpLeftHp = value;
		}
	}

	[ProtoMember(4, Name = "buff_list")]
	public uint[] BuffLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBossId()
	{
		return __pbn__BossId.HasValue;
	}

	public void ResetBossId()
	{
		__pbn__BossId = null;
	}

	public bool ShouldSerializeLeftHp()
	{
		return __pbn__LeftHp.HasValue;
	}

	public void ResetLeftHp()
	{
		__pbn__LeftHp = null;
	}

	public bool ShouldSerializeMpLeftHp()
	{
		return __pbn__MpLeftHp.HasValue;
	}

	public void ResetMpLeftHp()
	{
		__pbn__MpLeftHp = null;
	}
}
