# HoYoStudio / AssetStudio 梳理

> 对象：`D:\BH3\HoYoStudio-master`、`D:\BH3\AssetStudio-master`
> 日期：2026-11-03
> 结论先行：**HoYoStudio 能读 GI / SR / ZZZ / TOT，读不了本项目 BH3 7.0 的 `.wmv`**（已实测，见第 5 节）。原因是同一套容器布局、不同的头部密钥流。

---

## 1. 两个仓库是什么关系

| | AssetStudio-master | HoYoStudio-master |
|---|---|---|
| 定位 | Perfare 原版，通用 Unity 资源浏览器 | AssetStudio 的魔改分支，加米哈游 5 款游戏支持 |
| 版本 | 0.16.0.0 | 0.16.60 |
| 文件数 / 体积 | 249 / 5.52 MB | 272 / 5.78 MB |
| `.cs` 文件 | 176 | 197 |
| 项目 | AssetStudio、PInvoke、FBXNative、FBXWrapper、GUI、Utility、Texture2DDecoder×2 | 同上（目录名带 `X` 后缀，是解压时的残留） |

代码级差异（MD5 对比 `.cs`）：

- **共有且完全相同：136 个** —— 说明 HoYoStudio 是从一个较新的 AssetStudio 快照分叉的，不是重写
- **共有但被改过：40 个** —— 集中在 `BundleFile`、`SerializedFile`、`FileReader`、`EndianBinaryReader`、`ObjectReader`、`Mesh`、`Texture2D`、`BinaryReaderExtensions`、`CABManager` 相关
- **仅 HoYoStudio 有：21 个** —— 全部在 `AssetStudio\HoYo\` 子树 + `AIVersionManager.cs` + `ResourceIndex.cs` + `MiHoYoBinData.cs` + `Acl\`

**AssetStudio 原版完全没有米哈游相关代码**（`BH3|MiHoYo|Mr0k|GameManager` 只命中 `ClassIDType.cs` 里的一条注释）。所以拿原版去开 `.wmv` 一定失败——它连"游戏"这个概念都没有。

---

## 2. AssetStudio 基线能力

- 支持 Unity 3.4 – 2022.1
- 可导出类型：`Texture2D`(png/tga/jpg/bmp)、`Sprite`、`AudioClip`(mp3/ogg/wav/m4a/fsb)、`Font`、`Mesh`(obj)、`TextAsset`、`Shader`、`VideoClip`、`MonoBehaviour`(json)、`Animator`(fbx)
- 主流程：

```
FileReader → 判 FileType → BundleFile（拆 UnityFS）
          → SerializedFile（读元数据/类型树/对象表）
          → ObjectReader → Classes\*.cs（每个 Unity 类型一个类）
          → Exporter（导出）
```

- 关键工程点：`m_EnableTypeTree` 决定是否读内嵌类型树；`CheckStrippedVersion` 处理被剥离版本号的包（必须手工指定 Unity 版本）

---

## 3. HoYoStudio 的增量（逐模块）

### 3.1 游戏注册表 `GameManager.cs`

```csharp
GI   : .blk        Map=BLKMap    Path=GenshinImpact_Data|Yuanshen_Data
BH3  : .wmv        Map=WMVMap    Path=BH3_Data
ZZZ  : .bundle     Map=ZZZMap    Path=Win_Data/StreamingAssets/Bundles
SR   : .unity3d    Map=ENCRMap   Path=StarRail_Data
TOT  : .blk        Map=TOTMap    Path=AssetbundlesCache
```

`Game` 只有 5 个字段：`Name / DisplayName / Extension / MapName / Path`。整个"支持某游戏"= 扩展名 + 一个 Map 文件 + 一个分支。

### 3.2 分发器 `HoYo\HoYoFile.cs`

按扩展名 switch，给每种游戏预置 Mr0k 的四把密钥，然后选容器解析器：

| 游戏 | 容器 | 额外动作 |
|---|---|---|
| GI | `BlkFile` | — |
| BH3 | `WMVFile` | 设 `BH3ExpansionKey/BH3Key/BH3ConstKey/BH3SBox`，`BlockKey=null` |
| ZZZ | `BundleFile` 直读 | 设通用 Key 组，`SBox=null` |
| SR | `BundleFile` 直读 | 同上 |
| TOT | `TOTFile` | 通用 Key 组 + `BlockKey` |

### 3.3 BH3 容器 `HoYo\BH3\WMVFile.cs`（28 行）

```csharp
// 有 BundlePos（WMVMap 里查到的偏移）就按偏移跳；否则从头顺序读
while (reader.Position != reader.Length) {
    pos = reader.Position;
    Bundles.Add(pos, new BundleFile(reader).FileList);
}
```

**结论：`.wmv` = 多个 UnityFS 首尾相接**，没有额外容器格式。这一条和我们自己的结论一致。

### 3.4 GI 容器 `HoYo\GI\BlkFile.cs` + `Mhy0File.cs`

`blk` 头：`"blk"` + `int32 count` + `key[count]` + `skip[count]` + `uint16 blockSize` + 数据
→ `Crypto.Decrypt(key, data, blockSize)` → 明文里是若干 `mhy0` 串。

`mhy0`：`"mhy0"` + `int32 headerSize` + header
→ `Crypto.DescrambleHeader`（0x39/0x1C 行移位）→ 跳过 0x20 字节 → 长度 → LZ4
→ 目录表（路径/是否资源/偏移/大小）+ 块表
→ 每块：`Crypto.DescrambleEntry`（0x21/8）→ 去掉前 0xC 字节 → LZ4

### 3.5 `HoYo\TOT\TOTFile.cs`

和 `WMVFile` 逻辑相同（`.blk` 里也是 UnityFS 拼接）。

### 3.6 加密件 `HoYo\Crypto\`

| 文件 | 用途 |
|---|---|
| `Crypto.cs` (36 KB) | blk 的 AES-256-ECB 变体（GF(256) 表 + 行移位）；mhy0 的 `DescrambleHeader/Entry`；各游戏常量（`BH3Key / BH3ExpansionKey / BH3ConstKey / BH3SBox`、通用 `Key / ConstKey / BlockKey`） |
| `Mr0k.cs` | `mr0k` 块解密（UnityFS 的 `CompressionType.Lz4Mr0k = 5`） |
| `XORShift128.cs` | UnityFS **头部字段**解密（BH3 / SR / ZZZ） |
| `AES.cs` / `MT19937_64.cs` | 基础件 |

### 3.7 `CABManager.cs` — 最有工程价值的一块

`BuildMap` 扫一遍游戏目录里所有目标扩展名的文件，对每个文件：

1. 用 `HoYoFile` 拆出所有 bundle
2. 每个 bundle 里的 CAB 若是 SerializedFile，就读它的 `m_Externals`（依赖）
3. 写入 `Maps\<MapName>.bin`：`CAB名 → (所属文件, 文件内偏移, 依赖列表)`

之后 `AddCABOffset` 会**递归展开依赖**，于是"只给一个 CAB 名"就能自动定位并加载它的全部前置包。这就是它能"点一个资源就自动把依赖补齐"的原因。

### 3.8 GI 专用：`ResourceIndex.cs` + `AIVersionManager.cs`

从 `raw.githubusercontent.com/radioegor146/gi-asset-indexes` 拉 `asset_index`（容器路径表），带 commit 版本校验、本地缓存到 `AI\`。
**只对 GI 有用**，BH3/SR 的容器路径本来就在 `AssetBundle.m_Container` 里，不需要外部表。

### 3.9 `Acl\` —— 注意别误解

`acl.dll` + `acl.cs` 是 **AnimationClip 的 ACL 曲线压缩解压器**（`DecompressAll → values[] + times[]`），
**不是反作弊（Anti-Cheat Expert）**。名字撞车。

### 3.10 `MiHoYoBinData.cs`

mhy0 里的数据对象：`int32 length` + 裸字节，支持单字节 XOR，自动判 JSON / 二进制。`doXOR` + `Key` 是全局开关。

---

## 4. 三个可复用算法

### 4.1 XORShift128 —— UnityFS 头部字段解密

```csharp
InitSeed(key):  x=key; y=MT*x+1; z=MT*y+1; w=MT*z+1        // MT=0x6C078965
XORShift():     t = x ^ (x<<11); x=y; y=z; z=w;
                return w = w ^ (w>>19) ^ t ^ (t>>8);

NextDecrypt(size):
    key = size * initseed - 0x61C8864E7A143579    // 64 位
    for i in size: out[i] = key_bytes[ XORShift() % 8 ]

DecryptHeader(key):
    flags ^= NextInt();  size ^= NextLong();
    uncompressedBlocksInfoSize ^= NextUInt();  compressedBlocksInfoSize ^= NextUInt();
```

对应 BH3 的头部布局（`BundleFile.ReadHeader`）：

```
"UnityFS\0" | int32 key | uint32 flags | int64 size
            | uint32 ubis | uint32 cbis | cstring encUnityVersion | cstring encUnityRevision
            → blocksInfo
```

注意 `encUnityVersion/encUnityRevision` 读完就丢，代码里**硬编码** `version=6, "5.x.x", "2017.4.18f1"`。

### 4.2 Mr0k —— LZ4Mr0k 块

```
1. 前 4 字节 == "mr0k"
2. key1 = bytes[4..0x14]   key2 = bytes[0x74..0x84]   key3 = bytes[0x84..0x94]
3. key2 ^= ConstKey;  key1[i] = SBox[(i%4)*0x100 | key1[i]]
4. AES-ECB(ExpansionKey) 解 key1、key3；key1 ^= key3；写回 0x84
5. seed = seed2 ^ seed1 ^ (seed1 + size - 0x14)
6. 密文块 = bytes[0x94 .. 0x94 + min(0x10*((size-0x94)>>7), 0x400)]
   逐字节 ^= (seed_bytes[i%8] ^ Key[i])
7. 去掉前 0x14 字节 → 剩下的就是 LZ4 数据
8. 若 BlockKey != null：前 0xC00 字节再 ^= BlockKey 循环
```

### 4.3 mhy0 的混淆整数

不是 varint，是**位序打乱**：

```csharp
ReadMhy0Int1: 读 7 字节 → b[1] | b[6]<<8 | b[3]<<16 | b[2]<<24
ReadMhy0Int2: 读 6 字节 → b[2] | b[4]<<8 | b[0]<<16 | b[5]<<24
ReadMhy0Bool: ReadMhy0Int2 → 反转字节序 → 取 bool
ReadMhy0String: 固定 0x100 字节，截到第一个 0
```

### 4.4 附带：classID 也可能是密的

`SerializedFile.ReadSerializedType`：

```csharp
if (classID > 0xFFFF || classID <= 0)
    classID = (BitConverter.ToInt32(reversed_bytes) ^ 0x23746FBE) - 3;
```

遇到非法 classID 时按"大端读 + 异或 0x23746FBE - 3"还原。这条是通用逻辑，不限游戏。

---

## 5. 实测：HoYoStudio 能不能读 7.0 的 `.wmv`

### 5.1 先看事实

扫描 `BH3_Data\StreamingAssets\Asb\pc\*.wmv` 全部 440 个文件的前 0x40 字节：

| 区间 | 唯一值数 | 内容 |
|---|---|---|
| `0x00..0x07` | 1 | `UnityFS\0` |
| `0x08..0x0B` | **1** | `ce 4e ee 1e` |
| `0x08..0x13` | **1** | `ce4eee1e 9e37713c 37797779` |
| `0x08..0x17` | 438 | 从 `0x14` 才开始随文件变化 |

`0x08..0x13` 在 440 个文件里**逐字节相同**，说明这是"固定 key + 固定 keystream + 高位恒为 0 的 size 字段"三者的叠加结果 —— 结构上**完全符合** HoYoStudio 的 BH3 布局。

再看两段加密串的位置：`0x20` 起 5 字节 + `\0`，`0x26` 起 11 字节 + `\0`。
`5.x.x` 正好 5 个字符，`2017.4.18f1` 正好 11 个字符 —— **长度完全对上**，`0x32` 正好是 blocksInfo 起点。

### 5.2 但按它的算法解不出来

我按 `XORShift128.cs` 逐行实现（`tools\wmv_hoyo_hdr.py`），用 `key = 0x1EEE4ECE`：

```
flags      = 0x9B324699      ← 不是合法 ArchiveFlags
size       = 1745…（1.7e18） ← 文件实际 52,625,303
blocksInfo = 210077881 / 2131113675  ← 明显荒谬
```

再把 `0x04..0x2F` 每个偏移都当 key 试一遍（`_ext\tmp\brute_wmv_key.py`），
判据"解出的 size == 文件大小"：**零命中**。

### 5.3 而 7.0 的真实结构是这样

`tools\decode_wmv_unit.py`（我们自己早就验证过的）直接跳过那 42 字节，从 `0x32` 开始 LZ4：

```
从 0x32 解压 blocksInfo：得到 253 字节
block 数=11  node 数=2
  off=0     size=4528      CAB-2aa9a7957d45627ad91365745960aecd
  off=4528  size=1398128   CAB-2aa9a7957d45627ad91365745960aecd.resS
```

而且字段是**大端**（`>I` / `>IIH` / `>qqI`），不是 UnityFS 标准的小端。

### 5.4 结论

| 项 | 判定 |
|---|---|
| 容器布局（key + flags + size + 两段串 + blocksInfo@0x32） | 与 HoYoStudio **完全一致** |
| 头部密钥流 | **不同**（BH3 7.0 换过 RNG / 常量） |
| blocksInfo 本体 | **明文 LZ4**，只需跳过 42 字节前缀 |
| HoYoStudio 直接开 7.0 `.wmv` | **失败** |

也就是说：**它离能用只差一个头部解密**，而那个头部我们根本不需要解——位置固定、长度固定、内容对我们没用。

---

## 6. 要让它读 7.0，改哪里

只改一处：`AssetStudio\BundleFile.cs` 的 `ReadHeader` BH3 分支 + `ReadBlocksInfoAndDirectory` 的字节序。

```csharp
// ReadHeader: BH3 分支替换为
if (reader.Game.Name == "BH3") {
    reader.Position = reader.BaseStream.Position + 4;   // 跳过 int32 key
    reader.Position += 16;                              // flags/size/ubis/cbis 全是密文，跳过
    reader.Position = 0x32;                             // 两段加密串读不读无所谓，直接对齐
    m_Header.version = 6;
    m_Header.unityVersion = "5.x.x";
    m_Header.unityRevision = "2017.4.18f1";
    m_Header.flags = ArchiveFlags.BlocksAndDirectoryInfoCombined | (ArchiveFlags)3; // LZ4HC
    m_Header.compressedBlocksInfoSize = /* 从 0x32 起按 LZ4 自然解压，不依赖长度 */;
}
```

配套：`ReadBlocksInfoAndDirectory` 里对 BH3 用 `EndianType.BigEndian` 读 blocksInfo 的
`blockCount / blocks / nodeCount / nodes`；`ReadBlocks` 的 block 数据仍是标准小端 LZ4。

> 更省事的做法：不改 C#，直接写个 Python 预处理器，把 `.wmv` 转成"标准小端 UnityFS"再喂给 HoYoStudio。42 字节前缀 → 重写成合法头部，blocksInfo 由我们解出后按小端重写。一次性脚本，几十行。

---

## 7. 编译情况（本机）

| 项 | 状态 |
|---|---|
| .NET SDK | 6.0.428 ✓ / 9.0.315 ✓ |
| .NET 运行时 | 6.0.36、8.0.14、9.0.17、10.0.11 ✓ |
| Visual Studio / MSVC | **无** ✗ |
| `AssetStudioFBXNative`、`Texture2DDecoderNative`（`.vcxproj`） | **编不了** |
| 纯 C# 三个项目（AssetStudio / Utility / GUI） | 可编 |

没有 MSVC 的后果：**贴图解码（DXT/ASTC/ETC）和 FBX 导出不可用**（P/Invoke 找不到原生 DLL）；
Mesh→OBJ、TextAsset、MonoBehaviour→JSON、Shader 文本不受影响。

仓库里**没有预编译 exe**，只有 `fmod.dll / acl.dll / OpenTK.WinForms.dll` 三个运行时依赖。

---

## 8. 对本项目的价值排序

| 用途 | 可行性 | 说明 |
|---|---|---|
| 浏览/导出 GI、SR、ZZZ、TOT 资源 | ✅ 直接可用 | 需自己编译 |
| 浏览 7.0 `.wmv` 的贴图/网格/材质 | ⚠️ 需改头部逻辑 | 见第 6 节；改完是完整的 GUI 浏览器 |
| 理解 `.wmv` 结构 | ✅ 已确认 | 多 UnityFS 拼接，与我们的结论一致 |
| 解决 `Blocks.xmf` / `BlockMeta` 内容级校验 | ❌ 无关 | 仓库里完全没有这部分 |
| 解 `editor_compressed/data` 的配置表加密 | ❌ 无关 | 那是米哈游自研流密码，Mr0k/Crypto 都不适用 |
| 跨版本 Mesh 移植 | ❌ 无关 | 序列化布局 + 蒙皮私有通道的问题，工具层解决不了 |
| 参考"如何做资源浏览器"的工程结构 | ✅ 高价值 | `CABManager` 的依赖递归、`FileReader` 的类型判定都值得抄 |

---

## 9. 使用方式（编译后）

GUI：

```
Options → Specify Game → 选 Honkai Impact 3rd
Misc.   → Build BLKMap / WMVMap        （扫一遍，生成 Maps\WMVMap.bin）
File    → Load Folder → BH3_Data\StreamingAssets\Asb\pc
```

CLI（同一个 exe）：

```
AssetStudioGUI.exe -g BH3 -m <输入目录> <输出目录>     # 只建 map
AssetStudioGUI.exe -g BH3 -t Mesh -f "dress" <in> <out>
```

已知坑（README 自己写的）：`MeshRenderer/SkinnedMeshRenderer` 报错时先勾 `Export Options → Disable Renderer`；
不想拉全部动画就勾 `Ignore Controller Anim`。

---

## 10. 三个容易踩的误解

1. **`acl.dll` 不是反作弊**，是 AnimationClip 的 ACL 曲线解压库。
2. **`AIVersionManager` 需要联网**（GitHub raw）。离线时 `Online=false`，`GetVersions()` 返回空数组——只影响 GI，不影响 BH3。
3. **HoYoStudio 不是"米哈游全格式支持"**。它只做三件事：容器拆包（blk/mhy0/wmv）、UnityFS 头/块解密（XORShift128/Mr0k）、依赖索引。**清单层（blk/Blocks.xmf）和配置表加密它都没碰。**
