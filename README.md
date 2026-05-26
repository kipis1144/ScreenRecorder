# 🎥 ScreenRecorder

[![Platform](https://img.shields.io/badge/platform-Windows-blue.svg)](https://dotnet.microsoft.com)
[![Framework](https://img.shields.io/badge/.NET-Framework%20%2F%20Core-purple.svg)](https://dotnet.microsoft.com)
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](https://opensource.org/licenses/MIT)
[![FFmpeg](https://img.shields.io/badge/FFmpeg-Powered-orange.svg)](https://ffmpeg.org)

A high-performance, lightweight, and rock-solid desktop application for Windows designed to capture your screen or specific application windows with simultaneous multi-channel audio recording. 

Built with **C# WinForms**, **NAudio**, and powered by **FFmpeg** for seamless post-process multiplexing (muxing).

---

## ✨ Features

- **Flexible Capture Targets:** Record your entire desktop or pick a specific application window from a dynamically updated list. Automatically selects the first available target on startup.
- **Adaptive UI Layout:** Fully responsive WinForms interface utilizing smart anchor bindings. Window titles and paths stretch perfectly on any display resolution without truncation.
- **Smart Resolution Downscaling:** Supports 100%, 75%, and 50% scaling options. Automatically calculates and enforces **even-pixel dimensions** to guarantee compatibility with the strict requirements of the `libx264` codec.
- **Multi-Channel Audio Capture:** Independent toggles for capturing **System Sounds (Loopback)** and **Microphone Input**.
- **Fail-Safe Muxing Pipeline:** Two-stage production recording. Video and audio are written to independent temporary files and safely remuxed only after recording successfully terminates. Zero corrupted outputs.
- **Automatic Cleanup:** Immediate, isolated post-mux purging of disk cache/temporary media files.

---

## 🛠️ Tech Stack

* **Frontend/UI:** C# Windows Forms (.NET)
* **Audio Core:** NAudio (WASAPI Loopback Capture & WaveIn provider)
* **Encoding Engine:** FFmpeg CLI Wrapper via `System.Diagnostics.Process`

---

## 📦 Architecture & How It Works


```

[Screen/Window Capture] ---> Raw Video Stream  ---> [ FFmpeg Process 1 ] ---> temp_video.mp4 

[System Audio (WASAPI)] ---> WAV Audio Stream  ---> [ NAudio Loopback ]  ---> temp_sys.wav   |--> [ FFmpeg Muxer ] -> Final.mp4
[Microphone Audio]      ---> WAV Audio Stream  ---> [ NAudio WaveIn ]    ---> temp_mic.wav   /

```

### The Muxing Logic
When you hit **Stop**, the application evaluates available channels:
1. **Dual Audio Active:** Re-samples both tracks to 44100Hz and blends them via FFmpeg's `amix=inputs=2:duration=longest` complex filter graph, packing audio into an `aac` stream.
2. **Single Audio Active:** Instantly maps the video track and the single audio track into a streamlined container.
3. **No Audio:** Direct copies the video stream (`-c:v copy`) instantly with absolutely zero re-encoding overhead.

---

## 🚀 Quick Start & Installation

### Prerequisites
1. Windows 10 / 11 (x64)
2. .NET Framework 4.7.2+ or .NET 6.0+ Runtime
3. **FFmpeg Static Build:** You must place the `ffmpeg.exe` binary into the relative project folder structure:

```

[YourAppFolder]/ffmpeg/bin/ffmpeg.exe

```

### Running from Source
1. Clone the repository:
```bash
git clone [https://github.com/YOUR_USERNAME/ScreenRecorder.git](https://github.com/YOUR_USERNAME/ScreenRecorder.git)

```

2. Open `ScreenRecorder.sln` in Visual Studio.
3. Restore NuGet packages (specifically `NAudio`).
4. Ensure the `ffmpeg` directory with the executable is present in your output build folder (`bin/Debug` or `bin/Release`).
5. Build and Run!

---

## 📋 Code Highlights

### Proportional Even-Pixel Calculation

To prevent `libx264` from crashing due to odd resolutions during downscaling, the app applies macroblock alignment filtering:

```csharp
int outWidth = ((int)(inWidth * 0.75) / 2) * 2;
int outHeight = ((int)(inHeight * 0.75) / 2) * 2;
scaleFilter = $"-vf scale={outWidth}:{outHeight} ";

```

### Dynamic Target Selection

Ensures a seamless user experience right out of the box by instantly focusing on the primary display:

```csharp
if (cmbMonitor.SelectedIndex == -1 && cmbMonitor.Items.Count > 0)
{
    cmbMonitor.SelectedIndex = 0;
}

```

---

## 📄 License

This project is open-source software licensed under the **MIT License**.


```
