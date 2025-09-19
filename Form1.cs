using System.Net;
using System.Windows.Forms;
using System.Threading;
using static System.Windows.Forms.ListView;
using System.Text.Json;
using System.IO;
using System;

namespace QueryStringView
{
    public partial class Form1 : Form
    {
        // 添加用于取消前一个线程的CancellationTokenSource
        private CancellationTokenSource _cancellationTokenSource;
        // 标记是否是通过双击文件打开的程序
        private bool _isFileLaunch = false;
        // 存储通过双击打开的文件路径
        private string _launchedFilePath = null;
        // 存储当前打开/保存的文件路径
        private string _currentFilePath = null;
        // 标记内容是否被修改
        private bool _isContentModified = false;
        // 临时标记，用于抑制文件加载时的修改标记更新
        private bool _suppressContentModified = false;

        public Form1()
        {
            InitializeComponent();
            _cancellationTokenSource = new CancellationTokenSource();
            // 初始化保存对话框
            saveFileDialog1.Filter = "查询字符串文件 (*.qsv)|*.qsv|所有文件 (*.*)|*.*";
            saveFileDialog1.DefaultExt = "qsv";
        }

        // 用于通过命令行参数打开文件的公共方法
        public void SetFileToLoad(string filePath)
        {
            if (File.Exists(filePath))
            {
                _isFileLaunch = true;
                _launchedFilePath = filePath;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 窗口加载时的初始化代码
            // 如果是通过双击文件打开的程序，则在Form加载完成后再加载文件
            if (_isFileLaunch && !string.IsNullOrEmpty(_launchedFilePath))
            {
                SafeLoadFile(_launchedFilePath);
                // 重置标记
                _isFileLaunch = false;
                _launchedFilePath = null;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // 只有在未抑制修改标记的情况下才设置修改标志
            if (!_suppressContentModified)
            {
                _isContentModified = true;
            }
            
            // 取消前一个可能还在运行的任务
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = new CancellationTokenSource();

            // 创建新的任务
            CancellationToken token = _cancellationTokenSource.Token;
            Thread thread = new Thread(() => ShowView(token));
            thread.IsBackground = true;
            thread.Start();
        }
        void ShowView(CancellationToken cancellationToken)
        {
            // 首先检查是否已取消
            if (cancellationToken.IsCancellationRequested)
                return;

            string Qeurystring = ""; 
            this.Invoke(() =>
            {
                Qeurystring = textBox1.Text;
            });

            // 再次检查是否已取消
            if (cancellationToken.IsCancellationRequested)
                return;

            Dictionary<string, string> keyValuePairs = QueryStringToDictionary(Qeurystring);

            // 再次检查是否已取消
            if (cancellationToken.IsCancellationRequested)
                return;

            // 使用try-finally确保无论如何都会调用EndUpdate()
            this.Invoke(() =>
            {
                listView1.Items.Clear();
                listView1.BeginUpdate();
            });

            try
            {
                // 循环中检查取消
                foreach (var pair in keyValuePairs)
                {
                    if (cancellationToken.IsCancellationRequested)
                        break;

                    this.Invoke(() =>
                    {
                        if (!cancellationToken.IsCancellationRequested)
                        {
                            listView1.Items.Add(new ListViewItem(new[] { $"{listView1.Items.Count + 1}", pair.Key, pair.Value, $"{pair.Key.Length}", $"{pair.Value.Length}" }));
                        }
                    });
                }
            }
            finally
            {
                // 确保更新UI，无论是否取消操作
                this.Invoke(() =>
                {
                    listView1.EndUpdate();
                });
            }
        }
        /// <summary>
        /// 将查询字符串转换为字典
        /// </summary>
        /// <param name="queryString">查询字符串，格式为key1=value1&key2=value2</param>
        /// <returns>包含键值对的字典</returns>
        public static Dictionary<string, string> QueryStringToDictionary(string queryString)
        {
            var parameters = new Dictionary<string, string>();

            if (string.IsNullOrWhiteSpace(queryString))
                return parameters;

            // 移除可能的问号前缀
            if (queryString.StartsWith('?'))
                queryString = queryString.Substring(1);

            var keyValuePairs = queryString.Split('&', StringSplitOptions.RemoveEmptyEntries);

            foreach (var pair in keyValuePairs)
            {
                var kv = pair.Split('=', 2);
                if (kv.Length == 0) continue;

                string key = kv[0];
                string value = kv.Length > 1 ? kv[1] : "";

                // URL解码值
                value = WebUtility.UrlDecode(value);

                // 如果键已存在，则覆盖
                if (parameters.ContainsKey(key))
                    parameters[key] = value;
                else
                    parameters.Add(key, value);
            }

            return parameters;
        }

        // 重写WndProc方法捕获窗口标题栏右键点击事件
        protected override void WndProc(ref Message m)
        {
            const int WM_NCRBUTTONDOWN = 0x00A4; // 非客户区右键按下
            const int HTCAPTION = 0x0002; // 标题栏区域

            if (m.Msg == WM_NCRBUTTONDOWN && m.WParam.ToInt32() == HTCAPTION)
            {
                // 显示自定义右键菜单
                titleContextMenuStrip.Show(Cursor.Position);
                return;
            }

            base.WndProc(ref m);
        }

        private void 修改窗口标题ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 使用现有的InputDialog类来让用户输入新的窗口标题
            using (var inputDialog = new InputDialog("修改窗口标题", "输入新的窗口标题："))
            {
                // 设置默认值为当前窗口标题
                inputDialog.Controls.OfType<TextBox>().FirstOrDefault()!.Text = this.Text;
                inputDialog.Controls.OfType<TextBox>().FirstOrDefault()!.SelectAll();
                inputDialog.StartPosition = FormStartPosition.CenterParent;

                // 显示对话框并处理结果
                if (inputDialog.ShowDialog(this) == DialogResult.OK)
                {
                    string newTitle = inputDialog.InputText.Trim();
                    if (!string.IsNullOrEmpty(newTitle))
                    {
                        this.Text = newTitle;
                        // 设置内容已修改标志
                        _isContentModified = true;
                    }
                }
            }
        }

        private void 复制键ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 直接使用缓存的单元格文本
            if (!string.IsNullOrEmpty(_currentCellText))
            {
                Clipboard.SetText(_currentCellText);
            }
        }
        /// <summary>
        /// 存储鼠标悬停的当前单元格信息
        /// </summary>
        public string _currentCellText = "";
        public int _currentColumnIndex = -1;
        public int _currentRowIndex = -1;
        private void listView1_MouseMove(object sender, MouseEventArgs e)
        {
            // 使用HitTest获取鼠标位置的详细信息
            ListViewHitTestInfo hitTest = listView1.HitTest(e.Location);

            if (hitTest.Item != null && hitTest.SubItem != null)
            {
                // 更新当前单元格信息
                _currentRowIndex = hitTest.Item.Index;
                _currentColumnIndex = hitTest.Item.SubItems.IndexOf(hitTest.SubItem);
                _currentCellText = hitTest.SubItem.Text;
            }
            else
            {
                // 鼠标不在单元格上时重置
                _currentCellText = "";
                _currentColumnIndex = -1;
                _currentRowIndex = -1;
            }
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_currentColumnIndex >= 0 &&
                    _currentColumnIndex < listView1.Columns.Count &&
                    !string.IsNullOrEmpty(_currentCellText))
            {
                // 直接使用ListView的列信息获取列名
                string columnName = listView1.Columns[_currentColumnIndex].Text;

                // 截断长文本（根据UI限制长度）
                string displayText = _currentCellText;
                if (displayText.Length > 20)
                {
                    displayText = displayText.Substring(0, 17) + "...";
                }

                // 更新菜单文本（符合格式）
                复制ToolStripMenuItem.Text = $"复制 [{columnName}]：{displayText}";
                复制ToolStripMenuItem.Visible = true;
                修改ToolStripMenuItem.Text = $"修改 [{columnName}]：{displayText}";
                修改ToolStripMenuItem.Visible = true;


            }
            else
            {
                复制ToolStripMenuItem.Visible = false;
            }
        }

        private void 修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (InputDialog inputDialog = new InputDialog("修改", "请输入新的值："))
            {
                if (inputDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = textBox1.Text.Replace(_currentCellText, inputDialog.InputText);
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 只有当内容被修改时才提示保存
            if (_isContentModified)
            {
                DialogResult result = MessageBox.Show("内容已修改，是否保存？", "提示", MessageBoxButtons.YesNoCancel);
                
                if (result == DialogResult.Yes)
                {
                    // 如果已有保存路径，直接保存；否则显示保存对话框
                    if (!string.IsNullOrEmpty(_currentFilePath))
                    {
                        SaveToFile(_currentFilePath);
                    }
                    else
                    {
                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            SaveToFile(saveFileDialog1.FileName);
                        }
                        else
                        {
                            // 用户取消了保存对话框，取消关闭操作
                            e.Cancel = true;
                        }
                    }
                }
                else if (result == DialogResult.Cancel)
                {
                    // 用户取消了关闭操作
                    e.Cancel = true;
                }
                // 如果选择No，则不保存直接关闭
            }
        }

        /// <summary>
        /// 安全地从文件加载内容（在Form完全加载后调用）
        /// </summary>
        /// <param name="filePath">文件路径</param>
        private void SafeLoadFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string jsonContent = File.ReadAllText(filePath);
                    var fileData = JsonSerializer.Deserialize<FileContent>(jsonContent);
                    
                    if (fileData != null)
                    {
                        // 直接更新UI，因为现在已经在UI线程上
                        this.Text = fileData.title ?? "QueryStringView";
                        textBox1.Text = fileData.queryString ?? "";
                        
                        // 更新当前文件路径和重置修改标志
                        _currentFilePath = filePath;
                        _isContentModified = false;
                        
                        // 设置抑制标志，防止触发textBox1_TextChanged时更新修改标志
                        _suppressContentModified = true;
                        try
                        {
                            // 触发文本更改事件以更新列表视图
                            if (textBox1 != null)
                            {
                                textBox1_TextChanged(this, EventArgs.Empty);
                            }
                        }
                        finally
                        {
                            // 确保在执行完后重置抑制标志
                            _suppressContentModified = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // 直接显示错误消息，因为现在已经在UI线程上
                MessageBox.Show("加载文件失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 保存内容到指定文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        private void SaveToFile(string filePath)
        {
            try
            {
                var FileContent = new { 
                    title = this.Text,
                    queryString = textBox1.Text,
                };
                string TextJson = JsonSerializer.Serialize(FileContent);
                File.WriteAllText(filePath, TextJson);
                
                // 更新当前文件路径和重置修改标志
                _currentFilePath = filePath;
                _isContentModified = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存文件失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 用于反序列化文件内容的类
        private class FileContent
        {
            public string? title { get; set; }
            public string? queryString { get; set; }
        }
    }
}
