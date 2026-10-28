using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CabinType
{
	[ProtoEnum(Name = "CABIN_POWER")]
	CabinPower = 1,
	[ProtoEnum(Name = "CABIN_ENHANCE_KIANA")]
	CabinEnhanceKiana,
	[ProtoEnum(Name = "CABIN_COLLECT")]
	CabinCollect,
	[ProtoEnum(Name = "CABIN_MISC")]
	CabinMisc,
	[ProtoEnum(Name = "CABIN_VENTURE")]
	CabinVenture,
	[ProtoEnum(Name = "CABIN_ENHANCE_MEI")]
	CabinEnhanceMei,
	[ProtoEnum(Name = "CABIN_ENHANCE_BRONYA")]
	CabinEnhanceBronya,
	[ProtoEnum(Name = "CABIN_ENHANCE_HIMEKO")]
	CabinEnhanceHimeko,
	[ProtoEnum(Name = "CABIN_ENHANCE_THERESA")]
	CabinEnhanceTheresa,
	[ProtoEnum(Name = "CABIN_ENHANCE_FUKA")]
	CabinEnhanceFuka
}
