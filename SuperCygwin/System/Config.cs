﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using System.ComponentModel;

namespace SuperCygwin
{
    class Config
    {
        public static Config Main;

        private bool _devBuilds = false;
        [Browsable(true)]
        public bool DevBuilds
        {
            get { return _devBuilds; }
            set { _devBuilds = value; }
        }

        private bool _autoHidePresets = false;
        [Browsable(true)]
        public bool AutoHidepresets
        {
            get { return _autoHidePresets; }
            set { _autoHidePresets = value; }
        }

        private string _cygPath = @"C:\cygwin\";
        [EditorAttribute(typeof(System.Windows.Forms.Design.FolderNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string CygwinPath
        {
            get
            {
                return _cygPath;
            }
            set
            {
                _cygPath = value;
                if (!_cygPath.EndsWith(@"\"))
                    _cygPath += @"\";
            }
        }

        private string _minPath = @"C:\cygwin\bin\mintty.exe";
        [EditorAttribute(typeof(System.Windows.Forms.Design.FileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string MinTTYPath
        {
            get
            {
                return _minPath;
            }
            set
            {
                _minPath = value;
            }
        }

        private string _telPath = @"/usr/bin/telnet";
        public string TelnetPath
        {
            get
            {
                return _telPath;
            }
            set
            {
                _telPath = value;
            }
        }

        private string _sshPath = @"/usr/bin/ssh";
        public string SSHPath
        {
            get
            {
                return _sshPath;
            }
            set
            {
                _sshPath = value;
            }
        }

        public Config()
        {
            Config.Main = this;
        }

        public static Config Load()
        {
            if (File.Exists("config.json"))
                return JsonConvert.DeserializeObject<Config>(File.ReadAllText("config.json"));
            else
                return new Config();
        }

        public void Save()
        {
            File.WriteAllText("config.json", JsonConvert.SerializeObject(this, Formatting.Indented));
        }
    }
}
