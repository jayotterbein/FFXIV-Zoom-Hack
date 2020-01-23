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
            this._autoApplyCheckbox = new System.Windows.Forms.CheckBox();
            this._zoomSettingsBox = new System.Windows.Forms.GroupBox();
            this._fovUpDown = new System.Windows.Forms.NumericUpDown();
            this._zoomUpDown = new System.Windows.Forms.NumericUpDown();
            this._fovDefaultButton = new System.Windows.Forms.Button();
            this._zoomDefaultButton = new System.Windows.Forms.Button();
            this._fovLabel = new System.Windows.Forms.Label();
            this._zoomLabel = new System.Windows.Forms.Label();
            this._processListBox = new System.Windows.Forms.GroupBox();
            this._autoQuitCheckbox = new System.Windows.Forms.CheckBox();
            this._gotoProcessButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this._processList = new System.Windows.Forms.ComboBox();
            this._updateOffsetsTextbox = new System.Windows.Forms.TextBox();
            this._updateOffsetsButton = new System.Windows.Forms.Button();
            this._updateLocationDefault = new System.Windows.Forms.Button();
            this._autoQuitTooltip = new System.Windows.Forms.ToolTip(this.components);
            this._zoomSettingsBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._fovUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._zoomUpDown)).BeginInit();
            this._processListBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // _autoApplyCheckbox
            // 
            this._autoApplyCheckbox.AutoSize = true;
            this._autoApplyCheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._autoApplyCheckbox.Location = new System.Drawing.Point(10, 25);
            this._autoApplyCheckbox.Name = "_autoApplyCheckbox";
            this._autoApplyCheckbox.Size = new System.Drawing.Size(219, 24);
            this._autoApplyCheckbox.TabIndex = 0;
            this._autoApplyCheckbox.Text = "Automatically apply on load";
            this._autoApplyCheckbox.UseVisualStyleBackColor = true;
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
            this._zoomSettingsBox.Location = new System.Drawing.Point(13, 150);
            this._zoomSettingsBox.Name = "_zoomSettingsBox";
            this._zoomSettingsBox.Size = new System.Drawing.Size(474, 109);
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
            this._fovUpDown.Location = new System.Drawing.Point(243, 83);
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
            this._fovUpDown.Size = new System.Drawing.Size(215, 26);
            this._fovUpDown.TabIndex = 7;
            this._fovUpDown.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // _zoomUpDown
            // 
            this._zoomUpDown.Location = new System.Drawing.Point(10, 83);
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
            this._zoomUpDown.Size = new System.Drawing.Size(215, 26);
            this._zoomUpDown.TabIndex = 6;
            this._zoomUpDown.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // _fovDefaultButton
            // 
            this._fovDefaultButton.Location = new System.Drawing.Point(362, 45);
            this._fovDefaultButton.Name = "_fovDefaultButton";
            this._fovDefaultButton.Size = new System.Drawing.Size(96, 26);
            this._fovDefaultButton.TabIndex = 5;
            this._fovDefaultButton.Text = "Default";
            this._fovDefaultButton.UseVisualStyleBackColor = true;
            this._fovDefaultButton.Click += new System.EventHandler(this._fovDefaultButton_Click);
            // 
            // _zoomDefaultButton
            // 
            this._zoomDefaultButton.Location = new System.Drawing.Point(129, 45);
            this._zoomDefaultButton.Name = "_zoomDefaultButton";
            this._zoomDefaultButton.Size = new System.Drawing.Size(96, 26);
            this._zoomDefaultButton.TabIndex = 4;
            this._zoomDefaultButton.Text = "Default";
            this._zoomDefaultButton.UseVisualStyleBackColor = true;
            this._zoomDefaultButton.Click += new System.EventHandler(this._zoomDefaultButton_Click);
            // 
            // _fovLabel
            // 
            this._fovLabel.AutoSize = true;
            this._fovLabel.Location = new System.Drawing.Point(239, 45);
            this._fovLabel.Name = "_fovLabel";
            this._fovLabel.Size = new System.Drawing.Size(99, 20);
            this._fovLabel.TabIndex = 2;
            this._fovLabel.Text = "Field of View";
            // 
            // _zoomLabel
            // 
            this._zoomLabel.AutoSize = true;
            this._zoomLabel.Location = new System.Drawing.Point(6, 45);
            this._zoomLabel.Name = "_zoomLabel";
            this._zoomLabel.Size = new System.Drawing.Size(83, 20);
            this._zoomLabel.TabIndex = 1;
            this._zoomLabel.Text = "Zoom Max";
            // 
            // _processListBox
            // 
            this._processListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._processListBox.Controls.Add(this._autoQuitCheckbox);
            this._processListBox.Controls.Add(this._gotoProcessButton);
            this._processListBox.Controls.Add(this.label1);
            this._processListBox.Controls.Add(this._processList);
            this._processListBox.Controls.Add(this._autoApplyCheckbox);
            this._processListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._processListBox.Location = new System.Drawing.Point(13, 50);
            this._processListBox.Name = "_processListBox";
            this._processListBox.Size = new System.Drawing.Size(474, 94);
            this._processListBox.TabIndex = 2;
            this._processListBox.TabStop = false;
            this._processListBox.Text = "Processes";
            // 
            // _autoQuitCheckbox
            // 
            this._autoQuitCheckbox.AutoSize = true;
            this._autoQuitCheckbox.Location = new System.Drawing.Point(243, 25);
            this._autoQuitCheckbox.Name = "_autoQuitCheckbox";
            this._autoQuitCheckbox.Size = new System.Drawing.Size(217, 24);
            this._autoQuitCheckbox.TabIndex = 11;
            this._autoQuitCheckbox.Text = "Quit when processes close";
            this._autoQuitTooltip.SetToolTip(this._autoQuitCheckbox, "Automatically close FFXIV Zoom Hack after \r\npreviously detected instances of FFXI" +
        "V are closed");
            this._autoQuitCheckbox.UseVisualStyleBackColor = true;
            // 
            // _gotoProcessButton
            // 
            this._gotoProcessButton.Location = new System.Drawing.Point(285, 57);
            this._gotoProcessButton.Name = "_gotoProcessButton";
            this._gotoProcessButton.Size = new System.Drawing.Size(143, 26);
            this._gotoProcessButton.TabIndex = 8;
            this._gotoProcessButton.Text = "Bring to Front";
            this._gotoProcessButton.UseVisualStyleBackColor = true;
            this._gotoProcessButton.Click += new System.EventHandler(this._gotoProcessButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "Processes";
            // 
            // _processList
            // 
            this._processList.FormattingEnabled = true;
            this._processList.Location = new System.Drawing.Point(95, 55);
            this._processList.Name = "_processList";
            this._processList.Size = new System.Drawing.Size(172, 28);
            this._processList.TabIndex = 1;
            // 
            // _updateOffsetsTextbox
            // 
            this._updateOffsetsTextbox.Location = new System.Drawing.Point(13, 13);
            this._updateOffsetsTextbox.Name = "_updateOffsetsTextbox";
            this._updateOffsetsTextbox.Size = new System.Drawing.Size(306, 20);
            this._updateOffsetsTextbox.TabIndex = 3;
            // 
            // _updateOffsetsButton
            // 
            this._updateOffsetsButton.Location = new System.Drawing.Point(404, 13);
            this._updateOffsetsButton.Name = "_updateOffsetsButton";
            this._updateOffsetsButton.Size = new System.Drawing.Size(83, 42);
            this._updateOffsetsButton.TabIndex = 9;
            this._updateOffsetsButton.Text = "Update Offsets";
            this._updateOffsetsButton.UseVisualStyleBackColor = true;
            this._updateOffsetsButton.Click += new System.EventHandler(this._updateOffsetsButton_Click);
            // 
            // _updateLocationDefault
            // 
            this._updateLocationDefault.Location = new System.Drawing.Point(325, 13);
            this._updateLocationDefault.Name = "_updateLocationDefault";
            this._updateLocationDefault.Size = new System.Drawing.Size(75, 42);
            this._updateLocationDefault.TabIndex = 10;
            this._updateLocationDefault.Text = "Default\r\nURL";
            this._updateLocationDefault.UseVisualStyleBackColor = true;
            this._updateLocationDefault.Click += new System.EventHandler(this._updateLocationDefault_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 271);
            this.Controls.Add(this._updateLocationDefault);
            this.Controls.Add(this._updateOffsetsButton);
            this.Controls.Add(this._updateOffsetsTextbox);
            this.Controls.Add(this._processListBox);
            this.Controls.Add(this._zoomSettingsBox);
            this.MaximumSize = new System.Drawing.Size(515, 310);
            this.MinimumSize = new System.Drawing.Size(515, 310);
            this.Name = "Form1";
            this.Text = "FFXIV Zoom Hack";
            this.Load += new System.EventHandler(this.Form1_Load);
            this._zoomSettingsBox.ResumeLayout(false);
            this._zoomSettingsBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._fovUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._zoomUpDown)).EndInit();
            this._processListBox.ResumeLayout(false);
            this._processListBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.TextBox _updateOffsetsTextbox;
        private System.Windows.Forms.Button _updateOffsetsButton;
        private System.Windows.Forms.Button _updateLocationDefault;
    }
}

