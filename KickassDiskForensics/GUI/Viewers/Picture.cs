using System;
using System.IO;
using System.Drawing;
using KFS.DataStream;
using KFA.Search;

namespace KFA.GUI.Viewers {
    public class Picture : IDisposable {
        IDataStream m_Stream;
        byte[] imageBytes;
        System.Drawing.Image cachedImage = null;
        public Picture(IDataStream stream) {
            m_Stream = stream;
        }

        public static System.Drawing.Image ToImage(byte[] bytes) {
            using (var stream = new MemoryStream(bytes)) {
                using (var image = System.Drawing.Image.FromStream(stream, false, false)) {
                    try {
                        return new Bitmap(image);
                    } catch (Exception) {
                        return null;
                    }
                }
            }
        }

        public System.Drawing.Image GetImage() {
            if (cachedImage == null && FileTypes.IsPicture(m_Stream)) {
                m_Stream.Open();
                imageBytes = new byte[m_Stream.StreamLength];
                for (ulong i = 0; i < m_Stream.StreamLength; i++) {
                    imageBytes[i] = m_Stream.GetByte(i);
                }
                m_Stream.Close();
                try {
                    cachedImage = ToImage(imageBytes);
                } catch (ArgumentException) { }
            }
            return cachedImage;
        }

        #region IDisposable Members

        public void Dispose() {
            if (cachedImage != null) {
                cachedImage.Dispose();
                cachedImage = null;
            }
        }

        #endregion
    }
}
