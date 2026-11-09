using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ExBossEnterSwitchData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ExBossId;

	[ProtoMember(1, Name = "ex_boss_id")]
	public uint ExBossId
	{
		get
		{
			return __pbn__ExBossId.GetValueOrDefault();
		}
		set
		{
			__pbn__ExBossId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeExBossId()
	{
		return __pbn__ExBossId.HasValue;
	}

	public void ResetExBossId()
	{
		__pbn__ExBossId = null;
	}
}
