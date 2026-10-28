using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using Common;
using Common.Database;
using Common.Utils;
using PemukulPaku.GameServer;
using PemukulPaku.GameServer.Commands;

namespace Server;

/// <summary>
/// 崩坏3 7.0 服务端 —— v2（.NET 8 + SQLite）
///
/// 与 v1（D:\BH3\7.0\server，.NET 6 + MongoDB）的关系：
///   * 协议层（封包格式 / 132 个 Handler / Common 的 3560 个 message）完全一致，
///     因为客户端是锁死的，任何字节级差异都会让客户端断连；
///   * 持久层从 MongoDB 换成 SQLite，schema 变成「id + 业务键列 + JSON payload」；
///   * 传输层从 TcpListener 手写循环换成同样的手写循环（协议是裸 TCP + magic 分帧，不能用 Kestrel）。
///
/// 启动顺序与原版一致：配置 → 命令处理器 → 包处理器 → HTTP（后台线程）→ TCP → 控制台。
/// </summary>
internal class Program
{
	private static readonly Logger c = new("Boot", ConsoleColor.Cyan);

	public static void Main(string[] args)
	{
		Global.config.VerboseLevel = VerboseLevel.Debug;

		c.Log("Starting server v2 (.NET 8 + SQLite)...");
		c.Log("Database : " + Global.Store.FilePath);
		c.Log("Listen   : TCP " + Global.config.Gameserver.Port
		       + "  HTTP " + Global.config.Http.HttpPort
		       + "  HTTPS " + Global.config.Http.HttpsPort);

		// 与原版一致：配成 127.0.0.1 时自动换成第一块可用网卡的 IPv4，
		// 否则客户端拿到 127.0.0.1 连不上（手机 / 局域网场景）
		if (Global.config.Gameserver.Host == "127.0.0.1")
		{
			Global.config.Gameserver.Host = (from i in NetworkInterface.GetAllNetworkInterfaces()
				where i.NetworkInterfaceType != NetworkInterfaceType.Loopback
				      && i.OperationalStatus == OperationalStatus.Up
				select i).First()
				.GetIPProperties().UnicastAddresses
				.Where(a => a.Address.AddressFamily == AddressFamily.InterNetwork)
				.First().Address.ToString();
		}
		c.Log("Gameserver.Host = " + Global.config.Gameserver.Host);

		CommandFactory.LoadCommandHandlers();
		PacketFactory.LoadPacketHandlers();

		// HTTP 用 Kestrel，跑在独立线程（和原版一样，不阻塞主线程）
		new Thread(HttpServer.Program.Main).Start();

		// TCP 16100：裸协议，自己写的 accept 循环
		// 注意：本文件的命名空间就叫 Server，所以这里必须写全名，否则会被解析成命名空间
		PemukulPaku.GameServer.Server.GetInstance();

		// 控制台命令（give / avatar / abyss ...）
		ReadLine.GetInstance().Start();
	}
}
