using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class LoginMissionActivity : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Id;

	private uint? __pbn__UnlockDays;

	private uint? __pbn__LoginDays;

	[ProtoMember(1, Name = "id")]
	public uint Id
	{
		get
		{
			return __pbn__Id.GetValueOrDefault();
		}
		set
		{
			__pbn__Id = value;
		}
	}

	[ProtoMember(2, Name = "has_finished_mission_list")]
	public uint[] HasFinishedMissionLists { get; set; }

	[ProtoMember(3, Name = "unlock_days")]
	public uint UnlockDays
	{
		get
		{
			return __pbn__UnlockDays.GetValueOrDefault();
		}
		set
		{
			__pbn__UnlockDays = value;
		}
	}

	[ProtoMember(4, Name = "login_days")]
	public uint LoginDays
	{
		get
		{
			return __pbn__LoginDays.GetValueOrDefault();
		}
		set
		{
			__pbn__LoginDays = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeId()
	{
		return __pbn__Id.HasValue;
	}

	public void ResetId()
	{
		__pbn__Id = null;
	}

	public bool ShouldSerializeUnlockDays()
	{
		return __pbn__UnlockDays.HasValue;
	}

	public void ResetUnlockDays()
	{
		__pbn__UnlockDays = null;
	}

	public bool ShouldSerializeLoginDays()
	{
		return __pbn__LoginDays.HasValue;
	}

	public void ResetLoginDays()
	{
		__pbn__LoginDays = null;
	}
}
