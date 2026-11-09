using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ExBossSweepReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1655
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__BossId;

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
}
