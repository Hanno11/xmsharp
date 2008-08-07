namespace XMSharp.Gui
{
	partial class frmMain
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
            this.picCurrentIcon = new System.Windows.Forms.PictureBox();
            this.labelCurrentSong = new System.Windows.Forms.Label();
            this.labelCurrentAlbum = new System.Windows.Forms.Label();
            this.labelCurrentArtist = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.listItems = new System.Windows.Forms.ListBox();
            this.timerUpdateAll = new System.Windows.Forms.Timer(this.components);
            this.timerUpdateCurrent = new System.Windows.Forms.Timer(this.components);
            this.buttonStop = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picCurrentIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // picCurrentIcon
            // 
            this.picCurrentIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.picCurrentIcon.Location = new System.Drawing.Point(12, 12);
            this.picCurrentIcon.Name = "picCurrentIcon";
            this.picCurrentIcon.Size = new System.Drawing.Size(119, 54);
            this.picCurrentIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picCurrentIcon.TabIndex = 7;
            this.picCurrentIcon.TabStop = false;
            // 
            // labelCurrentSong
            // 
            this.labelCurrentSong.AutoSize = true;
            this.labelCurrentSong.Location = new System.Drawing.Point(176, 39);
            this.labelCurrentSong.Name = "labelCurrentSong";
            this.labelCurrentSong.Size = new System.Drawing.Size(33, 13);
            this.labelCurrentSong.TabIndex = 6;
            this.labelCurrentSong.Text = "(N/A)";
            // 
            // labelCurrentAlbum
            // 
            this.labelCurrentAlbum.AutoSize = true;
            this.labelCurrentAlbum.Location = new System.Drawing.Point(176, 53);
            this.labelCurrentAlbum.Name = "labelCurrentAlbum";
            this.labelCurrentAlbum.Size = new System.Drawing.Size(33, 13);
            this.labelCurrentAlbum.TabIndex = 5;
            this.labelCurrentAlbum.Text = "(N/A)";
            // 
            // labelCurrentArtist
            // 
            this.labelCurrentArtist.AutoSize = true;
            this.labelCurrentArtist.Location = new System.Drawing.Point(176, 25);
            this.labelCurrentArtist.Name = "labelCurrentArtist";
            this.labelCurrentArtist.Size = new System.Drawing.Size(33, 13);
            this.labelCurrentArtist.TabIndex = 4;
            this.labelCurrentArtist.Text = "(N/A)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(137, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Song:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(137, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Album:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(137, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Artist:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(137, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "No Channel Selected";
            // 
            // listItems
            // 
            this.listItems.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listItems.FormattingEnabled = true;
            this.listItems.ItemHeight = 50;
            this.listItems.Location = new System.Drawing.Point(12, 72);
            this.listItems.Name = "listItems";
            this.listItems.Size = new System.Drawing.Size(700, 354);
            this.listItems.TabIndex = 8;
            this.listItems.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listItems_MouseDoubleClick);
            this.listItems.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listItems_DrawItem);
            this.listItems.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listItems_MouseClick);
            this.listItems.SelectedIndexChanged += new System.EventHandler(this.listItems_SelectedIndexChanged);
            this.listItems.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.listItems_KeyPress);
            this.listItems.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listItems_KeyDown);
            // 
            // timerUpdateAll
            // 
            this.timerUpdateAll.Enabled = true;
            this.timerUpdateAll.Interval = 60000;
            this.timerUpdateAll.Tick += new System.EventHandler(this.timerUpdateAll_Tick);
            // 
            // timerUpdateCurrent
            // 
            this.timerUpdateCurrent.Enabled = true;
            this.timerUpdateCurrent.Interval = 15000;
            this.timerUpdateCurrent.Tick += new System.EventHandler(this.timerUpdateCurrent_Tick);
            // 
            // buttonStop
            // 
            this.buttonStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStop.Location = new System.Drawing.Point(663, 12);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(49, 23);
            this.buttonStop.TabIndex = 9;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 441);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.listItems);
            this.Controls.Add(this.labelCurrentSong);
            this.Controls.Add(this.picCurrentIcon);
            this.Controls.Add(this.labelCurrentAlbum);
            this.Controls.Add(this.labelCurrentArtist);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Name = "frmMain";
            this.Text = "XM Sharp";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picCurrentIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelCurrentSong;
		private System.Windows.Forms.Label labelCurrentAlbum;
		private System.Windows.Forms.Label labelCurrentArtist;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox picCurrentIcon;
		private System.Windows.Forms.ListBox listItems;
		private System.Windows.Forms.Timer timerUpdateAll;
		private System.Windows.Forms.Timer timerUpdateCurrent;
        private System.Windows.Forms.Button buttonStop;
	}
}

