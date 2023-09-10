# Tic-Tac-Toe Console Game

## Overview

This project is a simple console-based Tic-Tac-Toe game implemented in C#. The game allows two players to take turns to place their marks ("X" or "O") on a 3x3 board. The game supports basic scoring and board reset features.

## Features

- Turn-based gameplay for two players ("X" and "O")
- Board state evaluation to determine wins, ties, and invalid moves
- Scoring system to keep track of wins for both players
- Ability to reset the board for a new game
- Unit tests for game logic

## Installation

1. Clone this repository:

    ```
    git clone https://github.com/Thullner/TicTacToe.git
    ```

2. Navigate to the project directory:

    ```
    cd TicTacToe
    ```

3. Build and run the program:

    ```
    dotnet build
    dotnet run
    ```

## Usage

1. Run the application.
2. Follow the prompts to input the row and column numbers where you'd like to place your mark.
3. The game will automatically switch turns between the two players.
4. The game will announce the winner and update the scores accordingly.
5. Choose to play another round or exit the game.

## Running Tests

This project uses xUnit for testing. To run tests, navigate to the project directory and execute:

```
dotnet test
```

## Technology Stack

- C#
- .NET 6.0
- xUnit for testing
- Moq for mocking
