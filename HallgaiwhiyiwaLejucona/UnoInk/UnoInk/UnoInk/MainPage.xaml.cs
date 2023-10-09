using Windows.Foundation;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Shapes;
using Path = Microsoft.UI.Xaml.Shapes.Path;
using System.Linq;

namespace UnoInk;

public sealed partial class MainPage : Page
{
    public MainPage()
    {
        this.InitializeComponent();
    }

    private void InkCanvas_OnPointerPressed(object sender, PointerRoutedEventArgs e)
    {
        var pointerPoint = e.GetCurrentPoint(InkCanvas);
        Point position = pointerPoint.Position;

        var inkInfo = new InkInfo();
        _inkInfoCache[e.Pointer.PointerId] = inkInfo;
        inkInfo.PointList.Add(position);

        DrawStroke(inkInfo);
    }


    private void InkCanvas_OnPointerMoved(object sender, PointerRoutedEventArgs e)
    {
        if (_inkInfoCache.TryGetValue(e.Pointer.PointerId, out var inkInfo))
        {
            var pointerPoint = e.GetCurrentPoint(InkCanvas);
            Point position = pointerPoint.Position;

            inkInfo.PointList.Add(position);
            DrawStroke(inkInfo);
        }
    }

    private void InkCanvas_OnPointerReleased(object sender, PointerRoutedEventArgs e)
    {
        if (_inkInfoCache.Remove(e.Pointer.PointerId, out var inkInfo))
        {
            var pointerPoint = e.GetCurrentPoint(InkCanvas);
            Point position = pointerPoint.Position;
            inkInfo.PointList.Add(position);
            DrawStroke(inkInfo);
        }
    }

    private void InkCanvas_OnPointerCanceled(object sender, PointerRoutedEventArgs e)
    {
        if (_inkInfoCache.Remove(e.Pointer.PointerId, out var inkInfo))
        {
            RemoveInkElement(inkInfo.InkElement);
        }
    }

    private readonly Dictionary<uint /*PointerId*/, InkInfo> _inkInfoCache = new Dictionary<uint, InkInfo>();

    private void RemoveInkElement(FrameworkElement? inkElement)
    {
        if (inkElement != null)
        {
            InkCanvas.Children.Remove(inkElement);
        }
    }

    private void DrawStroke(InkInfo inkInfo)
    {
        var pointList = inkInfo.PointList;
        if (pointList.Count < 2)
        {
            // С����������޷�Ӧ���㷨
            return;
        }

        // ģ��ʷ�

        // ���ڵ��ɱʷ�ĵ������
        var tipCount = 20;
        if (pointList.Count > tipCount)
        {
            for (int i = 0; i < pointList.Count; i++)
            {
                if ((pointList.Count - i) < tipCount)
                {
                    pointList[i] = pointList[i] with
                    {
                        Pressure = (pointList.Count - i) * 1f / tipCount
                    };
                }
                else
                {
                    pointList[i] = pointList[i] with
                    {
                        Pressure = 1.0f
                    };
                }
            }
        }

        // �ʼ���С���ʼ���ϸ
        int inkSize = 16;
        var inkElement = MyInkRender.CreatePath(inkInfo, inkSize);

        if (inkInfo.InkElement is null)
        {
            InkCanvas.Children.Add(inkElement);
        }
        else if (inkElement != inkInfo.InkElement)
        {
            RemoveInkElement(inkInfo.InkElement);
            InkCanvas.Children.Add(inkElement);
        }
        inkInfo.InkElement = inkElement;
    }
}