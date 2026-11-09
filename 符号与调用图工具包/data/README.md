# data/ —— 256 MB 派生数据，不进版本控制

这里的 TSV / JSON 是逆向提取的查表数据，体积太大（单文件最大 125 MB，超 GitHub
100 MB 硬限制），所以**不入库**，放在 Release 附件里：

| 文件 | 大小 | 内容 |
|---|---|---|
| `bh3_symtab.tsv` | 50 MB | rva / 命名空间 / 类 / 方法 / 返回 / 参数 / token / 模块 |
| `bh3_layout_resolved.tsv` | 63 MB | 类 → 每个字段的偏移 + 名字（il2cpp 布局还原） |
| `bh3_cg_funcs.tsv` | 13 MB | 函数表：va / body / thunk / name |
| `bh3_cg_edges.tsv` | 131 MB | 调用边：caller / callee / insn |
| `bh3_cg_modules.tsv` | 6 KB | 模块 → 地址区间 |
| `bh3_pdata_truth.tsv` | 10 MB | PE pdata 展开的函数边界 |
| `addr2name.json` | 10 MB | 地址 → 最接近的符号名 |

## 还原

从 Release 下载 `bh3-symtab-data.tar.xz`（约 23 MB），在本目录执行：

```bash
tar -xJf bh3-symtab-data.tar.xz --strip-components=1
```

`--strip-components=1` 是为了去掉包里的 `data/` 前缀，解到当前目录。

## 重新生成

不想下载的话，用 `../tools/` 里的脚本对 `UserAssembly.dll` 重新提取，
数据截止版本见 `../README.md`。注意游戏每次更新后 rva 都会变，旧数据会失配。
