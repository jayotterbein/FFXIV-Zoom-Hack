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
            this._autoApplyCheckbox = new System.Windows.Forms.CheckBox();
            this._zoomSettingsBox = new System.Windows.Forms.GroupBox();
            this._fovDefaultButton = new System.Windows.Forms.Button();
            this._zoomDefaultButton = new System.Windows.Forms.Button();
            this._fovLabel = new System.Windows.Forms.Label();
            this._zoomLabel = new System.Windows.Forms.Label();
            this._processListBox = new System.Windows.Forms.GroupBox();
            this._zoomMaxValue = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this._processList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this._gotoProcessButton = new System.Windows.Forms.Button();
            this._zoomSettingsBox.SuspendLayout();
            this._processListBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._zoomMaxValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this._autoApplyCheckbox.AutoSize = true;
            this._autoApplyCheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._autoApplyCheckbox.Location = new System.Drawing.Point(6, 19);
            this._autoApplyCheckbox.Name = "_autoApplyCheckbox";
            this._autoApplyCheckbox.Size = new System.Drawing.Size(219, 24);
            this._autoApplyCheckbox.TabIndex = 0;
            this._autoApplyCheckbox.Text = "Automatically apply on load";
            this._autoApplyCheckbox.UseVisualStyleBackColor = true;
            this._autoApplyCheckbox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // _zoomSettingsBox
            // 
            this._zoomSettingsBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._zoomSettingsBox.Controls.Add(this.numericUpDown1);
            this._zoomSettingsBox.Controls.Add(this._zoomMaxValue);
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
            // _fovDefaultButton
            // 
            this._fovDefaultButton.Location = new System.Drawing.Point(362, 45);
            this._fovDefaultButton.Name = "_fovDefaultButton";
            this._fovDefaultButton.Size = new System.Drawing.Size(96, 26);
            this._fovDefaultButton.TabIndex = 5;
            this._fovDefaultButton.Text = "Default";
            this._fovDefaultButton.UseVisualStyleBackColor = true;
            // 
            // _zoomDefaultButton
            // 
            this._zoomDefaultButton.Location = new System.Drawing.Point(129, 45);
            this._zoomDefaultButton.Name = "_zoomDefaultButton";
            this._zoomDefaultButton.Size = new System.Drawing.Size(96, 26);
            this._zoomDefaultButton.TabIndex = 4;
            this._zoomDefaultButton.Text = "Default";
            this._zoomDefaultButton.UseVisualStyleBackColor = true;
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
            this._processListBox.Controls.Add(this._gotoProcessButton);
            this._processListBox.Controls.Add(this.label1);
            this._processListBox.Controls.Add(this._processList);
            this._processListBox.Controls.Add(this._autoApplyCheckbox);
            this._processListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._processListBox.Location = new System.Drawing.Point(13, 12);
            this._processListBox.Name = "_processListBox";
            this._processListBox.Size = new System.Drawing.Size(474, 132);
            this._processListBox.TabIndex = 2;
            this._processListBox.TabStop = false;
            this._processListBox.Text = "Processes";
            // 
            // _zoomMaxValue
            // 
            this._zoomMaxValue.Location = new System.Drawing.Point(10, 83);
            this._zoomMaxValue.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this._zoomMaxValue.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this._zoomMaxValue.Name = "_zoomMaxValue";
            this._zoomMaxValue.Size = new System.Drawing.Size(215, 26);
            this._zoomMaxValue.TabIndex = 6;
            this._zoomMaxValue.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DecimalPlaces = 2;
            this.numericUpDown1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDown1.Location = new System.Drawing.Point(243, 83);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            131072});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(215, 26);
            this.numericUpDown1.TabIndex = 7;
            this.numericUpDown1.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // _processList
            // 
            this._processList.FormattingEnabled = true;
            this._processList.Location = new System.Drawing.Point(95, 57);
            this._processList.Name = "_processList";
            this._processList.Size = new System.Drawing.Size(172, 28);
            this._processList.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "Processes";
            // 
            // _gotoProcessButton
            // 
            this._gotoProcessButton.Location = new System.Drawing.Point(273, 54);
            this._gotoProcessButton.Name = "_gotoProcessButton";
            this._gotoProcessButton.Size = new System.Drawing.Size(143, 26);
            this._gotoProcessButton.TabIndex = 8;
            this._gotoProcessButton.Text = "Bring to Front";
            this._gotoProcessButton.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 271);
            this.Controls.Add(this._processListBox);
            this.Controls.Add(this._zoomSettingsBox);
            this.MaximumSize = new System.Drawing.Size(515, 310);
            this.MinimumSize = new System.Drawing.Size(515, 310);
            this.Name = "Form1";
            this.Text = "FFXIV Zoom Hack";
            this.Load += new System.EventHandler(this.Form1_Load);
            this._zoomSettingsBox.ResumeLayout(false);
            this._zoomSettingsBox.PerformLayout();
            this._processListBox.ResumeLayout(false);
            this._processListBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._zoomMaxValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox _autoApplyCheckbox;
        private System.Windows.Forms.GroupBox _zoomSettingsBox;
        private System.Windows.Forms.GroupBox _processListBox;
        private System.Windows.Forms.Label _fovLabel;
        private System.Windows.Forms.Label _zoomLabel;
        private System.Windows.Forms.Button _fovDefaultButton;
        private System.Windows.Forms.Button _zoomDefaultButton;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown _zoomMaxValue;
        private System.Windows.Forms.Button _gotoProcessButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox _processList;
    }
}

