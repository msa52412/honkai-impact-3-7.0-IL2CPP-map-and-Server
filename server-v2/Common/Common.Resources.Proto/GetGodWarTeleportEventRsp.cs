using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetGodWarTeleportEventRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6198
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "FEATURE_CLOSE")]
		FeatureClose,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen,
		[ProtoEnum(Name = "FORCE_SETTLE")]
		ForceSettle
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__GodWarId;

	private uint? __pbn__TaleId;

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

	[ProtoMember(3, Name = "tale_id")]
	public uint TaleId
	{
		get
		{
			return __pbn__TaleId.GetValueOrDefault();
		}
		set
		{
			__pbn__TaleId = value;
		}
	}

	[ProtoMember(4, Name = "teleport_id_list")]
	public uint[] TeleportIdLists { get; set; }

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

	public bool ShouldSerializeTaleId()
	{
		return __pbn__TaleId.HasValue;
	}

	public void ResetTaleId()
	{
		__pbn__TaleId = null;
	}
}
