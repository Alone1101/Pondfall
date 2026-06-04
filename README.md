<div align="center">

# Pondfall
### A Narrative Resource-Management Game

[![Unity](https://img.shields.io/badge/Unity-2022.3-000000?style=for-the-badge&logo=unity&logoColor=white)](https://unity.com/)
[![C#](https://img.shields.io/badge/C%23-Scripts-239120?style=for-the-badge&logo=csharp&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![Platform](https://img.shields.io/badge/Platform-Windows-0078D4?style=for-the-badge&logo=windows&logoColor=white)](https://github.com/Alone1101/Pondfall/releases)
[![Course](https://img.shields.io/badge/COMP3218-Storytelling%20in%20Games-6A0DAD?style=for-the-badge)](https://github.com/Alone1101/Pondfall)

*You are the Spirit of the Pond. The frogs need a king. Every choice you make will cost you something.*

</div>

---

## About

Pondfall is a fast-paced ecological decision-making game built for **COMP3218: Storytelling in Games** at the University of Southampton Malaysia. The player assumes the role of the **Spirit of the Pond**, managing a fragile frog civilization through a series of binary choices while balancing four interconnected resources.

The game is a spiritual retelling of Aesop's fable *The Frogs Who Desired a King* — where the lesson of responsibility unfolds differently depending on how wisely you steward the pond.

---

## Gameplay Loop

Every turn, the pond presents an environmental event (algae blooms, warming waters, etc.). The player makes a binary choice, and every decision trades one resource for another. The passive effects of the current king layer on top, constantly shifting the balance. When frog discontent peaks, the story branches.

```
START → Event → Choice → Consequence (stat change) → Check threshold?
           ↑                                                  │ No
           └──────────────────────────────────────────────────┘
                                                              │ Yes
                                                     Corresponding Ending
```

---

## The Four Stats (1–100)

| Stat | Description | Lose if... |
|---|---|---|
| **Stability** | Health of the pond ecosystem | Falls to 0 → collapse |
| **Discontent** | Frog civilization happiness | Reaches 80 for 2 turns → king change |
| **Favor** | Your spiritual influence | Too low → can't unlock better endings |
| **Population** | Number of frogs in the pond | Falls to 0 → game over |

---

## The Four Kings

Each king phase applies passive stat changes every turn, creating a different management challenge.

| King | Phase | Passive Effect | The Challenge |
|---|---|---|---|
| **The Log King** | Boredom | Stability +3, Discontent +5 | Pond is secure, but frogs grow restless |
| **The Eel King** | Chaos | Stability −8, Discontent −5 | Frogs love the freedom, ecosystem suffers |
| **The Heron King** | Tyranny | Discontent −10, Population −8 | Frogs are too terrified to complain — they're being eaten |
| **The Frog Council** | Volatility | Stability ±8, Discontent ±12 (random) | Wild swings as the frogs learn self-governance the hard way |

> King changes are triggered when **Discontent ≥ 80 for 2 consecutive turns** — *"The frogs pray for a new king!"*

---

## 🏁 The Six Endings

| # | Ending | Quote | Requirements |
|---|---|---|---|
| 1 | **The Tyrant's Feast** | *"In seeking order, you found only extinction."* | Reach Heron King phase → Population ≤ 0 |
| 2 | **Accelerated Collapse** | *"Your pond was too weak to survive the frogs' demands."* | Stability ≤ 0 at any time |
| 3 | **The Republic** | *"True betterment comes from responsibility, not changing rulers."* | Trigger prayer event + Stability ≥ 50 + Favor ≥ 40 + Survive 8 turns in Council phase |
| 4 | **Perfect Harmony** | *"The pond thrives in perfect balance."* | Achieve The Republic + Stability ≥ 70, Discontent ≤ 20, Population ≥ 50 for 3 consecutive Council turns |
| 5 | **Eternal Stagnation** | *"The frogs never complain, grow, and change."* | Keep Discontent ≤ 60 for 10 consecutive Log phase turns + never trigger a prayer event |
| 6 | **The Silent Pond** | *"The pond, once lively, now holds only echoes."* | Population ≤ 0 |

---

## Running the Game

1. Download the latest game archive from [**GitHub Releases**](https://github.com/Alone1101/Pondfall/releases).
2. Extract the downloaded `.zip` folder to your local machine.
3. Open the folder and run `Pondfall.exe` (No installation required).

To open the project in Unity:

```
Unity 2022.3 LTS or later
Open project from the root folder
```

---

## Team

| Member | Contributions |
|---|---|
| **Wong Jin Xuan** | **Core Systems & Assets (Lead):** Developed the Story Engine (70%), Mechanics & Controls (60%), and Graphics/Audio prompt engineering (70%). Co-developed game flow feedback systems (50%). |
| **Shaun Lim Jun Xian** | **Design & Pacing (Lead):** Designed Player Agency & event branches (70%), Storytelling techniques/content (70%), and Pacing/typewriter systems (60%). Co-developed UI elements & scene transitions (50%). |

Note on Git History: This project was actively developed using Unity Cloud for version control during production. The repository was migrated to GitHub post-completion for portfolio presentation, which is why the commit history reflects a single uploader.

---

## Course

**COMP3218 – Storytelling in Games**
University of Southampton Malaysia (UoSM) 2025/26

---

<div align="center">

![Unity](https://img.shields.io/badge/Unity-000000?style=flat-square&logo=unity&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=flat-square&logo=csharp&logoColor=white)
![Windows](https://img.shields.io/badge/Windows-0078D4?style=flat-square&logo=windows&logoColor=white)

*Based on Aesop's fable · Built with Unity · COMP3218 Coursework 2*

</div>
