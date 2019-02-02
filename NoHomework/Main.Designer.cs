namespace NoHomework
{
    partial class Main
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
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.Chinese = new System.Windows.Forms.RadioButton();
            this.Math = new System.Windows.Forms.RadioButton();
            this.English = new System.Windows.Forms.RadioButton();
            this.Politics = new System.Windows.Forms.RadioButton();
            this.History = new System.Windows.Forms.RadioButton();
            this.Physics = new System.Windows.Forms.RadioButton();
            this.Chemical = new System.Windows.Forms.RadioButton();
            this.Biology = new System.Windows.Forms.RadioButton();
            this.Geography = new System.Windows.Forms.RadioButton();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.listBox1);
            this.groupBox1.Location = new System.Drawing.Point(14, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(390, 404);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "作业";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(309, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "查看";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(6, 20);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(297, 376);
            this.listBox1.TabIndex = 0;
            // 
            // Chinese
            // 
            this.Chinese.AutoSize = true;
            this.Chinese.Location = new System.Drawing.Point(410, 34);
            this.Chinese.Name = "Chinese";
            this.Chinese.Size = new System.Drawing.Size(47, 16);
            this.Chinese.TabIndex = 2;
            this.Chinese.TabStop = true;
            this.Chinese.Text = "语文";
            this.Chinese.UseVisualStyleBackColor = true;
            this.Chinese.CheckedChanged += new System.EventHandler(this.Subject_Changed);
            // 
            // Math
            // 
            this.Math.AutoSize = true;
            this.Math.Location = new System.Drawing.Point(410, 56);
            this.Math.Name = "Math";
            this.Math.Size = new System.Drawing.Size(47, 16);
            this.Math.TabIndex = 3;
            this.Math.TabStop = true;
            this.Math.Text = "数学";
            this.Math.UseVisualStyleBackColor = true;
            this.Math.CheckedChanged += new System.EventHandler(this.Subject_Changed);
            // 
            // English
            // 
            this.English.AutoSize = true;
            this.English.Location = new System.Drawing.Point(410, 78);
            this.English.Name = "English";
            this.English.Size = new System.Drawing.Size(47, 16);
            this.English.TabIndex = 4;
            this.English.TabStop = true;
            this.English.Text = "英语";
            this.English.UseVisualStyleBackColor = true;
            this.English.CheckedChanged += new System.EventHandler(this.Subject_Changed);
            // 
            // Politics
            // 
            this.Politics.AutoSize = true;
            this.Politics.Location = new System.Drawing.Point(410, 100);
            this.Politics.Name = "Politics";
            this.Politics.Size = new System.Drawing.Size(47, 16);
            this.Politics.TabIndex = 5;
            this.Politics.TabStop = true;
            this.Politics.Text = "政治";
            this.Politics.UseVisualStyleBackColor = true;
            this.Politics.CheckedChanged += new System.EventHandler(this.Subject_Changed);
            // 
            // History
            // 
            this.History.AutoSize = true;
            this.History.Location = new System.Drawing.Point(410, 122);
            this.History.Name = "History";
            this.History.Size = new System.Drawing.Size(47, 16);
            this.History.TabIndex = 6;
            this.History.Text = "历史";
            this.History.UseVisualStyleBackColor = true;
            this.History.CheckedChanged += new System.EventHandler(this.Subject_Changed);
            // 
            // Physics
            // 
            this.Physics.AutoSize = true;
            this.Physics.Location = new System.Drawing.Point(410, 144);
            this.Physics.Name = "Physics";
            this.Physics.Size = new System.Drawing.Size(47, 16);
            this.Physics.TabIndex = 7;
            this.Physics.Text = "物理";
            this.Physics.UseVisualStyleBackColor = true;
            this.Physics.CheckedChanged += new System.EventHandler(this.Subject_Changed);
            // 
            // Chemical
            // 
            this.Chemical.AutoSize = true;
            this.Chemical.Location = new System.Drawing.Point(410, 166);
            this.Chemical.Name = "Chemical";
            this.Chemical.Size = new System.Drawing.Size(47, 16);
            this.Chemical.TabIndex = 8;
            this.Chemical.TabStop = true;
            this.Chemical.Text = "化学";
            this.Chemical.UseVisualStyleBackColor = true;
            this.Chemical.CheckedChanged += new System.EventHandler(this.Subject_Changed);
            // 
            // Biology
            // 
            this.Biology.AutoSize = true;
            this.Biology.Location = new System.Drawing.Point(410, 188);
            this.Biology.Name = "Biology";
            this.Biology.Size = new System.Drawing.Size(47, 16);
            this.Biology.TabIndex = 9;
            this.Biology.TabStop = true;
            this.Biology.Text = "生物";
            this.Biology.UseVisualStyleBackColor = true;
            this.Biology.CheckedChanged += new System.EventHandler(this.Subject_Changed);
            // 
            // Geography
            // 
            this.Geography.AutoSize = true;
            this.Geography.Location = new System.Drawing.Point(410, 210);
            this.Geography.Name = "Geography";
            this.Geography.Size = new System.Drawing.Size(47, 16);
            this.Geography.TabIndex = 10;
            this.Geography.TabStop = true;
            this.Geography.Text = "地理";
            this.Geography.UseVisualStyleBackColor = true;
            this.Geography.CheckedChanged += new System.EventHandler(this.Subject_Changed);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(309, 49);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 143);
            this.button2.TabIndex = 2;
            this.button2.Text = "做题";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Geography);
            this.Controls.Add(this.Biology);
            this.Controls.Add(this.Chemical);
            this.Controls.Add(this.Physics);
            this.Controls.Add(this.History);
            this.Controls.Add(this.Politics);
            this.Controls.Add(this.English);
            this.Controls.Add(this.Math);
            this.Controls.Add(this.Chinese);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Name = "Main";
            this.Text = "Main";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton Chinese;
        private System.Windows.Forms.RadioButton Math;
        private System.Windows.Forms.RadioButton English;
        private System.Windows.Forms.RadioButton Politics;
        private System.Windows.Forms.RadioButton History;
        private System.Windows.Forms.RadioButton Chemical;
        private System.Windows.Forms.RadioButton Biology;
        private System.Windows.Forms.RadioButton Geography;
        private System.Windows.Forms.RadioButton Physics;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button2;
    }
}