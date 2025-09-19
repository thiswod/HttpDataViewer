using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace QueryStringView
{
    public static class RegistryHelper
    {
        // 文件扩展名和描述
        private const string FileExtension = ".qsv";
        private const string FileDescription = "QueryStringView 文件";
        private const string ProgId = "QueryStringView.Document";

        /// <summary>
        /// 注册文件类型关联（同时注册大小写两种扩展名）
        /// </summary>
        public static void RegisterFileAssociation()
        {
            try
            {
                // 使用GetCurrentProcess().MainModule.FileName获取正确的可执行文件路径
                string exePath = Process.GetCurrentProcess().MainModule.FileName;

                // 创建 ProgId
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey("Software\\Classes\\" + ProgId))
                {
                    key.SetValue(null, FileDescription);
                    
                    // 设置默认图标
                    using (RegistryKey defaultIconKey = key.CreateSubKey("DefaultIcon"))
                    {
                        defaultIconKey.SetValue(null, exePath + ",0");
                    }

                    // 设置打开命令
                    using (RegistryKey shellKey = key.CreateSubKey("shell"))
                    using (RegistryKey openKey = shellKey.CreateSubKey("open"))
                    using (RegistryKey commandKey = openKey.CreateSubKey("command"))
                    {
                        commandKey.SetValue(null, "\"" + exePath + "\" \"%1\"");
                    }
                }

                // 关联小写文件扩展名
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey("Software\\Classes\\" + FileExtension))
                {
                    key.SetValue(null, ProgId);
                }
                
                // 关联大写文件扩展名（为了兼容性）
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey("Software\\Classes\\.QSV"))
                {
                    key.SetValue(null, ProgId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("注册文件关联失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 检查文件类型是否已关联，并且路径是否正确
        /// </summary>
        /// <returns>是否已正确关联</returns>
        public static bool IsFileAssociationRegistered()
        {
            try
            {
                // 检查小写扩展名
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Classes\\" + FileExtension))
                {
                    if (key != null)
                    {
                        object valueObj = key.GetValue(null);
                        if (valueObj != null && valueObj.ToString().Equals(ProgId))
                        {
                            // 检查路径是否正确
                            if (IsRegistryPathCorrect())
                            {
                                return true;
                            }
                        }
                    }
                }
                
                // 检查大写扩展名
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Classes\\.QSV"))
                {
                    if (key != null)
                    {
                        object valueObj = key.GetValue(null);
                        if (valueObj != null && valueObj.ToString().Equals(ProgId))
                        {
                            // 检查路径是否正确
                            if (IsRegistryPathCorrect())
                            {
                                return true;
                            }
                        }
                    }
                }
                
                return false;
            }
            catch
            {
                return false;
            }
        }
        
        /// <summary>
        /// 检查注册表中的可执行文件路径是否正确
        /// </summary>
        /// <returns>路径是否正确</returns>
        private static bool IsRegistryPathCorrect()
        {
            try
            {
                string currentExePath = Process.GetCurrentProcess().MainModule.FileName;
                
                // 获取注册表中的命令路径
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Classes\\" + ProgId + "\\shell\\open\\command"))
                {
                    if (key != null)
                    {
                        object valueObj = key.GetValue(null);
                        if (valueObj != null)
                        {
                            string registryCommand = valueObj.ToString();
                            // 移除引号和参数，获取可执行文件路径
                            string registryExePath = registryCommand.Replace("\"", "").Split(' ')[0];
                            
                            // 检查路径是否匹配
                            return string.Equals(registryExePath, currentExePath, System.StringComparison.OrdinalIgnoreCase);
                        }
                    }
                }
                
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}