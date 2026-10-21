# Setup

Allow 15 minutes. Do this before Block 1 if you can.

## Required

- .NET 10 SDK (LTS). Check with `dotnet --list-sdks`. The projects target `net10.0`. .NET 8 and 9 reach end of support on November 10, 2026, so the course uses .NET 10. An existing .NET 8 install can stay; versions coexist.
- Git
- A GitHub account with access to the lab repo and the team AI-Skills repo, and an SSH key added to that account. Both repos are private, so clone with SSH URLs. Check with `ssh -T git@github.com`: it should greet you by your username.
- VS Code with the C# Dev Kit extension
- Your AI assistant (Copilot by default) signed in inside VS Code
- Access to the team AI-Skills repo (you clone it into the lab project during setup)

## Verify

```
git clone git@github.com:JamesYopp/haul-road-analytics.git
cd haul-road-analytics
git remote set-url --push origin DISABLED
git switch -c block1-work block1-start
dotnet build csharp/HaulRoad.sln
```

The third command turns off pushing for your clone, so the shared repo stays clean. Any `git push` from it will fail on purpose.

The build should succeed with no warnings. If it does not, tell the facilitator before class.

## Add your team's skills

From inside the lab repo:

```
git clone git@github.com:Cascadia-Scientific-Inc/AI_Skills.git .agents
```

This puts your skills at `.agents/skills`, where Copilot looks. Gemini users clone into `.gemini` instead; Claude users into `.claude`. The folder is a separate git repo that this repo ignores. Commit skill changes from inside it.

## Troubleshooting

**VS Code says "The .NET SDK cannot be located" (Mac).** VS Code was started with a different PATH than your terminal. Fix it once:

1. Open VS Code, press Cmd+Shift+P, and run `Shell Command: Install 'code' command in PATH`.
2. Quit VS Code completely (Cmd+Q).
3. In a terminal, from the lab folder, run `code .`

After a restart, opening VS Code from the Dock also works.

## Optional

Python and JavaScript participants: your language card is in `docs/language-cards/` in this repo. The practice app is C#, but the loop is the same.
