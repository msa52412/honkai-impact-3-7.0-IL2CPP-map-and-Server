# -*- coding: utf-8 -*-
"""
callsite.py — 全量 call 指令索引（向量化），供枚举还原 / 调用图补充使用。

numpy 一次扫完 il2cpp 节（103 MB / 480 万个 E8 候选）约 0.4 秒，
过滤出「目标落在已映射节内」的真实调用点，缓存在 _analysis/_callsite_*.npz。
"""
import os
import sys
import numpy as np

sys.path.insert(0, os.path.dirname(os.path.abspath(__file__)))
import field_layout as F

CACHE = os.path.join(os.path.dirname(os.path.abspath(__file__)), "_callsite_cache.npz")
SCAN_SECTIONS = ("il2cpp", ".text")


def build(pe=None, use_cache=True):
    if use_cache and os.path.exists(CACHE):
        z = np.load(CACHE)
        return z["rva"], z["tgt"]
    pe = pe or F.PE(F.DLL)
    all_rva, all_tgt = [], []
    for nm, va, sz, roff in pe.secs:
        if nm not in SCAN_SECTIONS:
            continue
        blob = pe.d[roff:roff + sz]
        a = np.frombuffer(blob, dtype=np.uint8)
        pos = np.where(a == 0xE8)[0]
        pos = pos[pos + 5 <= len(a)]
        if pos.size == 0:
            continue
        rel = (a[pos + 1].astype(np.int64) | (a[pos + 2].astype(np.int64) << 8)
               | (a[pos + 3].astype(np.int64) << 16) | (a[pos + 4].astype(np.int64) << 24))
        rel = (rel ^ 0x80000000) - 0x80000000
        tgt = va + pos + 5 + rel
        # 只保留目标落在任意已映射节内的
        lo = min(s[1] for s in pe.secs)
        hi = max(s[1] + s[2] for s in pe.secs)
        keep = (tgt >= lo) & (tgt < hi)
        all_rva.append((va + pos)[keep].astype(np.int64))
        all_tgt.append(tgt[keep])
    rva = np.concatenate(all_rva) if all_rva else np.zeros(0, np.int64)
    tgt = np.concatenate(all_tgt) if all_tgt else np.zeros(0, np.int64)
    np.savez(CACHE, rva=rva, tgt=tgt)
    return rva, tgt


def sites_to(target_rva, pe=None):
    """返回所有 call 到 target_rva 的指令 RVA（rva/tgt 都用 RVA 表示）"""
    rva, tgt = build(pe)
    if target_rva >= F.BASE:
        target_rva -= F.BASE
    return rva[tgt == target_rva]


if __name__ == "__main__":
    pe = F.PE(F.DLL)
    import time
    t0 = time.time()
    rva, tgt = build(pe, use_cache=False)
    print("call 点 %d 个，用时 %.2fs" % (len(rva), time.time() - t0))
    print("tgt 范围 %#x .. %#x" % (tgt.min(), tgt.max()))
    print("rva 范围 %#x .. %#x" % (rva.min(), rva.max()))
