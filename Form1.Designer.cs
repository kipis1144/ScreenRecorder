namespace ScreenRecorder
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelSettings = new System.Windows.Forms.Panel();
            this.lblFileName = new System.Windows.Forms.Label();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.lblMonitor = new System.Windows.Forms.Label();
            this.cmbMonitor = new System.Windows.Forms.ComboBox();
            this.lblMic = new System.Windows.Forms.Label();
            this.cmbMicDevice = new System.Windows.Forms.ComboBox();
            this.lblFPS = new System.Windows.Forms.Label();
            this.cmbFPS = new System.Windows.Forms.ComboBox();
            this.lblResolution = new System.Windows.Forms.Label();
            this.cmbResolution = new System.Windows.Forms.ComboBox();
            this.chkSystemSound = new System.Windows.Forms.CheckBox();
            this.chkMicSound = new System.Windows.Forms.CheckBox();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.lblFolderPath = new System.Windows.Forms.Label();
            this.panelControls = new System.Windows.Forms.Panel();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblTimer = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.recTimer = new System.Windows.Forms.Timer(this.components);
            this.panelSettings.SuspendLayout();
            this.panelControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSettings
            // 
            this.panelSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(36)))));
            this.panelSettings.Controls.Add(this.lblFileName);
            this.panelSettings.Controls.Add(this.txtFileName);
            this.panelSettings.Controls.Add(this.lblMonitor);
            this.panelSettings.Controls.Add(this.cmbMonitor);
            this.panelSettings.Controls.Add(this.lblMic);
            this.panelSettings.Controls.Add(this.cmbMicDevice);
            this.panelSettings.Controls.Add(this.lblFPS);
            this.panelSettings.Controls.Add(this.cmbFPS);
            this.panelSettings.Controls.Add(this.lblResolution);
            this.panelSettings.Controls.Add(this.cmbResolution);
            this.panelSettings.Controls.Add(this.chkSystemSound);
            this.panelSettings.Controls.Add(this.chkMicSound);
            this.panelSettings.Controls.Add(this.btnSelectFolder);
            this.panelSettings.Controls.Add(this.lblFolderPath);
            this.panelSettings.Location = new System.Drawing.Point(15, 15);
            this.panelSettings.Name = "panelSettings";
            this.panelSettings.Size = new System.Drawing.Size(354, 280);
            this.panelSettings.TabIndex = 0;
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblFileName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(245)))));
            this.lblFileName.Location = new System.Drawing.Point(15, 18);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(117, 17);
            this.lblFileName.TabIndex = 0;
            this.lblFileName.Text = "Название файла:";
            // 
            // txtFileName
            // 
            this.txtFileName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(50)))));
            this.txtFileName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFileName.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtFileName.ForeColor = System.Drawing.Color.White;
            this.txtFileName.Location = new System.Drawing.Point(135, 15);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(204, 24);
            this.txtFileName.TabIndex = 1;
            this.txtFileName.Text = "Record";
            // 
            // lblMonitor
            // 
            this.lblMonitor.AutoSize = true;
            this.lblMonitor.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMonitor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(190)))));
            this.lblMonitor.Location = new System.Drawing.Point(15, 58);
            this.lblMonitor.Name = "lblMonitor";
            this.lblMonitor.Size = new System.Drawing.Size(43, 15);
            this.lblMonitor.TabIndex = 2;
            this.lblMonitor.Text = "Экран:";
            // 
            // cmbMonitor
            // 
            this.cmbMonitor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(50)))));
            this.cmbMonitor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMonitor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbMonitor.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbMonitor.ForeColor = System.Drawing.Color.White;
            this.cmbMonitor.Location = new System.Drawing.Point(135, 54);
            this.cmbMonitor.Name = "cmbMonitor";
            this.cmbMonitor.Size = new System.Drawing.Size(204, 23);
            this.cmbMonitor.TabIndex = 3;
            // 
            // lblMic
            // 
            this.lblMic.AutoSize = true;
            this.lblMic.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMic.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(190)))));
            this.lblMic.Location = new System.Drawing.Point(15, 96);
            this.lblMic.Name = "lblMic";
            this.lblMic.Size = new System.Drawing.Size(71, 15);
            this.lblMic.TabIndex = 4;
            this.lblMic.Text = "Микрофон:";
            // 
            // cmbMicDevice
            // 
            this.cmbMicDevice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(50)))));
            this.cmbMicDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMicDevice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbMicDevice.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbMicDevice.ForeColor = System.Drawing.Color.White;
            this.cmbMicDevice.Location = new System.Drawing.Point(135, 92);
            this.cmbMicDevice.Name = "cmbMicDevice";
            this.cmbMicDevice.Size = new System.Drawing.Size(204, 23);
            this.cmbMicDevice.TabIndex = 5;
            // 
            // lblFPS
            // 
            this.lblFPS.AutoSize = true;
            this.lblFPS.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFPS.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(190)))));
            this.lblFPS.Location = new System.Drawing.Point(15, 134);
            this.lblFPS.Name = "lblFPS";
            this.lblFPS.Size = new System.Drawing.Size(29, 15);
            this.lblFPS.TabIndex = 6;
            this.lblFPS.Text = "FPS:";
            // 
            // cmbFPS
            // 
            this.cmbFPS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(50)))));
            this.cmbFPS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFPS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbFPS.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbFPS.ForeColor = System.Drawing.Color.White;
            this.cmbFPS.Location = new System.Drawing.Point(135, 130);
            this.cmbFPS.Name = "cmbFPS";
            this.cmbFPS.Size = new System.Drawing.Size(80, 23);
            this.cmbFPS.TabIndex = 7;
            // 
            // lblResolution
            // 
            this.lblResolution.AutoSize = true;
            this.lblResolution.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblResolution.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(190)))));
            this.lblResolution.Location = new System.Drawing.Point(15, 172);
            this.lblResolution.Name = "lblResolution";
            this.lblResolution.Size = new System.Drawing.Size(62, 15);
            this.lblResolution.TabIndex = 8;
            this.lblResolution.Text = "Масштаб:";
            // 
            // cmbResolution
            // 
            this.cmbResolution.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(50)))));
            this.cmbResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbResolution.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbResolution.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbResolution.ForeColor = System.Drawing.Color.White;
            this.cmbResolution.Location = new System.Drawing.Point(135, 168);
            this.cmbResolution.Name = "cmbResolution";
            this.cmbResolution.Size = new System.Drawing.Size(204, 23);
            this.cmbResolution.TabIndex = 9;
            // 
            // chkSystemSound
            // 
            this.chkSystemSound.AutoSize = true;
            this.chkSystemSound.Checked = true;
            this.chkSystemSound.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSystemSound.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkSystemSound.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkSystemSound.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(205)))));
            this.chkSystemSound.Location = new System.Drawing.Point(18, 208);
            this.chkSystemSound.Name = "chkSystemSound";
            this.chkSystemSound.Size = new System.Drawing.Size(126, 19);
            this.chkSystemSound.TabIndex = 10;
            this.chkSystemSound.Text = "Звук системы (ПК)";
            // 
            // chkMicSound
            // 
            this.chkMicSound.AutoSize = true;
            this.chkMicSound.Checked = true;
            this.chkMicSound.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMicSound.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkMicSound.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkMicSound.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(205)))));
            this.chkMicSound.Location = new System.Drawing.Point(185, 208);
            this.chkMicSound.Name = "chkMicSound";
            this.chkMicSound.Size = new System.Drawing.Size(140, 19);
            this.chkMicSound.TabIndex = 11;
            this.chkMicSound.Text = "Включить микрофон";
            this.chkMicSound.CheckedChanged += new System.EventHandler(this.chkMicSound_CheckedChanged);
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(62)))));
            this.btnSelectFolder.FlatAppearance.BorderSize = 0;
            this.btnSelectFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectFolder.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSelectFolder.ForeColor = System.Drawing.Color.White;
            this.btnSelectFolder.Location = new System.Drawing.Point(15, 242);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(129, 26);
            this.btnSelectFolder.TabIndex = 12;
            this.btnSelectFolder.Text = "Изменить путь";
            this.btnSelectFolder.UseVisualStyleBackColor = false;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // lblFolderPath
            // 
            this.lblFolderPath.AutoEllipsis = true;
            this.lblFolderPath.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblFolderPath.ForeColor = System.Drawing.Color.Gray;
            this.lblFolderPath.Location = new System.Drawing.Point(150, 248);
            this.lblFolderPath.Name = "lblFolderPath";
            this.lblFolderPath.Size = new System.Drawing.Size(189, 15);
            this.lblFolderPath.TabIndex = 13;
            this.lblFolderPath.Text = "Не выбрана";
            // 
            // panelControls
            // 
            this.panelControls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(36)))));
            this.panelControls.Controls.Add(this.btnStart);
            this.panelControls.Controls.Add(this.btnPause);
            this.panelControls.Controls.Add(this.btnStop);
            this.panelControls.Controls.Add(this.lblTimer);
            this.panelControls.Controls.Add(this.lblStatus);
            this.panelControls.Location = new System.Drawing.Point(15, 308);
            this.panelControls.Name = "panelControls";
            this.panelControls.Size = new System.Drawing.Size(354, 95);
            this.panelControls.TabIndex = 1;
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStart.FlatAppearance.BorderSize = 0;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnStart.ForeColor = System.Drawing.Color.White;
            this.btnStart.Location = new System.Drawing.Point(15, 15);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(100, 36);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "СТАРТ";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnPause
            // 
            this.btnPause.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(196)))), ((int)(((byte)(15)))));
            this.btnPause.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPause.FlatAppearance.BorderSize = 0;
            this.btnPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPause.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnPause.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnPause.Location = new System.Drawing.Point(127, 15);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(100, 36);
            this.btnPause.TabIndex = 1;
            this.btnPause.Text = "Пауза";
            this.btnPause.UseVisualStyleBackColor = false;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnStop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStop.FlatAppearance.BorderSize = 0;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnStop.ForeColor = System.Drawing.Color.White;
            this.btnStop.Location = new System.Drawing.Point(239, 15);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(100, 36);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "СТОП";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // lblTimer
            // 
            this.lblTimer.AutoSize = true;
            this.lblTimer.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTimer.ForeColor = System.Drawing.Color.White;
            this.lblTimer.Location = new System.Drawing.Point(15, 63);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(72, 21);
            this.lblTimer.TabIndex = 3;
            this.lblTimer.Text = "00:00:00";
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.lblStatus.Location = new System.Drawing.Point(135, 65);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(204, 20);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "Готов к записи";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // recTimer
            // 
            this.recTimer.Interval = 1000;
            this.recTimer.Tick += new System.EventHandler(this.recTimer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(22)))));
            this.ClientSize = new System.Drawing.Size(384, 418);
            this.Controls.Add(this.panelControls);
            this.Controls.Add(this.panelSettings);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ScreenRecorder";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelSettings.ResumeLayout(false);
            this.panelSettings.PerformLayout();
            this.panelControls.ResumeLayout(false);
            this.panelControls.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSettings;
        private System.Windows.Forms.Label lblMonitor;
        private System.Windows.Forms.ComboBox cmbMonitor;
        private System.Windows.Forms.Label lblMic;
        private System.Windows.Forms.ComboBox cmbMicDevice;
        private System.Windows.Forms.Label lblFPS;
        private System.Windows.Forms.ComboBox cmbFPS;
        private System.Windows.Forms.Label lblResolution;
        private System.Windows.Forms.ComboBox cmbResolution;
        private System.Windows.Forms.CheckBox chkSystemSound;
        private System.Windows.Forms.CheckBox chkMicSound;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.Label lblFolderPath;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.TextBox txtFileName;

        private System.Windows.Forms.Panel panelControls;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.Label lblStatus;

        private System.Windows.Forms.Timer recTimer;
    }
}