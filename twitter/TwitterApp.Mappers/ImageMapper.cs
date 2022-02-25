using System;
using System.Collections.Generic;
using System.Text;
using TwitterApp.Domain.Models;
using TwitterApp.Models.Image;

namespace TwitterApp.Mappers
{
    public static class ImageMapper
    {

        public static Image ToImage(this ImageModel image)
        {
            return new Image
            {
                Id = image.Id,
                ImageContent = image.ImageContent
            };
        }


        public static ImageModel ToImageModel(this Image image)
        {
            return new ImageModel
            {
                Id = image.Id,
                ImageContent = image.ImageContent
            };
        }






    }
}
