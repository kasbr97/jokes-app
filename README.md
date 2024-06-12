## JokesApp

Web Application built on C# with ASP .Net MVC.

The JokesApp is a simple application that uses the Entity Framework ORM and connects to a database given the connection string. The users can create an account and start adding jokes to the application, which will be saved into the database. The jokes can then be edited, listed and deleted; however, only the user that created the joke, can edit and delete it, the other users can only read the joke. 

Additionally, the application consumes the JokesAPI and everytime the user accesses the homepage, a random joke from the API will show up.