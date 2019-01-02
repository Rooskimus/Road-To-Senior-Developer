namespace BasicWindowsEvent
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
            this.SubmitButton = new System.Windows.Forms.Button();
            this.CountriesComboBox = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // SubmitButton
            // 
            this.SubmitButton.Location = new System.Drawing.Point(44, 114);
            this.SubmitButton.Name = "SubmitButton";
            this.SubmitButton.Size = new System.Drawing.Size(161, 33);
            this.SubmitButton.TabIndex = 0;
            this.SubmitButton.Text = "Submit\r\n";
            this.SubmitButton.UseVisualStyleBackColor = true;
            this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
            // 
            // CountriesComboBox
            // 
            this.CountriesComboBox.FormattingEnabled = true;
            this.CountriesComboBox.Items.AddRange(new object[] {
            "USA",
            "UK",
            "Australia",
            "Canada"});
            this.CountriesComboBox.Location = new System.Drawing.Point(44, 71);
            this.CountriesComboBox.Name = "CountriesComboBox";
            this.CountriesComboBox.Size = new System.Drawing.Size(161, 21);
            this.CountriesComboBox.TabIndex = 1;
            this.CountriesComboBox.SelectedIndexChanged += new System.EventHandler(this.CountriesComboBox_SelectedIndexChanged);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(44, 52);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(134, 13);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "Countries";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(360, 260);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.CountriesComboBox);
            this.Controls.Add(this.SubmitButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SubmitButton;
        private System.Windows.Forms.ComboBox CountriesComboBox;
        private System.Windows.Forms.TextBox textBox1;
    }
}