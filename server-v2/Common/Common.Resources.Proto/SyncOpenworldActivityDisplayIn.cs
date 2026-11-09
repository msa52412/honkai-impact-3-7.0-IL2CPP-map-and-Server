using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SyncOpenworldActivityDisplayInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__AddBaseActivityExp;

	private uint? __pbn__AddBuffActivityExp;

	[ProtoMember(1, Name = "add_base_activity_exp")]
	public uint AddBaseActivityExp
	{
		get
		{
			return __pbn__AddBaseActivityExp.GetValueOrDefault();
		}
		set
		{
			__pbn__AddBaseActivityExp = value;
		}
	}

	[ProtoMember(2, Name = "add_buff_activity_exp")]
	public uint AddBuffActivityExp
	{
		get
		{
			return __pbn__AddBuffActivityExp.GetValueOrDefault();
		}
		set
		{
			__pbn__AddBuffActivityExp = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAddBaseActivityExp()
	{
		return __pbn__AddBaseActivityExp.HasValue;
	}

	public void ResetAddBaseActivityExp()
	{
		__pbn__AddBaseActivityExp = null;
	}

	public bool ShouldSerializeAddBuffActivityExp()
	{
		return __pbn__AddBuffActivityExp.HasValue;
	}

	public void ResetAddBuffActivityExp()
	{
		__pbn__AddBuffActivityExp = null;
	}
}
