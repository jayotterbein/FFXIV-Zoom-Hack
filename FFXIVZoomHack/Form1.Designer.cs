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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this._autoApplyCheckbox = new System.Windows.Forms.CheckBox();
            this._zoomSettingsBox = new System.Windows.Forms.GroupBox();
            this._fovUpDown = new System.Windows.Forms.NumericUpDown();
            this._zoomUpDown = new System.Windows.Forms.NumericUpDown();
            this._fovDefaultButton = new System.Windows.Forms.Button();
            this._zoomDefaultButton = new System.Windows.Forms.Button();
            this._fovLabel = new System.Windows.Forms.Label();
            this._zoomLabel = new System.Windows.Forms.Label();
            this._processListBox = new System.Windows.Forms.GroupBox();
            this.ReadyIndicator = new System.Windows.Forms.PictureBox();
            this._autoQuitCheckbox = new System.Windows.Forms.CheckBox();
            this._gotoProcessButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this._processList = new System.Windows.Forms.ComboBox();
            this._autoQuitTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this._zoomSettingsBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._fovUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._zoomUpDown)).BeginInit();
            this._processListBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ReadyIndicator)).BeginInit();
            this.SuspendLayout();
            // 
            // _autoApplyCheckbox
            // 
            this._autoApplyCheckbox.AutoSize = true;
            this._autoApplyCheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._autoApplyCheckbox.Location = new System.Drawing.Point(13, 29);
            this._autoApplyCheckbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._autoApplyCheckbox.Name = "_autoApplyCheckbox";
            this._autoApplyCheckbox.Size = new System.Drawing.Size(271, 29);
            this._autoApplyCheckbox.TabIndex = 0;
            this._autoApplyCheckbox.Text = "Automatically apply on load";
            this._autoApplyCheckbox.UseVisualStyleBackColor = true;
            this._autoApplyCheckbox.CheckedChanged += new System.EventHandler(this.AutoApplyCheckChanged);
            // 
            // _zoomSettingsBox
            // 
            this._zoomSettingsBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._zoomSettingsBox.Controls.Add(this._fovUpDown);
            this._zoomSettingsBox.Controls.Add(this._zoomUpDown);
            this._zoomSettingsBox.Controls.Add(this._fovDefaultButton);
            this._zoomSettingsBox.Controls.Add(this._zoomDefaultButton);
            this._zoomSettingsBox.Controls.Add(this._fovLabel);
            this._zoomSettingsBox.Controls.Add(this._zoomLabel);
            this._zoomSettingsBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._zoomSettingsBox.Location = new System.Drawing.Point(18, 143);
            this._zoomSettingsBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._zoomSettingsBox.Name = "_zoomSettingsBox";
            this._zoomSettingsBox.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._zoomSettingsBox.Size = new System.Drawing.Size(632, 148);
            this._zoomSettingsBox.TabIndex = 1;
            this._zoomSettingsBox.TabStop = false;
            this._zoomSettingsBox.Text = "Zoom Settings";
            // 
            // _fovUpDown
            // 
            this._fovUpDown.DecimalPlaces = 2;
            this._fovUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this._fovUpDown.Location = new System.Drawing.Point(324, 96);
            this._fovUpDown.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._fovUpDown.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            131072});
            this._fovUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this._fovUpDown.Name = "_fovUpDown";
            this._fovUpDown.Size = new System.Drawing.Size(287, 30);
            this._fovUpDown.TabIndex = 7;
            this._fovUpDown.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this._fovUpDown.ValueChanged += new System.EventHandler(this.FovChanged);
            // 
            // _zoomUpDown
            // 
            this._zoomUpDown.Location = new System.Drawing.Point(13, 96);
            this._zoomUpDown.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._zoomUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this._zoomUpDown.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this._zoomUpDown.Name = "_zoomUpDown";
            this._zoomUpDown.Size = new System.Drawing.Size(287, 30);
            this._zoomUpDown.TabIndex = 6;
            this._zoomUpDown.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this._zoomUpDown.ValueChanged += new System.EventHandler(this.ZoomChanged);
            // 
            // _fovDefaultButton
            // 
            this._fovDefaultButton.Location = new System.Drawing.Point(483, 52);
            this._fovDefaultButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._fovDefaultButton.Name = "_fovDefaultButton";
            this._fovDefaultButton.Size = new System.Drawing.Size(128, 30);
            this._fovDefaultButton.TabIndex = 5;
            this._fovDefaultButton.Text = "Default";
            this._fovDefaultButton.UseVisualStyleBackColor = true;
            this._fovDefaultButton.Click += new System.EventHandler(this._fovDefaultButton_Click);
            // 
            // _zoomDefaultButton
            // 
            this._zoomDefaultButton.Location = new System.Drawing.Point(172, 52);
            this._zoomDefaultButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._zoomDefaultButton.Name = "_zoomDefaultButton";
            this._zoomDefaultButton.Size = new System.Drawing.Size(128, 30);
            this._zoomDefaultButton.TabIndex = 4;
            this._zoomDefaultButton.Text = "Default";
            this._zoomDefaultButton.UseVisualStyleBackColor = true;
            this._zoomDefaultButton.Click += new System.EventHandler(this._zoomDefaultButton_Click);
            // 
            // _fovLabel
            // 
            this._fovLabel.AutoSize = true;
            this._fovLabel.Location = new System.Drawing.Point(319, 52);
            this._fovLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._fovLabel.Name = "_fovLabel";
            this._fovLabel.Size = new System.Drawing.Size(123, 25);
            this._fovLabel.TabIndex = 2;
            this._fovLabel.Text = "Field of View";
            // 
            // _zoomLabel
            // 
            this._zoomLabel.AutoSize = true;
            this._zoomLabel.Location = new System.Drawing.Point(8, 52);
            this._zoomLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._zoomLabel.Name = "_zoomLabel";
            this._zoomLabel.Size = new System.Drawing.Size(105, 25);
            this._zoomLabel.TabIndex = 1;
            this._zoomLabel.Text = "Zoom Max";
            // 
            // _processListBox
            // 
            this._processListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._processListBox.Controls.Add(this.ReadyIndicator);
            this._processListBox.Controls.Add(this._autoQuitCheckbox);
            this._processListBox.Controls.Add(this._gotoProcessButton);
            this._processListBox.Controls.Add(this.label1);
            this._processListBox.Controls.Add(this._processList);
            this._processListBox.Controls.Add(this._autoApplyCheckbox);
            this._processListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._processListBox.Location = new System.Drawing.Point(18, 12);
            this._processListBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._processListBox.Name = "_processListBox";
            this._processListBox.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._processListBox.Size = new System.Drawing.Size(632, 108);
            this._processListBox.TabIndex = 2;
            this._processListBox.TabStop = false;
            this._processListBox.Text = "Processes";
            // 
            // ReadyIndicator
            // 
            this.ReadyIndicator.Location = new System.Drawing.Point(595, 66);
            this.ReadyIndicator.Name = "ReadyIndicator";
            this.ReadyIndicator.Size = new System.Drawing.Size(30, 30);
            this.ReadyIndicator.TabIndex = 12;
            this.ReadyIndicator.TabStop = false;
            // 
            // _autoQuitCheckbox
            // 
            this._autoQuitCheckbox.AutoSize = true;
            this._autoQuitCheckbox.Location = new System.Drawing.Point(324, 29);
            this._autoQuitCheckbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._autoQuitCheckbox.Name = "_autoQuitCheckbox";
            this._autoQuitCheckbox.Size = new System.Drawing.Size(268, 29);
            this._autoQuitCheckbox.TabIndex = 11;
            this._autoQuitCheckbox.Text = "Quit when processes close";
            this._autoQuitTooltip.SetToolTip(this._autoQuitCheckbox, "Automatically close FFXIV Zoom Hack after \r\npreviously detected instances of FFXI" +
        "V are closed");
            this._autoQuitCheckbox.UseVisualStyleBackColor = true;
            this._autoQuitCheckbox.CheckedChanged += new System.EventHandler(this.AutoQuitCheckChanged);
            // 
            // _gotoProcessButton
            // 
            this._gotoProcessButton.Location = new System.Drawing.Point(380, 66);
            this._gotoProcessButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._gotoProcessButton.Name = "_gotoProcessButton";
            this._gotoProcessButton.Size = new System.Drawing.Size(191, 30);
            this._gotoProcessButton.TabIndex = 8;
            this._gotoProcessButton.Text = "Bring to Front";
            this._gotoProcessButton.UseVisualStyleBackColor = true;
            this._gotoProcessButton.Click += new System.EventHandler(this._gotoProcessButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 60);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 25);
            this.label1.TabIndex = 8;
            this.label1.Text = "Processes";
            // 
            // _processList
            // 
            this._processList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._processList.FormattingEnabled = true;
            this._processList.Location = new System.Drawing.Point(127, 63);
            this._processList.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._processList.Name = "_processList";
            this._processList.Size = new System.Drawing.Size(228, 33);
            this._processList.TabIndex = 1;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.Timer1Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 303);
            this.Controls.Add(this._processListBox);
            this.Controls.Add(this._zoomSettingsBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(681, 350);
            this.MinimumSize = new System.Drawing.Size(681, 350);
            this.Name = "Form1";
            this.Text = "FFXIV Zoom Hack";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this._zoomSettingsBox.ResumeLayout(false);
            this._zoomSettingsBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._fovUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._zoomUpDown)).EndInit();
            this._processListBox.ResumeLayout(false);
            this._processListBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ReadyIndicator)).EndInit();
            this.ResumeLayout(false);

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

