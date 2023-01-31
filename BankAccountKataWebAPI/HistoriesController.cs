using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankAccountKataWebAPI.BankAccountDb;

namespace BankAccountKataWebAPI
{
    [Route("api/HistoriesController")]
    [ApiController]
    public class HistoriesController : ControllerBase
    {
        private readonly BankAccountDbContext _context;

        public HistoriesController(BankAccountDbContext context)
        {
            _context = context;
        }  

        // GET: api/Histories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<History>>> GetHistory(string id)
        {
          if (_context.History == null)
          {
              return NotFound();
          }
            var history = _context.History.Where(a => a.Name == id).ToList();

            if (history == null)
            {
                return NotFound();
            }

            return history;
        }


        private bool HistoryExists(string id)
        {
            return (_context.History?.Any(e => e.Name == id)).GetValueOrDefault();
        }
    }
}
