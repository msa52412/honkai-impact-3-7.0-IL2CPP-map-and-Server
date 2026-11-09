using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetGodWarRoleRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6196
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__GodWarId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "god_war_id")]
	public uint GodWarId
	{
		get
		{
			return __pbn__GodWarId.GetValueOrDefault();
		}
		set
		{
			__pbn__GodWarId = value;
		}
	}

	[ProtoMember(3, Name = "role_info")]
	public GodWarRoleInfo RoleInfo { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeGodWarId()
	{
		return __pbn__GodWarId.HasValue;
	}

	public void ResetGodWarId()
	{
		__pbn__GodWarId = null;
	}
}
