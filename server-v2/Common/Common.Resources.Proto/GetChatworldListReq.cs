using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetChatworldListReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2411
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__StartId;

	private uint? __pbn__EndId;

	private uint? __pbn__SceneId;

	[ProtoMember(1, Name = "start_id")]
	public uint StartId
	{
		get
		{
			return __pbn__StartId.GetValueOrDefault();
		}
		set
		{
			__pbn__StartId = value;
		}
	}

	[ProtoMember(2, Name = "end_id")]
	public uint EndId
	{
		get
		{
			return __pbn__EndId.GetValueOrDefault();
		}
		set
		{
			__pbn__EndId = value;
		}
	}

	[ProtoMember(3, Name = "scene_id")]
	public uint SceneId
	{
		get
		{
			return __pbn__SceneId.GetValueOrDefault();
		}
		set
		{
			__pbn__SceneId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeStartId()
	{
		return __pbn__StartId.HasValue;
	}

	public void ResetStartId()
	{
		__pbn__StartId = null;
	}

	public bool ShouldSerializeEndId()
	{
		return __pbn__EndId.HasValue;
	}

	public void ResetEndId()
	{
		__pbn__EndId = null;
	}

	public bool ShouldSerializeSceneId()
	{
		return __pbn__SceneId.HasValue;
	}

	public void ResetSceneId()
	{
		__pbn__SceneId = null;
	}
}
