using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenRecorder
{
    public partial class Form1 : Form
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct POINT { public int x; public int y; }

        [StructLayout(LayoutKind.Sequential)]
        private struct CURSORINFO { public int cbSize; public int flags; public IntPtr hCursor; public POINT ptScreenPos; }

        [StructLayout(LayoutKind.Sequential)]
        private struct ICONINFO { public bool fIcon; public int xHotspot; public int yHotspot; public IntPtr hbmMask; public IntPtr hbmColor; }

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT { public int Left; public int Top; public int Right; public int Bottom; }

        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_TOOLWINDOW = 0x00000080;
        private const int DWMWA_EXTENDED_FRAME_BOUNDS = 9;
        private const int DWMWA_CLOAKED = 14;
        private const int CURSOR_SHOWING = 0x00000001;

        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("dwmapi.dll")]
        private static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, out RECT pvAttribute, int cbAttribute);

        [DllImport("dwmapi.dll")]
        private static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, out int pvAttribute, int cbAttribute);

        [DllImport("user32.dll")]
        private static extern bool GetCursorInfo(out CURSORINFO pci);

        [DllImport("user32.dll")]
        private static extern bool GetIconInfo(IntPtr hIcon, out ICONINFO piconinfo);

        [DllImport("user32.dll")]
        private static extern bool DrawIcon(IntPtr hDC, int X, int Y, IntPtr hIcon);

        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);

        public class CaptureTarget
        {
            public IntPtr Handle { get; set; }
            public string Title { get; set; }
            public bool IsMonitor { get; set; }
            public Screen TargetScreen { get; set; }
            public override string ToString() => Title;
        }
        private bool isWindowMode = false;
        private IntPtr targetWindowHandle = IntPtr.Zero;
        private Screen targetMonitorScreen = null;

        private bool isRecording = false;
        private bool isPaused = false;
        private int secondsElapsed = 0;
        private string outputFolder = string.Empty;

        private Process ffmpegProcess;
        private Stream ffmpegStream;
        private Task videoTask;

        private WasapiLoopbackCapture systemCapture;
        private WasapiCapture micCapture;
        private WaveFileWriter systemFileWriter;
        private WaveFileWriter micFileWriter;

        private string tempVideoPath;
        private string tempSystemAudioPath;
        private string tempMicAudioPath;

        private Rectangle captureBounds;
        private int targetFps = 30;

        public Form1()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;

            if (panelSettings != null)
                panelSettings.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            if (panelControls != null)
                panelControls.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            if (cmbMonitor != null)
                cmbMonitor.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            if (txtFileName != null)
                txtFileName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            if (lblFolderPath != null)
                lblFolderPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            this.Width = 650;


            cmbMonitor.DropDownWidth = 550;
            LoadRecordingSettings();

            cmbMonitor.DropDown += CmbMonitor_DropDown;

            RefreshCaptureTargets();
            if (cmbMonitor.Items.Count > 0)
            {
                cmbMonitor.SelectedIndex = 0;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadMicrophones();
            LoadRecordingSettings();
            UpdateUIState();
        }

        private void LoadMicrophones()
        {
            cmbMicDevice.Items.Clear();
            try
            {
                var enumerator = new MMDeviceEnumerator();
                var devices = enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active);

                foreach (var device in devices)
                {
                    cmbMicDevice.Items.Add(device);
                }

                if (cmbMicDevice.Items.Count > 0)
                    cmbMicDevice.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка инициализации микрофонов: {ex.Message}", "Звук", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadRecordingSettings()
        {
            cmbFPS.Items.Clear();
            cmbFPS.Items.AddRange(new object[] { "24", "30", "60" });
            cmbFPS.SelectedIndex = 1;

            cmbResolution.Items.Clear();
            cmbResolution.Items.AddRange(new object[] { "100% (Оригинал)", "75% (Меньше размер)", "50% (Минимум)" });
            cmbResolution.SelectedIndex = 0;

            outputFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
            lblFolderPath.Text = outputFolder;
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Выберите папку для сохранения готовых видеофайлов";
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    outputFolder = fbd.SelectedPath;
                    lblFolderPath.Text = outputFolder;
                }
            }
        }

        private void chkMicSound_CheckedChanged(object sender, EventArgs e)
        {
            UpdateUIState();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            string ffmpegPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ffmpeg", "bin", "ffmpeg.exe");
            if (!File.Exists(ffmpegPath))
            {
                MessageBox.Show("Файл 'ffmpeg.exe' не найден! Убедитесь, что он лежит по пути: bin\\Debug\\ffmpeg\\bin\\ffmpeg.exe", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(outputFolder) || !Directory.Exists(outputFolder))
            {
                MessageBox.Show("Укажите корректную папку для сохранения!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {

                if (cmbMonitor.SelectedItem is CaptureTarget selected)
                {
                    if (selected.IsMonitor)
                    {
                        isWindowMode = false;
                        targetMonitorScreen = selected.TargetScreen;
                        captureBounds = targetMonitorScreen.Bounds;
                    }
                    else
                    {
                        isWindowMode = true;
                        targetWindowHandle = selected.Handle;
                        targetMonitorScreen = null;

                        RECT rect;
                        DwmGetWindowAttribute(targetWindowHandle, DWMWA_EXTENDED_FRAME_BOUNDS, out rect, Marshal.SizeOf(typeof(RECT)));
                        captureBounds = new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
                    }
                }
                else
                {
                    isWindowMode = false;
                    targetMonitorScreen = Screen.PrimaryScreen;
                    captureBounds = targetMonitorScreen.Bounds;
                }

                targetFps = int.Parse(cmbFPS.SelectedItem.ToString());

                string sessionId = Guid.NewGuid().ToString().Substring(0, 8);
                tempVideoPath = Path.Combine(Path.GetTempPath(), $"video_{sessionId}.mp4");
                tempSystemAudioPath = Path.Combine(Path.GetTempPath(), $"sys_{sessionId}.wav");
                tempMicAudioPath = Path.Combine(Path.GetTempPath(), $"mic_{sessionId}.wav");

                StartFFmpegVideo(ffmpegPath);

                if (chkSystemSound.Checked) StartSystemAudioCapture();
                if (chkMicSound.Checked && cmbMicDevice.SelectedItem != null) StartMicAudioCapture((MMDevice)cmbMicDevice.SelectedItem);

                isRecording = true;
                isPaused = false;
                secondsElapsed = 0;
                lblTimer.Text = "00:00:00";

                videoTask = Task.Run(() => VideoCaptureLoop());
                recTimer.Start();
                UpdateUIState();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось начать запись: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CleanUpTempFiles();
            }
        }

        private void StartFFmpegVideo(string ffmpegPath)
        {
            int inWidth = captureBounds.Width;
            int inHeight = captureBounds.Height;
            string scaleFilter = "";

            if (cmbResolution.SelectedIndex == 1) // 75%
            {
                int outWidth = ((int)(inWidth * 0.75) / 2) * 2;
                int outHeight = ((int)(inHeight * 0.75) / 2) * 2;
                scaleFilter = $"-vf scale={outWidth}:{outHeight} ";
            }
            else if (cmbResolution.SelectedIndex == 2) // 50%
            {
                int outWidth = ((int)(inWidth * 0.5) / 2) * 2;
                int outHeight = ((int)(inHeight * 0.5) / 2) * 2;
                scaleFilter = $"-vf scale={outWidth}:{outHeight} ";
            }

            string args = $"-f rawvideo -pix_fmt bgr0 -s {inWidth}x{inHeight} -r {targetFps} -i - " +
                          $"{scaleFilter}-c:v libx264 -pix_fmt yuv420p -preset ultrafast -y \"{tempVideoPath}\"";

            ffmpegProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = ffmpegPath,
                    Arguments = args,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardInput = true
                }
            };
            ffmpegProcess.Start();
            ffmpegStream = ffmpegProcess.StandardInput.BaseStream;
        }

        private void StartSystemAudioCapture()
        {
            systemCapture = new WasapiLoopbackCapture();
            systemFileWriter = new WaveFileWriter(tempSystemAudioPath, systemCapture.WaveFormat);

            systemCapture.DataAvailable += (s, e) =>
            {
                if (isRecording && !isPaused)
                    systemFileWriter?.Write(e.Buffer, 0, e.BytesRecorded);
            };

            systemCapture.RecordingStopped += (s, e) =>
            {
                systemFileWriter?.Dispose();
                systemFileWriter = null;
                systemCapture?.Dispose();
                systemCapture = null;
            };
            systemCapture.StartRecording();
        }

        private void StartMicAudioCapture(MMDevice device)
        {
            micCapture = new WasapiCapture(device);
            micFileWriter = new WaveFileWriter(tempMicAudioPath, micCapture.WaveFormat);

            micCapture.DataAvailable += (s, e) =>
            {
                if (isRecording && !isPaused)
                    micFileWriter?.Write(e.Buffer, 0, e.BytesRecorded);
            };

            micCapture.RecordingStopped += (s, e) =>
            {
                micFileWriter?.Dispose();
                micFileWriter = null;
                micCapture?.Dispose();
                micCapture = null;
            };
            micCapture.StartRecording();
        }

        private void VideoCaptureLoop()
        {
            double frameDurationMs = 1000.0 / targetFps;
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            int fixedWidth = captureBounds.Width;
            int fixedHeight = captureBounds.Height;

            while (isRecording)
            {
                if (isPaused) { Thread.Sleep(10); continue; }
                long startTime = stopwatch.ElapsedMilliseconds;

                int currentX = targetMonitorScreen != null ? targetMonitorScreen.Bounds.X : 0;
                int currentY = targetMonitorScreen != null ? targetMonitorScreen.Bounds.Y : 0;

                if (isWindowMode && targetWindowHandle != IntPtr.Zero)
                {
                    RECT rect;
                    if (DwmGetWindowAttribute(targetWindowHandle, DWMWA_EXTENDED_FRAME_BOUNDS, out rect, Marshal.SizeOf(typeof(RECT))) == 0)
                    {
                        currentX = rect.Left;
                        currentY = rect.Top;
                    }
                }

                try
                {
                    using (Bitmap bmp = new Bitmap(fixedWidth, fixedHeight, PixelFormat.Format32bppRgb))
                    {
                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            g.CopyFromScreen(currentX, currentY, 0, 0, bmp.Size, CopyPixelOperation.SourceCopy);

                            CURSORINFO pci;
                            pci.cbSize = Marshal.SizeOf(typeof(CURSORINFO));
                            if (GetCursorInfo(out pci) && pci.flags == CURSOR_SHOWING)
                            {
                                int cursorX = pci.ptScreenPos.x - currentX;
                                int cursorY = pci.ptScreenPos.y - currentY;

                                ICONINFO iconInfo;
                                if (GetIconInfo(pci.hCursor, out iconInfo))
                                {
                                    cursorX -= iconInfo.xHotspot;
                                    cursorY -= iconInfo.yHotspot;
                                    if (iconInfo.hbmColor != IntPtr.Zero) DeleteObject(iconInfo.hbmColor);
                                    if (iconInfo.hbmMask != IntPtr.Zero) DeleteObject(iconInfo.hbmMask);
                                }

                                IntPtr hdc = g.GetHdc();
                                DrawIcon(hdc, cursorX, cursorY, pci.hCursor);
                                g.ReleaseHdc(hdc);
                            }
                        }

                        BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, bmp.PixelFormat);
                        int bytesCount = data.Stride * data.Height;
                        byte[] rawBytes = new byte[bytesCount];
                        Marshal.Copy(data.Scan0, rawBytes, 0, bytesCount);
                        bmp.UnlockBits(data);

                        ffmpegStream.Write(rawBytes, 0, rawBytes.Length);
                        ffmpegStream.Flush();
                    }
                }
                catch { break; }

                long endTime = stopwatch.ElapsedMilliseconds;
                int delay = (int)(frameDurationMs - (endTime - startTime));
                if (delay > 0) Thread.Sleep(delay);
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (!isRecording) return;

            isPaused = !isPaused;
            if (isPaused) recTimer.Stop();
            else recTimer.Start();

            UpdateUIState();
        }

        private async void btnStop_Click(object sender, EventArgs e)
        {
            if (!isRecording) return;

            isRecording = false;
            isPaused = false;
            recTimer.Stop();

            lblStatus.Text = "Обработка...";
            lblStatus.ForeColor = Color.Blue;
            panelControls.Enabled = false;

            if (systemCapture != null) try { systemCapture.StopRecording(); } catch { }
            if (micCapture != null) try { micCapture.StopRecording(); } catch { }

            int waitTimeout = 0;
            while ((systemFileWriter != null || micFileWriter != null) && waitTimeout < 200)
            {
                await Task.Delay(10);
                waitTimeout++;
            }

            try
            {
                if (ffmpegStream != null)
                {
                    ffmpegStream.Flush();
                    ffmpegStream.Close();
                    ffmpegStream.Dispose();
                }
            }
            catch { }

            if (videoTask != null) await Task.WhenAny(videoTask, Task.Delay(2000));
            if (ffmpegProcess != null && !ffmpegProcess.HasExited)
            {
                ffmpegProcess.WaitForExit(2000);
                if (!ffmpegProcess.HasExited) try { ffmpegProcess.Kill(); } catch { }
            }

            string rawName = string.IsNullOrWhiteSpace(txtFileName.Text) ? "Record" : txtFileName.Text.Trim();

            foreach (char invalidChar in Path.GetInvalidFileNameChars())
            {
                rawName = rawName.Replace(invalidChar, '_');
            }

            if (!rawName.EndsWith(".mp4", StringComparison.OrdinalIgnoreCase))
            {
                rawName += ".mp4";
            }

            string finalOutputPath = Path.Combine(outputFolder, rawName);

            if (File.Exists(finalOutputPath))
            {
                string directory = Path.GetDirectoryName(finalOutputPath);
                string nameWithoutExt = Path.GetFileNameWithoutExtension(finalOutputPath);
                string extension = Path.GetExtension(finalOutputPath);
                int counter = 1;

                while (File.Exists(finalOutputPath))
                {
                    finalOutputPath = Path.Combine(directory, $"{nameWithoutExt} ({counter}){extension}");
                    counter++;
                }
            }

            try
            {
                await Task.Run(() => MuxAudioVideo(finalOutputPath));
                MessageBox.Show($"Запись успешно сохранена:\n{Path.GetFileName(finalOutputPath)}\n\nВ папку:\n{outputFolder}",
                                "Запись завершена", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UpdateUIState();
                panelControls.Enabled = true;
            }
        }

        private void MuxAudioVideo(string outputPath)
        {
            if (!File.Exists(tempVideoPath))
            {
                throw new Exception($"Исходный видеофайл не был найден во временной папке!\nПуть: {tempVideoPath}\n\n" +
                                    "Это означает, что первый FFmpeg аварийно завершился прямо в момент старта записи.");
            }

            string ffmpegPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ffmpeg", "bin", "ffmpeg.exe");

            bool hasSys = chkSystemSound.Checked && File.Exists(tempSystemAudioPath) && new FileInfo(tempSystemAudioPath).Length > 100;
            bool hasMic = chkMicSound.Checked && File.Exists(tempMicAudioPath) && new FileInfo(tempMicAudioPath).Length > 100;

            string args = $"-i \"{tempVideoPath}\" ";
            if (hasSys) args += $"-i \"{tempSystemAudioPath}\" ";
            if (hasMic) args += $"-i \"{tempMicAudioPath}\" ";

            if (hasSys && hasMic)
            {
                args += "-filter_complex \"[1:a]aresample=44100[a1];[2:a]aresample=44100[a2];[a1][a2]amix=inputs=2:duration=longest[a]\" -map 0:v -map \"[a]\" -c:a aac ";
            }
            else if (hasSys || hasMic)
            {
                args += "-map 0:v -map 1:a -c:a aac ";
            }
            else
            {
                args += "-map 0:v ";
            }

            args += $"-c:v copy -shortest -y \"{outputPath}\"";

            var muxProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = ffmpegPath,
                    Arguments = args,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardError = true
                }
            };

            muxProcess.Start();
            string errLog = muxProcess.StandardError.ReadToEnd();
            muxProcess.WaitForExit();

            if (!File.Exists(outputPath) || new FileInfo(outputPath).Length == 0)
            {
                throw new Exception($"FFmpeg не смог собрать финальный файл. Лог:\n{errLog}");
            }
        }

        private void UpdateUIState()
        {
            panelSettings.Enabled = !isRecording;
            cmbMicDevice.Enabled = !isRecording && chkMicSound.Checked;

            txtFileName.Enabled = !isRecording;
            btnStart.Enabled = !isRecording;
            btnPause.Enabled = isRecording;
            btnStop.Enabled = isRecording;

            btnPause.Text = isPaused ? "Продолжить" : "Пауза";

            if (!isRecording)
            {
                lblStatus.Text = "Готов к записи";
                lblStatus.ForeColor = Color.Green;
            }
            else if (isPaused)
            {
                lblStatus.Text = "На паузе";
                lblStatus.ForeColor = Color.Orange;
            }
            else
            {
                lblStatus.Text = "Идет запись...";
                lblStatus.ForeColor = Color.Red;
            }
        }

        private void recTimer_Tick(object sender, EventArgs e)
        {
            secondsElapsed++;
            TimeSpan time = TimeSpan.FromSeconds(secondsElapsed);
            lblTimer.Text = time.ToString(@"hh\:mm\:ss");
        }

        private void RefreshCaptureTargets()
        {
            cmbMonitor.Items.Clear();

            var screens = Screen.AllScreens;
            for (int i = 0; i < screens.Length; i++)
            {
                string mainBadge = screens[i].Primary ? " (Основной)" : "";
                cmbMonitor.Items.Add(new CaptureTarget
                {
                    Title = $"🖥️ Монитор {i + 1}{mainBadge} [{screens[i].Bounds.Width}x{screens[i].Bounds.Height}]",
                    IsMonitor = true,
                    TargetScreen = screens[i]
                });
            }

            EnumWindows((hWnd, lParam) =>
            {
                if (IsWindowVisible(hWnd))
                {
                    int exStyle = GetWindowLong(hWnd, GWL_EXSTYLE);
                    if ((exStyle & WS_EX_TOOLWINDOW) != 0) return true;

                    DwmGetWindowAttribute(hWnd, DWMWA_CLOAKED, out int cloaked, sizeof(int));
                    if (cloaked != 0) return true;

                    StringBuilder sb = new StringBuilder(256);
                    GetWindowText(hWnd, sb, sb.Capacity);
                    string title = sb.ToString().Trim();

                    if (!string.IsNullOrWhiteSpace(title) && title.Length > 2 && title != this.Text && title != "Program Manager")
                    {
                        cmbMonitor.Items.Add(new CaptureTarget
                        {
                            Handle = hWnd,
                            Title = $"📦 Окно: {title}",
                            IsMonitor = false
                        });
                    }
                }
                return true;
            }, IntPtr.Zero);
        }

        private void CmbMonitor_DropDown(object sender, EventArgs e)
        {
            string lastSelectedTitle = cmbMonitor.SelectedItem?.ToString();

            RefreshCaptureTargets();

            if (!string.IsNullOrEmpty(lastSelectedTitle))
            {
                for (int i = 0; i < cmbMonitor.Items.Count; i++)
                {
                    if (cmbMonitor.Items[i].ToString() == lastSelectedTitle)
                    {
                        cmbMonitor.SelectedIndex = i;
                        break;
                    }
                }
            }
        }
        private void CleanUpTempFiles()
        {
            try { if (File.Exists(tempVideoPath)) File.Delete(tempVideoPath); } catch { }
            try { if (File.Exists(tempSystemAudioPath)) File.Delete(tempSystemAudioPath); } catch { }
            try { if (File.Exists(tempMicAudioPath)) File.Delete(tempMicAudioPath); } catch { }
        }
    }
}