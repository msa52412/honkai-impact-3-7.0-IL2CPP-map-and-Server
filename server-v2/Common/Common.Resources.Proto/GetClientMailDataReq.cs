using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetClientMailDataReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3800
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Start;

	private uint? __pbn__Stop;

	private ClientMailFilterType? __pbn__FilterType;

	[ProtoMember(1, Name = "start")]
	public uint Start
	{
		get
		{
			return __pbn__Start.GetValueOrDefault();
		}
		set
		{
			__pbn__Start = value;
		}
	}

	[ProtoMember(2, Name = "stop")]
	public uint Stop
	{
		get
		{
			return __pbn__Stop.GetValueOrDefault();
		}
		set
		{
			__pbn__Stop = value;
		}
	}

	[ProtoMember(3, Name = "filter_type")]
	[DefaultValue(ClientMailFilterType.ClientMailFilterTypeAll)]
	public ClientMailFilterType FilterType
	{
		get
		{
			return __pbn__FilterType.GetValueOrDefault();
		}
		set
		{
			__pbn__FilterType = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeStart()
	{
		return __pbn__Start.HasValue;
	}

	public void ResetStart()
	{
		__pbn__Start = null;
	}

	public bool ShouldSerializeStop()
	{
		return __pbn__Stop.HasValue;
	}

	public void ResetStop()
	{
		__pbn__Stop = null;
	}

	public bool ShouldSerializeFilterType()
	{
		return __pbn__FilterType.HasValue;
	}

	public void ResetFilterType()
	{
		__pbn__FilterType = null;
	}
}
