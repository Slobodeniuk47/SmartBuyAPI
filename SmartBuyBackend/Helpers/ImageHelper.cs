using System.Drawing.Imaging;
using System.Drawing;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace SmartBuyAPI.Helpers
{
    public class ImageHelper
    {
        static string imageName;
        public static string SaveAndGetImageName(IFormFile image, IConfiguration _iconfiguration)
        {
            SaveImage(image, _iconfiguration);
            return imageName;
        }
        public static void DeleteImage(string oldImageName)
        {
            var dirSave = Path.Combine(Directory.GetCurrentDirectory(), "images");
            var imgDelete = dirSave + "/" + oldImageName;
            if (System.IO.File.Exists(imgDelete))
            {
                System.IO.File.Delete(imgDelete);
            }
        }
        public async static void SaveImage(IFormFile image, IConfiguration _iconfiguration)
        {
            if (image != null)
            {
                var fileExp = Path.GetExtension(image.FileName);
                var dirSave = Path.Combine(Directory.GetCurrentDirectory(), "images");
                imageName = Path.GetRandomFileName() + fileExp;
                using (var steam = System.IO.File.Create(Path.Combine(dirSave, imageName)))
                {
                    await image.CopyToAsync(steam);
                }
                try
                {
                    using (var ms = new MemoryStream())
                    {
                        await image.CopyToAsync(ms);
                        var bmp = new Bitmap(Image.FromStream(ms));
                        string[] sizes = ((string)_iconfiguration.GetValue<string>("ImageSizes")).Split(" ");
                        foreach (var s in sizes)
                        {
                            int size = Convert.ToInt32(s);
                            var saveImage = ImageHelper.CompressImage(bmp, size, size, false);
                            saveImage.Save(Path.Combine(dirSave, s + "_" + imageName));
                        }
                    }
                }
                catch { }
            }
        }
        public static Bitmap CompressImage(Bitmap originalPic, int maxWidth, int maxHeight, bool watermark = true, bool transperent = false)
        {
            try
            {
                int width = originalPic.Width;
                int height = originalPic.Height;
                int widthDiff = width - maxWidth;
                int heightDiff = height - maxHeight;
                bool doWidthResize = (maxWidth > 0 && width > maxWidth && widthDiff > heightDiff);
                bool doHeightResize = (maxHeight > 0 && height > maxHeight && heightDiff > widthDiff);

                if (doWidthResize || doHeightResize || (width.Equals(height) && widthDiff.Equals(heightDiff)))
                {
                    int iStart;
                    Decimal divider;
                    if (doWidthResize)
                    {
                        iStart = width;
                        divider = Math.Abs((Decimal)iStart / maxWidth);
                        width = maxWidth;
                        height = (int)Math.Round((height / divider));
                    }
                    else
                    {
                        iStart = height;
                        divider = Math.Abs((Decimal)iStart / maxHeight);
                        height = maxHeight;
                        width = (int)Math.Round(width / divider);
                    }
                }
                using (Bitmap outBmp = new Bitmap(width, height, PixelFormat.Format24bppRgb))
                {
                    using (Graphics oGraphics = Graphics.FromImage(outBmp))
                    {
                        //oGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        //oGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        oGraphics.Clear(Color.White);
                        oGraphics.DrawImage(originalPic, 0, 0, width, height);
                        //Watermark
                        if (watermark)
                        {

                            using (Image watermarkImage = Image.FromFile(Path.Combine(Directory.GetCurrentDirectory(), "images", "lohika_watermark.png")))
                            using (TextureBrush watermarkBrush = new TextureBrush(watermarkImage))
                            {
                                //Not responsive
                                //int x = (outBmp.Width - watermarkImage.Width - 15);
                                //int y = (outBmp.Height - watermarkImage.Height -15);
                                double imageHeightBrand = Convert.ToDouble(watermarkImage.Height);
                                double imageWidthBrand = Convert.ToDouble(watermarkImage.Width);
                                double ratioBrand = imageWidthBrand / imageHeightBrand;

                                double imageHeightBild = Convert.ToDouble(outBmp.Height); //height of the image to watermark
                                double imageWidthBild = Convert.ToDouble(outBmp.Width);
                                var imageWidthTmpBranding = imageWidthBild * 0.2; //the watermark width, but only 20% size of the image to watermark
                                var imageHeightTmpBranding = imageWidthTmpBranding / ratioBrand; //height of watermark, preserve aspect ratio
                                int imageWidthBranding = Convert.ToInt32(imageWidthTmpBranding); //convert in into int32 (see method below)
                                int imageHeightBranding = Convert.ToInt32(imageHeightTmpBranding);

                                int watermarkX = (int)(imageWidthBild - imageWidthBranding); // Bottom Right
                                int watermarkY = (int)(imageHeightBild - imageHeightBranding);
                                oGraphics.DrawImage(watermarkImage,
                                    new Rectangle(watermarkX, watermarkY, imageWidthBranding, imageHeightBranding),
                                    new Rectangle(0, 0, (int)imageWidthBrand, (int)imageHeightBrand),
                                    GraphicsUnit.Pixel);
                                //watermarkBrush.TranslateTransform(watermarkX, watermarkY);
                                //oGraphics.FillRectangle(watermarkBrush, new Rectangle(new Point(watermarkX, watermarkY), new Size(watermarkImage.Width, watermarkImage.Height)));
                            }
                        }
                        if (transperent)
                        {
                            outBmp.MakeTransparent();
                        }

                        return new Bitmap(outBmp);
                    }
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
