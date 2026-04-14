[Setup]
#define MyAppName "Borsod Coding WPF Admin Setup"
#define MyAppExeName "BorsodCoding WPF Admin.exe"

AppId=Borsod Coding WPF Admin Setupper
AppName={#MyAppName}
AppVersion=1.0
AppPublisher=Borsod Coding
DefaultGroupName={#MyAppName}
DisableProgramGroupPage=yes
OutputBaseFilename={#MyAppName}
ArchitecturesAllowed=x64compatible
ArchitecturesInstallIn64BitMode=x64compatible
DefaultDirName=BorsodCodingWPFAdmin
UninstallDisplayIcon={app}\{#MyAppExeName}


[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "hungarian"; MessagesFile: "compiler:Languages\Hungarian.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "Desktop icons:"; Flags: unchecked
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "Additional icons:"; Flags: unchecked;

[Icons]
Name: "{group}\{#MyAppExeName}"; Filename: "{app}\{#MyAppExeName}"; WorkingDir: "{app}"; IconFilename: "{app}\{#MyAppExeName}"; IconIndex: 0
Name: "{commondesktop}\{#MyAppExeName}"; Filename: "{app}\{#MyAppExeName}"; WorkingDir: "{app}"; IconFilename: "{app}\{#MyAppExeName}"; IconIndex: 0; Tasks: desktopicon

[CustomMessages]
CreateDesktopIcon=Create a &desktop icon
CreateQuickLaunchIcon=Create a &Quick Launch icon

[UninstallDelete]
Type: filesandordirs; Name: "{app}"




[Files]
Source: ".\bin\Debug\net8.0-windows\BorsodCoding WPF Admin.runtimeconfig.json"; DestDir: "{app}"; Flags: ignoreversion
Source: ".\bin\Debug\net8.0-windows\BorsodCoding WPF Admin.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: ".\bin\Debug\net8.0-windows\BorsodCoding WPF Admin.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: ".\bin\Debug\net8.0-windows\BorsodCoding WPF Admin.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: ".\bin\Debug\net8.0-windows\BorsodCoding WPF Admin.deps.json"; DestDir: "{app}"; Flags: ignoreversion


[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent; WorkingDir: "{app}"
