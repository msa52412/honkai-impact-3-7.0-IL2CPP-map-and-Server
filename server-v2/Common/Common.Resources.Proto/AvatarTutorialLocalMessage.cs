using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AvatarTutorialLocalMessage : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "entered_avatar_tutorial_activity_id_list")]
	public uint[] EnteredAvatarTutorialActivityIdLists { get; set; }

	[ProtoMember(2, Name = "entered_newbie_guide_tutorial_id_list")]
	public uint[] EnteredNewbieGuideTutorialIdLists { get; set; }

	[ProtoMember(3, Name = "finished_picture_tutorial_list")]
	public List<PictureTutorial> FinishedPictureTutorialLists { get; } = new List<PictureTutorial>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
