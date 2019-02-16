using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiff2PDF
{
    public class Converter
    {
        public static bool ConvertTiffToPdf(string inFilename, string outFilename)
        {
            bool result = false;

            //iTextSharp.text.Rectangle pgSize = PageSize.LETTER;
            iTextSharp.text.Rectangle pgSize = PageSize.LEGAL;

            Bitmap bmp = new Bitmap(inFilename);

            int totalPages = bmp.GetFrameCount(FrameDimension.Page);

            float margin = 0.0f;
            //float margin = 50.0f;
            Document document = new Document(pgSize, margin, margin, margin, margin);

            try
            {
                // creation of the different writers
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(outFilename, FileMode.Create));

                // Which of the multiple images in the TIFF file do we want to load
                // 0 refers to the first, 1 to the second and so on.
                document.Open();
                PdfContentByte cb = writer.DirectContent;
                iTextSharp.text.Image img;

                for (int ii = 0; ii < totalPages; ii++)
                {
                    bmp.SelectActiveFrame(FrameDimension.Page, ii);
                    img = iTextSharp.text.Image.GetInstance(BitmapToBytes(bmp));
                    img.ScalePercent(72f / bmp.HorizontalResolution * 100);
                    img.SetAbsolutePosition(0, 0);
                    cb.AddImage(img);
                    document.NewPage();
                }

                document.Close();
                document = null;
                bmp.Dispose();
                bmp = null;
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Failed: {0}", ex.ToString()));
            }
            return result;
        }

        private static byte[] BitmapToBytes(Bitmap bmp)
        {
            byte[] data = new byte[0];
            using (MemoryStream ms = new MemoryStream())
            {
                bmp.Save(ms, ImageFormat.Png);
                ms.Seek(0, 0);
                data = ms.ToArray();
            }
            return data;
        }

    }
}
