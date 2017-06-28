namespace ScrEcord2
{
    partial class Setting
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
            this.browseTextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.browseButton = new System.Windows.Forms.Button();
            this.filetypeCombobox = new System.Windows.Forms.ComboBox();
            this.saveCloseButton = new System.Windows.Forms.Button();
            this.exploreButton = new System.Windows.Forms.Button();
            this.clipboardCheckbox = new System.Windows.Forms.CheckBox();
            this.exploreCheckbox = new System.Windows.Forms.CheckBox();
            this.splitCheckbox = new System.Windows.Forms.CheckBox();
            this.overrideCheckbox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // browseTextbox
            // 
            this.browseTextbox.Location = new System.Drawing.Point(81, 24);
            this.browseTextbox.Name = "browseTextbox";
            this.browseTextbox.Size = new System.Drawing.Size(230, 19);
            this.browseTextbox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "保存先";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "ファイル形式";
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(317, 22);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(55, 23);
            this.browseButton.TabIndex = 3;
            this.browseButton.Text = "参照";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // filetypeCombobox
            // 
            this.filetypeCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filetypeCombobox.FormattingEnabled = true;
            this.filetypeCombobox.Items.AddRange(new object[] {
            "png",
            "jpg",
            "bmp",
            "gif"});
            this.filetypeCombobox.Location = new System.Drawing.Point(81, 66);
            this.filetypeCombobox.Name = "filetypeCombobox";
            this.filetypeCombobox.Size = new System.Drawing.Size(230, 20);
            this.filetypeCombobox.TabIndex = 4;
            // 
            // saveCloseButton
            // 
            this.saveCloseButton.Location = new System.Drawing.Point(255, 201);
            this.saveCloseButton.Name = "saveCloseButton";
            this.saveCloseButton.Size = new System.Drawing.Size(117, 48);
            this.saveCloseButton.TabIndex = 5;
            this.saveCloseButton.Text = "保存して閉じる";
            this.saveCloseButton.UseVisualStyleBackColor = true;
            this.saveCloseButton.Click += new System.EventHandler(this.saveCloseButton_Click);
            // 
            // exploreButton
            // 
            this.exploreButton.Location = new System.Drawing.Point(124, 226);
            this.exploreButton.Name = "exploreButton";
            this.exploreButton.Size = new System.Drawing.Size(125, 23);
            this.exploreButton.TabIndex = 6;
            this.exploreButton.Text = "保存先フォルダを開く";
            this.exploreButton.UseVisualStyleBackColor = true;
            this.exploreButton.Click += new System.EventHandler(this.exploreButton_Click);
            // 
            // clipboardCheckbox
            // 
            this.clipboardCheckbox.AutoSize = true;
            this.clipboardCheckbox.Location = new System.Drawing.Point(81, 106);
            this.clipboardCheckbox.Name = "clipboardCheckbox";
            this.clipboardCheckbox.Size = new System.Drawing.Size(278, 16);
            this.clipboardCheckbox.TabIndex = 7;
            this.clipboardCheckbox.Text = "保存時に保存フォルダのパスをクリップボードに保存する";
            this.clipboardCheckbox.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.clipboardCheckbox.UseVisualStyleBackColor = true;
            // 
            // exploreCheckbox
            // 
            this.exploreCheckbox.AutoSize = true;
            this.exploreCheckbox.Location = new System.Drawing.Point(81, 128);
            this.exploreCheckbox.Name = "exploreCheckbox";
            this.exploreCheckbox.Size = new System.Drawing.Size(155, 16);
            this.exploreCheckbox.TabIndex = 8;
            this.exploreCheckbox.Text = "保存時に保存フォルダを開く";
            this.exploreCheckbox.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.exploreCheckbox.UseVisualStyleBackColor = true;
            // 
            // splitCheckbox
            // 
            this.splitCheckbox.AutoSize = true;
            this.splitCheckbox.Checked = true;
            this.splitCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.splitCheckbox.Location = new System.Drawing.Point(81, 150);
            this.splitCheckbox.Name = "splitCheckbox";
            this.splitCheckbox.Size = new System.Drawing.Size(142, 16);
            this.splitCheckbox.TabIndex = 9;
            this.splitCheckbox.Text = "月毎にフォルダ分けを行う";
            this.splitCheckbox.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.splitCheckbox.UseVisualStyleBackColor = true;
            // 
            // overrideCheckbox
            // 
            this.overrideCheckbox.AutoSize = true;
            this.overrideCheckbox.Location = new System.Drawing.Point(81, 172);
            this.overrideCheckbox.Name = "overrideCheckbox";
            this.overrideCheckbox.Size = new System.Drawing.Size(226, 16);
            this.overrideCheckbox.TabIndex = 10;
            this.overrideCheckbox.Text = "PrintScreenの既存の挙動をキャンセルする";
            this.overrideCheckbox.UseVisualStyleBackColor = true;
            // 
            // Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 261);
            this.Controls.Add(this.overrideCheckbox);
            this.Controls.Add(this.splitCheckbox);
            this.Controls.Add(this.exploreCheckbox);
            this.Controls.Add(this.clipboardCheckbox);
            this.Controls.Add(this.exploreButton);
            this.Controls.Add(this.saveCloseButton);
            this.Controls.Add(this.filetypeCombobox);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.browseTextbox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Setting";
            this.Text = "ScrEcord 環境設定ツール";
            this.Load += new System.EventHandler(this.Setting_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox browseTextbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.ComboBox filetypeCombobox;
        private System.Windows.Forms.Button saveCloseButton;
        private System.Windows.Forms.Button exploreButton;
        private System.Windows.Forms.CheckBox clipboardCheckbox;
        private System.Windows.Forms.CheckBox exploreCheckbox;
        private System.Windows.Forms.CheckBox splitCheckbox;
        private System.Windows.Forms.CheckBox overrideCheckbox;
    }
}