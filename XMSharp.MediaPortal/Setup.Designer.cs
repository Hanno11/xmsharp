namespace XMSharp.MP
{
	partial class frmSetup
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textPassword = new System.Windows.Forms.TextBox();
			this.textEmail = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.checkQualityLow = new System.Windows.Forms.RadioButton();
			this.checkQualityHigh = new System.Windows.Forms.RadioButton();
			this.label3 = new System.Windows.Forms.Label();
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.textMenuName = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.textTimeout = new System.Windows.Forms.NumericUpDown();
			this.label6 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.textTimeout)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.textTimeout);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.textPassword);
			this.groupBox1.Controls.Add(this.textEmail);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(289, 96);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "XM Radio Account:";
			// 
			// textPassword
			// 
			this.textPassword.Location = new System.Drawing.Point(91, 42);
			this.textPassword.Name = "textPassword";
			this.textPassword.PasswordChar = '*';
			this.textPassword.Size = new System.Drawing.Size(99, 20);
			this.textPassword.TabIndex = 3;
			// 
			// textEmail
			// 
			this.textEmail.Location = new System.Drawing.Point(91, 16);
			this.textEmail.Name = "textEmail";
			this.textEmail.Size = new System.Drawing.Size(188, 20);
			this.textEmail.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 45);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Password:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Email:";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.checkQualityLow);
			this.groupBox2.Controls.Add(this.checkQualityHigh);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Location = new System.Drawing.Point(12, 199);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(289, 45);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Playback Settings:";
			// 
			// checkQualityLow
			// 
			this.checkQualityLow.AutoSize = true;
			this.checkQualityLow.Location = new System.Drawing.Point(134, 18);
			this.checkQualityLow.Name = "checkQualityLow";
			this.checkQualityLow.Size = new System.Drawing.Size(45, 17);
			this.checkQualityLow.TabIndex = 2;
			this.checkQualityLow.Text = "Low";
			this.checkQualityLow.UseVisualStyleBackColor = true;
			// 
			// checkQualityHigh
			// 
			this.checkQualityHigh.AutoSize = true;
			this.checkQualityHigh.Checked = true;
			this.checkQualityHigh.Location = new System.Drawing.Point(72, 18);
			this.checkQualityHigh.Name = "checkQualityHigh";
			this.checkQualityHigh.Size = new System.Drawing.Size(47, 17);
			this.checkQualityHigh.TabIndex = 1;
			this.checkQualityHigh.TabStop = true;
			this.checkQualityHigh.Text = "High";
			this.checkQualityHigh.UseVisualStyleBackColor = true;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 20);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(42, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "Quality:";
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(226, 163);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 23);
			this.buttonOK.TabIndex = 2;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Location = new System.Drawing.Point(145, 163);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 3;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.textMenuName);
			this.groupBox3.Controls.Add(this.label4);
			this.groupBox3.Location = new System.Drawing.Point(12, 114);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(289, 43);
			this.groupBox3.TabIndex = 4;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Plugin Settings:";
			// 
			// textMenuName
			// 
			this.textMenuName.Location = new System.Drawing.Point(91, 13);
			this.textMenuName.Name = "textMenuName";
			this.textMenuName.Size = new System.Drawing.Size(188, 20);
			this.textMenuName.TabIndex = 1;
			this.textMenuName.Text = "XM Radio";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 16);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(68, 13);
			this.label4.TabIndex = 0;
			this.label4.Text = "Menu Name:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(6, 70);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(77, 13);
			this.label5.TabIndex = 4;
			this.label5.Text = "Login Timeout:";
			// 
			// textTimeout
			// 
			this.textTimeout.Location = new System.Drawing.Point(91, 68);
			this.textTimeout.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
			this.textTimeout.Name = "textTimeout";
			this.textTimeout.Size = new System.Drawing.Size(53, 20);
			this.textTimeout.TabIndex = 5;
			this.textTimeout.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(150, 70);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(43, 13);
			this.label6.TabIndex = 6;
			this.label6.Text = "minutes";
			// 
			// frmSetup
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(313, 195);
			this.ControlBox = false;
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "frmSetup";
			this.Text = "XM Radio Settings";
			this.Load += new System.EventHandler(this.frmSetup_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.textTimeout)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox textEmail;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textPassword;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RadioButton checkQualityLow;
		private System.Windows.Forms.RadioButton checkQualityHigh;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textMenuName;
        private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.NumericUpDown textTimeout;
		private System.Windows.Forms.Label label5;
	}
}