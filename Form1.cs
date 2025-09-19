using System.Net;
using System.Windows.Forms;
using System.Threading;
using static System.Windows.Forms.ListView;

namespace QueryStringView
{
    public partial class Form1 : Form
    {
        // 添加用于取消前一个线程的CancellationTokenSource
        private CancellationTokenSource _cancellationTokenSource;

        public Form1()
        {
            InitializeComponent();
            _cancellationTokenSource = new CancellationTokenSource();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
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

            this.Invoke(() =>
            {
                listView1.Items.Clear();
                listView1.BeginUpdate();
            });

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

            // 最后检查取消，确保更新UI
            if (!cancellationToken.IsCancellationRequested)
            {
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

        private void Form1_Load(object sender, EventArgs e)
        {

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
                    textBox1.Text = textBox1.Text.Replace(_currentCellText,inputDialog.InputText);
                }
            }
        }
    }
}
