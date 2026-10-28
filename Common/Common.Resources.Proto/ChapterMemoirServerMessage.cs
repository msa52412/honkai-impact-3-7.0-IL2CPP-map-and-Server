using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChapterMemoirServerMessage : IExtensible
{
	private IExtension __pbn__extensionData;

	private bool? __pbn__HasAccessEnderPanel;

	[ProtoMember(1, Name = "has_access_ender_panel")]
	public bool HasAccessEnderPanel
	{
		get
		{
			return __pbn__HasAccessEnderPanel.GetValueOrDefault();
		}
		set
		{
			__pbn__HasAccessEnderPanel = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeHasAccessEnderPanel()
	{
		return __pbn__HasAccessEnderPanel.HasValue;
	}

	public void ResetHasAccessEnderPanel()
	{
		__pbn__HasAccessEnderPanel = null;
	}
}
