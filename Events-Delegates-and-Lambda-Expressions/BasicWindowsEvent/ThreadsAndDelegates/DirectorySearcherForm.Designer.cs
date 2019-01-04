namespace ThreadsAndDelegates
{
    partial class DirectorySearcherForm
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
            this.SearchButton = new System.Windows.Forms.Button();
            this.SearchBar = new System.Windows.Forms.TextBox();
            this.SearchLabel = new System.Windows.Forms.Label();
            this.directorySearcher = new System.DirectoryServices.DirectorySearcher();
            this._ListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(12, 12);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(120, 53);
            this.SearchButton.TabIndex = 0;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // SearchBar
            // 
            this.SearchBar.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.SearchBar.Location = new System.Drawing.Point(138, 29);
            this.SearchBar.Name = "SearchBar";
            this.SearchBar.Size = new System.Drawing.Size(214, 20);
            this.SearchBar.TabIndex = 2;
            // 
            // SearchLabel
            // 
            this.SearchLabel.AutoSize = true;
            this.SearchLabel.Location = new System.Drawing.Point(289, 52);
            this.SearchLabel.Name = "SearchLabel";
            this.SearchLabel.Size = new System.Drawing.Size(0, 13);
            this.SearchLabel.TabIndex = 3;
            // 
            // directorySearcher
            // 
            this.directorySearcher.ClientTimeout = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher.ServerPageTimeLimit = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher.ServerTimeLimit = System.TimeSpan.Parse("-00:00:01");
            // 
            // _ListBox
            // 
            this._ListBox.FormattingEnabled = true;
            this._ListBox.Location = new System.Drawing.Point(12, 80);
            this._ListBox.Name = "_ListBox";
            this._ListBox.Size = new System.Drawing.Size(340, 147);
            this._ListBox.TabIndex = 4;
            // 
            // DirectorySearcherForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 239);
            this.Controls.Add(this._ListBox);
            this.Controls.Add(this.SearchLabel);
            this.Controls.Add(this.SearchBar);
            this.Controls.Add(this.SearchButton);
            this.Name = "DirectorySearcherForm";
            this.Text = "DirectorySearcherForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.TextBox SearchBar;
        private System.Windows.Forms.Label SearchLabel;
        private System.DirectoryServices.DirectorySearcher directorySearcher;
        private System.Windows.Forms.ListBox _ListBox;
    }
}