using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ReportRpgTaleStageReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2374
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__StageId;

	private uint? __pbn__CurFloor;

	private string __pbn__ClientData;

	private uint? __pbn__TaleId;

	private uint? __pbn__Score;

	[ProtoMember(1, Name = "stage_id")]
	public uint StageId
	{
		get
		{
			return __pbn__StageId.GetValueOrDefault();
		}
		set
		{
			__pbn__StageId = value;
		}
	}

	[ProtoMember(2, Name = "cur_floor")]
	public uint CurFloor
	{
		get
		{
			return __pbn__CurFloor.GetValueOrDefault();
		}
		set
		{
			__pbn__CurFloor = value;
		}
	}

	[ProtoMember(3, Name = "client_data")]
	[DefaultValue("")]
	public string ClientData
	{
		get
		{
			return __pbn__ClientData ?? "";
		}
		set
		{
			__pbn__ClientData = value;
		}
	}

	[ProtoMember(4, Name = "tale_id")]
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

	[ProtoMember(5, Name = "score")]
	public uint Score
	{
		get
		{
			return __pbn__Score.GetValueOrDefault();
		}
		set
		{
			__pbn__Score = value;
		}
	}

	[ProtoMember(6, Name = "monster_unique_id_list")]
	public uint[] MonsterUniqueIdLists { get; set; }

	[ProtoMember(7, Name = "monster_card_unique_id_list")]
	public uint[] MonsterCardUniqueIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeStageId()
	{
		return __pbn__StageId.HasValue;
	}

	public void ResetStageId()
	{
		__pbn__StageId = null;
	}

	public bool ShouldSerializeCurFloor()
	{
		return __pbn__CurFloor.HasValue;
	}

	public void ResetCurFloor()
	{
		__pbn__CurFloor = null;
	}

	public bool ShouldSerializeClientData()
	{
		return __pbn__ClientData != null;
	}

	public void ResetClientData()
	{
		__pbn__ClientData = null;
	}

	public bool ShouldSerializeTaleId()
	{
		return __pbn__TaleId.HasValue;
	}

	public void ResetTaleId()
	{
		__pbn__TaleId = null;
	}

	public bool ShouldSerializeScore()
	{
		return __pbn__Score.HasValue;
	}

	public void ResetScore()
	{
		__pbn__Score = null;
	}
}
