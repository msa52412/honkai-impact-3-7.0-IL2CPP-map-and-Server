"""换「世界地图挑战面板」入口卡的展示图（卡面 Cover / 标题图 Title）。

原理（反汇编实证）
----------------
`MonoWorldMapEntryContainerBase::SetupEntryImage` @0x2CFE570：
    path = ContainerData::FNBHMJNJAOC(cd)            @0xA51940
         -> FIGGCBHEGMC::BBPJBPALLLE(meta)           @0x44FBC60
         -> MINFHABIHNC::MNCMBNNOOCF(entry, col=2, 16) @0x43B37D0
    SetImageAsync(image, path)  /  (transform+0x28)  /  (transform+0x88)

而 `MNCMBNNOOCF` 是**带缓存的列访问器**：
    if (entry.cache[col] != null) return entry.cache[col];
    entry.cache[col] = 解析该行该列;
    return ...

所以只要把 `entry+0x28`（object[] 列缓存）里第 2 项换成另一个 String 对象，
下次 `SetupEntryImage` 就会用新图 —— 不动配置行、不动 ExcelMeta。

列约定（实测 ID 11/14/15/12/10/8/5 全部吻合）：
    cache[1] Int32[]   feature 位图
    cache[2] String    卡面 Cover
    cache[3] String    标题图 Title

entry 地址从 `app/bh3bypass.log` 里最近一次点击记录里取：
    [entry] 点击 id=11 meta=0x00000700FC847A20  => 命中
meta -> entry 是 `[meta+0x18]`（ExcelMeta 行内那个 ptr 字段）。

用法
----
    python card_cover.py --show                    # 看当前卡的 Cover / Title
    python card_cover.py --path SpriteOutput/WorldMap/OWEndless/Cover_12
    python card_cover.py --path ... --col 3        # 换标题图
    python card_cover.py --id 14 --show            # 换别的卡

改完去游戏里关掉挑战面板再打开，卡片会重新 SetupEntryImage。
"""
import ctypes
import ctypes.wintypes as wt
import glob
import os
import re
import struct
import sys

HERE = os.path.dirname(os.path.abspath(__file__))
sys.path.insert(0, os.path.dirname(HERE))
sys.path.insert(0, HERE)
try:
    import _paths
except ImportError:
    _paths = None
import icall as I          # noqa: E402

LOG_CANDIDATES = [
    # 优先用 _paths 找出来的游戏目录；下面的老路径只作兜底（app 可能已改名）
    (os.path.join(_paths.GAME, "bh3bypass.log") if _paths and _paths.GAME else ""),
    os.path.join(os.path.dirname(os.path.dirname(HERE)), "app", "bh3bypass.log"),
]
LOG_CANDIDATES = [p for p in LOG_CANDIDATES if p]

OFF_META_ENTRY = 0x18      # ExcelMeta 行 -> entry (MINFHABIHNC)
OFF_META_ID = 0x20         # ExcelMeta 行 -> int ID
OFF_ENTRY_CACHE = 0x28     # entry -> object[] 列缓存
ARR_LEN = 0x18             # Il2CppArray 长度
ARR_ITEMS = 0x20           # Il2CppArray 元素起点
STR_LEN = 0x10             # Il2CppString 长度（字符数）
STR_BODY = 0x14            # Il2CppString 正文（UTF-16）


# ---------------------------------------------------------------- 基础读写
# 注意：find_str.iter_regions 会把 ReadProcessMemory 的 argtypes 改成 c_ulonglong，
# 而 icall.read_mem 按 c_void_p 传参，混用会 TypeError。这里统一用自己的 rd()。
_k32 = I.k32
_rd = ctypes.c_size_t()


def restore_argtypes():
    """icall 的 read_mem 按 c_void_p 传参；find_str.iter_regions 会改成 c_ulonglong。
    任何一处改完都要调这个复原，否则同一进程里 icall 立刻报 TypeError。"""
    _k32.ReadProcessMemory.argtypes = [wt.HANDLE, ctypes.c_void_p, ctypes.c_void_p,
                                       ctypes.c_size_t,
                                       ctypes.POINTER(ctypes.c_size_t)]


restore_argtypes()


def rd(h, addr, n):
    r = I.read_mem(h, addr, n)
    return r if r else None


def q(h, a):
    r = rd(h, a, 8)
    return struct.unpack("<Q", r)[0] if r and len(r) == 8 else None


def i32(h, a):
    r = rd(h, a, 4)
    return struct.unpack("<i", r)[0] if r and len(r) == 4 else None


def kname(h, obj):
    k = q(h, obj)
    if not k:
        return None
    np_ = q(h, k + 0xA8)
    if not np_:
        return None
    r = rd(h, np_, 48)
    return r[:r.find(b"\x00")].decode("ascii", "replace") if r else None


def read_str(h, obj):
    """Il2CppString -> python str（非法返回 None）"""
    if not obj:
        return None
    if kname(h, obj) != "String":
        return None
    n = i32(h, obj + STR_LEN)
    if n is None or n < 0 or n > 512:
        return None
    r = rd(h, obj + STR_BODY, n * 2)
    return r.decode("utf-16-le", "replace") if r else None


# ---------------------------------------------------------------- 定位 entry
def meta_from_log(want_id):
    """从 DLL 日志里抓最近一次点击的 meta 地址"""
    for p in LOG_CANDIDATES:
        if not os.path.exists(p):
            continue
        txt = open(p, "rb").read().decode("utf-8", "replace")
        hits = re.findall(r"\[entry\] 点击 id=(\d+) meta=([0-9A-Fa-f]+)", txt)
        for mid, addr in reversed(hits):
            if int(mid) == want_id:
                v = int(addr, 16)
                if v:
                    return v, p
    return None, None


def resolve(want_id, meta_arg=None):
    h, pid, base = I.open_game()
    if not h:
        print("游戏没在跑")
        return None

    meta = meta_arg
    src = "命令行"
    if not meta:
        meta, lp = meta_from_log(want_id)
        src = lp or "?"
    if not meta:
        print("没找到 meta 地址。先在游戏里点一下那张卡（DLL 会写日志），或手动 --meta 0x...")
        return None
    if i32(h, meta + OFF_META_ID) != want_id:
        print("meta=%#x 处的 ID 不是 %d（读到 %s）—— 地址过期了，重新点一下卡"
              % (meta, want_id, i32(h, meta + OFF_META_ID)))
        return None

    entry = q(h, meta + OFF_META_ENTRY)
    if not entry:
        print("meta+0x18 为空，entry 还没解析")
        return None
    cache = q(h, entry + OFF_ENTRY_CACHE)
    if not cache:
        print("entry+0x28（列缓存）为空 —— 面板没装配过？开关一次面板再来")
        return None
    print("meta=%#x (来源: %s)" % (meta, src))
    print("entry=%#x  cache=%#x  len=%d" % (entry, cache, i32(h, cache + ARR_LEN)))
    return h, entry, cache


# ---------------------------------------------------------------- 扫字符串
def find_string_obj(h, path):
    """在进程内存里找内容等于 path 的 Il2CppString，返回对象地址"""
    import find_str as FS

    needle = path.encode("utf-16-le")
    k32 = I.k32
    k32.ReadProcessMemory.argtypes = [wt.HANDLE, ctypes.c_ulonglong, ctypes.c_void_p,
                                      ctypes.c_size_t,
                                      ctypes.POINTER(ctypes.c_size_t)]
    k32.ReadProcessMemory.restype = wt.BOOL

    mem = bytearray(2 << 20)
    buf = (ctypes.c_char * len(mem)).from_buffer(mem)
    got = ctypes.c_size_t()

    try:
        for rbase, rsize in FS.iter_regions(h):
            if rsize > (256 << 20):
                continue
            off = 0
            while off < rsize:
                chunk = min(len(mem), rsize - off)
                addr = rbase + off
                ok = k32.ReadProcessMemory(h, addr, buf, chunk, ctypes.byref(got))
                off += chunk
                if not ok or got.value < len(needle):
                    continue
                data = mem[:got.value]
                i = data.find(needle)
                while i >= 0:
                    # 正文前 4 字节就是 Il2CppString 的 length（int32）。
                    # 用它做校验，不再回读进程内存 —— 扫描期间 argtypes 被
                    # 改成整数版，任何 read_str 都会 TypeError。
                    if i >= STR_BODY:
                        n = struct.unpack("<i", data[i - 4:i])[0]
                        if n * 2 == len(needle):
                            return addr + i - STR_BODY
                    i = data.find(needle, i + 2)
    finally:
        restore_argtypes()
    return None


# ---------------------------------------------------------------- 主流程
def main():
    want = 11
    if "--id" in sys.argv:
        want = int(sys.argv[sys.argv.index("--id") + 1])
    meta_arg = None
    if "--meta" in sys.argv:
        meta_arg = int(sys.argv[sys.argv.index("--meta") + 1], 16)

    got = resolve(want, meta_arg)
    if not got:
        return 1
    h, entry, cache = got

    # 列出缓存内容
    n = i32(h, cache + ARR_LEN) or 0
    print("\n列缓存：")
    for i in range(n):
        v = q(h, cache + ARR_ITEMS + i * 8)
        s = read_str(h, v)
        print("  [%d] %#012x  %s" % (i, v or 0, ('"%s"' % s) if s else (kname(h, v) or "null")))

    if "--show" in sys.argv or "--path" not in sys.argv:
        return 0

    col = 2
    if "--col" in sys.argv:
        col = int(sys.argv[sys.argv.index("--col") + 1])
    path = sys.argv[sys.argv.index("--path") + 1]

    print("\n目标：col=%d -> \"%s\"" % (col, path))
    obj = find_string_obj(h, path)
    if not obj:
        print("没找到该路径的 String 对象（路径拼错？或这张图本局没加载过）")
        return 1
    print("命中 String 对象 @ %#x" % obj)

    old = q(h, cache + ARR_ITEMS + col * 8)
    if not I.write_mem(h, cache + ARR_ITEMS + col * 8, struct.pack("<Q", obj)):
        print("写入失败")
        return 1
    back = q(h, cache + ARR_ITEMS + col * 8)
    print("写入 %#x -> %#x  (%s)" % (old or 0, back, "OK" if back == obj else "不一致!"))
    print("\n现在去游戏里关掉挑战面板再打开，卡面就会用新图。")
    return 0


if __name__ == "__main__":
    sys.exit(main())
