# 客户端资源包（ASB）格式与皮肤注入可行性

> 2026-11-01 整理。目标是给 7.0 私服补「月下誓约 Theresa_C7」的第二套皮肤
> （`04_01` 此生相携 / `04_02` 红繁香满）。
> 本文记录摸清的全部格式、启动逻辑、已排除路线和最终卡点。

---

## 0. 一句话结论

**资源完全没问题，卡在「客户端不接受被改过的资源包」——因为改包必须重建运行时索引
`asb.dat`，而重建依赖一个我们取不到的 ASB 清单。**

---

## 1. 资源现状

| 客户端 | 路径 | C7 皮肤 |
|---|---|---|
| 7.0 | `D:\BH3\7.0\BH3_Data\StreamingAssets\Asb\pc` | **只有一套** `theresa_c7_bw` |
| 9.1.0 | `D:\BH3\miHoYo Launcher\games\Honkai Impact 3rd Game\BH3_Data\StreamingAssets\Asb\pc` | **两套都在** |

- 7.0：440 个 `.wmv` / 16.13 GB，文件名 32 位 hex
- 9.1：816 个 `.wmv` / 32.15 GB，文件名 16 位 hex
- 两边 `UnityPlayer.dll` 都是 `2017.4.18.0`，`globalgamemanagers` 都是 `2017.4.18f1`
  → **资源二进制兼容**

### 7.0 里没有的

在 16.13 GB 明文里全量搜：

- `_04_01` / `_04_02` → **0 命中**
- `Dress_` 前缀 → **0 命中**
- 对照：C5 有 `Avatar_Theresa_C5_04_02_Texture_Hair_Lightmap`、C2 有
  `Avatar_Theresa_C2_05_01_TL_Mask` → `NN_MM` 变体命名确实存在，只是 C7 没有

### 9.1 里有的（32 个单元 / 8.85 MB）

```
Avatar_Theresa_C7_04_01_Model / _Texture_Alpha / _Texture_Alpha_02
                            / _Texture_Body_LightMap / _Texture_Hair_LightMap / _Texture_Veil_Color
Avatar_Theresa_C7_04_02_Model / _Material_Body_Effect01/02 / _Material_Body_Skirt
                            / _Material_Veil / _Texture_Body_Effect / _Texture_Body_LightMap
                            / _Texture_Hair_Color
Avatar_BW_C7_04_01_Sleeve_R_Root / _Hair_BR_B03 / _Metal_BR_01 / _Ribbon_BL_A06 / _Bow_L_A02
Avatar_BW_C7_04_02_Hair_BL_C03 / _Hair_BR_A05 / _Sleeve_L_B01 / _Ribbon_BR_06
Bone_Q_Theresa_C7_04_02_Spine_Skirt_BL_01 / _Head_Hair_BL_02P
assets/asbres/entities/qavatar/theresa_c7_04_02/_dependenc...
```
外加成组的 `_Dress01`/`_Dress02` 动画特效。

**单元清单见 `tools\c7_dress_units.json`**（每个单元的包名、偏移、大小、CAB 名）。

---

## 2. `.wmv` 包格式

**不是单个 AssetBundle，是很多个「单元」的拼接**（7.0 一个 44 MB 包里有 4188 个单元）。

```
单元 = [78 字节私有头] + ["CAB-<32hex>\0" 37 字节] + [明文 SerializedFile]

私有头前 21 字节恒定签名：
  55 6E 69 74 79 46 53 00   "UnityFS\0"
  CE 4E EE 1E
  9E 37 71 3C 37 79 77 79 3B
```

- 头**不是标准 UnityFS**（version 字段读出来 `0xCE4EEE1E`），但**载荷不加密**：
  单元内能看到明文 `2017.4.18f1`
- 头部是**变长**的（`CAB-` 相对单元起点 78/81/87/95/103/138… 都出现过），
  不能按定长 78 处理
- **`.wmv` 文件名 = 文件内容 MD5**（60/60 命中，内容寻址）

### 切分单元

按 21 字节签名扫即可：

```python
SIG = bytes.fromhex("556e697479465300" "ce4eee1e" "9e37713c377977793b")
offs = [i for i in range(len(d)) if d.startswith(SIG, i)]   # 实际用 find 循环
```

---

## 3. 清单格式

### 3.1 `BlockMeta.xmf`（14121 字节）

```
37 字节头（0x21 处 u32BE = 包数 440）
440 × 32 字节定长记录
4 字节尾（00000000）
```

记录 = FlatBuffers `BlockData`：

```
+0x00 16B  包 MD5（= .wmv 文件名，两个 long）
+0x10 u32  FileTag
+0x14 u32  DownloadTag
+0x18 u32  BlockID
+0x1C u32  Length（包大小）
```

### 3.2 `Blocks.xmf`（9427457 字节）

```
37 字节头（0x21 处 u32BE = 包数 440）
440 个变长包记录：
    16B 包 MD5 + FileTag + DownloadTag + BlockID + Length
    u32 单元数
    单元0      : u8 Y + u8 len + 名字            （偏移隐式 0）
    单元1..n-1 : u32 上一单元偏移 + u8 Y + u8 len + 名字
    包尾 4 字节
4 字节尾
```

**关键验证**：`X 列表 == 真实 UnityFS 偏移列表[:-1]` → True。
即每个单元的 `X` 存的是**前一个单元**在包内的字节偏移，最后一个单元的偏移不存。

单元名形如 `<19-20位id>_<9-10位hash>.unity3d`，对应 `AssetBundleData.Name` / `.Hash`。

### 3.3 顶层 16 字节

`Blocks.xmf` 和 `BlockMeta.xmf` 前 16 字节完全相同：
`937E804AFE1AB34D3B0D9D5F0D060099`

它不是包内容哈希，而是 **`asb.dat` 的缓存键**（见 §4.3）。

### 3.4 编码器已验证

`build(parse(原文件))` 与原文件**逐字节一致**（两个文件都通过）。
工具：`tools\roundtrip_xmf.py`。

---

## 4. 客户端运行时索引

### 4.1 `asb.dat`

位置：`%LocalLow%\miHoYo\Honkai Impact 3\StreamingAsb\pc\asb.dat`，16,734,584 字节。

```
16,734,584 = 24 字节头 + 522,955 × 32 字节记录     （整数，无余数）
```

是**资源级索引**（522,955 ≈ 资源数，不是单元数 213,218），
FlatBuffers 的 `HashTable64<AssetBundleData>`：**资产名 u64 哈希 → 单元定位**。

证据：目标包的特征单元偏移（`401636 / 782564 / 2715994 / 3102267 / 3365929`）
在 asb.dat 里**各出现恰好 1 次**；440 个包大小也全部出现。

### 4.2 数据模型（来自 `rea\_rea\src` 反编译产物）

```csharp
struct BlockData {
    long MD51; long MD52;                 // 包 MD5
    int FileTag; int DownloadTag; int BlockID; int Length;
}
struct AssetBundleData {
    ulong Name; uint Hash;                // 清单名 <id>_<hash>
    int Index; int Flags;
    int BlockIndex;                       // 属于哪个包
    int BlockOffset;                      // 包内偏移
    int DependenciesLength;
}
struct StreamingAssetBundleData {
    Version Version;                      // Major/Minor/Build/Resource/Data（Data 必须 == 2）
    long MD51; long MD52;                 // 顶层 16 字节
    Blocks[]; AssetBundles[];
    HashTable64<AssetBundleData> AssetToAssetBundle;
    bool HavePatch;
}
```

`StreamingAssetBundleManager` 关键常量：

```
BlockFilePattern = "*.wmv"                       // 按 *.wmv 枚举本地包
BlockVerifiedVersionFile = "blockVerifiedVersion.txt"   // 内容 7_0_0_0
MemoryMappedFlatBuffer<StreamingAssetBundleData> _data; // = asb.dat
```

### 4.3 启动逻辑（反汇编确认）

```
InitLocal(bool reload) @ 0x182FAB980
  InitBlockConfig()
  if (!InitMemoryMappedData(reload))  LoadCurrentAsbInfoList()

InitMemoryMappedData(bool) @ 0x182FABA40
  ClearMemoryMappedData()
  if (!reload && !asb.dat 存在)  SaveMemoryMappedData()
  LoadMemoryMappedData()
  if (NeedRefreshMemoryMappedData()) { Clear(); Save(); Load(); }

NeedRefreshMemoryMappedData() @ 0x182FAD6D0
  _data == null                    → true
  _data.Version.Data != 2          → true
  _data.MD51/MD52 != 块配置哈希     → true
  Version 四元组任一不等            → true
  全等                              → false（用缓存，不重建、不校验包）

SaveMemoryMappedData() @ 0x182FB0580
  CreateParentDirectory()
  var list = LoadAsbList(versionName.ToLowerInvariant(), crc)
  if (list == null) goto fail          ← 失败点
  HashTable64 / FlatBufferBuilder / StartBlocksVector
  for i in 0..块数: 取 MD51、MD52
```

### 4.4 名字查找链（反汇编确认）

```
LoadAsbList(name, crc) @ 0x182FAC1D0
  → LoadAsbByPath @ 0x182FAC150
      → get_fileMapSystem().LCFBIOKADKE(path) @ 0x18330B860
          → LCFBIOKADKE(name, pos, crc) @ 0x18330B710
              → IJDCGAKKOGG(name) @ 0x18330AFC0
                  → ulong h = CPBIHDLCBCC(name, 0) @ 0x1830A77B0
                  → IJDCGAKKOGG(h) @ 0x18330B030 → FAEGFPADJKO(handle, h)
```

`InitBlockConfig() @ 0x182FAB670`：

```
inPackageFileMapSystem(+0x68) = IDBBMMADED( NPath.Combine(get_localPath(), <常量>) )
currentVersionDownload(+0x70) = IDBBMMADED( this->_downloadChangeBlockConfigPath )
_fileMapSystem(+0x80)         只被清空、没被赋值（由 patch/predownload 系统设置）
```

### 4.5 哈希函数 = MurmurHash64B（已破解）

`CPBIHDLCBCC(string, ulong seed)`：

| 特征 | 值 |
|---|---|
| 常量 m | `0x5BD1E995` |
| 右移 r | 24 |
| 初始 | `h1 = (uint)seed ^ (len*2)`，`h2 = (uint)(seed>>32)` |
| 主循环 | 每 8 字节两个 32 位字，各自 `k*=m; k^=k>>24; k*=m` 后异或进 h1 / h2 |
| 尾部 | 剩 1~3 字节拼进 h2 再 `*m` |
| 收尾 | `h1^=h2>>18,h1*=m; h2^=h1>>22,h2*=m; h1^=h2>>17,h1*=m; h2^=h1>>19,h2*=m` |
| 返回 | `(h1 << 32) \| h2` |
| 输入 | **UTF-16LE 字节**，seed = 0 |

实现：`tools\test_murmur64b.py` 的 `murmur64b()` / `murmur2_32()`。

---

## 5. 已排除的路线

| 路线 | 结论 |
|---|---|
| 破解资源包 AES 密钥 | AES 是原生 OpenSSL（`.text` RVA `0x73740`–`0x75D8E`），**没被虚拟化**，但 33 个调用点无硬编码 key，key 来自运行时 |
| 全量 devirt `.tvm0` | 773 函数、人月级；且目标本身不成立（当时误判「C7 没资源」） |
| 调试器 / 硬件断点抓 key | ACE 反调试，实测命中诱饵 `RIP=0x08887777` 后进程被杀 |
| 只追加单元、不改清单 | `asb.dat` 逐字节不变（客户端不扫包内容） |
| 改包 + 改清单（不改名） | 启动弹「需要下载 51 MB」（尺寸校验） |
| 改包 + 按新 MD5 改名 + 改清单 | 卡在 `SaveMemoryMappedData` |
| 复制 7.0 自己的单元（单变量） | 同样失败 → **问题不在 9.1 数据** |
| 保留顶层 16 字节 + 不删 `asb.dat` | 仍然失败，客户端主动**删除** `asb.dat` → 缓存键是从实际包集合算的 |

其它已排除：

- 包名引用全盘只有 `BlockMeta.xmf` / `Blocks.xmf` / `Blocks_7_0.xmf` 三处（都已改）
- 所有包大小之和 `17,314,601,503` 全盘无匹配（没被记录）
- `asbresversion.unity3d` 从未被 HTTP 请求过（全量 42667 行 nginx 日志 0 命中）
  → 它是**包内 ASB**，名字字符串确实在 `cce7489f41c29ecc3e100af489988f23.wmv` @0xBAFF88

---

## 6. 最终卡点

```
任何改包 → 强制重建 asb.dat → SaveMemoryMappedData
  → LoadAsbList("asbresversion.unity3d") 返回 null
  → Save 失败 → InitMemoryMappedData 返回 false
  → InitLocal 转调 LoadCurrentAsbInfoList（也失败）
  → 资源索引为空 → Scene 'GameEntry' couldn't be loaded
```

**对照实验**：删 `asb.dat` 后
- 包**未改动** → 10 秒内重建成功，内容逐字节一致
- 包**被改动** → 重建失败，`asb.dat` 被删

所以 `LoadAsbList` 确实会读包，且只在包被改动时失败。

---

## 7. 剩余未解 & 下一步

### 未解

1. `<id>_<hash>` 的**输入字符串**未确定。已试全部 0 命中：
   - `asbresversion.unity3d`（含 `bundlestreaming/configs/` 前缀变体）
   - 单元内**所有**可打印串（60 单元 / 18723 个）
   - `CAB-<32hex>` / 纯 hex / `archive:/CAB-x/CAB-x`（720 组）
   - 已知真实资源路径（8 条 × 7 形态 × UTF-8/UTF-16）

   → 推断是**构建期分配的 bundle 名**的哈希，而 bundle 名不写在单元内容里。

2. `asbresversion.unity3d` 为什么查不到。

### 下一步

- **拿一个已知 `(bundle名, id)` 对**来校准 MurmurHash64B。
  可行来源：
  - 9.1 客户端的清单（`BlockMeta.xmf` / `Blocks_9_1.xmf` 目前看是加密的，需再确认）
  - 让 9.1 客户端跑一次，生成它自己的 `asb.dat`（明文 FlatBuffer，含 Name/Hash 对）
- 或直接看 `IDBBMMADED` / `FAEGFPADJKO` 的实现

---

## 8. 工具索引

| 工具 | 用途 |
|---|---|
| `tools\split_wmv_units.py` | 按签名切分 `.wmv` 单元 |
| `tools\export_c7_units.py` | 导出皮肤单元清单 `c7_dress_units.json` |
| `tools\parse_blocks_xmf3.py` | 解析 `Blocks.xmf` |
| `tools\verify_blockmeta.py` | 校验 `BlockMeta.xmf` |
| `tools\roundtrip_xmf.py` | 清单编码往返测试 |
| `tools\verify_x_offset.py` | 验证 X = 单元偏移 |
| `tools\patch_manifest.py` | 清单补丁（改包 + 改清单 + 备份/还原） |
| `tools\patch_v2.py` | 同上 + 按新 MD5 改名 |
| `tools\dup_unit_test.py` | 复制自身单元的单变量实验 |
| `tools\disasm_annotated.py` | **带符号标注的反汇编器**（31 万条 VA→方法名） |
| `tools\test_murmur64b.py` | MurmurHash64B 实现 |
| `tools\find_asbresversion.py` | 在包内搜 asbresversion |
| `tools\probe_asbdat*.py` | 反推 `asb.dat` 结构 |
| `tools\test_wmv_contenthash.py` | 验证文件名 = 内容 MD5 |

反编译源码：`rea\_rea\src`（469 个 .cs / 75.9 MB，**只有签名+地址，方法体为空**）。

---

## 9. 环境还原

```powershell
# 还原包 + 三份清单
python tools\patch_v2.py --restore
# 校验：448 文件 / 17,475,366,063 字节 / Blocks.xmf sha256 前16 = 3A3D0BF7E607041D
```

备份位置：`backup\asb\`（包 + 三份清单的 `.orig`）。
`asb.dat` 删掉后客户端会自动重建（未改包时），SHA256 = `D71A532E…5F67B`。
