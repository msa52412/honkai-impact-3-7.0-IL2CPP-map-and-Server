using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ExBossSwitchData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__BossGroupId;

	[ProtoMember(1, Name = "boss_group_id")]
	public uint BossGroupId
	{
		get
		{
			return __pbn__BossGroupId.GetValueOrDefault();
		}
		set
		{
			__pbn__BossGroupId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBossGroupId()
	{
		return __pbn__BossGroupId.HasValue;
	}

	public void ResetBossGroupId()
	{
		__pbn__BossGroupId = null;
	}
}
