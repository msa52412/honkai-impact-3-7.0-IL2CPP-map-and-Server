using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetDLCRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3551
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

	private string __pbn__Name;

	private uint? __pbn__Level;

	private uint? __pbn__Exp;

	private uint? __pbn__HasTakeRewardLevel;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "name")]
	[DefaultValue("")]
	public string Name
	{
		get
		{
			return __pbn__Name ?? "";
		}
		set
		{
			__pbn__Name = value;
		}
	}

	[ProtoMember(3, Name = "level")]
	public uint Level
	{
		get
		{
			return __pbn__Level.GetValueOrDefault();
		}
		set
		{
			__pbn__Level = value;
		}
	}

	[ProtoMember(4, Name = "exp")]
	public uint Exp
	{
		get
		{
			return __pbn__Exp.GetValueOrDefault();
		}
		set
		{
			__pbn__Exp = value;
		}
	}

	[ProtoMember(5, Name = "has_take_reward_level")]
	public uint HasTakeRewardLevel
	{
		get
		{
			return __pbn__HasTakeRewardLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__HasTakeRewardLevel = value;
		}
	}

	[ProtoMember(6, Name = "support_npc_list")]
	public List<DLCSupportNPC> SupportNpcLists { get; } = new List<DLCSupportNPC>();


	[ProtoMember(7, Name = "finished_dialog_id_list")]
	public uint[] FinishedDialogIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeName()
	{
		return __pbn__Name != null;
	}

	public void ResetName()
	{
		__pbn__Name = null;
	}

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}

	public bool ShouldSerializeExp()
	{
		return __pbn__Exp.HasValue;
	}

	public void ResetExp()
	{
		__pbn__Exp = null;
	}

	public bool ShouldSerializeHasTakeRewardLevel()
	{
		return __pbn__HasTakeRewardLevel.HasValue;
	}

	public void ResetHasTakeRewardLevel()
	{
		__pbn__HasTakeRewardLevel = null;
	}
}
