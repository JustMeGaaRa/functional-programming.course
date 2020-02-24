# Game Of Life

## Table of Contents
- [Technical Stack](#tech-stack)
- [Roadmap](#roadmap)


## Technical Stack <a name="tech-stack"></a>

|Tier\Language  |F#                         |C#                         |TypeScript     |
|---            |---                        |---                        |---            |
|Store          |[ ] EventStore             |[ ] SQL Server             |-              |
|Engine         |[+] Pure F#                |[+] Pure C#                |[ ] Pure TS    |
|Api            |[ ] Giraffe/SignalR        |[+] ASP.NET Core/SignalR   |-              |
|Desktop        |[+] Console                |[+] Console                |[ ] Console    |
|Web            |[ ] Fable/Elmish           |[ ] ASP.NET Blazor         |[+] React.js   |
|Tests          |-                          |[+] xUnit                  |-              |

## Roadmap <a name="roadmap"></a>

### Version 1
- Fully working Conway's Game Of Life.
- Support for desktop and web.
- Interchangeable packages/layers (engine, API and web client) written in C#, F# and TypeScript.

### Version 2
- Rendering the game to LED light matrix
- Conect to LED light matrix via wireless network for rendering.

### Version 3
- Ability to merge two game world into single one.
- LED light matrix with magnets to detect two combined worlds.
