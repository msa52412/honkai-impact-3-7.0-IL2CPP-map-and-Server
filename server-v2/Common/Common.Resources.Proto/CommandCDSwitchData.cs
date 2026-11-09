using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class CommandCDSwitchData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__CmdId;

	private uint? __pbn__Cd;

	[ProtoMember(1, Name = "cmd_id")]
	public uint CmdId
	{
		get
		{
			return __pbn__CmdId.GetValueOrDefault();
		}
		set
		{
			__pbn__CmdId = value;
		}
	}

	[ProtoMember(2, Name = "cd")]
	public uint Cd
	{
		get
		{
			return __pbn__Cd.GetValueOrDefault();
		}
		set
		{
			__pbn__Cd = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeCmdId()
	{
		return __pbn__CmdId.HasValue;
	}

	public void ResetCmdId()
	{
		__pbn__CmdId = null;
	}

	public bool ShouldSerializeCd()
	{
		return __pbn__Cd.HasValue;
	}

	public void ResetCd()
	{
		__pbn__Cd = null;
	}
}
