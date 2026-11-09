' ============================================================================




'    1)  launch_v2 v2_state.json -> proxy_prev
'    2)  127.0.0.1:8891  mitmdump  -> ProxyEnable=0

'    3)  8891 -> 

'   VBS + WMI  pythonpython 
'  ASCII-ONLY: wscript reads ANSI; any CJK byte here becomes mojibake.
' ============================================================================
Option Explicit

Dim fso, wsh, here, root, stateFile, json, prevServer, prevEnable
Dim enableVal, proxyServer, mitmAlive, port8891

Set fso = CreateObject("Scripting.FileSystemObject")
Set wsh = CreateObject("WScript.Shell")

here = fso.GetParentFolderName(WScript.ScriptFullName)
root = FindGameRoot(here)

Const REG = "HKCU\Software\Microsoft\Windows\CurrentVersion\Internet Settings\"

' ---- current state ----
enableVal = -1
proxyServer = ""
On Error Resume Next
enableVal = wsh.RegRead(REG & "ProxyEnable")
proxyServer = wsh.RegRead(REG & "ProxyServer")
On Error GoTo 0

If enableVal = 0 Then
    MsgBox "System proxy is already OFF. Nothing to do." & vbCrLf & _
           "ProxyServer = " & proxyServer, 64, "BH3 restore proxy"
    WScript.Quit 0
End If

' ---- is mitmdump (8891) alive? via WMI netstat-free check ----
mitmAlive = False
port8891 = False
On Error Resume Next
Dim wmi, ports, p
Set wmi = GetObject("winmgmts:\\.\root\StandardCimv2")
If Not wmi Is Nothing Then
    Set ports = wmi.ExecQuery("SELECT * FROM MSFT_TCPEndpointConnection")
    ' fallback: simple TCP connect test instead (WMI class may not exist)
End If
On Error GoTo 0
If Not port8891 Then
    port8891 = TcpOpen("127.0.0.1", 8891)
End If
mitmAlive = port8891

' ---- decide ----
If InStr(proxyServer, "8891") > 0 And Not mitmAlive Then
    ' proxy points to dead mitmdump -> force off (case 2/3)
    wsh.RegWrite REG & "ProxyEnable", 0, "REG_DWORD"
    MsgBox "Proxy pointed to dead mitmdump (127.0.0.1:8891)." & vbCrLf & _
           "ProxyEnable forced to 0. You should be online now.", 64, "BH3 restore proxy"
    WScript.Quit 0
End If

' ---- layer 1: restore from launch_v2 backup (v2_state.json) ----
prevServer = ""
prevEnable = 0
stateFile = fso.BuildPath(root, "app\_launch\logs\v2_state.json")
' 游戏目录可能已经改名（app -> 7.0），app 下没有就退回根下的 _launch。
If root <> "" And Not fso.FileExists(stateFile) Then
    stateFile = fso.BuildPath(root, "_launch\logs\v2_state.json")
End If
If root <> "" And fso.FileExists(stateFile) Then
    json = ReadFile(stateFile)
    prevServer = ExtractJsonString(json, "ProxyServer")
    prevEnable = ExtractJsonBool(json, "ProxyEnable")
End If

If prevServer <> "" Then
    wsh.RegWrite REG & "ProxyServer", prevServer, "REG_SZ"
    wsh.RegWrite REG & "ProxyEnable", prevEnable, "REG_DWORD"
    ' refresh WinINET so the change takes effect immediately
    On Error Resume Next
    wsh.Run "rundll32.exe wininet.dll,InternetSetOption 0 39 0 0", 0, True
    wsh.Run "rundll32.exe wininet.dll,InternetSetOption 0 37 0 0", 0, True
    On Error GoTo 0
    MsgBox "Proxy restored from backup:" & vbCrLf & _
           "  ProxyServer = " & prevServer & vbCrLf & _
           "  ProxyEnable = " & prevEnable, 64, "BH3 restore proxy"
    WScript.Quit 0
End If

' ---- no backup: current proxy is ours -> just disable ----
wsh.RegWrite REG & "ProxyEnable", 0, "REG_DWORD"
MsgBox "No backup found; current proxy was " & proxyServer & vbCrLf & _
       "ProxyEnable set to 0. You should be online now.", 64, "BH3 restore proxy"


Function TcpOpen(host, port)
    Dim sh2, ts, tmp, body
    TcpOpen = False
    On Error Resume Next
    tmp = fso.GetSpecialFolder(2).Path & "\_bhtest_" & CLng(Timer * 100) & ".tmp"
    sh2 = "cmd.exe /c powershell -NoProfile -Command ""(Test-NetConnection " & _
          host & " -Port " & port & " -WarningAction SilentlyContinue).TcpTestSucceeded"""
    ' simpler and dependency-free: raw socket via MSWinsock is not available
    ' in plain VBS; use .NET TcpCheck through powershell only if quick.
    ' To keep this script dependency-free and instant, do a lightweight
    ' heuristic instead: mitmdump process check via tasklist output file.
    Dim sh3, out2
    Set sh3 = CreateObject("WScript.Shell")
    sh3.Run "cmd.exe /c tasklist /FI ""IMAGENAME eq mitmdump.exe"" > """ & tmp & """", 0, True
    If fso.FileExists(tmp) Then
        body = ReadFile(tmp)
        fso.DeleteFile tmp, True
        If InStr(body, "mitmdump.exe") > 0 Then
            TcpOpen = True
        End If
    End If
    On Error GoTo 0
End Function


Function ReadFile(path)
    Dim f, s
    Set f = fso.OpenTextFile(path, 1, False, -2)   ' ANSI read
    s = ""
    Do While Not f.AtEndOfStream
        s = s & f.ReadLine & vbLf
    Loop
    f.Close
    ReadFile = s
End Function


Function ExtractJsonString(json, key)
    Dim i, a, b
    ExtractJsonString = ""
    i = InStr(json, """" & key & """")
    If i = 0 Then Exit Function
    i = InStr(i, json, ":")
    If i = 0 Then Exit Function
    a = InStr(i, json, """")
    If a = 0 Then Exit Function
    b = InStr(a + 1, json, """")
    If b = 0 Then Exit Function
    ExtractJsonString = Mid(json, a + 1, b - a - 1)
End Function


Function ExtractJsonBool(json, key)
    Dim i, seg
    ExtractJsonBool = 0
    i = InStr(json, """" & key & """")
    If i = 0 Then Exit Function
    seg = Mid(json, i, 40)
    If InStr(LCase(seg), "true") > 0 Then ExtractJsonBool = 1
End Function


Function FindGameRoot(here)
    Dim f, s, deep, inner
    FindGameRoot = ""
    If fso.FileExists(fso.BuildPath(here, "tools\python\python.exe")) Then
        FindGameRoot = here
        Exit Function
    End If
    If fso.FileExists(fso.BuildPath(here, "7.0\tools\python\python.exe")) Then
        FindGameRoot = fso.BuildPath(here, "7.0")
        Exit Function
    End If
    If Not fso.FolderExists(here) Then Exit Function
    For Each f In fso.GetFolder(here).SubFolders
        If fso.FileExists(fso.BuildPath(f.Path, "tools\python\python.exe")) Then
            FindGameRoot = f.Path
            Exit Function
        End If
    Next
    For Each s In fso.GetFolder(here).SubFolders
        For Each deep In s.SubFolders
            If fso.FileExists(fso.BuildPath(deep.Path, "tools\python\python.exe")) Then
                FindGameRoot = deep.Path
                Exit Function
            End If
            For Each inner In deep.SubFolders
                If fso.FileExists(fso.BuildPath(inner.Path, "tools\python\python.exe")) Then
                    FindGameRoot = inner.Path
                    Exit Function
                End If
            Next
        Next
    Next
End Function
