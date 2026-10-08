# Black Myth: Wukong Benchmark Runner

A Windows console app that automates the Black Myth: Wukong Benchmark Tool, runs CPU and GPU benchmark passes, and prints the results. Written for VK application.

## Requirements

- Windows
- .NET 9 SDK
- Black Myth: Wukong Benchmark Tool installed on your system

The app looks for the benchmark executable and config file in `Program.cs` by default:

- `D:\Steam\steamapps\common\Black Myth Wukong Benchmark Tool\b1\Binaries\Win64\b1-Win64-Shipping.exe`
- `D:\Steam\steamapps\common\Black Myth Wukong Benchmark Tool\b1\Saved\Config\Windows\GameUserSettings.ini`

If your installation uses a different folder, you can pass custom paths when running the built executable.

## Prepare the benchmark tool first

Before using this runner for the first time:

1. Start **Black Myth: Wukong Benchmark Tool** yourself.
2. Make sure shader compilation is completed as well as brightness and EULA accepted.
3. Close the benchmark tool and make sure it has exited.
4. Back up `GameUserSettings.ini`. The runner changes this file to apply its CPU and GPU benchmark settings.

This first manual run is important: the runner's automated passes are timed and do not wait for shader compilation prompts nor support autoclicking through additional menus.

## Build the project

Open PowerShell in the repository directory and build the Release version:

```powershell
dotnet build --configuration Release
```

After the build completes, run the generated executable from the project output folder, not `dotnet run`.

## Run the built EXE

With the benchmark tool still closed, launch the built executable directly:

```powershell
.\bin\Release\net9.0\WukongBenchmark.exe
```

If the benchmark tool is installed in a custom location, pass the executable and config file paths explicitly:

```powershell
.\bin\Release\net9.0\WukongBenchmark.exe [exe-path] [config-path]
```

The runner starts the benchmark tool twice: first with its CPU settings, then with its GPU settings. Each pass waits 45 seconds for startup, clicks through the benchmark menus, runs for four minutes, and then stops the benchmark process. Keep the desktop available and do not use the mouse or keyboard while it is running.

The CPU and GPU results are printed in the console. Benchmark logs are read from:

```text
%LOCALAPPDATA%\Temp\b1\BenchMarkHistory\Tool\
```

The runner leaves `GameUserSettings.ini` set to the GPU preset after it finishes. Restore your backup if you want your previous game settings back.