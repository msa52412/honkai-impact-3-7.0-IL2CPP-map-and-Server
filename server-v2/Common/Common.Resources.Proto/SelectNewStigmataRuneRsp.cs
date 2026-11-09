using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SelectNewStigmataRuneRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 196
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "STIGMATA_NOT_EXIST")]
		StigmataNotExist,
		[ProtoEnum(Name = "NO_NEW_RUNE")]
		NoNewRune,
		[ProtoEnum(Name = "SELECT_EMPTY")]
		SelectEmpty,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen,
		[ProtoEnum(Name = "TOO_FAST")]
		TooFast
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__SelectUniqueId;

	private bool? __pbn__IsSelect;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "select_unique_id")]
	public uint SelectUniqueId
	{
		get
		{
			return __pbn__SelectUniqueId.GetValueOrDefault();
		}
		set
		{
			__pbn__SelectUniqueId = value;
		}
	}

	[ProtoMember(3, Name = "is_select")]
	public bool IsSelect
	{
		get
		{
			return __pbn__IsSelect.GetValueOrDefault();
		}
		set
		{
			__pbn__IsSelect = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSelectUniqueId()
	{
		return __pbn__SelectUniqueId.HasValue;
	}

	public void ResetSelectUniqueId()
	{
		__pbn__SelectUniqueId = null;
	}

	public bool ShouldSerializeIsSelect()
	{
		return __pbn__IsSelect.HasValue;
	}

	public void ResetIsSelect()
	{
		__pbn__IsSelect = null;
	}
}
