using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QAvatarBattleRefreshGadgetGenerateNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5623
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__RefreshId;

	[ProtoMember(1, Name = "refresh_id")]
	public uint RefreshId
	{
		get
		{
			return __pbn__RefreshId.GetValueOrDefault();
		}
		set
		{
			__pbn__RefreshId = value;
		}
	}

	[ProtoMember(2, Name = "refresh_gadget_list")]
	public List<QAvatarBattleRefreshGadget> RefreshGadgetLists { get; } = new List<QAvatarBattleRefreshGadget>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeRefreshId()
	{
		return __pbn__RefreshId.HasValue;
	}

	public void ResetRefreshId()
	{
		__pbn__RefreshId = null;
	}
}
