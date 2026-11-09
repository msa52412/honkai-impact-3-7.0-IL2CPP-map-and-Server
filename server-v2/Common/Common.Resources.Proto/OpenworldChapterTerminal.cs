using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldChapterTerminal : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Level;

	private uint? __pbn__Exp;

	private bool? __pbn__IsPeriodUpgraded;

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

	[ProtoMember(2, Name = "exp")]
	public uint Exp
	{
		get
		{
			return __pbn__Exp.GetValueOrDefault();
		}
		set
		{
			__pbn__Exp = value;
		}
	}

	[ProtoMember(3, Name = "is_period_upgraded")]
	public bool IsPeriodUpgraded
	{
		get
		{
			return __pbn__IsPeriodUpgraded.GetValueOrDefault();
		}
		set
		{
			__pbn__IsPeriodUpgraded = value;
		}
	}

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

	public bool ShouldSerializeExp()
	{
		return __pbn__Exp.HasValue;
	}

	public void ResetExp()
	{
		__pbn__Exp = null;
	}

	public bool ShouldSerializeIsPeriodUpgraded()
	{
		return __pbn__IsPeriodUpgraded.HasValue;
	}

	public void ResetIsPeriodUpgraded()
	{
		__pbn__IsPeriodUpgraded = null;
	}
}
