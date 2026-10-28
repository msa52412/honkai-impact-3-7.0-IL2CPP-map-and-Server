using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SlgMainPageReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6069
	}

	private IExtension __pbn__extensionData;

	private bool? __pbn__IsInMainPage;

	[ProtoMember(1, Name = "is_in_main_page")]
	public bool IsInMainPage
	{
		get
		{
			return __pbn__IsInMainPage.GetValueOrDefault();
		}
		set
		{
			__pbn__IsInMainPage = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeIsInMainPage()
	{
		return __pbn__IsInMainPage.HasValue;
	}

	public void ResetIsInMainPage()
	{
		__pbn__IsInMainPage = null;
	}
}
