# BH3 7.0 单机服务端 + 逆向工具链

崩坏3 7.0（国服 PC）本地单机化的一套东西：自研服务端、客户端注入载荷、
il2cpp 符号/调用图查询工具，以及逆向过程文档。

> 全部为本地单机自用。客户端资源、游戏本体、存档不进仓库（见文末）。

---

## 目录

| 目录 | 内容 |
|---|---|
| `server-v2/` | **服务端**，.NET 8 + SQLite。协议层与客户端一对一对齐，129 个 Handler。有独立 git 历史 |
| `注入修复inject_pkg/` | **客户端注入载荷**。`version.dll` 侧加载 → `bh3bypass.dll`，C 源码 + zig cc 构建 |
| `符号与调用图工具包/` | **逆向查询工具**。符号表 / 调用图 / 字段布局的离线查询（`route.py` 一条命令出方案） |
| `docs/` | 逆向过程文档：元数据解密、资源包格式、注入通道、玩法解析等 40+ 篇 |
| `一键启动.vbs` 等 4 个 | 提权启动链、停止、代理还原（断网应急） |

---

## 快速开始（服务端）

```bat
cd server-v2
build.bat              :: dotnet publish -> run\，自包含，不需要装 .NET
```

然后：

1. 解压仓库里的 `server-v2\Resources.tar.xz`（12.8 MB）到 `server-v2\run\`：

   ```bat
   tar -xJf server-v2\Resources.tar.xz -C server-v2\run
   ```

   解压后是 `run\Resources\ExcelOutputAsset\*.json`，1658 个配置表，必需。
2. 双击 `run\Server.exe`
3. 客户端改 hosts / 证书指向本机，启动

配置表展开后 399 MB、单文件最大 117 MB，超 GitHub 红线，所以以压缩包形式入库。

---

## 仓库里没有的东西

| 缺失 | 原因 | 怎么拿回来 |
|---|---|---|
| `run/Resources/` 展开后的 399MB json | 单文件最大 117MB，超 GitHub 红线 | 解压仓库内 `server-v2/Resources.tar.xz`（12.8MB） |
| `符号与调用图工具包/data/` 的 256MB 数据 | 同上（单文件 125MB） | 解压 Release 里的 `bh3-symtab-data.tar.xz`，或用 `tools/` 重新提取 |
| `server-v2/run/` 的 dll / exe / runtimes | 构建产物 | `build.bat` |
| `run/data/` 存档数据库 | 个人存档 | 自己的 sqlite 文件 |
| `bin/ obj/ *.pdb *.exe *.bak*` | 编译中间产物与备份 | 编译产生 |

---

## 注意

- 仓库不会 `git pull` 游戏资源，客户端本体（`BH3.exe` / `UserAssembly.dll` / 资源包）一律不入库。
- 建议**私有仓库**：`docs/` 里含逆向分析与元数据解密记录，`inject_pkg` 是注入器，公开分发容易招惹版权问题。
- 换行：`.gitattributes` 全局 `* -text`，`.bat` / `.vbs` / `.cs` 必须保持 CRLF，否则脚本解析会坏。
