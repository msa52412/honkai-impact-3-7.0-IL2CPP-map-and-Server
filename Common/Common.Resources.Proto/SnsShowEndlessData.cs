using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SnsShowEndlessData : IExtensible
{
	private IExtension __pbn__extensionData;

	private EndlessType? __pbn__CurType;

	[ProtoMember(1, Name = "cur_type")]
	[DefaultValue(EndlessType.EndlessTypeNone)]
	public EndlessType CurType
	{
		get
		{
			return __pbn__CurType.GetValueOrDefault();
		}
		set
		{
			__pbn__CurType = value;
		}
	}

	[ProtoMember(3, Name = "openworld_endless_player_data")]
	public OpenworldEndlessPlayer OpenworldEndlessPlayerData { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeCurType()
	{
		return __pbn__CurType.HasValue;
	}

	public void ResetCurType()
	{
		__pbn__CurType = null;
	}
}
