using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GeneralAvatarActivityServeLocalMessage : IExtensible
{
	private IExtension __pbn__extensionData;

	private bool? __pbn__EnteredSourecPage;

	[ProtoMember(1, Name = "entered_sourec_page")]
	public bool EnteredSourecPage
	{
		get
		{
			return __pbn__EnteredSourecPage.GetValueOrDefault();
		}
		set
		{
			__pbn__EnteredSourecPage = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeEnteredSourecPage()
	{
		return __pbn__EnteredSourecPage.HasValue;
	}

	public void ResetEnteredSourecPage()
	{
		__pbn__EnteredSourecPage = null;
	}
}
