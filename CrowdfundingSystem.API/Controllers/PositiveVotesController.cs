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
    [Route("api/PositiveVotes")]
    public class PositiveVotesController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public PositiveVotesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // No need for this action

        //// GET: api/PositiveVotes
        //[HttpGet]
        //public IEnumerable<PositiveVote> GetPositiveVotes()
        //{
        //    return _context.PositiveVotes;
        //}


        // No need for this action

        //// GET: api/PositiveVotes/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetPositiveVote([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var positiveVote = await _context.PositiveVotes.SingleOrDefaultAsync(m => m.Id == id);

        //    if (positiveVote == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(positiveVote);
        //}


        // POST: api/PositiveVotes
        [HttpPost]
        public async Task<IActionResult> PostPositiveVote([FromBody] PositiveVotePostResourceModel positiveVote)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var positiveVoteToInsert = new PositiveVote()
            {
                VoterId = positiveVote.VoterId,
                IdeaId = positiveVote.IdeaId
            };
            await unitOfWork.PositiveVoteRepository.InsertAsync(positiveVoteToInsert);
            unitOfWork.Complete();

            //return CreatedAtAction("GetPositiveVote", new { id = positiveVoteToInsert.Id }, positiveVoteToInsert);
            return Ok(positiveVoteToInsert);
        }

        // DELETE: api/PositiveVotes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePositiveVote([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var deletionSucceded = await unitOfWork.PositiveVoteRepository.DeleteAsync(id);
            await unitOfWork.CompleteAsync();
            if (deletionSucceded)
                return Ok();
            else
                return NotFound();
        }

    }
}