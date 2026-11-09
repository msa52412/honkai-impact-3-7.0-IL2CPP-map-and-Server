using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class EndlessSelectTypeReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3462
	}

	private IExtension __pbn__extensionData;

	private EndlessType? __pbn__EndlessType;

	private uint? __pbn__Level;

	[ProtoMember(1, Name = "endless_type")]
	[DefaultValue(EndlessType.EndlessTypeNone)]
	public EndlessType EndlessType
	{
		get
		{
			return __pbn__EndlessType.GetValueOrDefault();
		}
		set
		{
			__pbn__EndlessType = value;
		}
	}

	[ProtoMember(2, Name = "level")]
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeEndlessType()
	{
		return __pbn__EndlessType.HasValue;
	}

	public void ResetEndlessType()
	{
		__pbn__EndlessType = null;
	}

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}
}
