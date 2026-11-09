using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum WareHouseStatus
{
	[ProtoEnum(Name = "WAREHOUSE_STATUS_NONE")]
	WarehouseStatusNone,
	[ProtoEnum(Name = "WAREHOUSE_STATUS_NORMAL")]
	WarehouseStatusNormal,
	[ProtoEnum(Name = "WAREHOUSE_STATUS_DEMANDING")]
	WarehouseStatusDemanding,
	[ProtoEnum(Name = "WAREHOUSE_STATUS_FINISH")]
	WarehouseStatusFinish,
	[ProtoEnum(Name = "WAREHOUSE_STATUS_CD")]
	WarehouseStatusCd
}
