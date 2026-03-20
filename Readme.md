## Disclaimer
This project is for **educational purposes only** as part of a university 
assignment. Mortal Kombat X and all related images, characters, and assets 
are trademarks of **Warner Bros. Interactive Entertainment** and 
**NetherRealm Studios**. No copyright infringement intended.

Some images used in this project were sourced from Google Images and belong 
to their respective owners. No copyright infringement intended.

# Mortal Kombat Chat - Online Gaming Lobby

A distributed online gaming lobby system for Mortal Kombat X built using 
.NET Windows Communication Foundation (WCF) and Windows Presentation 
Foundation (WPF).

## Features
- User login with unique username validation
- Create and join lobby rooms
- Real-time lobby room messaging
- Private messaging between players
- Image and file sharing within lobby rooms
- Multi-threaded message and room updates
- Player logout

## Project Structure
- **DataServer** – Manages data storage and retrieval
- **BusinessServer** – Handles business logic and WCF service operations
- **Client** – WPF front-end application for players

## Getting Started

### Prerequisites
- Visual Studio 2022
- .NET Framework

### Running the Application
This project requires **multiple startup projects** in the following order:

1. Go to **Solution** in Solution Explorer → right-click → **Properties**
2. Select **Multiple Startup Projects**
3. Set the following projects to **Start** in this order:
   - `DataServer`
   - `BusinessServer`
   - `Client`
4. Click **Apply** → **OK**
5. Press **F5** to run
