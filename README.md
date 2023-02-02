# CardGame

![image sample](./img/sample.png)

A simple ASP NET 6 API round based game where players draw cards until first player gets a matching card pair, e.g. two kings, two aces, etc.

The game connects to https://deckofcardsapi.com/ API service for serving cards.

Core features include:
1. New player creation.
2. Game creation with set of players.
3. A simple round simulation returning game statistics, e.g. if game has a winner and rounds played.

## Docker Image

To access the the latest docker image see GitHub Container Registry [here](https://github.com/ralphfe/CardGame/pkgs/container/cardgame).

## Build and Run Locally

1. Make sure that [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) is installed
2. Clone project and via terminal navigate to project root directory, e.g. `cd ./CardGame`
3. Using dotnet CLI, run command `dotnet run --project src/CardGame.API --configuration Debug` and wait until the api server is built and initialized.
4. Open swagger UI to test the API [on localhost swagger UI](https://localhost:7163/swagger) 
5. Please note that server is running on `https`, therefore a valid dev certificate is required. See [dotnet dev-certs](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-dev-certs) if generating a new certificate is required.