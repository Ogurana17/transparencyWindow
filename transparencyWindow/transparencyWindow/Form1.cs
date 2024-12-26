using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Drawing;

namespace TransparencyWindow
{
    public partial class MainForm : Form
    {
        private const int HOTKEY_ID = 1;
        private byte loadTransparency = 224; // デフォルト値: 半透明
        private byte currentTransparency = 224; // デフォルト値: 半透明
        private string loadHotkey = "Win+F2"; // 初期値: ショートカットキー
        private byte nowTransparency = 224;
        private string nowHotkey = "Win+F2";

        public MainForm()
        {
            LoadSettings();
            InitializeComponent_();
            IntPtr hwnd = WinAPI.GetForegroundWindow();
            //currentTransparency = loadTransparency;
            //SetWindowTransparency(hwnd, currentTransparency); // 起動時に透明度を適用
        }

        // WinAPI
        public class WinAPI
        {
            [DllImport("user32.dll", SetLastError = true)]
            public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

            [DllImport("user32.dll", SetLastError = true)]
            public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

            [DllImport("user32.dll", SetLastError = true)]
            public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

            [DllImport("user32.dll")]
            public static extern IntPtr GetForegroundWindow();

            [DllImport("user32.dll", SetLastError = true)]
            public static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

            [DllImport("user32.dll", SetLastError = true)]
            public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

            [DllImport("user32.dll", SetLastError = true)]
            public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

            public const uint LWA_ALPHA = 0x02;
            public const int GWL_EXSTYLE = -20;
            public const int WS_EX_LAYERED = 0x80000;
        }

        // ウィンドウ透明度設定
        public static void SetWindowTransparency(IntPtr hwnd, byte transparency)
        {
            int exStyle = WinAPI.GetWindowLong(hwnd, WinAPI.GWL_EXSTYLE);
            WinAPI.SetWindowLong(hwnd, WinAPI.GWL_EXSTYLE, exStyle | WinAPI.WS_EX_LAYERED);
            WinAPI.SetLayeredWindowAttributes(hwnd, 0, transparency, WinAPI.LWA_ALPHA);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // 閉じるボタンを押してもウィンドウを閉じず、タスクトレイに常駐
            e.Cancel = true;
            this.WindowState = FormWindowState.Minimized;
            this.Hide();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
            else
            {
                // ウィンドウが表示されたらショートカットキーを再登録
                RegisterHotKey(loadHotkey);
            }
        }

        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();

            // ショートカットキーを再登録
            RegisterHotKey(loadHotkey);
        }

        // ショートカットキー登録
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            IntPtr hwnd = WinAPI.GetForegroundWindow();
            InitializeWindowTransparency(hwnd); // 初期化して即時対応

            RegisterHotKey(loadHotkey); // ショートカットキー登録

            // タスクトレイにアイコンを設定
            NotifyIcon notifyIcon = new NotifyIcon
            {
                Icon = SystemIcons.Application,
                Visible = true,
                Text = "TransparencyWindow"
            };

            // タスクトレイのコンテキストメニューを定義
            notifyIcon.ContextMenu = new ContextMenu(new MenuItem[]
            {
        new MenuItem("Exit", (s, ev) => Application.Exit()) // アプリケーションを終了
            });

            // ダブルクリックでウィンドウ表示
            notifyIcon.DoubleClick += (s, ev) =>
            {
                this.ShowInTaskbar = true;
                this.Show();
                this.WindowState = FormWindowState.Normal; // 最小化を解除
            };

            // タスクバーから非表示
            this.ShowInTaskbar = false;
            this.WindowState = FormWindowState.Minimized;
            this.Hide(); // ウィンドウ非表示

            hwnd = WinAPI.GetForegroundWindow();
            InitializeWindowTransparency(hwnd);

            RegisterHotKey(loadHotkey);
        }

        private bool IsWindowInitialized(IntPtr hwnd)
        {
            return initializedWindows.Contains(hwnd);
        }

        private void InitializeWindowTransparency(IntPtr hwnd)
        {
            //SetWindowTransparency(hwnd, currentTransparency); // 初期透明度を適用
            initializedWindows.Add(hwnd); // 初期化済みとしてフラグを設定
        }

        private HashSet<IntPtr> initializedWindows = new HashSet<IntPtr>();

        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;
            if (m.Msg == WM_HOTKEY)
            {
                IntPtr hwnd = WinAPI.GetForegroundWindow();
                ToggleTransparency(hwnd); // 透明化の切り替え
            }
            base.WndProc(ref m);
        }


        private void ToggleTransparency(IntPtr hwnd)
        {
            byte newTransparency = (byte)(currentTransparency == 255 ? loadTransparency : 255); // 切り替え
            SetWindowTransparency(hwnd, newTransparency);
            currentTransparency = newTransparency;
        }


        private void RegisterHotKey(string hotkey)
        {
            uint modifier = 0x0000;
            Keys key = Keys.None;

            string[] keys = hotkey.Split('+');
            foreach (string part in keys)
            {
                switch (part)
                {
                    case "Win":
                        modifier |= 0x0008;
                        break;
                    case "Ctrl":
                        modifier |= 0x0002;
                        break;
                    case "Alt":
                        modifier |= 0x0001;
                        break;
                    case "Shift":
                        modifier |= 0x0004;
                        break;
                    default:
                        key = (Keys)Enum.Parse(typeof(Keys), part);
                        break;
                }
            }

            if (key != Keys.None)
            {
                WinAPI.RegisterHotKey(this.Handle, HOTKEY_ID, modifier, (uint)key);
            }
        }

        // レジストリ登録（スタート時に起動）
        private void RegisterStartup()
        {
            try
            {
                string appPath = Application.ExecutablePath;
                string appName = "TransparencyWindow";

                using (var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true))
                {
                    if (key == null)
                        throw new Exception("Unable to access the registry key.");

                    key.SetValue(appName, $"\"{appPath}\"");
                }

                MessageBox.Show("Startup registration successful.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during startup registration: {ex.Message}");
            }
        }

        private void UnregisterStartup()
        {
            try
            {
                string appName = "TransparencyWindow";

                using (var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true))
                {
                    if (key == null)
                        throw new Exception("Unable to access the registry key.");

                    key.DeleteValue(appName, false);
                }

                MessageBox.Show("Startup unregistration successful.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during startup unregistration: {ex.Message}");
            }
        }


        // INIファイル操作
        public static class IniFile
        {
            public static void WriteValue(string filePath, string key, string value)
            {
                if (!File.Exists(filePath))
                {
                    File.WriteAllText(filePath, "");
                }

                var lines = File.ReadAllLines(filePath);
                bool keyExists = false;

                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].StartsWith(key + "="))
                    {
                        lines[i] = key + "=" + value;
                        keyExists = true;
                        break;
                    }
                }

                if (!keyExists)
                {
                    File.AppendAllText(filePath, key + "=" + value + Environment.NewLine);
                }
                else
                {
                    File.WriteAllLines(filePath, lines);
                }
            }

            public static string ReadValue(string filePath, string key)
            {
                if (!File.Exists(filePath)) return null;
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    if (line.StartsWith(key + "="))
                        return line.Substring(key.Length + 1);
                }
                return null;
            }
        }

        private void SaveSettings()
        {
            IniFile.WriteValue("config.ini", "Transparency", nowTransparency.ToString());
            IniFile.WriteValue("config.ini", "Hotkey", nowHotkey);
        }

        private void LoadSettings()
        {
            string transparencyValue = IniFile.ReadValue("config.ini", "Transparency");
            if (byte.TryParse(transparencyValue, out byte transparency))
            {
                loadTransparency = transparency;
            }

            string hotkeyValue = IniFile.ReadValue("config.ini", "Hotkey");
            if (!string.IsNullOrEmpty(hotkeyValue))
            {
                loadHotkey = hotkeyValue;
            }

            // UIの初期値を更新
            foreach (Control control in this.Controls)
            {
                if (control is TextBox textBox && textBox.Name == "transparencyTextBox")
                {
                    textBox.Text = loadTransparency.ToString();
                }
            }
        }


        // UI要素の初期化
        private void InitializeComponent_()
        {
            this.Text = "TransparencyWindow";
            this.Width = 300;
            this.Height = 200;

            TabControl tabControl1 = new TabControl()
            {
                Dock = DockStyle.Fill
            };

            TabPage tabPage1 = new TabPage("Transparency");
            TabPage tabPage2 = new TabPage("Shortcut");

            // Transparency settings controls
            TrackBar transparencySlider = new TrackBar()
            {
                Minimum = 1,
                Maximum = 254,
                Value = loadTransparency,
                TickFrequency = 11,
                SmallChange = 11,
                LargeChange = 11,
                Dock = DockStyle.Top
            }; 

            TextBox transparencyTextBox = new TextBox()
            {
                Text = loadTransparency.ToString(),
                Dock = DockStyle.Top
            };

            transparencySlider.ValueChanged += (s, e) =>
            {
                transparencyTextBox.Text = transparencySlider.Value.ToString();
                currentTransparency = (byte)transparencySlider.Value;
                IntPtr hwnd = WinAPI.GetForegroundWindow();
                //SetWindowTransparency(hwnd, currentTransparency);
            };

            transparencyTextBox.TextChanged += (s, e) =>
            {
                if (byte.TryParse(transparencyTextBox.Text, out byte transparency) && transparency >= 1 && transparency <= 254)
                {
                    transparencySlider.Value = transparency;
                }
            };

            Label transparencyLabel = new Label()
            {
                Text = "Transparency:",
                Dock = DockStyle.Top
            };

            tabPage1.Controls.Add(transparencySlider);
            tabPage1.Controls.Add(transparencyTextBox);
            tabPage1.Controls.Add(transparencyLabel);

            // Shortcut settings controls
            Label shortcutLabel = new Label()
            {
                Text = "Shortcut Key:",
                Dock = DockStyle.Top
            };

            TextBox shortcutTextBox = new TextBox()
            {
                Text = loadHotkey,
                Dock = DockStyle.Top
            };

            Button saveButton = new Button()
            {
                Text = "Save",
                Dock = DockStyle.Bottom
            };

            saveButton.Click += (s, e) =>
            {
                nowHotkey = shortcutTextBox.Text;
                nowTransparency = (byte)transparencySlider.Value;
                SaveSettings();
                loadHotkey = nowHotkey;
                RegisterHotKey(loadHotkey);
                loadTransparency = nowTransparency;
                //MessageBox.Show("Settings saved successfully.");
            };


            //tabPage2.Controls.Add(shortcutButton);
            tabPage2.Controls.Add(shortcutTextBox);
            tabPage2.Controls.Add(shortcutLabel);

            tabControl1.TabPages.Add(tabPage1);
            tabControl1.TabPages.Add(tabPage2);

            this.Controls.Add(tabControl1);
            this.Controls.Add(saveButton);

            Button registerTaskButton = new Button()
            {
                Text = "Register Task Scheduler",
                Dock = DockStyle.Bottom
            };

            registerTaskButton.Click += (s, e) =>
            {
                RegisterStartup();
            };

            Button unregisterTaskButton = new Button()
            {
                Text = "Unregister Task Scheduler",
                Dock = DockStyle.Bottom
            };

            unregisterTaskButton.Click += (s, e) =>
            {
                UnregisterStartup();
            };

            this.Controls.Add(registerTaskButton);
            this.Controls.Add(unregisterTaskButton);

        }
    }
}
