using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JokesWebApp.Data;
using JokesWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;


namespace JokesWebApp.Controllers
{
    public class JokesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UserManager<IdentityUser> _userManager;
        private readonly ILogger<JokesController> _logger;
        public JokesController(ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            ILogger<JokesController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        // GET: Jokes
        public async Task<IActionResult> Index(string searchChar)
        {
            _logger.LogInformation("Accessed JokesController Index Index");
            var jokes = from j in _context.Joke 
                        select j;
            
            _logger.LogInformation("JokesController:Index: Looking for User Emails for each Joke");
            for ( var i = 0; i<jokes.Count(); i++)
            {
                if (jokes.ElementAt(i).UserId != null)
                {
                    var user = await _userManager.FindByIdAsync(jokes.ElementAt(i).UserId);
                    if (user != null)
                    {
                        jokes.ElementAt(i).UserId = user.Email;
                    }
                }
            }

            if (!string.IsNullOrEmpty(searchChar))
            {
                _logger.LogInformation("JokesController:Index: Executing search...");
                jokes = jokes.Where(j => 
                    j.JokeQuestion.Contains(searchChar) || 
                    j.JokeAnswer.Contains(searchChar)
                );
                return View("Index", await jokes.ToListAsync());
            }
            return View("Index", await jokes.ToListAsync());
        }

        // GET: Jokes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            _logger.LogInformation("Accessed: JokesController Details");
            if (id == null)
            {
                return NotFound();
            }
            var joke = await _context.Joke
                .FirstOrDefaultAsync(m => m.Id == id);
            if (joke == null)
            {
                return NotFound();
            }

            return View(joke);
        }

        // GET: Jokes/Create
        [Authorize]
        public IActionResult Create()
        {
            _logger.LogInformation("Accessed: JokesController Create");
            return View();
        }

        // POST: Jokes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,JokeQuestion,JokeAnswer,UserId")] Joke joke)
        {
            _logger.LogInformation("Accessed: JokesController Create, Saving Joke into db");
            if (ModelState.IsValid)
            {
                _context.Add(joke);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(joke);
        }

        // GET: Jokes/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            _logger.LogInformation("Accessed: JokesController Edit, Showing Editable Joke");
            if (id == null)
            {
                return NotFound();
            }

            var joke = await _context.Joke.FindAsync(id);
            if (joke == null)
            {
                return NotFound();
            }
            return View(joke);
        }

        // POST: Jokes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,JokeQuestion,JokeAnswer,UserId")] Joke joke)
        {
            _logger.LogInformation("Accessed: JokesController Edit, Editing Joke");
            if (id != joke.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(joke);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JokeExists(joke.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(joke);
        }

        // GET: Jokes/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            _logger.LogInformation("Accessed: JokesController Delete Joke");
            if (id == null)
            {
                return NotFound();
            }

            var joke = await _context.Joke
                .FirstOrDefaultAsync(m => m.Id == id);
            if (joke == null)
            {
                return NotFound();
            }

            return View(joke);
        }

        // POST: Jokes/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _logger.LogInformation("Accessed: JokesController Delete Joke, DELETING Joke");
            var joke = await _context.Joke.FindAsync(id);
            if (joke != null)
            {
                _context.Joke.Remove(joke);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JokeExists(int id)
        {
            return _context.Joke.Any(e => e.Id == id);
        }
    }
}
