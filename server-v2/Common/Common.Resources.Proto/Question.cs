using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class Question : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Id;

	[ProtoMember(1, Name = "id")]
	public uint Id
	{
		get
		{
			return __pbn__Id.GetValueOrDefault();
		}
		set
		{
			__pbn__Id = value;
		}
	}

	[ProtoMember(2, Name = "answer_list")]
	public uint[] AnswerLists { get; set; }

	[ProtoMember(3, Name = "right_answer_list")]
	public uint[] RightAnswerLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeId()
	{
		return __pbn__Id.HasValue;
	}

	public void ResetId()
	{
		__pbn__Id = null;
	}
}
