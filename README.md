## JokesApp

Web Application built on C# with ASP .Net MVC.

The JokesApp is a simple application that uses the Entity Framework ORM and connects to a database given the connection string. The users can create an account and start adding jokes to the application, which will be saved into the database. The jokes can then be edited, listed and deleted; however, only the user that created the joke, can edit and delete it, the other users can only read the joke. 

Additionally, the application consumes the JokesAPI and everytime the user accesses the homepage, a random joke from the API will show up.

The Home page of the App shows a random joke (currently set to be Safe jokes) The "Another Joke!" button will call the controller again which will call the JokesAPI again to show a new joke.
![image](https://github.com/kasbr97/jokes-app/assets/39348173/b21433b2-a00a-4dd7-a989-8d84bca32e24)

The user can head to the Jokes tab in the navigation bar and it will show the jokes in the current database; however, the user cannot alter the jokes because they're not authenticated.
![image](https://github.com/kasbr97/jokes-app/assets/39348173/f38e8b14-4902-4f72-be6b-63c1464b6909)

The user can search a Joke even if they're not authenticated. The JokesController will look for the keyword in both columns of Joke Question and Joke Answer.  
![image](https://github.com/kasbr97/jokes-app/assets/39348173/72c40951-891b-4a27-8768-f59c8f7830bc)

If the user tries to Create a New Joke, the page will redirect it to the Log In page.
![image](https://github.com/kasbr97/jokes-app/assets/39348173/00ca7e14-5462-4c45-b3c8-db5f7cfa3eda)

Once authenticated, the user can Create New Jokes and Delete or Edit the jokes that are registered to them. 
![image](https://github.com/kasbr97/jokes-app/assets/39348173/52e34171-2eb4-4794-92f1-b02b87555617)

An authenticated user can create Jokes that will be saved into the database this way.
![image](https://github.com/kasbr97/jokes-app/assets/39348173/d6bad3b6-e422-4b48-a760-82791d6d0191)

The user can then Edit it, check the details on it or delete it.
![image](https://github.com/kasbr97/jokes-app/assets/39348173/6e42c7f8-44ab-4cad-a9ae-efe728b9b48a)

The Edit page looks like this:
![image](https://github.com/kasbr97/jokes-app/assets/39348173/a50040d6-97ef-49e6-99e3-e79f762e630e)

Details Page:
![image](https://github.com/kasbr97/jokes-app/assets/39348173/fc56bb36-8209-4856-80e4-c325aa6bd59f)

Delete Page:
![image](https://github.com/kasbr97/jokes-app/assets/39348173/1a49cb71-2bcf-400e-8d9c-d7faf8d4a7aa)

JokesController code to Create a Joke:
```
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
```

JokesController code to Delete a Joke:
```
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
```







