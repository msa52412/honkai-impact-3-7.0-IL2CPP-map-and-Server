using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MasterPupilCard : IExtensible
{
	private IExtension __pbn__extensionData;

	private MasterPupilType? __pbn__Type;

	private PlayerGenderType? __pbn__Gender;

	private PlayerAreaType? __pbn__Area;

	private PlayerInterestType? __pbn__Interest;

	private string __pbn__Declaration;

	private uint? __pbn__Uid;

	private bool? __pbn__IsRecommend;

	private uint? __pbn__Score;

	[ProtoMember(1, Name = "type")]
	[DefaultValue(MasterPupilType.MasterPupilNoneType)]
	public MasterPupilType Type
	{
		get
		{
			return __pbn__Type.GetValueOrDefault();
		}
		set
		{
			__pbn__Type = value;
		}
	}

	[ProtoMember(2, Name = "gender")]
	[DefaultValue(PlayerGenderType.GenderNoneType)]
	public PlayerGenderType Gender
	{
		get
		{
			return __pbn__Gender.GetValueOrDefault();
		}
		set
		{
			__pbn__Gender = value;
		}
	}

	[ProtoMember(3, Name = "area")]
	[DefaultValue(PlayerAreaType.AreaNoLimit)]
	public PlayerAreaType Area
	{
		get
		{
			return __pbn__Area.GetValueOrDefault();
		}
		set
		{
			__pbn__Area = value;
		}
	}

	[ProtoMember(4, Name = "interest")]
	[DefaultValue(PlayerInterestType.InterestNone)]
	public PlayerInterestType Interest
	{
		get
		{
			return __pbn__Interest.GetValueOrDefault();
		}
		set
		{
			__pbn__Interest = value;
		}
	}

	[ProtoMember(5, Name = "declaration")]
	[DefaultValue("")]
	public string Declaration
	{
		get
		{
			return __pbn__Declaration ?? "";
		}
		set
		{
			__pbn__Declaration = value;
		}
	}

	[ProtoMember(6, Name = "uid")]
	public uint Uid
	{
		get
		{
			return __pbn__Uid.GetValueOrDefault();
		}
		set
		{
			__pbn__Uid = value;
		}
	}

	[ProtoMember(7, Name = "is_recommend")]
	public bool IsRecommend
	{
		get
		{
			return __pbn__IsRecommend.GetValueOrDefault();
		}
		set
		{
			__pbn__IsRecommend = value;
		}
	}

	[ProtoMember(8, Name = "score")]
	public uint Score
	{
		get
		{
			return __pbn__Score.GetValueOrDefault();
		}
		set
		{
			__pbn__Score = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeType()
	{
		return __pbn__Type.HasValue;
	}

	public void ResetType()
	{
		__pbn__Type = null;
	}

	public bool ShouldSerializeGender()
	{
		return __pbn__Gender.HasValue;
	}

	public void ResetGender()
	{
		__pbn__Gender = null;
	}

	public bool ShouldSerializeArea()
	{
		return __pbn__Area.HasValue;
	}

	public void ResetArea()
	{
		__pbn__Area = null;
	}

	public bool ShouldSerializeInterest()
	{
		return __pbn__Interest.HasValue;
	}

	public void ResetInterest()
	{
		__pbn__Interest = null;
	}

	public bool ShouldSerializeDeclaration()
	{
		return __pbn__Declaration != null;
	}

	public void ResetDeclaration()
	{
		__pbn__Declaration = null;
	}

	public bool ShouldSerializeUid()
	{
		return __pbn__Uid.HasValue;
	}

	public void ResetUid()
	{
		__pbn__Uid = null;
	}

	public bool ShouldSerializeIsRecommend()
	{
		return __pbn__IsRecommend.HasValue;
	}

	public void ResetIsRecommend()
	{
		__pbn__IsRecommend = null;
	}

	public bool ShouldSerializeScore()
	{
		return __pbn__Score.HasValue;
	}

	public void ResetScore()
	{
		__pbn__Score = null;
	}
}
