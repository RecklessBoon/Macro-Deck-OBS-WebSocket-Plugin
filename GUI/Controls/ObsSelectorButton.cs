using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using Font = System.Drawing.Font;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using SuchByte.MacroDeck.GUI;

namespace SuchByte.OBSWebSocketPlugin.GUI.Controls
{

    public class ContentSelectorButton : PictureBox
    {
        private bool _notification;
        private bool _selected;

        public void SetNotification(bool notification)
        {
            _notification = notification;
            Invalidate();
        }

        public bool Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                Invalidate();
            }
        }

        public ContentSelectorButton()
        {
            DoubleBuffered = true;
            BackColor = Color.Transparent;
            BackgroundImageLayout = ImageLayout.Stretch;
            ForeColor = Color.White;
            Font = new Font("Tahoma", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Text = "";
            Height = 44;
            Width = 44;
            Margin = new Padding(left: 0, top: 3, right: 0, bottom: 3);
            Cursor = Cursors.Hand;
            MouseEnter += MouseEnterEvent;
            MouseLeave += MouseLeaveEvent;
        }

        private void MouseEnterEvent(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void MouseLeaveEvent(object sender, EventArgs e)
        {
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            if (_notification)
            {
                pe.Graphics.FillEllipse(Brushes.Red, Width - 12, 5, 10, 10);
            }
            if (ClientRectangle.Contains(PointToClient(Cursor.Position)) && !_selected)
            {
                pe.Graphics.FillRectangle(Brushes.White, Width - 3, 8, 3, Height - 16);
            }
            if (_selected)
            {
                pe.Graphics.FillRectangle(new SolidBrush(Colors.AccentColor), Width - 3, 4, 3, Height - 8);
            }
        }
    }

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
