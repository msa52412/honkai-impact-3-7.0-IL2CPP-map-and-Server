using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Common.Utils;

namespace PemukulPaku.GameServer;

public class Server
{
	public static readonly Logger c = new Logger("TCP", ConsoleColor.Blue);

	public readonly ConcurrentDictionary<string, Session> Sessions = new ConcurrentDictionary<string, Session>();

	private static Server? Instance;

	public static Server GetInstance()
	{
		return Instance ?? (Instance = new Server());
	}

	public Server()
	{
		Task.Run((Action)Start);
	}

	public void Start()
	{
		TcpListener tcpListener = new TcpListener(IPAddress.Parse("0.0.0.0"), (int)Global.config.Gameserver.Port);
		while (true)
		{
			try
			{
				tcpListener.Start();
				Logger logger = c;
				string[] array = new string[1];
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(27, 1);
				defaultInterpolatedStringHandler.AppendLiteral("TCP server started on port ");
				defaultInterpolatedStringHandler.AppendFormatted(Global.config.Gameserver.Port);
				array[0] = defaultInterpolatedStringHandler.ToStringAndClear();
				logger.Log(array);
				while (true)
				{
					TcpClient tcpClient = tcpListener.AcceptTcpClient();
					string text = tcpClient.Client.RemoteEndPoint!.ToString();
					c.Warn(text + " connected");
					// 用索引器而不是 Add：同一个 ip:port 重连时 Add 会抛
					// "An item with the same key has already been added"，
					// 被外层 catch 接住后会 Sleep(3s) 再重建 TcpListener —— 看起来像「服务端抽风」。
					Sessions[text] = new Session(text, tcpClient);
					LogClients();
				}
			}
			catch (Exception ex)
			{
				c.Error("TCP server error: " + ex.Message);
				Thread.Sleep(3000);
			}
		}
	}

	public void LogClients()
	{
		Logger logger = c;
		string[] array = new string[1];
		DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(19, 1);
		defaultInterpolatedStringHandler.AppendLiteral("Connected clients: ");
		defaultInterpolatedStringHandler.AppendFormatted(Sessions.Count);
		array[0] = defaultInterpolatedStringHandler.ToStringAndClear();
		logger.Log(array);
	}
}
