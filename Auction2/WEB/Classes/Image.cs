using BLL.Interface.BLLModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB.Classes
{
    public static class Image
    {
        public static BllImage CreateBllImage(int Id, HttpPostedFileBase LoadImage)
        {
                BllImage bllimage = new BllImage();
                bllimage.MimeType = LoadImage.ContentType;
                bllimage.Image = new byte[LoadImage.ContentLength];
                bllimage.LotId = Id;
                LoadImage.InputStream.Read(bllimage.Image, 0, LoadImage.ContentLength);
                LoadImage.InputStream.Close();
                return bllimage;
        }
    }
}