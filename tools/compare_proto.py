#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
把 Common.dll 的 proto 定义（common_dump.tsv）与权威 .proto（来自 KianaProto.dll 的 descriptor）
逐字段比对，找出：
  1. 同 tag 不同名      —— 会导致客户端读错字段语义
  2. 同 tag 不同类型    —— 会导致解码失败或值错乱
  3. 同 tag 不同 label  —— repeated/optional 不符，packed 编码直接崩
  4. 缺失字段           —— 客户端下发/期望的字段服务端没有
  5. 缺失 message       —— 整个消息没有

用法: python compare_proto.py kiana.desc common_dump.tsv
"""
import sys, collections
import desc2proto as d2p


def norm_type(t):
    t = t.lstrip('.')
    if t.startswith('KianaBH.Proto.'):
        t = t[len('KianaBH.Proto.'):]
    # 嵌套枚举 GetMainDataRsp.Retcode -> Retcode
    return t.split('.')[-1]


def load_kiana(desc_path):
    fd = d2p.parse_file(open(desc_path, 'rb').read())
    msgs = {}
    enums = {}

    def walk(m, prefix=''):
        key = prefix + m['name']
        fields = {}
        for f in m['fields']:
            fields[f['number']] = (f['name'], norm_type(f['type_name']) if f['type'] in ('message', 'enum') else f['type'],
                                   f['label'])
        msgs[key] = fields
        for e in m['enums']:
            enums[key + '.' + e['name']] = dict(e['values'])
        for n in m['nested']:
            walk(n, key + '.')

    for m in fd['messages']:
        walk(m)
    for e in fd['enums']:
        enums[e['name']] = dict(e['values'])
    return fd, msgs, enums


def load_common(tsv_path):
    msgs = collections.defaultdict(dict)
    enums = collections.defaultdict(dict)
    for line in open(tsv_path, encoding='utf-8'):
        line = line.rstrip('\n')
        if not line or line.startswith('#'):
            continue
        p = line.split('\t')
        if p[0] == 'M':
            _, msg, tag, name, ptype, label, packed, cs = p
            msgs[msg][int(tag)] = (name, norm_type(ptype), label, packed)
        elif p[0] == 'E':
            _, enum, val, name = p
            enums[enum][name] = int(val)
    return msgs, enums


def main():
    desc, tsv = sys.argv[1], sys.argv[2]
    fd, kiana, k_enum = load_kiana(desc)
    common, c_enum = load_common(tsv)

    both = sorted(set(kiana) & set(common))
    only_common = sorted(set(common) - set(kiana))
    only_kiana = sorted(set(kiana) - set(common))

    print('=' * 78)
    print('权威 .proto : %s' % fd['name'])
    print('  message %d / enum %d' % (len(kiana), len(k_enum)))
    print('Common.dll  : message %d / enum %d' % (len(common), len(c_enum)))
    print('  两边都有  : %d' % len(both))
    print('  仅权威有  : %d  （版本更新，7.0 本就该没有）' % len(only_kiana))
    print('  仅 Common : %d  （7.0 独有或命名不同）' % len(only_common))
    print('=' * 78)

    name_diff, type_diff, label_diff, missing, extra = [], [], [], [], []
    packed_diff = []

    for msg in both:
        kf, cf = kiana[msg], common[msg]
        for tag in sorted(set(kf) & set(cf)):
            kn, kt, kl = kf[tag]
            cn, ct, cl, cp = cf[tag]
            if kn != cn:
                name_diff.append((msg, tag, kn, cn, kt))
            if kt != ct:
                type_diff.append((msg, tag, kn, kt, ct))
            if kl != cl:
                label_diff.append((msg, tag, kn, kl, cl))
            # packed：repeated 基础类型在 proto3 默认为 packed，proto2 需显式
            if kl == 'repeated' and kt in ('uint32', 'int32', 'bool', 'enum', 'uint64', 'int64', 'sint32', 'sint64'):
                if cp != 'packed':
                    packed_diff.append((msg, tag, kn, kt))
        for tag in sorted(set(kf) - set(cf)):
            missing.append((msg, tag, kf[tag][0], kf[tag][1], kf[tag][2]))
        for tag in sorted(set(cf) - set(kf)):
            extra.append((msg, tag, cf[tag][0], cf[tag][1]))

    def head(title, rows, n=25):
        print()
        print('【%s】 %d 处' % (title, len(rows)))
        for r in rows[:n]:
            print('   ' + '  '.join(str(x) for x in r))
        if len(rows) > n:
            print('   ... 其余 %d 处' % (len(rows) - n))

    head('同 tag 不同字段名', name_diff)
    head('同 tag 不同类型', type_diff)
    head('同 tag 不同 label', label_diff)
    head('repeated 基础类型未标 packed', packed_diff, 10)
    head('权威有 / Common 缺字段', missing, 30)
    head('Common 有 / 权威无字段', extra, 30)

    # 枚举比对（按成员名 + 值）
    e_both = set(c_enum) & set(k_enum)
    e_bad = []
    for e in sorted(e_both):
        c, k = c_enum[e], k_enum[e]
        common_names = set(c) & set(k)
        for nm in sorted(common_names):
            if c[nm] != k[nm]:
                e_bad.append((e, nm, k[nm], c[nm]))
    print()
    print('【枚举 同名成员值不同】 %d 处（共比对 %d 个同名枚举）' % (len(e_bad), len(e_both)))
    for r in e_bad[:20]:
        print('   ' + '  '.join(str(x) for x in r))

    print()
    print('=' * 78)
    print('结论要点：')
    print('  ① tag 冲突（名字/类型/label）= 真错误，必须改')
    print('  ② 缺字段 = 7.0 的 Common 落后于 8.4.0，属版本差，通常无害')
    print('  ③ 未标 packed 的 repeated 基础类型 = 客户端可能按 packed 解，需实测')
    print('=' * 78)


if __name__ == '__main__':
    main()
