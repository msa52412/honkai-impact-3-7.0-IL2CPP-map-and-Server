using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TriggerBurdenAlleviationReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4354
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__AlleviationId;

	[ProtoMember(1, Name = "alleviation_id")]
	public uint AlleviationId
	{
		get
		{
			return __pbn__AlleviationId.GetValueOrDefault();
		}
		set
		{
			__pbn__AlleviationId = value;
		}
	}

	[ProtoMember(2, Name = "para_list")]
	public uint[] ParaLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAlleviationId()
	{
		return __pbn__AlleviationId.HasValue;
	}

	public void ResetAlleviationId()
	{
		__pbn__AlleviationId = null;
	}
}
