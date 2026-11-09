using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class PictureTutorial : IExtensible
{
	private IExtension __pbn__extensionData;

	private int? __pbn__ActivityId;

	[ProtoMember(1, Name = "activity_id")]
	public int ActivityId
	{
		get
		{
			return __pbn__ActivityId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityId = value;
		}
	}

	[ProtoMember(2, Name = "tutorial_id_list")]
	public int[] TutorialIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeActivityId()
	{
		return __pbn__ActivityId.HasValue;
	}

	public void ResetActivityId()
	{
		__pbn__ActivityId = null;
	}
}
