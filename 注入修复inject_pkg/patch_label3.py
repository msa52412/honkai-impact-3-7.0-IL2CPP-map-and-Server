# -*- coding: utf-8 -*-
"""[label] 改挂 Text::OnPopulateMesh（网格重建那一刻），不再挂 OnEnable。

原因：OnEnable 里把 m_Text 换成自建串后，颜色留下了、文字却又被写回原串
（实测 m_Color 是我们写的 FF4D4D，m_Text 还是堆里的原字符串），说明
OnEnable 之后有代码直接改 m_Text 且不走 set_text。改在 OnPopulateMesh
入口动手，是「最后一道关」——之后网格就按这个串生成字形。

入口 21 字节（都无 rip 相对寻址，可整段搬到 trampoline 回放）：
  4889542410        mov [rsp+0x10], rdx
  55 53 57          push rbp / push rbx / push rdi
  488dac2460ffffff  lea rbp, [rsp-0xa0]
  4881eca0010000    sub rsp, 0x1a0
之后 0x6905D37 是 xor eax,eax。
"""
import io

p = 'bh3bypass.c'
s = io.open(p, encoding='utf-8').read()

if 'RVA_LABEL_ENABLE  0x06905D20' in s:
    print('already patched')
    raise SystemExit(0)

old_def = u"""#define RVA_LABEL_ENABLE  0x06905CD0
#define LEN_LABEL_ENABLE  9
static const BYTE ORIG_LABEL_ENABLE[LEN_LABEL_ENABLE] = {
    0x40, 0x53, 0x48, 0x83, 0xEC, 0x20, 0x48, 0x8B, 0xD9
};"""
new_def = u"""#define RVA_LABEL_ENABLE  0x06905D20
#define LEN_LABEL_ENABLE  21
static const BYTE ORIG_LABEL_ENABLE[LEN_LABEL_ENABLE] = {
    0x48, 0x89, 0x54, 0x24, 0x10,
    0x55, 0x53, 0x57,
    0x48, 0x8D, 0xAC, 0x24, 0x60, 0xFF, 0xFF, 0xFF,
    0x48, 0x81, 0xEC, 0xA0, 0x01, 0x00, 0x00
};"""
assert old_def in s, 'label defs not found'
s = s.replace(old_def, new_def, 1)

# 替换 apply_label 里「E9 相对跳转 + alloc_near」那一段
start = s.index(u'static void apply_label(HMODULE ua)')
end   = s.index(u'static DWORD WINAPI label_watch(LPVOID param)')
body = s[start:end]

new_body = u'''static void apply_label(HMODULE ua)
{
    BYTE *fn = (BYTE *)ua + RVA_LABEL_ENABLE;
    BYTE *stub, *tramp, *end, *tend;
    BYTE  patch[LEN_LABEL_ENABLE];
    int   i;
    char  hs[256];

    hexdump(fn, LEN_LABEL_ENABLE, hs, sizeof(hs));
    logf("[label] Text::OnPopulateMesh %p 当前: %s", fn, hs);

    if (fn[0] == 0xFF && fn[1] == 0x25) {
        logf("[label] 已经是补丁状态，跳过");
        return;
    }
    if (!is_original(fn, ORIG_LABEL_ENABLE, LEN_LABEL_ENABLE)) {
        logf("[label] !! 原始字节不匹配，拒绝打补丁");
        return;
    }
    stub = (BYTE *)VirtualAlloc(NULL, 0x1000, MEM_COMMIT | MEM_RESERVE,
                                PAGE_EXECUTE_READWRITE);
    if (!stub) {
        logf("[label] !! VirtualAlloc 失败 err=%lu", GetLastError());
        return;
    }
    tramp = stub + 0x400;
    end   = build_label_stub(stub, tramp);
    tend  = emit_bytes(tramp, ORIG_LABEL_ENABLE, LEN_LABEL_ENABLE);
    *tend++ = 0xFF; *tend++ = 0x25;
    *tend++ = 0x00; *tend++ = 0x00; *tend++ = 0x00; *tend++ = 0x00;
    {
        ULONGLONG v = (ULONGLONG)(ULONG_PTR)((BYTE *)ua + RVA_LABEL_ENABLE + LEN_LABEL_ENABLE);
        memcpy(tend, &v, 8);
        tend += 8;
    }
    FlushInstructionCache(GetCurrentProcess(), stub, (DWORD)(tend - stub));
    logf("[label] stub @ %p（%d 字节）  trampoline @ %p（%d 字节）",
         stub, (int)(end - stub), tramp, (int)(tend - tramp));

    patch[0] = 0xFF;
    patch[1] = 0x25;
    patch[2] = patch[3] = patch[4] = patch[5] = 0x00;
    *(ULONGLONG *)(patch + 6) = (ULONGLONG)(ULONG_PTR)stub;
    for (i = 14; i < LEN_LABEL_ENABLE; i++) patch[i] = 0x90;

    if (!write_code(fn, patch, sizeof(patch))) {
        logf("[label] !! 写入口跳转失败 err=%lu", GetLastError());
        return;
    }
    if (memcmp(fn, patch, sizeof(patch)) == 0)
        logf("[label] OK  入口已接管（from len=%d -> to len=%d）",
             g_label_fromlen, g_label_tolen);
    else
        logf("[label] !! 写入后回读不一致");
}

'''
s = s[:start] + new_body + s[end:]
io.open(p, 'w', encoding='utf-8', newline='').write(s)
print('patched to OnPopulateMesh')
