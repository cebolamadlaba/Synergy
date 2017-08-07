Currently logged database design changes:

- created table for regions (table design, scripts, repository design, tests) - 2 hours
- created table for user regions (table design, scripts, repository design, tests) - 2 hours
- redesign to change concessions to be based on risk group and not legal entity (table design, scripts, repository design, tests) - 16 hours
- created table for period (table design, scripts, repository design, tests) - 4 hours
- created table for period type (table design, scripts, repository design, tests) - 2 hours
- audit table design and implementation (table design, scripts, repository design, tests) - 8 hours

But every piece of functionality also now by default has extra overhead because of the existing database:

- figuring out what is being used for the required function - 4 hours
- figuring out why things were done the way they were done i.e. what was the person designing the database thinking - 4 hours
- does the current structure cater for the requirement and what changes need to be made for the requirement - 4 hours
