
using System.Drawing;

namespace RaiwairwofayfuHeehenagelki.GifImage
{

    /// <summary>
    ///     ��ɫ�帨����
    /// </summary>
    internal static class PaletteHelper
    {
        #region ���������л�ȡ��ɫ�б�

        /// <summary>
        ///     ���������л�ȡ��ɫ�б�
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        internal static Color[] GetColor32s(byte[] table)
        {
            var tab = new Color[table.Length / 3];
            var i = 0;
            var j = 0;
            while (i < table.Length)
            {
                var r = table[i++];
                var g = table[i++];
                var b = table[i++];
                byte a = 255;
                var c = Color.FromArgb(a, r, g, b);
                tab[j++] = c;
            }

            return tab;
        }

        #endregion
    }

}