' ============================================================================
'  One-click FULL chain start (PROXY CHAIN fallback mode)
'
'  The existing full-chain one-click vbs runs everything in DIRECT mode
'  (no mitm, no proxy). This one is the FALLBACK: the same full chain
'  (server + login + client + patches) but with the old proxy chain
'  (mitmdump :8891 + system proxy) via launch_all.py start --mitm-proxy.
'
'  !! It sets the SYSTEM PROXY to 127.0.0.1:8891. If the stack dies without
'  !! the one-click STOP, the whole PC loses internet. The stop script now
'  !! restores the proxy (double layer); if that also fails, kill mitmdump
'  !! and set ProxyEnable=0 manually.
'
'  No hard-coded paths: game root (folder containing tools\python\python.exe)
'  is discovered from this file's location, same rule as the other vbs.
'  ASCII-ONLY: wscript reads ANSI; any CJK byte here becomes mojibake.
' ============================================================================
Option Explicit

Dim fso, sh, here, root, py, launcher, cmdline

Set fso = CreateObject("Scripting.FileSystemObject")
Set sh = CreateObject("Shell.Application")

here = fso.GetParentFolderName(WScript.ScriptFullName)
root = FindGameRoot(here)

If root = "" Then
    MsgBox "Cannot find the game folder." & vbCrLf & vbCrLf & _
           "Looked for tools\python\python.exe under:" & vbCrLf & _
           "  " & here & vbCrLf & "  (and its sub folders)", 16, "BH3"
    WScript.Quit 1
End If

py = fso.BuildPath(root, "tools\python\python.exe")
launcher = fso.BuildPath(root, "app\_launch\launch_all.py")

' 游戏目录可能已经改名（app -> 7.0），所以 app 下找不到就退回根下的 _launch。
' 主启动脚本一直有这层回退，这个备用脚本当初漏了，改名后直接弹
' "Launcher not found"。
If Not fso.FileExists(launcher) Then
    launcher = fso.BuildPath(root, "_launch\launch_all.py")
End If

If Not fso.FileExists(launcher) Then
    MsgBox "Launcher not found:" & vbCrLf & launcher, 16, "BH3"
    WScript.Quit 1
End If

' Full chain via launch_all with the PROXY CHAIN enabled (--mitm-proxy
' flips stack_args no_proxy/no_mitm inside launch_all; stop restores proxy).
cmdline = "/c chcp 65001>nul & """ & py & """ """ & launcher & _
          """ start --root """ & root & """ --mitm-proxy"

sh.ShellExecute "cmd.exe", cmdline, root, "runas", 1


Function FindGameRoot(here)
    ' game root = the folder that contains app\tools\python\python.exe
    ' (embedded python lives under app\ since 2026-11-05). Older layouts
    ' with <root>\tools\python are still accepted.
    Dim f, s, deep, inner
    FindGameRoot = ""
    If fso.FileExists(fso.BuildPath(here, "app\tools\python\python.exe")) Then
        FindGameRoot = here
        Exit Function
    End If
    If fso.FileExists(fso.BuildPath(here, "tools\python\python.exe")) Then
        FindGameRoot = here
        Exit Function
    End If
    If fso.FileExists(fso.BuildPath(here, "7.0\app\tools\python\python.exe")) Then
        FindGameRoot = fso.BuildPath(here, "7.0")
        Exit Function
    End If
    If fso.FileExists(fso.BuildPath(here, "7.0\tools\python\python.exe")) Then
        FindGameRoot = fso.BuildPath(here, "7.0")
        Exit Function
    End If
    If Not fso.FolderExists(here) Then Exit Function
    For Each f In fso.GetFolder(here).SubFolders
        If fso.FileExists(fso.BuildPath(f.Path, "app\tools\python\python.exe")) _
           Or fso.FileExists(fso.BuildPath(f.Path, "tools\python\python.exe")) Then
            FindGameRoot = f.Path
            Exit Function
        End If
    Next
    For Each s In fso.GetFolder(here).SubFolders
        For Each deep In s.SubFolders
            If fso.FileExists(fso.BuildPath(deep.Path, "app\tools\python\python.exe")) _
               Or fso.FileExists(fso.BuildPath(deep.Path, "tools\python\python.exe")) Then
                FindGameRoot = deep.Path
                Exit Function
            End If
            For Each inner In deep.SubFolders
                If fso.FileExists(fso.BuildPath(inner.Path, "app\tools\python\python.exe")) _
                   Or fso.FileExists(fso.BuildPath(inner.Path, "tools\python\python.exe")) Then
                    FindGameRoot = inner.Path
                    Exit Function
                End If
            Next
        Next
    Next
End Function
