
using System;

namespace RaiwairwofayfuHeehenagelki.GifImage
{

    /// <summary>
    ///     �߼���Ļ��ʶ��(Logical Screen Descriptor)
    /// </summary>
    internal class LogicalScreenDescriptor
    {
        /// <summary>
        ///     �߼���Ļ��� ������������GIFͼ��Ŀ��
        /// </summary>
        internal short Width { get; set; }

        /// <summary>
        ///     �߼���Ļ�߶� ������������GIFͼ��ĸ߶�
        /// </summary>
        internal short Height { get; set; }

        internal byte Packed { get; set; }

        /// <summary>
        ///     ����ɫ,������ɫ(��ȫ����ɫ�б��е����������û��ȫ����ɫ�б���ֵû������)
        /// </summary>
        internal byte BgColorIndex { get; set; }

        /// <summary>
        ///     ���ؿ�߱�,���ؿ�߱�(Pixel Aspect Radio)
        /// </summary>
        internal byte PixcelAspect { get; set; }

        /// <summary>
        ///     m - ȫ����ɫ�б��־(Global Color Table Flag)������λʱ��ʾ��ȫ����ɫ�б�pixelֵ������.
        /// </summary>
        internal bool GlobalColorTableFlag { get; set; }

        /// <summary>
        ///     cr - ��ɫ���(Color ResoluTion)��cr+1ȷ��ͼ�����ɫ���.
        /// </summary>
        internal byte ColorResoluTion { get; set; }

        /// <summary>
        ///     s - �����־(Sort Flag)�������λ��ʾȫ����ɫ�б��������.
        /// </summary>
        internal int SortFlag { get; set; }

        /// <summary>
        ///     ȫ����ɫ�б��С��pixel+1ȷ����ɫ�б����������2��pixel+1�η���.
        /// </summary>
        internal int GlobalColorTableSize { get; set; }


        internal byte[] GetBuffer()
        {
            var buffer = new byte[7];
            Array.Copy(BitConverter.GetBytes(Width), 0, buffer, 0, 2);
            Array.Copy(BitConverter.GetBytes(Height), 0, buffer, 2, 2);
            var m = 0;
            if (GlobalColorTableFlag)
            {
                m = 1;
            }

            var pixel = (byte) (Math.Log(GlobalColorTableSize, 2) - 1);
            Packed = (byte) (pixel | (SortFlag << 4) | (ColorResoluTion << 5) | (m << 7));
            buffer[4] = Packed;
            buffer[5] = BgColorIndex;
            buffer[6] = PixcelAspect;
            return buffer;
        }
    }

}