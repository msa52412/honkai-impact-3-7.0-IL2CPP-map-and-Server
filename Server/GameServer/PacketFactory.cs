using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Common.Resources.Proto;
using Common.Utils;

namespace PemukulPaku.GameServer;

public static class PacketFactory
{
	public static readonly Dictionary<CmdId, IPacketHandler> Handlers = new Dictionary<CmdId, IPacketHandler>();

	private static readonly Logger c = new Logger("Factory", ConsoleColor.Yellow);

	public static void LoadPacketHandlers()
	{
		c.Log("Loading Packet Handlers...");
		IEnumerable<Type> source = from t in Assembly.GetExecutingAssembly().GetTypes()
			select (t);
		foreach (var (type, packetCmdId) in from Type t in source.ToList()
			let attrs = (Attribute[])t.GetCustomAttributes(typeof(PacketCmdId), inherit: false)
			where attrs.Length != 0
			let attr = (PacketCmdId)attrs[0]
			where !Handlers.ContainsKey(attr.Id)
			select (t, attr))
		{
			Handlers.Add(packetCmdId.Id, (IPacketHandler)Activator.CreateInstance(type));
			Logger logger = c;
			string[] array = new string[1];
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(38, 2);
			defaultInterpolatedStringHandler.AppendLiteral("Loaded PacketHandler ");
			defaultInterpolatedStringHandler.AppendFormatted(type.Name);
			defaultInterpolatedStringHandler.AppendLiteral(" for Packet Type ");
			defaultInterpolatedStringHandler.AppendFormatted(packetCmdId.Id);
			array[0] = defaultInterpolatedStringHandler.ToStringAndClear();
			logger.Log(array);
		}
		c.Log("Finished Loading Packet Handlers");
	}

	public static IPacketHandler? GetPacketHandler(CmdId cmdId)
	{
		Handlers.TryGetValue(cmdId, out var value);
		return value;
	}
}
