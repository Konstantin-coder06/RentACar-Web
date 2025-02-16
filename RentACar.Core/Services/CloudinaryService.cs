using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RentACar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Core.Services
{
    public class CloudinaryService
    {
        private readonly Cloudinary _cloudinary;



        public CloudinaryService(IConfiguration config)

        {

            var account = new Account(

            config["Cloudinary:CloudName"],

            config["Cloudinary:ApiKey"],

            config["Cloudinary:ApiSecret"]);



            _cloudinary = new Cloudinary(account);

        }





        public async Task<string> UploadImageAsync(IFormFile file)

        {

            if (file == null || file.Length == 0)

                return null;



            using var stream = file.OpenReadStream();

            var uploadParams = new ImageUploadParams

            {

                File = new FileDescription(file.FileName, stream),
                PublicId = $"cars/{file.Name}/{Guid.NewGuid()}"


            };



            var uploadResult = await _cloudinary.UploadAsync(uploadParams);



            if (uploadResult == null || uploadResult.SecureUrl == null)

            {

                return null;

            }



            return uploadResult.SecureUrl.AbsoluteUri;

        }

    }
}

