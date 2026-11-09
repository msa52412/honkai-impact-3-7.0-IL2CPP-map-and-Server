using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;

namespace PemukulPaku.GameServer;

// TCP delivers arbitrary byte chunks, not protocol messages. Retain incomplete
// headers/bodies and use the authored extension/body lengths, never delimiter
// searches inside a protobuf payload. This does not change the wire format.
internal sealed class PacketFrameBuffer
{
    private const uint Head=0x01234567, Tail=0x89abcdef;
    private const int MaxBodyLength=16*1024*1024;
    private const int MaxFrameLength=38+ushort.MaxValue+MaxBodyLength;
    private byte[] buffer=new byte[65536];
    private int count;

    internal List<byte[]> Append(ReadOnlySpan<byte> bytes)
    {
        if(bytes.Length>65536)throw new InvalidDataException("Receive chunk exceeds socket read buffer");
        int required=checked(count+bytes.Length);
        if(required>MaxFrameLength+65536)throw new InvalidDataException("Receive buffer exceeds protocol limit");
        if(required>buffer.Length)Array.Resize(ref buffer,Math.Min(MaxFrameLength+65536,Math.Max(required,buffer.Length*2)));
        bytes.CopyTo(buffer.AsSpan(count));count=required;
        var frames=new List<byte[]>();int consumed=0;
        while(count-consumed>=4)
        {
            var remaining=buffer.AsSpan(consumed,count-consumed);
            if(BinaryPrimitives.ReadUInt32BigEndian(remaining)!=Head)throw new InvalidDataException("Invalid packet header");
            if(remaining.Length<34)break;
            int extra=BinaryPrimitives.ReadUInt16BigEndian(remaining.Slice(28));
            uint body=BinaryPrimitives.ReadUInt32BigEndian(remaining.Slice(30));
            if(body>MaxBodyLength)throw new InvalidDataException("Packet body exceeds protocol limit");
            int length=checked(38+extra+(int)body);
            if(remaining.Length<length)break;
            if(BinaryPrimitives.ReadUInt32BigEndian(remaining.Slice(length-4))!=Tail)throw new InvalidDataException("Invalid packet trailer");
            frames.Add(remaining.Slice(0,length).ToArray());consumed+=length;
        }
        if(consumed>0){buffer.AsSpan(consumed,count-consumed).CopyTo(buffer);count-=consumed;}
        return frames;
    }
}
