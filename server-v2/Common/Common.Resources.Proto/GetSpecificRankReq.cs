using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetSpecificRankReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1684
	}

	private IExtension __pbn__extensionData;

	private SpecificRankType? __pbn__Type;

	private uint? __pbn__Param;

	[ProtoMember(1, Name = "type")]
	[DefaultValue(SpecificRankType.SpecificRankNone)]
	public SpecificRankType Type
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

	[ProtoMember(2, Name = "param")]
	public uint Param
	{
		get
		{
			return __pbn__Param.GetValueOrDefault();
		}
		set
		{
			__pbn__Param = value;
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

	public bool ShouldSerializeParam()
	{
		return __pbn__Param.HasValue;
	}

	public void ResetParam()
	{
		__pbn__Param = null;
	}
}
