' One-click stop for BH3 -> elevates, then runs the Python launcher.
'
' Same reasoning as the start .vbs: BH3.exe runs elevated, so a non-elevated
' taskkill cannot close it ("access denied"). Elevation needs ShellExecute's
' "runas" verb from an outer shell, so this file is that shell and the Python
' script does the rest.
'
' The stop pipeline also RESTORES the system proxy (double layer):
'   layer 1: launch_all stop -> launch_v2 cmd_stop -> restore_system_proxy()
'   layer 2: registry guard inside launch_all cmd_stop
'   layer 3: if python itself is broken, run app\_launch\restore_proxy.py
'
' The game folder is discovered the same way as in the start script.
'
Option Explicit

Dim fso, sh, here, root, py, pyScript, cmdline

Set fso = CreateObject("Scripting.FileSystemObject")
Set sh = CreateObject("Shell.Application")

here = fso.GetParentFolderName(WScript.ScriptFullName)
root = FindGameRoot(here)

If root = "" Then
    MsgBox "Cannot find the game folder." & vbCrLf & vbCrLf & _
           "Looked for app\tools\python\python.exe (or tools\python\python.exe) under:" & vbCrLf & _
           "  " & here & vbCrLf & "  (and its sub folders)", 16, "BH3"
    WScript.Quit 1
End If

If fso.FileExists(fso.BuildPath(root, "app\tools\python\python.exe")) Then
    py = fso.BuildPath(root, "app\tools\python\python.exe")
Else
    py = fso.BuildPath(root, "tools\python\python.exe")
End If
pyScript = fso.BuildPath(root, "app\_launch\launch_all.py")
If Not fso.FileExists(pyScript) Then
    pyScript = fso.BuildPath(root, "_launch\launch_all.py")
End If

If Not fso.FileExists(pyScript) Then
    MsgBox "Cannot find the launcher script:" & vbCrLf & vbCrLf & pyScript, 16, "BH3"
    WScript.Quit 1
End If

cmdline = "/c chcp 65001>nul & """ & py & """ """ & pyScript & """ stop"

sh.ShellExecute "cmd.exe", cmdline, root, "runas", 1


' Game root = the folder whose app\tools\python\python.exe exists
' (tools\python as legacy fallback). Order: this folder -> its "7.0" sub
' folder -> recursive scan (3 levels deep), nearest first.
Function FindGameRoot(here)
    Dim s, deep, inner
    FindGameRoot = ""
    If HasPy(here) Then
        FindGameRoot = here
        Exit Function
    End If
    If fso.FolderExists(fso.BuildPath(here, "7.0")) Then
        If HasPy(fso.BuildPath(here, "7.0")) Then
            FindGameRoot = fso.BuildPath(here, "7.0")
            Exit Function
        End If
    End If
    If Not fso.FolderExists(here) Then Exit Function
    ' level 1
    For Each s In fso.GetFolder(here).SubFolders
        If HasPy(s.Path) Then
            FindGameRoot = s.Path
            Exit Function
        End If
    Next
    ' level 2 and 3 (covers e.g. D:\BH3\7.0\game\)
    For Each s In fso.GetFolder(here).SubFolders
        For Each deep In s.SubFolders
            If HasPy(deep.Path) Then
                FindGameRoot = deep.Path
                Exit Function
            End If
            For Each inner In deep.SubFolders
                If HasPy(inner.Path) Then
                    FindGameRoot = inner.Path
                    Exit Function
                End If
            Next
        Next
    Next
End Function

' python under app\ (current) or directly under root (legacy)?
Function HasPy(dirPath)
    HasPy = fso.FileExists(fso.BuildPath(dirPath, "app\tools\python\python.exe")) _
         Or fso.FileExists(fso.BuildPath(dirPath, "tools\python\python.exe"))
End Function
