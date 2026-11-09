# BH3 符号与调用图工具包

给崩坏3 7.0（il2cpp / UserAssembly.dll）做逆向时用的**离线查询工具集**：
符号表告诉你函数在哪，调用图告诉你谁在调谁，`route.py` 把这两样拼成
「这个功能怎么调」的完整方案。

打包日期 2026-11-08　数据截止 2026-11-07

---

## 目录

```
data/                                  约 265 MB，全是查表用的原始数据
  bh3_symtab.tsv            50 MB   rva / 命名空间 / 类 / 方法 / 返回 / 参数 / isStatic / token / 模块
  bh3_layout_resolved.tsv   63 MB   类 → 每个字段的偏移 + 名字（il2cpp 布局还原）
  bh3_cg_funcs.tsv          13 MB   函数表：va / body / thunk / name
  bh3_cg_edges.tsv         131 MB   调用边：caller / callee / insn
  bh3_cg_modules.tsv         6 KB   模块 → 地址区间
  bh3_pdata_truth.tsv       10 MB   PE pdata 展开的函数边界（callers.py 用它精解）
  addr2name.json            10 MB   地址 → 最接近的符号名（find_callers_all 用）

tools/                                 查询与执行
  route.py            一条命令给方案：签名 + 实现特征 + 全局槽 + 参数装箱
  callers.py          who（谁调它）/ calls（它调谁）/ find（搜索），按 pdata 边界精解
  find_callers_all.py 暴力扫所有可执行段找 call 点（快，可能有误报）
  icall.py            跨进程调用（用 nstack 往游戏里调 C# 方法）
  mgr.py              从 [rip+slot] 全局槽取单例实例
  card_cover.py       卡面/标题图读写，附带从日志取 entry 的例子
  field_layout.py     类字段偏移查询（layout tsv 的读取层，被 callers/icall 依赖）
  callsite.py         调用点判定辅助
  _paths.py           统一路径解析（data / game / dll 都现算，不写死）

docs/
  调用路线_泛化_20261107.md   六步法：定位 → 校验 → 读实现 → 拿实例 → 定类型 → 验证
```

---

## 依赖

1. **Python 3.8+** 和 `capstone`
2. **`numpy`** —— 只有 `callers.py`（经 `callsite.py`）需要
3. **游戏本体的 `UserAssembly.dll`**（182 MB）——
   只有需要反汇编（`route.py <方法>`、`callers.py`）时才用，纯查表不需要。

本机现状：

| 解释器 | capstone | numpy | 能跑什么 |
|---|---|---|---|
| `7.0\tools\python\python.exe`（嵌入式） | ✅ 5.0.7 | ❌ | route.py / find_callers_all.py / field_layout.py |
| 系统 `python` | ✅ 5.0.7 | ✅ 2.5.4 | 全部，含 callers.py |

所以涉及 `callers.py` 的命令请用系统 `python`，其余用哪个都行。

`_paths.py` 会自动往上找含 `BH3.exe` 的目录，再去 `BH3_Data\Native\UserAssembly.dll`。
找不到就说明这个包放错位置了，或者游戏目录被挪走。

自检：

```bash
python tools/_paths.py
```

应该打印 `DATA / GAME / DLL` 三个路径，以及几张表全 `OK`。

---

## 五个常用查询（下面的输出都是实测跑出来的）

### 例 1　只记得半个方法名，先找出来

```bash
python tools/route.py --search "GetLevelScore"
```

```
  0x008fd320  LevelActor::GetLevelScorePlugin() -> MoleMole_LevelScorePlugin
  0x024c4130  LevelScoreManager::SetTargetLevelScore(long targetScore) -> void
  0x034103d0  LevelDesignManager::GetLevelScore() -> long
  0x0365b310  LevelDesignManager::GetLevelScoreDLCPluginV2State() -> int
```

四条全是 `GetLevelScore*` 家族——**同名/近名方法很多**，这一步就是先把候选摊开，
别急着按名字猜（`get_position` 有几十个重载）。

### 例 2　这个功能到底怎么调

```bash
python tools/route.py "LevelDesignManager::SetOWEndlessLevelScoreMode"
# 或按 RVA：python tools/route.py 0x3444aa0
```

```
=== LevelDesignManager::SetOWEndlessLevelScoreMode
  RVA    0x3444aa0   段 il2cpp
  返回   void
  参数   bool show, bool playAnim, string textmap, bool isExtra
  模块   Assembly-CSharp.dll   isStatic 0   token 0600ADB6

--- 实现特征（前 60 条指令）
  IFix   是（首个 patch id = 0xa88f）。真实逻辑在 ILFixDynamicMethodWrapper 里，
         静态读不出；但直接 call 原 RVA 仍会走 patch。
  this   不需要 —— 保存了 rcx 但从未使用，是静态访问器（可从 [rip+slot] 直接起链）

--- 静态全局槽：该类单例就在这里，可直接起链
  槽 RVA 0x9a994a8   （在 0x3444ae4 处被引用）
     取实例: python mgr.py get 0x9a994a8
  ⇒ 判据：槽 → k1=*(G0) → *(k1+0x90) → *(..) → *(..+0x28) → inst

--- 参数装箱
  1. bool   -> int    只保证 al 有效，判断请 & 0xFF
  3. string -> S      托管字符串，需先 icall.py bootstrap
  返回 -> --ret void  （不回读）
```

**这块信息是这套工具最值钱的地方**：它把「函数在哪」升级成「它要什么、this 从哪来、
参数怎么装箱」。少看一条就会踩坑——比如把这个方法的 `rcx` 当 this 去写，直接写穿对象头。

### 例 3　谁在调它（反查调用者）

```bash
python tools/callers.py who "LevelDesignManager::SetOWEndlessLevelScoreMode"
```

```
symtab 36079 类 / 398316 方法 / pdata 424760 函数

谁调用 LevelDesignManager::SetOWEndlessLevelScoreMode(...)  @0x3444aa0
  (无静态调用者：可能虚调用 / 委托 / TVM 保护)
```

「无静态调用者」本身也是有效结论：说明它是从 **Lua 侧动态调的**
（LuaInterface 反射绑定，C# 里没有 call 边）。这条比瞎猜快得多。

### 例 4　它调用了谁

```bash
python tools/callers.py calls "LevelDesignManager::GetLevelScore"
```

```
函数 LevelDesignManager::GetLevelScore  @0x34103d0  长度 89 字节

  0x034103de  call  WrappersManagerImpl::IsPatched
  0x034103fd  call  sub_43E4C0
  0x03410418  call  sub_43E4C0
```

`IsPatched` 一出现，说明这个方法是 IFix 包装过的，真实现要去 patch 表里找。

（`calls` / `who` 需要 numpy，用**系统 python**；下面两个用哪个都行。）

### 例 5　查类有哪些方法、字段在哪个偏移

```bash
python tools/field_layout.py --grep "LevelDesignManager"
```

```
DLL 182.0 MB / symtab 398316 条 / 36079 个类 / pdata 424760 个函数

匹配 2 个类：
   LevelDesignManager                       2984 方法
   MPLevelDesignManager                     93 方法
```

确定类名后再查具体字段：

```bash
python tools/field_layout.py LevelDesignManager
```

它的输出是「类 → 字段偏移 + 名字」，底层读的就是 `data/bh3_layout_resolved.tsv`
（这个表 63 MB，用 Excel/VSCode 打开也行，第一列是类名）。

---

## 从「想调」到「敢调」的完整走法

五个查询只是零件。真正的流程在 `docs/调用路线_泛化_20261107.md` 里，六步：

```
① 定位     功能关键字 → 方法表 → 类::方法          （例 1）
② 校验     RVA 是函数入口？是折叠桩？是 IFix 壳？    （例 2 的"实现特征"）
③ 读实现   反汇编主路径 → 要不要 this？在哪个寄存器？ （例 2）
④ 拿实例   静态访问器 → 全局槽 → 五跳链             （例 2 的"全局槽" + mgr.py）
⑤ 定类型   参数按简写表装箱；返回值判 sret          （例 2 的"参数装箱"）
⑥ 验证     先调只读 getter 交叉印证，再调写操作
```

第 ④⑤ 步要**游戏开着**（`mgr.py get` / `icall.py` 是跨进程调用），前四步全是离线的。
建议顺序：离线把 ①②③ 走完、确认安全，再开游戏做 ④⑤⑥。

---

## 为什么单独成包

这套东西原来是散在 `rea/_rea/` 和 `_analysis/` 里的：符号表在一边、调用图在另一边、
反汇编脚本各自写死 `D:\BH3\7.0\app\...`。游戏目录一改名（`app` → `7.0` 就发生过）
就全废。

现在所有路径都走 `_paths.py` 现算：

- `data/` 跟着包走，挪到哪都能用
- 游戏目录靠**找 `BH3.exe`**，不假设叫什么名字
- 脚本里没有任何 `D:\BH3\...` 的绝对路径

`_paths.py` 的逻辑就三条：优先 `包根/data`，其次脚本同级 `data`；
游戏目录从包根往上找含 `BH3.exe` 的那一级。

---

## 已知取舍

- **首次跑 `callers.py` 会在 `tools/` 下生成 `_callsite_cache.npz`（约 75 MB）**，
  那是 callsite 把 pdata 函数边界预解析成 numpy 数组的缓存，第二次起就快了。
  嫌占地方可以删，下次自动重建。
- **没打包 `bh3_cg_hubs.tsv` / `hubs2.tsv`**（31 MB + 34 MB）——
  那是「被调用次数排行」，属于统计视图，需要的话可以从 `bh3_cg_edges.tsv`
  一行 `awk` 重算。
- **没有 UserAssembly.dll**：182 MB 的游戏本体文件，不适合塞进工具包。
  把工具包放到游戏目录旁边（或游戏目录里面）就能自动找到。
- `route.py` 的输出里会出现 `python mgr.py get 0x...` 和 `icall.py bootstrap`
  这类提示，那些是**运行期**动作（要游戏开着），和离线查表是两回事。
