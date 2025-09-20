namespace QueryStringView
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            textBox1 = new TextBox();
            listView1 = new SuperListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
            contextMenuStrip1 = new ContextMenuStrip(components);
            复制ToolStripMenuItem = new ToolStripMenuItem();
            修改ToolStripMenuItem = new ToolStripMenuItem();
            titleContextMenuStrip = new ContextMenuStrip(components);
            修改窗口标题ToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripSeparator();
            窗口置顶ToolStripMenuItem = new ToolStripMenuItem();
            saveFileDialog1 = new SaveFileDialog();
            toolStripMenuItem2 = new ToolStripSeparator();
            保存ToolStripMenuItem = new ToolStripMenuItem();
            另存为ToolStripMenuItem = new ToolStripMenuItem();
            新窗口ToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem3 = new ToolStripSeparator();
            toolStripMenuItem4 = new ToolStripSeparator();
            退出ToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem5 = new ToolStripSeparator();
            打开ToolStripMenuItem = new ToolStripMenuItem();
            contextMenuStrip1.SuspendLayout();
            titleContextMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.Dock = DockStyle.Top;
            textBox1.Location = new Point(0, 0);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.Size = new Size(567, 180);
            textBox1.TabIndex = 0;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // listView1
            // 
            listView1.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader4, columnHeader5 });
            listView1.ContextMenuStrip = contextMenuStrip1;
            listView1.Dock = DockStyle.Fill;
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            listView1.Location = new Point(0, 180);
            listView1.Name = "listView1";
            listView1.OwnerDraw = true;
            listView1.SavePath = null;
            listView1.Size = new Size(567, 504);
            listView1.TabIndex = 1;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            listView1.MouseMove += listView1_MouseMove;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "";
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "键";
            columnHeader2.TextAlign = HorizontalAlignment.Center;
            columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "值";
            columnHeader3.TextAlign = HorizontalAlignment.Center;
            columnHeader3.Width = 200;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "键长度";
            // 
            // columnHeader5
            // 
            columnHeader5.Text = "值长度";
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.BackgroundImageLayout = ImageLayout.None;
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { 复制ToolStripMenuItem, 修改ToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.RenderMode = ToolStripRenderMode.Professional;
            contextMenuStrip1.ShowImageMargin = false;
            contextMenuStrip1.Size = new Size(88, 48);
            contextMenuStrip1.Opening += contextMenuStrip1_Opening;
            // 
            // 复制ToolStripMenuItem
            // 
            复制ToolStripMenuItem.Name = "复制ToolStripMenuItem";
            复制ToolStripMenuItem.Size = new Size(87, 22);
            复制ToolStripMenuItem.Text = "复制键";
            复制ToolStripMenuItem.Click += 复制键ToolStripMenuItem_Click;
            // 
            // 修改ToolStripMenuItem
            // 
            修改ToolStripMenuItem.Name = "修改ToolStripMenuItem";
            修改ToolStripMenuItem.Size = new Size(87, 22);
            修改ToolStripMenuItem.Text = "修改";
            修改ToolStripMenuItem.Click += 修改ToolStripMenuItem_Click;
            // 
            // titleContextMenuStrip
            // 
            titleContextMenuStrip.Items.AddRange(new ToolStripItem[] { 修改窗口标题ToolStripMenuItem, toolStripMenuItem1, 窗口置顶ToolStripMenuItem, toolStripMenuItem5, 打开ToolStripMenuItem, toolStripMenuItem3, 新窗口ToolStripMenuItem, toolStripMenuItem2, 保存ToolStripMenuItem, 另存为ToolStripMenuItem, toolStripMenuItem4, 退出ToolStripMenuItem });
            titleContextMenuStrip.Name = "titleContextMenuStrip";
            titleContextMenuStrip.ShowImageMargin = false;
            titleContextMenuStrip.Size = new Size(156, 210);
            // 
            // 修改窗口标题ToolStripMenuItem
            // 
            修改窗口标题ToolStripMenuItem.Name = "修改窗口标题ToolStripMenuItem";
            修改窗口标题ToolStripMenuItem.Size = new Size(155, 22);
            修改窗口标题ToolStripMenuItem.Text = "修改窗口标题";
            修改窗口标题ToolStripMenuItem.Click += 修改窗口标题ToolStripMenuItem_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(152, 6);
            // 
            // 窗口置顶ToolStripMenuItem
            // 
            窗口置顶ToolStripMenuItem.Name = "窗口置顶ToolStripMenuItem";
            窗口置顶ToolStripMenuItem.Size = new Size(155, 22);
            窗口置顶ToolStripMenuItem.Text = "窗口置顶";
            窗口置顶ToolStripMenuItem.Click += 窗口置顶ToolStripMenuItem_Click;
            // 
            // saveFileDialog1
            // 
            saveFileDialog1.DefaultExt = "Qsv";
            saveFileDialog1.Filter = "QueryStringView 文件|*.qsv";
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(152, 6);
            // 
            // 保存ToolStripMenuItem
            // 
            保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            保存ToolStripMenuItem.Size = new Size(155, 22);
            保存ToolStripMenuItem.Text = "保存";
            保存ToolStripMenuItem.Click += 保存ToolStripMenuItem_Click;
            // 
            // 另存为ToolStripMenuItem
            // 
            另存为ToolStripMenuItem.Name = "另存为ToolStripMenuItem";
            另存为ToolStripMenuItem.Size = new Size(155, 22);
            另存为ToolStripMenuItem.Text = "另存为";
            另存为ToolStripMenuItem.Click += 另存为ToolStripMenuItem_Click;
            // 
            // 新窗口ToolStripMenuItem
            // 
            新窗口ToolStripMenuItem.Name = "新窗口ToolStripMenuItem";
            新窗口ToolStripMenuItem.Size = new Size(155, 22);
            新窗口ToolStripMenuItem.Text = "新窗口";
            新窗口ToolStripMenuItem.ToolTipText = "打开一个新窗口";
            新窗口ToolStripMenuItem.Click += 新窗口ToolStripMenuItem_Click;
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new Size(152, 6);
            // 
            // toolStripMenuItem4
            // 
            toolStripMenuItem4.Name = "toolStripMenuItem4";
            toolStripMenuItem4.Size = new Size(152, 6);
            // 
            // 退出ToolStripMenuItem
            // 
            退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            退出ToolStripMenuItem.Size = new Size(155, 22);
            退出ToolStripMenuItem.Text = "退出";
            退出ToolStripMenuItem.Click += 退出ToolStripMenuItem_Click;
            // 
            // toolStripMenuItem5
            // 
            toolStripMenuItem5.Name = "toolStripMenuItem5";
            toolStripMenuItem5.Size = new Size(152, 6);
            // 
            // 打开ToolStripMenuItem
            // 
            打开ToolStripMenuItem.Name = "打开ToolStripMenuItem";
            打开ToolStripMenuItem.Size = new Size(155, 22);
            打开ToolStripMenuItem.Text = "打开";
            打开ToolStripMenuItem.ToolTipText = "打开一个QSV文件";
            打开ToolStripMenuItem.Click += 打开ToolStripMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(567, 684);
            ContextMenuStrip = titleContextMenuStrip;
            Controls.Add(listView1);
            Controls.Add(textBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "QueryStringView";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            contextMenuStrip1.ResumeLayout(false);
            titleContextMenuStrip.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private SuperListView listView1;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem 复制ToolStripMenuItem;
        private ToolStripMenuItem 修改ToolStripMenuItem;
        private ContextMenuStrip titleContextMenuStrip;
        private ToolStripMenuItem 修改窗口标题ToolStripMenuItem;
        private SaveFileDialog saveFileDialog1;
        private ToolStripMenuItem 窗口置顶ToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripSeparator toolStripMenuItem5;
        private ToolStripMenuItem 打开ToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem3;
        private ToolStripMenuItem 新窗口ToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem 保存ToolStripMenuItem;
        private ToolStripMenuItem 另存为ToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem4;
        private ToolStripMenuItem 退出ToolStripMenuItem;
    }
}
