using System;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer;

[AttributeUsage(AttributeTargets.Class)]
public class PacketCmdId : Attribute
{
	public CmdId Id { get; }

	public PacketCmdId(CmdId id)
	{
		Id = id;
	}
}
