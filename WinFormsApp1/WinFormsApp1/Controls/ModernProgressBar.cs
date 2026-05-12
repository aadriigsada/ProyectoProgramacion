using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinFormsApp1.Controls
{
    public class ModernProgressBar : Control
    {
        private int _minimum = 0;
        private int _maximum = 100;
        private int _value = 0;

        [DefaultValue(0)]
        public int Minimum
        {
            get => _minimum;
            set
            {
                _minimum = value;
                if (_maximum < _minimum) _maximum = _minimum;
                if (_value < _minimum) _value = _minimum;
                Invalidate();
            }
        }

        [DefaultValue(100)]
        public int Maximum
        {
            get => _maximum;
            set
            {
                _maximum = Math.Max(value, _minimum);
                if (_value > _maximum) _value = _maximum;
                Invalidate();
            }
        }

        [DefaultValue(0)]
        public int Value
        {
            get => _value;
            set
            {
                int nuevoValor = Math.Max(_minimum, Math.Min(_maximum, value));
                if (_value == nuevoValor) return;
                _value = nuevoValor;
                Invalidate();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color FillColorStart { get; set; } = Color.FromArgb(38, 198, 218);

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color FillColorEnd { get; set; } = Color.FromArgb(0, 172, 193);

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color TrackColor { get; set; } = Color.FromArgb(28, 34, 46);

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BorderColor { get; set; } = Color.FromArgb(85, 98, 120);

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color TextColor { get; set; } = Color.WhiteSmoke;

        [DefaultValue(10)]
        public int CornerRadius { get; set; } = 10;

        [DefaultValue(true)]
        public bool ShowValueText { get; set; } = true;

        public ModernProgressBar()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint, true);

            Size = new Size(220, 26);
            Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);

            using (GraphicsPath fondoPath = CrearRectanguloRedondeado(rect, CornerRadius))
            using (SolidBrush fondo = new SolidBrush(TrackColor))
            using (Pen borde = new Pen(BorderColor, 1.2f))
            {
                e.Graphics.FillPath(fondo, fondoPath);
                e.Graphics.DrawPath(borde, fondoPath);
            }

            float rango = Math.Max(1, _maximum - _minimum);
            float porcentaje = (_value - _minimum) / rango;
            int anchoRelleno = (int)((Width - 2) * porcentaje);

            if (anchoRelleno > 0)
            {
                Rectangle fillRect = new Rectangle(1, 1, anchoRelleno, Height - 2);
                int radioFill = Math.Min(CornerRadius, Math.Max(2, fillRect.Width / 2));

                using (GraphicsPath fillPath = CrearRectanguloRedondeado(fillRect, radioFill))
                using (LinearGradientBrush degradado = new LinearGradientBrush(
                           fillRect, FillColorStart, FillColorEnd, LinearGradientMode.Horizontal))
                {
                    e.Graphics.FillPath(degradado, fillPath);
                }
            }

            if (ShowValueText)
            {
                string texto = $"{_value}/{_maximum}";
                TextRenderer.DrawText(e.Graphics, texto, Font, rect, TextColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
        }

        private static GraphicsPath CrearRectanguloRedondeado(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int d = Math.Max(1, radius * 2);

            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();

            return path;
        }
    }
}
