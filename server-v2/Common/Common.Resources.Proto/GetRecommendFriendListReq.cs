using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetRecommendFriendListReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 76
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Level;

	[ProtoMember(1, Name = "level")]
	public uint Level
	{
		get
		{
			return __pbn__Level.GetValueOrDefault();
		}
		set
		{
			__pbn__Level = value;
		}
	}

	[ProtoMember(2, Name = "filter_uid_list")]
	public uint[] FilterUidLists { get; set; }

	[ProtoMember(3, Name = "recommend_uid_list")]
	public uint[] RecommendUidLists { get; set; }

	[ProtoMember(4, Name = "mp_recommend_uid_list")]
	public uint[] MpRecommendUidLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}
}
