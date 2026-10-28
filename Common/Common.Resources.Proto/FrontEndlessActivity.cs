using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class FrontEndlessActivity : IExtensible
{
	[ProtoContract]
	public class FloorScore : IExtensible
	{
		private IExtension __pbn__extensionData;

		private uint? __pbn__Floor;

		private uint? __pbn__Score;

		[ProtoMember(1, Name = "floor")]
		public uint Floor
		{
			get
			{
				return __pbn__Floor.GetValueOrDefault();
			}
			set
			{
				__pbn__Floor = value;
			}
		}

		[ProtoMember(2, Name = "score")]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
		}

		public bool ShouldSerializeFloor()
		{
			return __pbn__Floor.HasValue;
		}

		public void ResetFloor()
		{
			__pbn__Floor = null;
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

	private IExtension __pbn__extensionData;

	private uint? __pbn__CurFloor;

	[ProtoMember(1, Name = "cur_floor")]
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

	[ProtoMember(2, Name = "floor_score_list")]
	public List<FloorScore> FloorScoreLists { get; } = new List<FloorScore>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeCurFloor()
	{
		return __pbn__CurFloor.HasValue;
	}

	public void ResetCurFloor()
	{
		__pbn__CurFloor = null;
	}
}
