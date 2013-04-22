﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Xml.Linq;
using System.IO;
using SuperCygwin.Forms.Presets;

namespace SuperCygwin.Forms
{
    public partial class DebugForm : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public DebugForm()
        {
            InitializeComponent();
        }

        private void DebugForm_Load(object sender, EventArgs e)
        {
            Timer t = new Timer();
            t.Interval = 1000;
            t.Enabled = true;
            t.Tick += new EventHandler((se, ev) =>
            {
                t.Enabled = false;
                t.Dispose();
                ProcessContainer pc = new ProcessContainer(Program.Config.MinTTYPath, "-");
                pc.TopLevel = false;
                pc.Parent = tabPage2;
                tabPage2.Controls.Add(pc);
                //pc.WindowState = FormWindowState.Maximized;
                pc.Dock = DockStyle.Fill;
                pc.FormBorderStyle = FormBorderStyle.None;
                pc.Show();
            });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            checkedListBox1.Items.Clear();
            string[] names=new string[]{"bash","ssh","mintty"};
            foreach(string name in names)
                foreach (Process p in Process.GetProcessesByName(name))
                {
                    //if (Native.GetParent(p.MainWindowHandle).ToInt32() == 0)
                        checkedListBox1.Items.Add(string.Format(
                            "{0} {1} {3} {2}",
                            p.ProcessName,
                            Native.GetAncestor(p.MainWindowHandle,GetAncestorFlags.GetRoot),
                            p.MainWindowHandle,
                            p.Id),
                            Native.GetParent(p.MainWindowHandle).ToString() != "0");
                }
        }
    }
}
