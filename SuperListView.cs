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
    
    // 当前排序的列索引
    private int _sortColumn = -1;
    // 当前排序方向
    private SortOrder _sortOrder = SortOrder.None;
    
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
        
        // 添加列点击事件处理
        this.ColumnClick += SuperListView_ColumnClick;
    }
    
    private void SuperListView_ColumnClick(object sender, ColumnClickEventArgs e)
    {
        // 切换排序方向或重置
        if (e.Column == _sortColumn)
        {
            // 点击同一列，切换排序方向
            _sortOrder = _sortOrder == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
        }
        else
        {
            // 点击新列，重置排序状态
            _sortColumn = e.Column;
            _sortOrder = SortOrder.Ascending;
        }
        
        // 执行排序
        this.ListViewItemSorter = new ListViewItemComparer(_sortColumn, _sortOrder);
        
        // 更新列头显示（可选）
        this.Sort();
    }
    
    // 自定义比较器类
    private class ListViewItemComparer : System.Collections.IComparer
    {
        private int _column;
        private SortOrder _order;
        
        public ListViewItemComparer(int column, SortOrder order)
        {
            _column = column;
            _order = order;
        }
        
        public int Compare(object x, object y)
        {
            var itemX = x as ListViewItem;
            var itemY = y as ListViewItem;
            
            if (itemX == null || itemY == null)
                return 0;
            
            // 确保索引有效
            if (_column >= itemX.SubItems.Count || _column >= itemY.SubItems.Count)
                return 0;
            
            string textX = itemX.SubItems[_column].Text;
            string textY = itemY.SubItems[_column].Text;
            
            // 尝试按数值排序（适用于序号和长度列）
            if (_column == 0 || _column == 3 || _column == 4) // 序号、键长度、值长度列
            {
                if (int.TryParse(textX, out int numX) && int.TryParse(textY, out int numY))
                {
                    return _order == SortOrder.Ascending ? numX.CompareTo(numY) : numY.CompareTo(numX);
                }
            }
            
            // 按字符串排序（适用于键和值列）
            int result = string.Compare(textX, textY, StringComparison.Ordinal);
            return _order == SortOrder.Ascending ? result : -result;
        }
    }
    
    // 清除排序状态的方法
    public void ClearSort()
    {
        _sortColumn = -1;
        _sortOrder = SortOrder.None;
        this.ListViewItemSorter = null;
    }
}