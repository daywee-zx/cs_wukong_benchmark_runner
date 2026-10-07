# Black Myth: Wukong Benchmark Runner

A Windows console app that automates the Black Myth: Wukong Benchmark Tool, runs CPU and GPU benchmark passes, and prints the results.

## Requirements

- Windows
- .NET 9 SDK
- Black Myth: Wukong Benchmark Tool installed at the paths configured in `Program.cs`:
  - `D:\Steam\steamapps\common\Black Myth Wukong Benchmark Tool\b1\Binaries\Win64\b1-Win64-Shipping.exe`
  - `D:\Steam\steamapps\common\Black Myth Wukong Benchmark Tool\b1\Saved\Config\Windows\GameUserSettings.ini`

## Prepare the benchmark tool first

Before using this runner for the first time:

1. Start **Black Myth: Wukong Benchmark Tool** yourself.
2. Run its benchmark and let it finish, including any shader compilation. Do not proceed until shader compilation is complete.
3. Close the benchmark tool and make sure it has exited.
4. Back up `GameUserSettings.ini`. The runner changes this file to apply its CPU and GPU benchmark settings.

This first manual run is important: the runner's automated passes are timed and do not wait for shader compilation prompts.

## Build

Open PowerShell in the repository directory and compile the Release build:

```powershell
dotnet build --configuration Release
```

## Run

With the benchmark tool still closed, run:

```powershell
dotnet run --configuration Release --no-build
```

The runner starts the benchmark tool twice: first with its CPU settings, then with its GPU settings. Each pass waits 45 seconds for startup, clicks through the benchmark menus, runs for four minutes, and then stops the benchmark process. Keep the desktop available and do not use the mouse or keyboard while it is running.

The CPU and GPU results are printed in the console. Benchmark logs are read from:

```text
%LOCALAPPDATA%\Temp\b1\BenchMarkHistory\Tool\
```

The runner leaves `GameUserSettings.ini` set to the GPU preset after it finishes. Restore your backup if you want your previous game settings back.

## Custom install location

The executable and settings paths are currently hard-coded in `Program.cs`; command-line path arguments are not enabled. If the Benchmark Tool is installed elsewhere, update `defaultExePath` and `defaultConfigPath` there, then build and run the project again.
