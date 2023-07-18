using SuchByte.MacroDeck.GUI.CustomControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuchByte.OBSWebSocketPlugin.GUI.Controls
{
    public partial class ObsSelectorButton : ContentSelectorButton
    {
        public string AlertText { get; set; }
        public Color AlertBackgroundColor { get; set; } = Color.CornflowerBlue;
        public Color AlertForeColor { get; set; } = Color.White;

        public ObsSelectorButton()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            var indicatorSize = 18;
            var indicatorRectangle = new RectangleF(
                ClientRectangle.Width - indicatorSize * 1.25f, 
                ClientRectangle.Height - indicatorSize * 1.5f, 
                indicatorSize, 
                indicatorSize
            );

            using var indicatorBrush = new SolidBrush(AlertBackgroundColor);
            pe.Graphics.FillEllipse(indicatorBrush, indicatorRectangle);
            var brush = new SolidBrush(AlertForeColor);
            var format = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
                FormatFlags = StringFormatFlags.NoWrap,
                Trimming = StringTrimming.None
            };
            pe.Graphics.DrawString(AlertText, Font, brush, indicatorRectangle, format);
        }
    }
}
