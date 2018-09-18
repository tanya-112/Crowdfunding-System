using CrowdfundingSystem.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CrowdfundingSystem.API.Services.Interfaces;

namespace CrowdfundingSystem.API.Services
{
    public class AddMediaToIdeaService : IAddMediaToIdeaService
    {
        private readonly IHostingEnvironment hostingEnvironment;

        public AddMediaToIdeaService(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }
        string[] photoContentTypes = new string[] { "image/jpeg", "image/bmp", "image/png" };
        string[] videoContentTypes = new string[] { "video/mpeg", "video/mp4", "video/webm", "video/ogg" };
        string[] audioContentTypes = new string[] { "audio/mpeg" };
        string[] documentContentTypes = new string[] { "application/pdf" };



        public async Task AddMediaToIdea(Idea idea, IFormFile mainPhoto, IFormFile mainVideo, IFormFileCollection Photos,
            IFormFileCollection Videos, IFormFileCollection Audios, IFormFileCollection Documents)
        {
            if (idea != null)
            {
                if (mainPhoto != null && photoContentTypes.Contains(mainPhoto.ContentType))
                    await AddMainPhotoAsync(idea, mainPhoto);

                if (mainVideo != null && videoContentTypes.Contains(mainVideo.ContentType))
                    await AddMainVideoAsync(idea, mainVideo);
                if (Photos != null)
                    foreach (var photo in Photos)
                    {
                        if (photo != null && photoContentTypes.Contains(photo.ContentType))
                            await AddPhotoToCollectionAsync(idea, photo);
                    }
                if (Videos != null)
                    foreach (var video in Videos)
                    {
                        if (video != null && videoContentTypes.Contains(video.ContentType))
                            await AddVideoToCollectionAsync(idea, video);
                    }
                if (Audios != null)
                    foreach (var audio in Audios)
                    {
                        if (audio != null && audioContentTypes.Contains(audio.ContentType))
                            await AddAudioToCollectionAsync(idea, audio);
                    }
                if (Documents != null)
                    foreach (var document in Documents)
                    {
                        if (document != null && documentContentTypes.Contains(document.ContentType))
                            await AddDocumentToCollectionAsync(idea, document);
                    }
            }
        }
        public async Task AddMainPhotoAsync(Idea idea, IFormFile mainPhoto)
        {
            if (mainPhoto != null && photoContentTypes.Contains(mainPhoto.ContentType))
            {
                var file = mainPhoto;
                var pathInsideTheWwwRoot = "Images/" + Guid.NewGuid() + Path.GetFileName(file.FileName);
                var filePath = Path.Combine(hostingEnvironment.WebRootPath, pathInsideTheWwwRoot);
                await file.CopyToAsync(new FileStream(filePath, FileMode.Create));
                var photo = new Photo()
                {
                    //Url = fileName
                    Url = "/" + pathInsideTheWwwRoot
                };
                idea.MainPhoto = photo;
            }
        }

        public async Task AddMainVideoAsync(Idea idea, IFormFile mainVideo)
        {
            if (mainVideo != null && videoContentTypes.Contains(mainVideo.ContentType))
            {
                var file = mainVideo;
                var pathInsideTheWwwRoot = "Videos/" + Guid.NewGuid() + Path.GetFileName(file.FileName);
                var filePath = Path.Combine(hostingEnvironment.WebRootPath, pathInsideTheWwwRoot);
                await file.CopyToAsync(new FileStream(filePath, FileMode.Create));
                var video = new Video()
                {
                    //Url = fileName
                    Url = "/" + pathInsideTheWwwRoot
                };
                idea.MainVideo = video;
            }
        }

        public async Task AddPhotoToCollectionAsync(Idea idea, IFormFile photo)
        {
            if (photo != null && photoContentTypes.Contains(photo.ContentType))
            {
                var file = photo;
                var pathInsideTheWwwRoot = "Images/" + Guid.NewGuid() + Path.GetFileName(file.FileName);
                var filePath = Path.Combine(hostingEnvironment.WebRootPath, pathInsideTheWwwRoot);
                await file.CopyToAsync(new FileStream(filePath, FileMode.Create));
                var photoToAdd = new Photo()
                {
                    //Url = fileName
                    Url = "/" + pathInsideTheWwwRoot
                };
                idea.Photos.Add(photoToAdd);
            }
        }
        public async Task AddVideoToCollectionAsync(Idea idea, IFormFile video)
        {
            if (video != null && videoContentTypes.Contains(video.ContentType))
            {
                var file = video;
                var pathInsideTheWwwRoot = "Videos/" + Guid.NewGuid() + Path.GetFileName(file.FileName);
                var filePath = Path.Combine(hostingEnvironment.WebRootPath, pathInsideTheWwwRoot);
                await file.CopyToAsync(new FileStream(filePath, FileMode.Create));
                var videoToAdd = new Video()
                {
                    //Url = fileName
                    Url = "/" + pathInsideTheWwwRoot
                };
                idea.Videos.Add(videoToAdd);
            }
        }
        public async Task AddAudioToCollectionAsync(Idea idea, IFormFile audio)
        {
            if (audio != null && audioContentTypes.Contains(audio.ContentType))
            {
                var file = audio;
                var pathInsideTheWwwRoot = "Audios/" + Guid.NewGuid() + Path.GetFileName(file.FileName);
                var filePath = Path.Combine(hostingEnvironment.WebRootPath, pathInsideTheWwwRoot);
                await file.CopyToAsync(new FileStream(filePath, FileMode.Create));
                var audioToAdd = new Audio()
                {
                    //Url = fileName
                    Url = "/" + pathInsideTheWwwRoot
                };
                idea.Audios.Add(audioToAdd);
            }
        }
        public async Task AddDocumentToCollectionAsync(Idea idea, IFormFile document)
        {
            if (document != null && documentContentTypes.Contains(document.ContentType))
            {
                var file = document;
                var pathInsideTheWwwRoot = "Documents/" + Guid.NewGuid() + Path.GetFileName(file.FileName);
                var filePath = Path.Combine(hostingEnvironment.WebRootPath, pathInsideTheWwwRoot);
                await file.CopyToAsync(new FileStream(filePath, FileMode.Create));
                var documentToAdd = new Document()
                {
                    //Url = fileName
                    Url = "/" + pathInsideTheWwwRoot
                };
                idea.DocumentsAboutTheIdea.Add(documentToAdd);
            }
        }
    }
}
