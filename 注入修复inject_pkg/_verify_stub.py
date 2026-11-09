# -*- coding: utf-8 -*-
"""复现 C 侧 build_entry_stub / build_trampoline 的字节序列，用 capstone 核对。

假地址：g_entry 按结构体偏移，card_id +0 / hit_seq +8 / last_id +12 / last_meta +16
"""
import struct
import capstone

BASE     = 0x10000000
CARD     = BASE + 0
HITSEQ   = BASE + 8
LASTID   = BASE + 12
LASTMETA = BASE + 16
RETADDR  = BASE + 0x40
ONRET    = 0x10001000

UA = 0x7FFA00000000
RVA_DOCLICK = 0x2CFB880
ORIG = bytes.fromhex("4055565741544155415641574883ec70")
TRAMP = BASE + 0x200


def mov_r11(a):
    return b"\x49\xBB" + struct.pack("<Q", a)


def mov_rax(a):
    return b"\x48\xB8" + struct.pack("<Q", a)


# ---------------- stub ----------------
p = bytearray()
p += b"\x48\x8B\x81\xC8\x00\x00\x00"
p += b"\x48\x85\xC0"
j_a = len(p); p += b"\x74\x00"
p += b"\x4C\x8B\x48\x18"
p += b"\x4D\x85\xC9"
j_b = len(p); p += b"\x74\x00"
p += b"\x41\x8B\x41\x20"
p += mov_r11(LASTID) + b"\x41\x89\x03"
p += mov_r11(LASTMETA) + b"\x4D\x89\x0B"
p += mov_r11(CARD) + b"\x41\x3B\x03"
j_hit = len(p); p += b"\x74\x00"
j_skip = len(p); p += b"\xEB\x00"

hit = len(p)
p += mov_r11(HITSEQ) + b"\xF0\x41\xFF\x03"
p += b"\x4C\x8B\x1C\x24"                      # mov r11,[rsp]
p += mov_rax(RETADDR) + b"\x4C\x89\x18"       # mov [rax],r11
p += mov_rax(TRAMP) + b"\x48\x89\x04\x24"     # mov [rsp],rax

replay = len(p)
p += ORIG
p += b"\xFF\x25\x00\x00\x00\x00" + struct.pack("<Q", UA + RVA_DOCLICK + 16)

for at, tgt in ((j_a, replay), (j_b, replay), (j_hit, hit), (j_skip, replay)):
    rel = tgt - (at + 2)
    assert 0 <= rel <= 127, rel
    p[at + 1] = rel

# ---------------- trampoline ----------------
t = bytearray()
t += b"\x50\x51\x52"
t += b"\x41\x50\x41\x51\x41\x52\x41\x53"
t += b"\x48\x83\xEC\x28"
t += mov_rax(ONRET)
t += b"\xFF\xD0"
t += b"\x48\x83\xC4\x28"
t += b"\x41\x5B\x41\x5A\x41\x59\x41\x58"
t += b"\x5A\x59"
t += b"\x41\x5A"
t += mov_rax(RETADDR)
t += b"\x48\x8B\x00"
t += b"\x49\x92"
t += b"\x41\xFF\xE2"

md = capstone.Cs(capstone.CS_ARCH_X86, capstone.CS_MODE_64)

print("================ stub（%d 字节，hit@%d replay@%d）================" % (len(p), hit, replay))
for ins in md.disasm(bytes(p), BASE):
    off = ins.address - BASE
    tag = "  <== hit" if off == hit else ("  <== replay" if off == replay else "")
    print("  +%03d  %-26s %-8s %s%s" % (off, ins.bytes.hex(" "), ins.mnemonic, ins.op_str, tag))

print("\n================ trampoline（%d 字节）================" % len(t))
for ins in md.disasm(bytes(t), TRAMP):
    print("  +%03d  %-26s %-8s %s" % (ins.address - TRAMP, ins.bytes.hex(" "),
                                      ins.mnemonic, ins.op_str))

# ---- 自检 ----
assert ORIG in bytes(p), "原始字节没内嵌"
assert p.endswith(struct.pack("<Q", UA + RVA_DOCLICK + 16))

pops = [i.op_str for i in md.disasm(bytes(t), TRAMP) if i.mnemonic == "pop"]
expect = ["r11", "r10", "r9", "r8", "rdx", "rcx", "r10"]
assert pops == expect, (pops, expect)

i_xchg = [i for i in md.disasm(bytes(t), TRAMP) if i.mnemonic == "xchg"]
assert i_xchg and set(i_xchg[0].op_str.replace(" ", "").split(",")) == {"rax", "r10"}, i_xchg
print("\n[ok] stub 内嵌原始字节 + 尾部指向 DoClickAction+16")
print("[ok] push/pop 对称: %s" % " ".join(pops))
print("[ok] xchg rax,r10 保住返回值，jmp r10 回到真实返回地址")
