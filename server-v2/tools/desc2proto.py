#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
把 FileDescriptorProto 的二进制（.desc）还原成可读的 .proto 文本。
来源：KianaProto.dll 里 protoc 生成的 *Reflection 类，其 descriptorData 是 base64。

用法：
    python desc2proto.py kiana.desc SeaRelWin8.4.0.proto
    python desc2proto.py --stats kiana.desc      # 只看统计
"""
import sys, json, collections

# ---------- 通用 protobuf wire 解析 ----------

def read_varint(buf, pos):
    result = 0
    shift = 0
    while True:
        b = buf[pos]
        pos += 1
        result |= (b & 0x7F) << shift
        if not (b & 0x80):
            return result, pos
        shift += 7


def parse_msg(buf):
    """把一段 protobuf 消息解析成 {field_number: [values...]}，varint 返回 int，其余返回 bytes。"""
    out = collections.defaultdict(list)
    pos = 0
    n = len(buf)
    while pos < n:
        key, pos = read_varint(buf, pos)
        field = key >> 3
        wire = key & 7
        if wire == 0:                       # varint
            v, pos = read_varint(buf, pos)
            out[field].append(v)
        elif wire == 1:                     # fixed64
            out[field].append(buf[pos:pos + 8]); pos += 8
        elif wire == 2:                     # length-delimited
            ln, pos = read_varint(buf, pos)
            out[field].append(buf[pos:pos + ln]); pos += ln
        elif wire == 5:                     # fixed32
            out[field].append(buf[pos:pos + 4]); pos += 4
        elif wire == 3:                     # group start -> 跳过（本 descriptor 里不该出现）
            raise ValueError("group 不支持")
        else:
            raise ValueError("未知 wire type %d @%d" % (wire, pos))
    return out


def s(v):
    return v.decode('utf-8', 'replace') if v else ''


# ---------- descriptor 结构 ----------

TYPE_MAP = {1: 'double', 2: 'float', 3: 'int64', 4: 'uint64', 5: 'int32',
            6: 'fixed64', 7: 'fixed32', 8: 'bool', 9: 'string', 10: 'group',
            11: 'message', 12: 'bytes', 13: 'uint32', 14: 'enum',
            15: 'sfixed32', 16: 'sfixed64', 17: 'sint32', 18: 'sint64'}

LABEL_MAP = {1: 'optional', 2: 'required', 3: 'repeated'}


def parse_enum(buf):
    m = parse_msg(buf)
    name = s(m[1][0]) if 1 in m else '?'
    values = []
    for v in m.get(2, []):
        vm = parse_msg(v)
        values.append((s(vm[1][0]) if 1 in vm else '?', vm[2][0] if 2 in vm else 0))
    return {'name': name, 'values': values}


def parse_field(buf):
    m = parse_msg(buf)
    f = {
        'name': s(m[1][0]) if 1 in m else '?',
        'number': m[3][0] if 3 in m else 0,
        'label': LABEL_MAP.get(m[4][0] if 4 in m else 1, 'optional'),
        'type': TYPE_MAP.get(m[5][0] if 5 in m else 0, '?'),
        'type_name': s(m[6][0]) if 6 in m else '',
        'oneof_index': m[9][0] if 9 in m else None,
        'proto3_optional': bool(m[17][0]) if 17 in m else False,
    }
    return f


def parse_message(buf):
    m = parse_msg(buf)
    msg = {
        'name': s(m[1][0]) if 1 in m else '?',
        'fields': [parse_field(f) for f in m.get(2, [])],
        'nested': [parse_message(x) for x in m.get(3, [])],
        'enums': [parse_enum(e) for e in m.get(4, [])],
        'oneofs': [s(o) for o in m.get(8, [])],
    }
    # oneof_decl 的名字还要再解一层
    if 8 in m:
        names = []
        for o in m[8]:
            om = parse_msg(o)
            names.append(s(om[1][0]) if 1 in om else '?')
        msg['oneofs'] = names
    return msg


def parse_file(buf):
    m = parse_msg(buf)
    return {
        'name': s(m[1][0]) if 1 in m else '?',
        'package': s(m[2][0]) if 2 in m else '',
        'dependency': [s(d) for d in m.get(3, [])],
        'messages': [parse_message(x) for x in m.get(4, [])],
        'enums': [parse_enum(e) for e in m.get(5, [])],
        'syntax': s(m[12][0]) if 12 in m else 'proto2',
    }


# ---------- 输出 .proto ----------

def emit_enum(e, indent=0):
    pad = '  ' * indent
    lines = [pad + 'enum %s {' % e['name']]
    for name, num in e['values']:
        lines.append('%s  %s = %d;' % (pad, name, num))
    lines.append(pad + '}')
    return lines


def emit_message(msg, indent=0):
    pad = '  ' * indent
    lines = [pad + 'message %s {' % msg['name']]
    for e in msg['enums']:
        lines += emit_enum(e, indent + 1)
    for f in msg['fields']:
        t = f['type_name'].lstrip('.') if f['type'] in ('message', 'enum') else f['type']
        label = '' if f['proto3_optional'] else (f['label'] + ' ' if f['label'] != 'optional' else '')
        extra = (' = %d;' % f['number'])
        if f['oneof_index'] is not None and f['oneof_index'] < len(msg['oneofs']):
            extra += '  // oneof ' + msg['oneofs'][f['oneof_index']]
        lines.append('%s  %s%s %s%s' % (pad, label, t, f['name'], extra))
    for n in msg['nested']:
        lines += emit_message(n, indent + 1)
    lines.append(pad + '}')
    return lines


def emit_file(fd):
    lines = ['// 由 desc2proto.py 从 KianaProto.dll 内嵌 descriptor 还原',
             '// 原始文件: %s  syntax: %s' % (fd['name'], fd['syntax']),
             'syntax = "%s";' % fd['syntax'], '']
    if fd['package']:
        lines.append('package %s;' % fd['package'])
        lines.append('')
    for d in fd['dependency']:
        lines.append('import "%s";' % d)
    if fd['dependency']:
        lines.append('')
    for e in fd['enums']:
        lines += emit_enum(e, 0)
        lines.append('')
    for m_ in fd['messages']:
        lines += emit_message(m_, 0)
        lines.append('')
    return '\n'.join(lines)


# ---------- main ----------

def main():
    args = sys.argv[1:]
    if not args:
        print(__doc__)
        return 1

    stats_only = '--stats' in args
    args = [a for a in args if not a.startswith('--')]
    src = args[0]
    dst = args[1] if len(args) > 1 else None

    raw = open(src, 'rb').read()
    fd = parse_file(raw)

    def count(msg):
        return 1 + sum(count(n) for n in msg['nested'])

    n_msg = sum(count(m_) for m_ in fd['messages'])
    n_field = 0

    def field_count(msg):
        return len(msg['fields']) + sum(field_count(n) for n in msg['nested'])

    n_field = sum(field_count(m_) for m_ in fd['messages'])
    n_enum = len(fd['enums']) + sum(len(m_['enums']) for m_ in fd['messages'])

    print('文件       : %s' % fd['name'])
    print('package    : %s' % fd['package'])
    print('syntax     : %s' % fd['syntax'])
    print('依赖       : %s' % (', '.join(fd['dependency']) or '(无)'))
    print('message    : %d 个' % n_msg)
    print('字段       : %d 个' % n_field)
    print('顶层 enum  : %d 个' % n_enum)

    if stats_only:
        return 0

    text = emit_file(fd)
    if dst:
        open(dst, 'w', encoding='utf-8', newline='\n').write(text)
        print('已写出     : %s  (%d 行, %d KB)' % (dst, text.count('\n'), len(text) // 1024))
    else:
        sys.stdout.write(text)
    return 0


if __name__ == '__main__':
    sys.exit(main())
