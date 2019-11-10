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
- Conway's Game Of Life
- A fully working game with rendering on console or/and web.

### Version 2
- LED Screen / Lights
- An IoT gadget that renders game to LED lights.

### Version 3
- Modular LED Lights
- A set of square LED light that can be combined together to shape the "world".