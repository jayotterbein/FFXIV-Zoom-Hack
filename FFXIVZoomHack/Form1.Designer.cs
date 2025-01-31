namespace FFXIVZoomHack
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            _autoApplyCheckbox = new System.Windows.Forms.CheckBox();
            _zoomSettingsBox = new System.Windows.Forms.GroupBox();
            _fovUpDown = new System.Windows.Forms.NumericUpDown();
            _zoomUpDown = new System.Windows.Forms.NumericUpDown();
            _fovDefaultButton = new System.Windows.Forms.Button();
            _zoomDefaultButton = new System.Windows.Forms.Button();
            _fovLabel = new System.Windows.Forms.Label();
            _zoomLabel = new System.Windows.Forms.Label();
            _processListBox = new System.Windows.Forms.GroupBox();
            ReadyIndicator = new System.Windows.Forms.PictureBox();
            _autoQuitCheckbox = new System.Windows.Forms.CheckBox();
            _gotoProcessButton = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            _processList = new System.Windows.Forms.ComboBox();
            _autoQuitTooltip = new System.Windows.Forms.ToolTip(components);
            timer1 = new System.Windows.Forms.Timer(components);
            _zoomSettingsBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_fovUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)_zoomUpDown).BeginInit();
            _processListBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ReadyIndicator).BeginInit();
            SuspendLayout();
            // 
            // _autoApplyCheckbox
            // 
            _autoApplyCheckbox.AutoSize = true;
            _autoApplyCheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            _autoApplyCheckbox.Location = new System.Drawing.Point(18, 46);
            _autoApplyCheckbox.Margin = new System.Windows.Forms.Padding(6);
            _autoApplyCheckbox.Name = "_autoApplyCheckbox";
            _autoApplyCheckbox.Size = new System.Drawing.Size(330, 33);
            _autoApplyCheckbox.TabIndex = 0;
            _autoApplyCheckbox.Text = "Automatically apply on load";
            _autoApplyCheckbox.UseVisualStyleBackColor = true;
            _autoApplyCheckbox.CheckedChanged += AutoApplyCheckChanged;
            // 
            // _zoomSettingsBox
            // 
            _zoomSettingsBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            _zoomSettingsBox.Controls.Add(_fovUpDown);
            _zoomSettingsBox.Controls.Add(_zoomUpDown);
            _zoomSettingsBox.Controls.Add(_fovDefaultButton);
            _zoomSettingsBox.Controls.Add(_zoomDefaultButton);
            _zoomSettingsBox.Controls.Add(_fovLabel);
            _zoomSettingsBox.Controls.Add(_zoomLabel);
            _zoomSettingsBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            _zoomSettingsBox.Location = new System.Drawing.Point(26, 229);
            _zoomSettingsBox.Margin = new System.Windows.Forms.Padding(6);
            _zoomSettingsBox.Name = "_zoomSettingsBox";
            _zoomSettingsBox.Padding = new System.Windows.Forms.Padding(6);
            _zoomSettingsBox.Size = new System.Drawing.Size(869, 236);
            _zoomSettingsBox.TabIndex = 1;
            _zoomSettingsBox.TabStop = false;
            _zoomSettingsBox.Text = "Zoom Settings";
            // 
            // _fovUpDown
            // 
            _fovUpDown.DecimalPlaces = 2;
            _fovUpDown.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            _fovUpDown.Location = new System.Drawing.Point(446, 153);
            _fovUpDown.Margin = new System.Windows.Forms.Padding(6);
            _fovUpDown.Maximum = new decimal(new int[] { 300, 0, 0, 131072 });
            _fovUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            _fovUpDown.Name = "_fovUpDown";
            _fovUpDown.Size = new System.Drawing.Size(394, 35);
            _fovUpDown.TabIndex = 7;
            _fovUpDown.Value = new decimal(new int[] { 2, 0, 0, 0 });
            _fovUpDown.ValueChanged += FovChanged;
            // 
            // _zoomUpDown
            // 
            _zoomUpDown.Location = new System.Drawing.Point(18, 153);
            _zoomUpDown.Margin = new System.Windows.Forms.Padding(6);
            _zoomUpDown.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            _zoomUpDown.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            _zoomUpDown.Name = "_zoomUpDown";
            _zoomUpDown.Size = new System.Drawing.Size(394, 35);
            _zoomUpDown.TabIndex = 6;
            _zoomUpDown.Value = new decimal(new int[] { 2, 0, 0, 0 });
            _zoomUpDown.ValueChanged += ZoomChanged;
            // 
            // _fovDefaultButton
            // 
            _fovDefaultButton.Location = new System.Drawing.Point(664, 83);
            _fovDefaultButton.Margin = new System.Windows.Forms.Padding(6);
            _fovDefaultButton.Name = "_fovDefaultButton";
            _fovDefaultButton.Size = new System.Drawing.Size(176, 48);
            _fovDefaultButton.TabIndex = 5;
            _fovDefaultButton.Text = "Default";
            _fovDefaultButton.UseVisualStyleBackColor = true;
            _fovDefaultButton.Click += _fovDefaultButton_Click;
            // 
            // _zoomDefaultButton
            // 
            _zoomDefaultButton.Location = new System.Drawing.Point(236, 83);
            _zoomDefaultButton.Margin = new System.Windows.Forms.Padding(6);
            _zoomDefaultButton.Name = "_zoomDefaultButton";
            _zoomDefaultButton.Size = new System.Drawing.Size(176, 48);
            _zoomDefaultButton.TabIndex = 4;
            _zoomDefaultButton.Text = "Default";
            _zoomDefaultButton.UseVisualStyleBackColor = true;
            _zoomDefaultButton.Click += _zoomDefaultButton_Click;
            // 
            // _fovLabel
            // 
            _fovLabel.AutoSize = true;
            _fovLabel.Location = new System.Drawing.Point(446, 93);
            _fovLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            _fovLabel.Name = "_fovLabel";
            _fovLabel.Size = new System.Drawing.Size(153, 29);
            _fovLabel.TabIndex = 2;
            _fovLabel.Text = "Field of View";
            // 
            // _zoomLabel
            // 
            _zoomLabel.AutoSize = true;
            _zoomLabel.Location = new System.Drawing.Point(18, 93);
            _zoomLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            _zoomLabel.Name = "_zoomLabel";
            _zoomLabel.Size = new System.Drawing.Size(125, 29);
            _zoomLabel.TabIndex = 1;
            _zoomLabel.Text = "Zoom Max";
            // 
            // _processListBox
            // 
            _processListBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            _processListBox.Controls.Add(ReadyIndicator);
            _processListBox.Controls.Add(_autoQuitCheckbox);
            _processListBox.Controls.Add(_gotoProcessButton);
            _processListBox.Controls.Add(label1);
            _processListBox.Controls.Add(_processList);
            _processListBox.Controls.Add(_autoApplyCheckbox);
            _processListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            _processListBox.Location = new System.Drawing.Point(26, 18);
            _processListBox.Margin = new System.Windows.Forms.Padding(6);
            _processListBox.Name = "_processListBox";
            _processListBox.Padding = new System.Windows.Forms.Padding(6);
            _processListBox.Size = new System.Drawing.Size(869, 174);
            _processListBox.TabIndex = 2;
            _processListBox.TabStop = false;
            _processListBox.Text = "Processes";
            // 
            // ReadyIndicator
            // 
            ReadyIndicator.Anchor = System.Windows.Forms.AnchorStyles.None;
            ReadyIndicator.Location = new System.Drawing.Point(766, 95);
            ReadyIndicator.Margin = new System.Windows.Forms.Padding(0);
            ReadyIndicator.Name = "ReadyIndicator";
            ReadyIndicator.Size = new System.Drawing.Size(48, 48);
            ReadyIndicator.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            ReadyIndicator.TabIndex = 12;
            ReadyIndicator.TabStop = false;
            // 
            // _autoQuitCheckbox
            // 
            _autoQuitCheckbox.AutoSize = true;
            _autoQuitCheckbox.Location = new System.Drawing.Point(498, 46);
            _autoQuitCheckbox.Margin = new System.Windows.Forms.Padding(6);
            _autoQuitCheckbox.Name = "_autoQuitCheckbox";
            _autoQuitCheckbox.Size = new System.Drawing.Size(329, 33);
            _autoQuitCheckbox.TabIndex = 11;
            _autoQuitCheckbox.Text = "Quit when processes close";
            _autoQuitTooltip.SetToolTip(_autoQuitCheckbox, "Automatically close FFXIV Zoom Hack after \r\npreviously detected instances of FFXIV are closed");
            _autoQuitCheckbox.UseVisualStyleBackColor = true;
            _autoQuitCheckbox.CheckedChanged += AutoQuitCheckChanged;
            // 
            // _gotoProcessButton
            // 
            _gotoProcessButton.Location = new System.Drawing.Point(498, 95);
            _gotoProcessButton.Margin = new System.Windows.Forms.Padding(6);
            _gotoProcessButton.Name = "_gotoProcessButton";
            _gotoProcessButton.Size = new System.Drawing.Size(262, 48);
            _gotoProcessButton.TabIndex = 8;
            _gotoProcessButton.Text = "Bring to Front";
            _gotoProcessButton.UseVisualStyleBackColor = true;
            _gotoProcessButton.Click += _gotoProcessButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(18, 105);
            label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(127, 29);
            label1.TabIndex = 8;
            label1.Text = "Processes";
            // 
            // _processList
            // 
            _processList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            _processList.FormattingEnabled = true;
            _processList.Location = new System.Drawing.Point(157, 102);
            _processList.Margin = new System.Windows.Forms.Padding(6);
            _processList.Name = "_processList";
            _processList.Size = new System.Drawing.Size(329, 37);
            _processList.TabIndex = 1;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 500;
            timer1.Tick += Timer1Tick;
            // 
            // Form1
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(904, 467);
            Controls.Add(_processListBox);
            Controls.Add(_zoomSettingsBox);
            Font = new System.Drawing.Font("Microsoft YaHei UI", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(6);
            MaximizeBox = false;
            MaximumSize = new System.Drawing.Size(926, 523);
            MinimumSize = new System.Drawing.Size(926, 523);
            Name = "Form1";
            Text = "FFXIV Zoom Hack";
            FormClosed += Form1_FormClosed;
            Load += Form1_Load;
            Resize += Form1_Resize;
            _zoomSettingsBox.ResumeLayout(false);
            _zoomSettingsBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_fovUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)_zoomUpDown).EndInit();
            _processListBox.ResumeLayout(false);
            _processListBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ReadyIndicator).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.CheckBox _autoApplyCheckbox;
        private System.Windows.Forms.CheckBox _autoQuitCheckbox;
        private System.Windows.Forms.ToolTip _autoQuitTooltip;
        private System.Windows.Forms.GroupBox _zoomSettingsBox;
        private System.Windows.Forms.GroupBox _processListBox;
        private System.Windows.Forms.Label _fovLabel;
        private System.Windows.Forms.Label _zoomLabel;
        private System.Windows.Forms.Button _fovDefaultButton;
        private System.Windows.Forms.Button _zoomDefaultButton;
        private System.Windows.Forms.NumericUpDown _fovUpDown;
        private System.Windows.Forms.NumericUpDown _zoomUpDown;
        private System.Windows.Forms.Button _gotoProcessButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox _processList;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox ReadyIndicator;
    }
}