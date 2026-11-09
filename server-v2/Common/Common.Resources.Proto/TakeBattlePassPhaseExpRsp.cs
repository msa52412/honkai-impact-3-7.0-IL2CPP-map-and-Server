using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeBattlePassPhaseExpRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3759
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen,
		[ProtoEnum(Name = "HAS_TAKE")]
		HasTake
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__AddExp;

	private uint? __pbn__AddThemeExp;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "add_exp")]
	public uint AddExp
	{
		get
		{
			return __pbn__AddExp.GetValueOrDefault();
		}
		set
		{
			__pbn__AddExp = value;
		}
	}

	[ProtoMember(3, Name = "add_theme_exp")]
	public uint AddThemeExp
	{
		get
		{
			return __pbn__AddThemeExp.GetValueOrDefault();
		}
		set
		{
			__pbn__AddThemeExp = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAddExp()
	{
		return __pbn__AddExp.HasValue;
	}

	public void ResetAddExp()
	{
		__pbn__AddExp = null;
	}

	public bool ShouldSerializeAddThemeExp()
	{
		return __pbn__AddThemeExp.HasValue;
	}

	public void ResetAddThemeExp()
	{
		__pbn__AddThemeExp = null;
	}
}
