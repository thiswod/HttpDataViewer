using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QueryStringView
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // 检查并注册文件关联
            if (!RegistryHelper.IsFileAssociationRegistered())
            {
                RegistryHelper.RegisterFileAssociation();
            }
            
            // 创建主窗口实例
            Form1 mainForm = new Form1();
            
            // 检查是否有命令行参数（双击文件打开时会有文件路径参数）
            if (args.Length > 0)
            {
                // 使用SetFileToLoad方法设置要加载的文件路径，而不是直接加载
                // 实际加载会在Form1_Load事件中进行，确保窗口完全初始化
                mainForm.SetFileToLoad(args[0]);
            }
            
            // 启动应用程序消息循环
            Application.Run(mainForm);
        }
    }
}