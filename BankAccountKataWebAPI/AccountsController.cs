using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankAccountKataWebAPI.BankAccountDb;
using Grpc.Net.Client;
using BankAccountKata;
using Grpc.Core;

namespace BankAccountKataWebAPI
{
    [Route("api/AccountsController")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly BankAccountDbContext _context;

        public AccountsController(BankAccountDbContext context)
        {
            _context = context;
        }

        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Accounts>>> GetAccounts()
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }
            return await _context.Accounts.ToListAsync();
        }

        // GET: api/Accounts/5
        [HttpGet("{name}")]
        public async Task<ActionResult<Accounts>> GetAccounts(string name)
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }
            var accounts = await _context.Accounts.FindAsync(name);

            if (accounts == null)
            {
                return NotFound();
            }

            return accounts;
        }

        // PUT: api/Accounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkname=2123754
        [HttpPut("{name}/{action}")]
        public async Task<IActionResult> PutAccounts(string name, string action, Accounts accounts)
        {
            if (name != accounts.Name)
            {
                return BadRequest();
            }

            _context.Entry(accounts).State = EntityState.Modified;
            var channel = GrpcChannel.ForAddress("https://localhost:7245", new GrpcChannelOptions { MaxReceiveMessageSize = 1 * 1024 * 1024 * 4 });
            var client = new BankAccountKata.BankAccountKata.BankAccountKataClient(channel);
            if (action =="deposit")
            {
                await client.MakeDepositRequestAsync(new AccountEntity { Name = accounts.Name, Amount = accounts.Balance });
            }
            else if(action =="withdraw")
            {
                await client.MakeWithdrawRequestAsync(new AccountEntity { Name = accounts.Name, Amount = accounts.Balance });

            }


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountsExists(name))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Accounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkname=2123754
        [HttpPost]
        public async Task<ActionResult<Accounts>> PostAccounts(Accounts accounts)
        {
          if (_context.Accounts == null)
          {
              return Problem("Entity set 'BankAccountDbContext.Accounts'  is null.");
          }
            _context.Accounts.Add(accounts);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AccountsExists(accounts.Name))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(GetAccounts), new { name = accounts.Name }, accounts);
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteAccounts(string name)
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }
            var accounts = await _context.Accounts.FindAsync(name);
            if (accounts == null)
            {
                return NotFound();
            }

            _context.Accounts.Remove(accounts);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccountsExists(string name)
        {
            return (_context.Accounts?.Any(e => e.Name == name)).GetValueOrDefault();
        }
    }
}
