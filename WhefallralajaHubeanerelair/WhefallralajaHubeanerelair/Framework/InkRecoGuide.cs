using System.Runtime.InteropServices;

namespace WhefallralajaHubeanerelair;

[TypeLibType(16)]
[StructLayout(LayoutKind.Sequential, Pack = 4)]
internal struct InkRecoGuide
{
    // �ο� https://learn.microsoft.com/zh-cn/windows/win32/tablet/inkanalysisrecognizerguide ����������ͬ
    public PenImcRect rectWritingBox;
    public PenImcRect rectDrawnBox;
    public int cRows;
    public int cColumns;
    public int Midline;
}