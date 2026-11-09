# -*- coding: utf-8 -*-
"""校验 [card] 段 stub 的机器码（复刻 bh3bypass.c 的 build_setup_stub 输出并反汇编）。"""
import struct
import sys

from capstone import Cs, CS_ARCH_X86, CS_MODE_64

ORIG = bytes([0x40, 0x57, 0x48, 0x83, 0xEC, 0x20, 0x48, 0x8B, 0xF9,
              0xB9, 0x5F, 0x0B, 0x00, 0x00])
OFF_EID = 0x18
OFF_IDX = 0x1C

EID_FROM = 1013
EID_TO = 9999
IDX_TO = 8
HITS_VA = 0x1122334455667788   # 占位

RVA_SETUPDATA = 0x02CFE4C0
UA_DUMMY = 0x7FFA37500000


def build():
    out = bytearray()
    # mov eax, [rcx+0x18]
    out += bytes([0x8B, 0x41, OFF_EID])
    # cmp eax, imm32
    out += bytes([0x3D]) + struct.pack("<i", EID_FROM)
    # jne rel8
    j = len(out)
    out += bytes([0x75, 0x00])
    # mov dword [rcx+0x18], imm32
    out += bytes([0xC7, 0x41, OFF_EID]) + struct.pack("<i", EID_TO)
    # mov dword [rcx+0x1C], imm32
    out += bytes([0xC7, 0x41, OFF_IDX]) + struct.pack("<i", IDX_TO)
    # mov r11, imm64
    out += bytes([0x49, 0xBB]) + struct.pack("<Q", HITS_VA)
    # lock inc dword [r11]
    out += bytes([0xF0, 0x41, 0xFF, 0x03])
    replay = len(out)
    out += ORIG
    out += bytes([0xFF, 0x25, 0x00, 0x00, 0x00, 0x00])
    out += struct.pack("<Q", UA_DUMMY + RVA_SETUPDATA + len(ORIG))
    out[j + 1] = (replay - (j + 2)) & 0xFF
    return bytes(out), replay


def main():
    code, replay = build()
    print("stub %d 字节，replay 在 +%d，回跳目标 %#x\n" % (len(code), replay, UA_DUMMY + RVA_SETUPDATA + len(ORIG)))
    md = Cs(CS_ARCH_X86, CS_MODE_64)
    md.detail = True
    for ins in md.disasm(code, 0x1000):
        mark = ""
        if ins.address == 0x1000:
            mark = "  <- 入口"
        if ins.address == 0x1000 + len(code) - 14 - 6:
            mark = "  <- 回跳"
        print("  +%-3d  %-22s %-8s %s%s" %
              (ins.address - 0x1000, ins.bytes.hex(), ins.mnemonic, ins.op_str, mark))
    print()
    # 检查跳转目标
    ok = True
    for ins in md.disasm(code, 0x1000):
        if ins.mnemonic.startswith("j"):
            tgt = ins.operands[0].imm if ins.operands and ins.operands[0].type == 2 else None
            if tgt is not None:
                off = tgt - 0x1000
                okmark = "OK" if 0 <= off <= len(code) else "越界!"
                print("  跳转 %s @+%d -> +%d  [%s]" % (ins.mnemonic, ins.address - 0x1000, off, okmark))
                ok = ok and 0 <= off <= len(code)
    return 0 if ok else 1


if __name__ == "__main__":
    sys.exit(main())
