using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class BuffAssistPublishBuffPoolReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5947
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__PoolId;

	private bool? __pbn__IsAnonymous;

	[ProtoMember(1, Name = "pool_id")]
	public uint PoolId
	{
		get
		{
			return __pbn__PoolId.GetValueOrDefault();
		}
		set
		{
			__pbn__PoolId = value;
		}
	}

	[ProtoMember(2, Name = "assist_word_id_list")]
	public uint[] AssistWordIdLists { get; set; }

	[ProtoMember(3, Name = "buff_pool")]
	public BuffAssistBuffPool BuffPool { get; set; }

	[ProtoMember(4, Name = "is_anonymous")]
	public bool IsAnonymous
	{
		get
		{
			return __pbn__IsAnonymous.GetValueOrDefault();
		}
		set
		{
			__pbn__IsAnonymous = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializePoolId()
	{
		return __pbn__PoolId.HasValue;
	}

	public void ResetPoolId()
	{
		__pbn__PoolId = null;
	}

	public bool ShouldSerializeIsAnonymous()
	{
		return __pbn__IsAnonymous.HasValue;
	}

	public void ResetIsAnonymous()
	{
		__pbn__IsAnonymous = null;
	}
}
