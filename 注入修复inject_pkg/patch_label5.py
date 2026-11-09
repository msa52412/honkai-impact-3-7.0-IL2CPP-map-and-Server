# -*- coding: utf-8 -*-
"""给 patch 写入加「挂起其它线程」保护。

Text::OnPopulateMesh 是每帧都被调用的热函数。我们从一个后台线程写它的
入口 21 个字节，主线程极可能正好走到函数头，读到「写了一半」的指令
（前几字节已经是 FF 25，后面还是 old）—— 直接执行到垃圾字节，启动即崩。
冷函数（封面/标题图 getter）不会有这个问题，所以之前一直没暴露。
"""
import io

p = 'bh3bypass.c'
s = io.open(p, encoding='utf-8').read()

if 'suspend_other_threads' in s:
    print('already patched')
    raise SystemExit(0)

s = s.replace('#include <stdarg.h>', '#include <stdarg.h>\n#include <tlhelp32.h>', 1)

# 在 alloc_near 之前插入工具函数（alloc_near 一定在 apply_label 之前）
anchor = u'/* \u5728 anchor \u00b12GB \u5185\u627e\u4e00\u9875\u53ef\u6267\u884c\u5185\u5b58\uff08E9 rel32 \u53ea\u80fd\u8df3 \u00b12GB\uff09 */'
assert anchor in s, 'alloc_near anchor not found'

helper = u'''/* \u6253\u8865\u4e01\u65f6\u6302\u8d77\u5176\u5b83\u7ebf\u7a0b\uff1a\u70ed\u51fd\u6570\u88ab\u5176\u5b83\u7ebf\u7a0b\u6267\u884c\u5230\u4e00\u534a\u65f6\u88ab\u6211\u4eec\u6539\u5199\uff0c
 * \u4f1a\u76f4\u63a5\u6267\u884c\u5230\u5199\u4e86\u4e00\u534a\u7684\u6307\u4ee4\u3002\u6302\u8d77\u540e\u518d\u5199\uff0c\u5199\u5b8c\u518d\u653e\u5f00\u3002 */
#define MAX_SUSPEND 256
static int suspend_other_threads(HANDLE *out, int max)
{
    DWORD  pid = GetCurrentProcessId();
    DWORD  me  = GetCurrentThreadId();
    HANDLE snap;
    THREADENTRY32 te;
    int    n = 0;

    snap = CreateToolhelp32Snapshot(TH32CS_SNAPTHREAD, 0);
    if (snap == INVALID_HANDLE_VALUE) return 0;
    te.dwSize = sizeof(te);
    if (Thread32First(snap, &te)) {
        do {
            HANDLE h;
            if (te.th32OwnerProcessID != pid) continue;
            if (te.th32ThreadID == me) continue;
            if (n >= max) break;
            h = OpenThread(THREAD_SUSPEND_RESUME, FALSE, te.th32ThreadID);
            if (!h) continue;
            if (SuspendThread(h) == (DWORD)-1) { CloseHandle(h); continue; }
            out[n++] = h;
        } while (Thread32Next(snap, &te));
    }
    CloseHandle(snap);
    return n;
}

static void resume_other_threads(HANDLE *h, int n)
{
    int i;
    for (i = 0; i < n; i++) { ResumeThread(h[i]); CloseHandle(h[i]); }
}

'''
s = s.replace(anchor, helper + anchor, 1)

# apply_label 里用带保护的写法
old = u"""    if (!write_code(fn, patch, sizeof(patch))) {
        logf("[label] !! \u5199\u5165\u53e3\u8df3\u8f6c\u5931\u8d25 err=%lu", GetLastError());
        return;
    }
    if (memcmp(fn, patch, sizeof(patch)) == 0)"""
new = u"""    {
        HANDLE th[MAX_SUSPEND];
        int    nt = suspend_other_threads(th, MAX_SUSPEND);
        BOOL   wok = write_code(fn, patch, sizeof(patch));
        resume_other_threads(th, nt);
        logf("[label] \u5199\u5165\u65f6\u6302\u8d77\u4e86 %d \u4e2a\u7ebf\u7a0b", nt);
        if (!wok) {
            logf("[label] !! \u5199\u5165\u53e3\u8df3\u8f6c\u5931\u8d25 err=%lu", GetLastError());
            return;
        }
    }
    if (memcmp(fn, patch, sizeof(patch)) == 0)"""
assert old in s, 'apply_label write anchor not found'
s = s.replace(old, new, 1)

io.open(p, 'w', encoding='utf-8', newline='').write(s)
print('patched ok')
