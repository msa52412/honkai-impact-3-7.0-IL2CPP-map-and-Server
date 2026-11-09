using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class InLevelMonsterActorSnapshot : IExtensible
{
	private IExtension __pbn__extensionData;

	private string __pbn__MonsterName;

	[ProtoMember(1, Name = "monster_name")]
	[DefaultValue("")]
	public string MonsterName
	{
		get
		{
			return __pbn__MonsterName ?? "";
		}
		set
		{
			__pbn__MonsterName = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMonsterName()
	{
		return __pbn__MonsterName != null;
	}

	public void ResetMonsterName()
	{
		__pbn__MonsterName = null;
	}
}
