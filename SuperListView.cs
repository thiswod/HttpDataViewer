using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ListView = System.Windows.Forms.ListView;

public class SuperListView : ListView
{
    public string SavePath { get; set; }
    public SuperListView()
    {
        // 优化双缓冲设置
        this.DoubleBuffered = true;
        SetStyle(ControlStyles.OptimizedDoubleBuffer |
                 ControlStyles.AllPaintingInWmPaint, true);
        // 简化OwnerDraw设置
        this.OwnerDraw = true;
        this.DrawItem += (s, e) => e.DrawDefault = true;
        this.DrawSubItem += (s, e) => e.DrawDefault = true;
        this.DrawColumnHeader += (s, e) => e.DrawDefault = true;
        this.View = View.Details;//固定为详情列表视图
                                 //显示网格线
        this.GridLines = true;
        //整行选择 
        this.FullRowSelect = true;
    }
}