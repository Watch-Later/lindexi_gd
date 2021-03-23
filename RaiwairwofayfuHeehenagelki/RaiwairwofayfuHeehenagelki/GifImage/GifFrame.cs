
using System.Drawing;
using System.Windows.Media;
using Color = System.Drawing.Color;

namespace RaiwairwofayfuHeehenagelki.GifImage
{

    /// <summary>
    ///     Gif�ļ��п��԰������ͼ��ÿ��ͼ�����ͼ���һЩ�������������֡:GifFrame
    /// </summary>
    internal class GifFrame
    {
    

        #region internal property

        /// <summary>
        ///     ����ı���ɫ
        /// </summary>
        public Color BgColor
        {
            get
            {
                var act = PaletteHelper.GetColor32s(LocalColorTable);
                return act[GraphicExtension.TranIndex];
            }
        }

        /// <summary>
        ///     ͼ���ʶ��(Image Descriptor)
        ///     һ��GIF�ļ��ڿ��԰������ͼ��
        ///     һ��ͼ�����֮�����������һ��ͼ��ı�ʶ����
        ///     ͼ���ʶ����0x2C(',')�ַ���ʼ��
        ///     �������������ͼ������ʣ�����ͼ��������߼���Ļ�߽��ƫ������
        ///     ͼ���С�Լ����޾ֲ���ɫ�б����ɫ�б��С
        /// </summary>
        internal ImageDescriptor ImageDescriptor { get; set; }

        /// <summary>
        ///     Gif�ĵ�ɫ��
        /// </summary>
        internal Color[] Palette
        {
            get
            {
                var act = PaletteHelper.GetColor32s(LocalColorTable);
                if (GraphicExtension != null && GraphicExtension.TransparencyFlag)
                {
                    act[GraphicExtension.TranIndex] = Color.FromArgb(0,0,0,0);
                }

                return act;
            }
        }
       
        /// <summary>
        ///     ͼ��
        /// </summary>
        public ImageSource Image { set; get; }

        /// <summary>
        ///     ����λ��С
        /// </summary>
        internal int ColorDepth { get; set; } = 3;

        /// <summary>
        ///     �ֲ���ɫ�б�(Local Color Table)
        ///     �������ľֲ���ɫ�б��־��λ�Ļ�������Ҫ�����������ͼ���ʶ��֮��
        ///     ����һ���ֲ���ɫ�б��Թ�����������ͼ��ʹ�ã�ע��ʹ��ǰӦ�߱���ԭ������ɫ�б�
        ///     ʹ�ý���֮��ظ�ԭ�������ȫ����ɫ�б����һ��GIF�ļ���û���ṩȫ����ɫ�б�
        ///     Ҳû���ṩ�ֲ���ɫ�б������Լ�����һ����ɫ�б���ʹ��ϵͳ����ɫ�б�
        ///     RGBRGB......
        /// </summary>
        internal byte[] LocalColorTable { get; set; }

        /// <summary>
        ///     ͼ�ο�����չ(Graphic Control Extension)��һ�����ǿ�ѡ�ģ���Ҫ89a�汾����
        ///     ���Է���һ��ͼ���(����ͼ���ʶ�����ֲ���ɫ�б��ͼ������)���ı���չ���ǰ�棬
        ///     �������Ƹ���������ĵ�һ��ͼ�󣨻��ı�������Ⱦ(Render)��ʽ
        /// </summary>
        internal GraphicEx GraphicExtension { get; set; }

        /// <summary>
        ///     �ӳ�-����һ֮֡���ʱ����
        /// </summary>
        internal short Delay
        {
            get => GraphicExtension.Delay;
            set => GraphicExtension.Delay = value;
        }

        /// <summary>
        ///     ����Ǿ���LZWѹ���㷨���������
        /// </summary>
        internal byte[] IndexedPixel { get; set; }

        #endregion
    }

}