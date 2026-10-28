using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RecommendMissionPanel : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__PanelId;

	private bool? __pbn__IsPanelShow;

	private uint? __pbn__MissionBeginTime;

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

	[ProtoMember(2, Name = "is_panel_show")]
	public bool IsPanelShow
	{
		get
		{
			return __pbn__IsPanelShow.GetValueOrDefault();
		}
		set
		{
			__pbn__IsPanelShow = value;
		}
	}

	[ProtoMember(3, Name = "mission_begin_time")]
	public uint MissionBeginTime
	{
		get
		{
			return __pbn__MissionBeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__MissionBeginTime = value;
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

	public bool ShouldSerializeIsPanelShow()
	{
		return __pbn__IsPanelShow.HasValue;
	}

	public void ResetIsPanelShow()
	{
		__pbn__IsPanelShow = null;
	}

	public bool ShouldSerializeMissionBeginTime()
	{
		return __pbn__MissionBeginTime.HasValue;
	}

	public void ResetMissionBeginTime()
	{
		__pbn__MissionBeginTime = null;
	}
}
