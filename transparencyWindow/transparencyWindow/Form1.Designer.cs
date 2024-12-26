namespace TransparencyWindow
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.transparencySlider = new System.Windows.Forms.TrackBar();
            this.transparencyLabel = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.transparencyInput = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.saveHotkeyButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.hotkeyInput = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.transparencySlider)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // transparencySlider
            // 
            this.transparencySlider.AutoSize = false;
            this.transparencySlider.Location = new System.Drawing.Point(18, 64);
            this.transparencySlider.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.transparencySlider.Name = "transparencySlider";
            this.transparencySlider.Size = new System.Drawing.Size(324, 45);
            this.transparencySlider.TabIndex = 0;
            // 
            // transparencyLabel
            // 
            this.transparencyLabel.AutoSize = true;
            this.transparencyLabel.Location = new System.Drawing.Point(16, 14);
            this.transparencyLabel.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.transparencyLabel.Name = "transparencyLabel";
            this.transparencyLabel.Size = new System.Drawing.Size(35, 12);
            this.transparencyLabel.TabIndex = 1;
            this.transparencyLabel.Text = "label1";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(164, 226);
            this.saveButton.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(84, 34);
            this.saveButton.TabIndex = 2;
            this.saveButton.Text = "button1";
            this.saveButton.UseVisualStyleBackColor = true;
            // 
            // transparencyInput
            // 
            this.transparencyInput.Location = new System.Drawing.Point(212, 40);
            this.transparencyInput.Name = "transparencyInput";
            this.transparencyInput.Size = new System.Drawing.Size(130, 19);
            this.transparencyInput.TabIndex = 3;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(423, 291);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.transparencyLabel);
            this.tabPage1.Controls.Add(this.saveButton);
            this.tabPage1.Controls.Add(this.transparencyInput);
            this.tabPage1.Controls.Add(this.transparencySlider);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(415, 265);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.saveHotkeyButton);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.hotkeyInput);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(415, 265);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // saveHotkeyButton
            // 
            this.saveHotkeyButton.Location = new System.Drawing.Point(170, 204);
            this.saveHotkeyButton.Name = "saveHotkeyButton";
            this.saveHotkeyButton.Size = new System.Drawing.Size(75, 23);
            this.saveHotkeyButton.TabIndex = 2;
            this.saveHotkeyButton.Text = "button1";
            this.saveHotkeyButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // hotkeyInput
            // 
            this.hotkeyInput.Location = new System.Drawing.Point(23, 46);
            this.hotkeyInput.Name = "hotkeyInput";
            this.hotkeyInput.Size = new System.Drawing.Size(100, 19);
            this.hotkeyInput.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 315);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.Name = "MainForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.transparencySlider)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TrackBar transparencySlider;
        private System.Windows.Forms.Label transparencyLabel;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.TextBox transparencyInput;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox hotkeyInput;
        private System.Windows.Forms.Button saveHotkeyButton;
    }
}

