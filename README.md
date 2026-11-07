# Movie Administrator Web App

## Under Contruction

## Purpose

This app is function over form acting as a backend for a public facing React app that will be available at a later date.

Captured in this backend `c#` .NET app will be Admin/Critic login credentials, data for each movie object, and a place for critic's to leave their reviews.

## Tech Stack
- **Language:** C#
- **Framework:** ASP.NET Core Web API (.NET 10.0)
- **Database:** mysql/server/2022 - azure-storage
- **User Authentication:** auth0

## Movie Data Captured

- Title
- Synopsis
- Genre
- Rating (G - NC-17)
- Runtime hours/minutes
- Amount of critic reviews
- Release date
- Now playing

## Critic Review Data

- Movie title
- Review
- Critic rating
- Created by
- Created date
- Pubished status

## Genres

- Genre name

## Audience Ratings

- Rating name

At this stage the fundamentals of CRUD will be implemented for proper showcasing of data.
All P/F keys are properly implemented for data integrity.
Images are given random GUIDs.
Roles and their limits are enforced.

## Styling and Branding

The placeholder name of "SilverScreen Marquee" is being used with a "Hollywood" colour palette (Silver, Crimson, Gold).
SilverScreen Marquee logo created.
Limelight font chosen for titles.

## Next Stage

In the coming sprints I will be implementing the public website where users can access the data that has been collected on the admin website.
