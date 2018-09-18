using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrowdfundingSystem.Data;
using CrowdfundingSystem.Data.Entities;
using CrowdfundingSystem.Data.Interfaces;
using CrowdfundingSystem.Data.ResourceModels.Vote;

namespace CrowdfundingSystem.API.Controllers
{
    [Produces("application/json")]
    [Route("api/NegativeVotes")]
    public class NegativeVotesController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public NegativeVotesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        //// GET: api/NegativeVotes
        //[HttpGet]
        //public IEnumerable<NegativeVote> GetNegativeVotes()
        //{
        //    return _context.NegativeVotes;
        //}

        //// GET: api/NegativeVotes/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetNegativeVote([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var negativeVote = await _context.NegativeVotes.SingleOrDefaultAsync(m => m.Id == id);

        //    if (negativeVote == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(negativeVote);
        //}


        // POST: api/NegativeVotes
        [HttpPost]
        public async Task<IActionResult> PostNegativeVote([FromBody] NegativeVotePostResourceModel negativeVote)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var negativeVoteToInsert = new NegativeVote()
            {
                VoterId = negativeVote.VoterId,
                IdeaId = negativeVote.IdeaId
            };

            await unitOfWork.NegativeVoteRepository.InsertAsync(negativeVoteToInsert);
            await unitOfWork.CompleteAsync();

            //return CreatedAtAction("GetNegativeVote", new { id = negativeVoteToInsert.Id }, negativeVoteToInsert);
            return Ok(negativeVoteToInsert);
        }

        // DELETE: api/NegativeVotes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNegativeVote([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var deletionSucceded = await unitOfWork.NegativeVoteRepository.DeleteAsync(id);
            await unitOfWork.CompleteAsync();
            if (deletionSucceded)
                return Ok();
            else
                return NotFound();
        }

    }
}