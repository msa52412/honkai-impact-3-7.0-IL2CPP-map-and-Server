using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ArmadaOptionalLeader : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Id;

	private uint? __pbn__Contribution;

	[ProtoMember(1, Name = "id")]
	public uint Id
	{
		get
		{
			return __pbn__Id.GetValueOrDefault();
		}
		set
		{
			__pbn__Id = value;
		}
	}

	[ProtoMember(2, Name = "contribution")]
	public uint Contribution
	{
		get
		{
			return __pbn__Contribution.GetValueOrDefault();
		}
		set
		{
			__pbn__Contribution = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeId()
	{
		return __pbn__Id.HasValue;
	}

	public void ResetId()
	{
		__pbn__Id = null;
	}

	public bool ShouldSerializeContribution()
	{
		return __pbn__Contribution.HasValue;
	}

	public void ResetContribution()
	{
		__pbn__Contribution = null;
	}
}
