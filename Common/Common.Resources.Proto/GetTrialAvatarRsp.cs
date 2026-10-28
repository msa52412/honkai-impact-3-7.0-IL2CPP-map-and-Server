using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetTrialAvatarRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 586
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail
	}

	private IExtension __pbn__extensionData;

	private bool? __pbn__IsAllUpdate;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "avatar_list")]
	public List<TrialAvatar> AvatarLists { get; } = new List<TrialAvatar>();


	[ProtoMember(3, Name = "is_all_update")]
	public bool IsAllUpdate
	{
		get
		{
			return __pbn__IsAllUpdate.GetValueOrDefault();
		}
		set
		{
			__pbn__IsAllUpdate = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeIsAllUpdate()
	{
		return __pbn__IsAllUpdate.HasValue;
	}

	public void ResetIsAllUpdate()
	{
		__pbn__IsAllUpdate = null;
	}
}
