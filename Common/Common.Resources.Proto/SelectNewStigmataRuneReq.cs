using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SelectNewStigmataRuneReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 195
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__UniqueId;

	private bool? __pbn__IsSelect;

	private StigmataRefineTimesType? __pbn__TimesType;

	private uint? __pbn__SelectUniqueId;

	[ProtoMember(1, Name = "unique_id")]
	public uint UniqueId
	{
		get
		{
			return __pbn__UniqueId.GetValueOrDefault();
		}
		set
		{
			__pbn__UniqueId = value;
		}
	}

	[ProtoMember(2, Name = "is_select")]
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

	[ProtoMember(3, Name = "times_type")]
	[DefaultValue(StigmataRefineTimesType.StigmataRefineTimesOne)]
	public StigmataRefineTimesType TimesType
	{
		get
		{
			return __pbn__TimesType ?? StigmataRefineTimesType.StigmataRefineTimesOne;
		}
		set
		{
			__pbn__TimesType = value;
		}
	}

	[ProtoMember(4, Name = "select_unique_id")]
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeUniqueId()
	{
		return __pbn__UniqueId.HasValue;
	}

	public void ResetUniqueId()
	{
		__pbn__UniqueId = null;
	}

	public bool ShouldSerializeIsSelect()
	{
		return __pbn__IsSelect.HasValue;
	}

	public void ResetIsSelect()
	{
		__pbn__IsSelect = null;
	}

	public bool ShouldSerializeTimesType()
	{
		return __pbn__TimesType.HasValue;
	}

	public void ResetTimesType()
	{
		__pbn__TimesType = null;
	}

	public bool ShouldSerializeSelectUniqueId()
	{
		return __pbn__SelectUniqueId.HasValue;
	}

	public void ResetSelectUniqueId()
	{
		__pbn__SelectUniqueId = null;
	}
}
