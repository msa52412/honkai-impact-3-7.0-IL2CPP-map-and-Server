using System;
using System.Buffers.Binary;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Common.Resources.Proto;
using Common.Utils;
using ProtoBuf;

namespace PemukulPaku.GameServer;

public class Packet
{
	public readonly byte[] Raw;

	public readonly byte[] HeadMagic;

	public readonly uint UserId;

	public readonly uint CmdId;

	public readonly uint BodyLen;

	public readonly byte[] Body;

	public readonly byte[] TailMagic;

	public static readonly Logger c = new Logger("Packet", ConsoleColor.Magenta);

	public Packet(byte[] buf)
	{
		Raw = buf;
		CmdId = BinaryPrimitives.ReadUInt32BigEndian(buf.AsSpan(24));
		string name = Enum.GetName(typeof(CmdId), CmdId);
		if (name == null)
		{
			Logger logger = c;
			string[] array = new string[1];
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(22, 1);
			defaultInterpolatedStringHandler.AppendLiteral("CmdId ");
			defaultInterpolatedStringHandler.AppendFormatted(CmdId);
			defaultInterpolatedStringHandler.AppendLiteral(" not recognized!");
			array[0] = defaultInterpolatedStringHandler.ToStringAndClear();
			logger.Warn(array);
		}
		HeadMagic = buf.Take(4).ToArray();
		UserId = BinaryPrimitives.ReadUInt32BigEndian(buf.AsSpan(12));
		ushort num = BinaryPrimitives.ReadUInt16BigEndian(buf.AsSpan(28));
		BodyLen = BinaryPrimitives.ReadUInt32BigEndian(buf.AsSpan(30));
		Body = buf.Skip(34 + num).Take((int)BodyLen).ToArray();
		TailMagic = buf.Skip(buf.Length - 4).ToArray();
	}

	public T GetDecodedBody<T>()
	{
		T result = default(T);
		try
		{
			MemoryStream source = new MemoryStream(Body);
			result = Serializer.Deserialize<T>(source);
			return result;
		}
		catch
		{
			string name = Enum.GetName(typeof(CmdId), CmdId);
			c.Error("Failed to deserialize " + (name ?? CmdId.ToString()) + "!");
		}
		return result;
	}

	public static bool IsValid(byte[] data)
	{
		string text = BitConverter.ToString(data).Replace("-", "");
		return text.StartsWith("01234567", StringComparison.OrdinalIgnoreCase) && text.EndsWith("89ABCDEF", StringComparison.OrdinalIgnoreCase);
	}

	public static Packet FromProto<T>(T proto, CmdId cmdId)
	{
		MemoryStream memoryStream = new MemoryStream();
		Serializer.Serialize((Stream)memoryStream, proto);
		byte[] array = memoryStream.ToArray();
		byte[] array2 = new byte[38 + array.Length];
		Array.Fill(array2, (byte)0);
		BinaryPrimitives.WriteUInt32BigEndian(array2, 19088743u);
		BinaryPrimitives.WriteUInt16BigEndian(array2.AsSpan(4), 1);
		BinaryPrimitives.WriteUInt16BigEndian(array2.AsSpan(6), 0);
		BinaryPrimitives.WriteUInt32BigEndian(array2.AsSpan(8), 0u);
		BinaryPrimitives.WriteUInt32BigEndian(array2.AsSpan(12), 0u);
		BinaryPrimitives.WriteUInt32BigEndian(array2.AsSpan(16), 0u);
		BinaryPrimitives.WriteUInt32BigEndian(array2.AsSpan(20), 0u);
		BinaryPrimitives.WriteUInt32BigEndian(array2.AsSpan(24), (uint)cmdId);
		BinaryPrimitives.WriteUInt16BigEndian(array2.AsSpan(28), 0);
		BinaryPrimitives.WriteUInt32BigEndian(array2.AsSpan(30), (uint)array.Length);
		array.CopyTo(array2.AsSpan(34));
		BinaryPrimitives.WriteUInt32BigEndian(array2.AsSpan(34 + array.Length), 2309737967u);
		return new Packet(array2);
	}
}
