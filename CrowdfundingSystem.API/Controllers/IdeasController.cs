using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CrowdfundingSystem.API.Services.Interfaces;
using CrowdfundingSystem.Data.Entities;
using CrowdfundingSystem.Data.Interfaces;
using CrowdfundingSystem.Data.ResourceModels;
using CrowdfundingSystem.Data.ResourceModels.Idea;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace CrowdfundingSystem.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Ideas")]
    public class IdeasController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IDeleteIdeaService deleteIdeaService;
        private readonly IAddMediaToIdeaService addMediaToIdeaService;
        private readonly IHostingEnvironment hostingEnvironment;
        public IdeasController(IUnitOfWork unitOfWork, IHostingEnvironment hostingEnvironment, IDeleteIdeaService deleteIdeaService, IAddMediaToIdeaService addMediaToIdeaService)
        {
            this.unitOfWork = unitOfWork;
            this.hostingEnvironment = hostingEnvironment;
            this.deleteIdeaService = deleteIdeaService;
            this.addMediaToIdeaService = addMediaToIdeaService;
        }

        // GET: api/Ideas/Top10
        [HttpGet("Top{amount}", Name = "GetTop")]
        public IActionResult GetTopIdeas(int amount)
        {
            //var query = await unitOfWork.IdeaRepository.GetAsync(orderBy: q => q.OrderByDescending(i => (i.PositiveVotes.Count + i.MoneyTransfers.Count) / 2), includeProperties: "PositiveVotes,MoneyTransfers");
            // isn't working due to a bug of the current vertion of EF Core
            var query = unitOfWork.IdeaRepository.GetWithSpecifiedAmount(
                filter: q => q.DeadlineForMoneyCollecting > System.DateTime.Now,
                orderBy: q => q.OrderByDescending(i => ((i.PositiveVotes.Count + i.MoneyTransfers.Count)) / 2.0),
                includeProperties: "PositiveVotes,MoneyTransfers,Owner,MainPhoto",
                amountToBeReturned: amount);
            var topIdeas = query.Select(q => new TopIdeaResourceModel()
            {
                IdeaId = q.Id,
                IdeaName = q.IdeaName,
                OwnerId = q.Owner.Id,
                OwnerFirstName = q.Owner.FirstName,
                OwnerLastName = q.Owner.LastName,
                SumRequired = q.SumRequired,
                TotalDonations = q.MoneyTransfers.Sum(t => t.TransferredSum),
                TotalLikes = q.PositiveVotes.Count(),
                MainPhotoUrl = q.MainPhoto?.Url ?? "",
                DeadlineForMoneyCollecting = q.DeadlineForMoneyCollecting.Date.ToShortDateString()
            });
            return Ok(topIdeas);
        }


        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                //{".txt", "text/plain"},
                {".pdf", "application/pdf"},
                //{".doc", "application/vnd.ms-word"},
                //{".docx", "application/vnd.ms-word"},
                //{".xls", "application/vnd.ms-excel"},
                //{".xlsx", "application/vnd.openxmlformats
                //           officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }

        // GET: api/Ideas/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            var query = await unitOfWork.IdeaRepository.GetByIdAsync(id, includeProperties: "Owner,PositiveVotes,NegativeVotes,MoneyTransfers,BankAccountForTransfers,MainPhoto,MainVideo,Photos,Videos,Audios,DocumentsAboutTheIdea");
            if (query != null)
            {
                //string webRootPath = hostingEnvironment.WebRootPath;
                //var path = string.Concat(webRootPath, query.MainPhoto.Url);
                //var memory = new MemoryStream();
                //using (var stream = new FileStream(path, FileMode.Open))
                //{
                //    await stream.CopyToAsync(memory);
                //}
                //memory.Position = 0;
                //return File(memory, GetContentType(path), Path.GetFileName(path));

                var resourceModel = new IdeaGetResourceModel
                {
                    IdeaId = query.Id,
                    IdeaName = query.IdeaName,
                    Description = query.Description,
                    OwnerId = query.OwnerId,
                    OwnerFirstName = query.Owner.FirstName,
                    OwnerLastName = query.Owner.LastName,
                    SumRequired = query.SumRequired,
                    AccountNumber = query.BankAccountForTransfers.AccountNumber,
                    TotalDonations = query.MoneyTransfers.Sum(t => t.TransferredSum),
                    TotalLikes = query.PositiveVotes.Count(),
                    TotalDislikes = query.NegativeVotes.Count(),
                    //PositiveVotes = query.PositiveVotes,
                    //NegativeVotes = query.NegativeVotes,
                    MainPhotoUrl = query.MainPhoto?.Url ?? "",
                    MainVideoUrl = query.MainVideo?.Url ?? "",
                    DeadlineForMoneyCollecting = query.DeadlineForMoneyCollecting.ToShortDateString(),
                    SuccededInCollectingTheMoney = query.CollectedTheMoney,
                    IsStillCollectingMoney = query.IsStillCollectingMoney,
                    DateOfGoalAchieving = query.DateOfGoalAchieving?.ToShortDateString(),
                    PhotosUrls = query.Photos.Select(p=>p.Url),
                    VideosUrls = query.Videos.Select(v=>v.Url),
                    AudiosUrls = query.Audios.Select(a => a.Url),
                    DocumentsUrls = query.DocumentsAboutTheIdea.Select(d=>d.Url)
                };
                return Ok(resourceModel);
            }
            return NotFound();
        }

        // POST: api/Ideas
        [HttpPost]
        public async Task<IActionResult> Post(IdeaPostResourceModel idea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ideaToInsert = new Idea
            {
                IdeaName = idea.IdeaName,
                OwnerId = idea.OwnerId,
                Description = idea.Description,
                SumRequired = idea.SumRequired,
                BankAccountForTransfers = new BankAccount()
                {
                    AccountOwnerId = idea.OwnerId,
                    AccountNumber = idea.BankAccountNumber
                },
                DateOfPublishing = DateTime.Now,
                DeadlineForMoneyCollecting = DateTime.Parse(idea.DeadlineForMoneyCollecting),
                IsStillCollectingMoney = true,
                CollectedTheMoney = false,
                InnerAccountForTransfers = new InnerMoneyAccount()
                {
                    //IdeaId = idea.Id,
                    SumTransferredToRealBankAccount = false,
                    SumAvailable = 0
                }
            };
            unitOfWork.IdeaRepository.Insert(ideaToInsert);
            await addMediaToIdeaService.AddMediaToIdea(ideaToInsert, idea.MainPhoto, idea.MainVideo, idea.Photos, idea.Videos, idea.Audios, idea.Documents);
            await unitOfWork.CompleteAsync();
            return CreatedAtAction("Get", new { id = ideaToInsert.Id }, ideaToInsert);

        }

        // PUT: api/Ideas/5
        //[HttpPut("{id}")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, IdeaPutResourceModel idea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var ideaToUpdate = await unitOfWork.IdeaRepository.GetByIdAsync(id);
                //if (ideaToUpdate != null) // will be used when adding functionality of editing "draft",not active yet ideas
                //{
                //    ideaToUpdate.IdeaName = idea.IdeaName;
                //    ideaToUpdate.OwnerId = idea.OwnerId;
                //    ideaToUpdate.Description = idea.Description;
                //    ideaToUpdate.SumRequired = idea.SumRequired;
                //    ideaToUpdate.BankAccountForTransfers.AccountNumber = idea.BankAccountNumber
                //};

                if (ideaToUpdate != null)
                {
                    await addMediaToIdeaService.AddMediaToIdea(ideaToUpdate, idea.MainPhoto, idea.MainVideo, idea.Photos, idea.Videos, idea.Audios, idea.Documents);                   
                }

                unitOfWork.IdeaRepository.Update(ideaToUpdate);
                await unitOfWork.CompleteAsync();

                return Ok(ideaToUpdate);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            //if there are any MoneyTransfers assosiated with the Idea then the money will be returned to the contributors and then the idea will be deleted
            //if not - the idea is deleted without any previous actions
            var deletionSucceeded = deleteIdeaService.DeleteIdea(unitOfWork, id);
            await unitOfWork.CompleteAsync();
            if (deletionSucceeded)
                return Ok();
            else
                return NotFound();
        }
    }
}
