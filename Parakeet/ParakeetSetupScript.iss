; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{C3666F4B-F306-4727-AEF6-539A1CD08300}
AppName=Parakeet
AppVersion=0.0.2
AppPublisher=Harpie
AppPublisherURL=https://github.com/Serasvatie/Parakeet
AppSupportURL=https://github.com/Serasvatie/Parakeet
AppUpdatesURL=https://github.com/Serasvatie/Parakeet
DefaultDirName={pf}\Parakeet
DisableProgramGroupPage=yes
OutputBaseFilename=ParakeetSetup
Compression=lzma
SolidCompression=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "french"; MessagesFile: "compiler:Languages\French.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Dirs]
Name: "{app}\en"
Name: "{app}\fr"

[Files]
Source: "D:\Code\Parakeet\Parakeet\Parakeet\bin\Release\net5.0-windows\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs
;Source: "D:\Code\Parakeet\Parakeet\Parakeet\bin\Release\FFManager.dll"; DestDir: "{app}"; Flags: ignoreversion
;Source: "D:\Code\Parakeet\Parakeet\Parakeet\bin\Release\Manager.dll"; DestDir: "{app}"; Flags: ignoreversion
;Source: "D:\Code\Parakeet\Parakeet\Parakeet\bin\Release\SManager.dll"; DestDir: "{app}"; Flags: ignoreversion
;Source: "D:\Code\Parakeet\Parakeet\Parakeet\bin\Release\en\Parakeet.resources.dll"; DestDir: "{app}\en"; Flags: ignoreversion 
;Source: "D:\Code\Parakeet\Parakeet\Parakeet\bin\Release\fr\Parakeet.resources.dll"; DestDir: "{app}\fr"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{commonprograms}\Parakeet"; Filename: "{app}\Parakeet.exe"
Name: "{commondesktop}\Parakeet"; Filename: "{app}\Parakeet.exe"; Tasks: desktopicon

[Run]
Filename: "{app}\Parakeet.exe"; Description: "{cm:LaunchProgram,Parakeet}"; Flags: nowait postinstall skipifsilent

