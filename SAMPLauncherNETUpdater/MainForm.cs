﻿using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Windows.Forms;
using System.Net;
using System.IO;

/// <summary>
/// SA:MP launcher .NET updater namespace
/// </summary>
namespace SAMPLauncherNETUpdater
{
    /// <summary>
    /// Main form class
    /// </summary>
    public partial class MainForm : MaterialForm
    {
        /// <summary>
        /// Animation state
        /// </summary>
        private uint animationState = 0U;
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            MaterialSkinManager material_skin_manager = MaterialSkinManager.Instance;
            material_skin_manager.AddFormToManage(this);
            material_skin_manager.Theme = MaterialSkinManager.Themes.DARK;
            material_skin_manager.ColorScheme = new ColorScheme(Primary.Blue700, Primary.Blue800, Primary.Blue500, Accent.LightBlue200, TextShade.WHITE);
            TaskbarProgress.SetState(this, TaskbarProgress.TaskbarStates.Normal);
        }

        /// <summary>
        /// Main form load event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event arguments</param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            UpdaterNET.Update update = new UpdaterNET.Update("https://raw.githubusercontent.com/BigETI/SAMPLauncherNET/master/update.json", Directory.GetCurrentDirectory() + "\\SAMPLauncherNET.exe");
            if (update.IsUpdateAvailable)
                update.InstallUpdates(OnDownloadProgressChanged, OnUpdatesInstalled);
        }

        /// <summary>
        /// On download progress changed
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Download progress changed event arguments</param>
        private void OnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            downloadProgressBar.Maximum = (int)(e.TotalBytesToReceive);
            downloadProgressBar.Value = (int)(e.BytesReceived);
            TaskbarProgress.SetValue(this, e.BytesReceived, e.TotalBytesToReceive);
        }

        /// <summary>
        /// On updates installed
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event arguments</param>
        private void OnUpdatesInstalled(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Animation timer tick event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void animationTimer_Tick(object sender, EventArgs e)
        {
            animationState++;
            if (animationState > 3U)
                animationState = 0U;
            progressLabel.Text = "Updating" + new string('.', (int)animationState);
        }
    }
}