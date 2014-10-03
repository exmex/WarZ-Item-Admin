using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WarZLocal_Admin
{
    public partial class LoadingCircle : Control
{
    private readonly Timer _timer;
    private bool _isLoading;
    private int _numberOfSpoke = 12;
    private int _progressValue;
    private PointF _centerPointF;
    private int _innerCircleRadius = 5;
    private int _outnerCircleRadius = 10;
    private Color _themeColor = Color.DimGray;
    private int _speed = 100;
    private Color[] _colors;
    private int _lineWidth = 3;

    public int Speed
    {
        get { return _speed; }
        set
        {
            _speed = value;
            if(_timer != null)
                _timer.Interval = _speed;
        }
    }
    public int SpokesMember
    {
        get { return _numberOfSpoke; }
        set
        {
            _numberOfSpoke = value;
            Invalidate();
        }
    }
    public int InnerCircleRadius
    {
        get { return _innerCircleRadius; }
        set
        {
            _innerCircleRadius = value;
            Invalidate();
        }
    }
    public int OutnerCircleRadius
    {
        get { return _outnerCircleRadius; }
        set
        {
            _outnerCircleRadius = value;
            Invalidate();
        }
    }
    public Color ThemeColor
    {
        get { return _themeColor; }
        set
        {
            _themeColor = value;
            _colors = GetColors(_themeColor, _numberOfSpoke, _isLoading);
            Invalidate();
        }
    }
    public int LineWidth
    {
        get { return _lineWidth; }
        set
        {
            _lineWidth = value;
            Invalidate();
        }
    }
    public bool IsActive
    {
        get
        {
            return _timer.Enabled;
        }
    }

    public LoadingCircle(IContainer container)
    {
        container.Add(this);

        InitializeComponent();

        SetStyle(ControlStyles.UserPaint, true);
        SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        SetStyle(ControlStyles.ResizeRedraw, true);
        SetStyle(ControlStyles.SupportsTransparentBackColor, true);

        _timer = new Timer();
        _timer.Tick += _Timer_Tick;
        _timer.Interval = _speed;
        _colors = GetColors(_themeColor, _numberOfSpoke, _isLoading);
    }

    public LoadingCircle()
    {
        InitializeComponent();
        SetStyle(ControlStyles.UserPaint, true);
        SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        SetStyle(ControlStyles.ResizeRedraw, true);
        SetStyle(ControlStyles.SupportsTransparentBackColor, true);

        _timer = new Timer();
        _timer.Tick += _Timer_Tick;
        _timer.Interval = _speed;
        _colors = GetColors(_themeColor, _numberOfSpoke, _isLoading);
    }

    protected override void OnPaint(PaintEventArgs pe)
    {
        _centerPointF = new PointF(Width / 2.0f, Height / 2.0f);
        if (_numberOfSpoke > 0)
        {
            pe.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            var offsetAngle = (double) 360/_numberOfSpoke;
            var currentAngle = _progressValue*offsetAngle;
            for (var i = 0; i < _numberOfSpoke; i++)
            {
                DrawLine(pe.Graphics, GetPointF(_centerPointF, _innerCircleRadius, currentAngle),
                    GetPointF(_centerPointF, _outnerCircleRadius, currentAngle), _colors[i]);
                currentAngle += offsetAngle;
            }
        }
        base.OnPaint(pe);
    }

    private void DrawLine(Graphics g, PointF pointF1, PointF pointF2, Color color)
    {
        using (var pen = new Pen(new SolidBrush(color), _lineWidth))
        {
            pen.StartCap = LineCap.Round;
            pen.EndCap = LineCap.Round;
            g.DrawLine(pen, pointF1, pointF2);
        }
    }

    private PointF GetPointF(PointF centerPointF, int r, double angle)
    {
        var a = Math.PI*angle/180; //(angle/360)*2PI
        var xF = centerPointF.X + r*(float) Math.Cos(a);
        var yF = centerPointF.Y + r*(float) Math.Sin(a);

        return (new PointF(xF, yF));
    }

    private Color[] GetColors(Color color, int spokeMember, bool isLoading)
    {
        var colors = new Color[spokeMember];
        var offseAlpha = 255/spokeMember;
        if (isLoading)
        {
            for (var i = 0; i < spokeMember; i++)
            {
                colors[i] = Color.FromArgb(i*offseAlpha, color);
            }
        }
        else
        {
            for (var i = 0; i < spokeMember; i++)
            {
                colors[i] = color;
            }
        }
        return colors;
    }

    private void _Timer_Tick(object sender, EventArgs e)
    {
        _progressValue++;
        if (_progressValue > 11)
            _progressValue = 0;
        Invalidate();
    }

    public void Start()
    {
        _isLoading = true;
        _colors = GetColors(_themeColor, _numberOfSpoke, _isLoading);
        _timer.Start();
    }
    public void Stop()
    {
        _timer.Stop();
        _isLoading = false;
        _colors = GetColors(_themeColor, _numberOfSpoke, _isLoading);
        Invalidate();
    }
}
}
