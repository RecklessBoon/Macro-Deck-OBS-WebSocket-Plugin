using SuchByte.OBSWebSocketPlugin.Controllers;
using SuchByte.OBSWebSocketPlugin.GUI.Controls;
using SuchByte.OBSWebSocketPlugin.GUI.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuchByte.OBSWebSocketPlugin.GUI
{
    public partial class ConnectionTogglerList : Form
    {
        public List<ConnectionToggler> _Connections = new();

        public ConnectionTogglerList()
        {
            InitializeComponent();
            this._Connections = new List<ConnectionToggler>();
        }

        public ConnectionTogglerList(List<Connection> connections)
        {
            InitializeComponent();
            foreach (var connection in connections)
            {
                AddConnection(connection);
            }
        }

        public void AddConnection(Connection connection)
        {
            this.InvokeIfRequired(() =>
            {
                var toggler = _Connections.FirstOrDefault(x => x.Connection.Name == connection.Name);

                if (toggler != null)
                {
                    toggler.Connection = connection;
                }
                else
                {
                    toggler = new ConnectionToggler(connection);
                    _Connections.Add(toggler);
                    flowLayoutPanel1.SuspendLayout();
                    flowLayoutPanel1.Controls.Add(toggler);
                    flowLayoutPanel1.ResumeLayout();
                }
            });

        }

        public void RemoveConnection(Connection connection)
        {
            this.InvokeIfRequired(() =>
            {
                var toggler = _Connections.FirstOrDefault(x => x.Connection.Name == connection.Name);
                if (toggler == null) return;

                _Connections.Remove(toggler);
                flowLayoutPanel1.SuspendLayout();
                flowLayoutPanel1.Controls.Remove(toggler);
                flowLayoutPanel1.ResumeLayout();
            });
        }
    }
}
