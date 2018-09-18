using CrowdfundingSystem.Data.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrowdfundingSystem.API.Services.Interfaces
{
    public interface IAddMediaToIdeaService
    {
        Task AddMediaToIdea(Idea idea, IFormFile mainPhoto, IFormFile mainVideo, IFormFileCollection Photos,
            IFormFileCollection Videos, IFormFileCollection Audios, IFormFileCollection Documents);
        Task AddMainPhotoAsync(Idea idea, IFormFile mainPhoto);
        Task AddMainVideoAsync(Idea idea, IFormFile mainVideo);
        Task AddPhotoToCollectionAsync(Idea idea, IFormFile photo);
        Task AddVideoToCollectionAsync(Idea idea, IFormFile video);
        Task AddAudioToCollectionAsync(Idea idea, IFormFile audio);
        Task AddDocumentToCollectionAsync(Idea idea, IFormFile document);

    }
}
