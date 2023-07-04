﻿using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Language;
using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Plugins;
using SuchByte.OBSWebSocketPlugin.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuchByte.OBSWebSocketPlugin.GUI
{
    public partial class PluginConfig : DialogForm
    {
        public PluginConfig()
        {
            InitializeComponent();

            btnOk.Text = LanguageManager.Strings.Ok;

            List<Dictionary<string, string>> credentials = PluginCredentials.GetPluginCredentials(PluginInstance.Main);
            if (credentials != null && credentials.Count > 0)
            {
                var first = true;
                foreach(Dictionary<string, string> page in credentials)
                {
                    if (page.ContainsKey("host") && page.ContainsKey("password"))
                    {
                        var config = new ConnectionConfig
                        {
                            name = page.ContainsKey("name") ? page["name"] : "",
                            host = page.ContainsKey("host") ? page["host"] : "",
                            password = page.ContainsKey("password") ? page["password"] : ""
                        };
                        if (!first)
                        {
                            AddRow(config);
                        }
                        else
                        {
                            UpdateFirstRow(config);
                        }
                    }
                    first = false;
                }
            }
            else
            {
                UpdateFirstRow();
            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            PluginCredentials.DeletePluginCredentials(PluginInstance.Main);
            foreach (Control control in repeatingLayout.Controls)
            {
                if (control is ConnectionConfigurator config)
                {
                    PluginCredentials.AddCredentials(PluginInstance.Main, config.Value.ToCredentials());
                }
            }

            try
            {
                var self = this;
                _ = Task.Run(async () =>
                {
                    await PluginInstance.Main.SetupAndStartAsync();
                    if (PluginInstance.Main.GetNumConnected() > 0)
                    {
                        self.Invoke((MethodInvoker)delegate { self.Close(); });
                    }
                });
            }
            catch (Exception ex)
            {
                MacroDeckLogger.Error(PluginInstance.Main, $"Error: {ex.Message + Environment.NewLine + ex.StackTrace} ");
            }
        }

        private void AddRow(ConnectionConfig config = null)
        {
            repeatingLayout.RowCount = repeatingLayout.RowCount + 1;
            repeatingLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            var elm = new ConnectionConfigurator();
            elm.name.Text = config?.name;
            elm.host.Text = config?.host;
            elm.password.Text = config?.password;
            elm.Dock = DockStyle.Fill;
            elm.Margin = new Padding(13);
            repeatingLayout.Controls.Add(elm, 0, repeatingLayout.RowCount - 1);

            var btnRemove = new Button
            {
                Text = "-",
                Dock = DockStyle.Top,
                BackColor = Color.Maroon,
                Margin = new Padding(13),
            };
            btnRemove.Click += BtnRemove_Click;
            repeatingLayout.Controls.Add(btnRemove, 1, repeatingLayout.RowCount - 1);
        }

        private void UpdateFirstRow(ConnectionConfig config = null)
        {
            connectionConfig_1.name.Text = config?.name ?? "";
            connectionConfig_1.host.Text = config?.host ?? "ws://127.0.0.1:4455";
            connectionConfig_1.password.Text = config?.password ?? "";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddRow();
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            var row = repeatingLayout.GetRow(sender as Control);
            TableLayoutHelper.RemoveArbitraryRow(repeatingLayout, row);
        }

        private void buttonPrimary1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void repeatingLayout_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            var bottomLeft = new Point(e.CellBounds.Left, e.CellBounds.Bottom);
            var bottomRight = new Point(e.CellBounds.Right, e.CellBounds.Bottom);
            e.Graphics.DrawLine(Pens.White, bottomLeft, bottomRight);
        }
    }
}
