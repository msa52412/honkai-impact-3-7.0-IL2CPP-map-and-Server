using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RpgTaleSetOverallReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2386
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TaleId;

	private uint? __pbn__OverallId;

	private uint? __pbn__Value;

	[ProtoMember(1, Name = "tale_id")]
	public uint TaleId
	{
		get
		{
			return __pbn__TaleId.GetValueOrDefault();
		}
		set
		{
			__pbn__TaleId = value;
		}
	}

	[ProtoMember(2, Name = "overall_id")]
	public uint OverallId
	{
		get
		{
			return __pbn__OverallId.GetValueOrDefault();
		}
		set
		{
			__pbn__OverallId = value;
		}
	}

	[ProtoMember(3, Name = "value")]
	public uint Value
	{
		get
		{
			return __pbn__Value.GetValueOrDefault();
		}
		set
		{
			__pbn__Value = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTaleId()
	{
		return __pbn__TaleId.HasValue;
	}

	public void ResetTaleId()
	{
		__pbn__TaleId = null;
	}

	public bool ShouldSerializeOverallId()
	{
		return __pbn__OverallId.HasValue;
	}

	public void ResetOverallId()
	{
		__pbn__OverallId = null;
	}

	public bool ShouldSerializeValue()
	{
		return __pbn__Value.HasValue;
	}

	public void ResetValue()
	{
		__pbn__Value = null;
	}
}
