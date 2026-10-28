using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ActivityPanelSwitchData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__PanelId;

	[ProtoMember(1, Name = "panel_id")]
	public uint PanelId
	{
		get
		{
			return __pbn__PanelId.GetValueOrDefault();
		}
		set
		{
			__pbn__PanelId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializePanelId()
	{
		return __pbn__PanelId.HasValue;
	}

	public void ResetPanelId()
	{
		__pbn__PanelId = null;
	}
}
