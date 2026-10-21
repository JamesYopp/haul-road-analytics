# Haul Road Analytics (practice repo)

Practice application for the TDD with AI course. It is a small .NET 10 app, inspired by haul truck telemetry, built up across four blocks. It contains synthetic data and no client code.

## How to use this repo

1. Read `SETUP.md` once.
2. Clone it. Do not push to it; it is read-only for participants.
3. For each block, branch from that block's start tag. Never work directly on `main` or a tag:

```
git clone git@github.com:JamesYopp/haul-road-analytics.git
cd haul-road-analytics
git remote set-url --push origin DISABLED
git switch -c block1-work block1-start
```

The `set-url --push` line turns off pushing for your clone on purpose.

For later blocks: `git fetch --tags` then `git switch -c block2-work block2-start`.

## Tags

| Tag | Meaning |
| --- | --- |
| `block1-start` | Solution file, core project, PRDs. No test project yet |
| `block1-setup-done` | Checkpoint: test project exists with one green smoke test |
| `block1-solution` | Reference solution built as red, green, refactor commits |

Tags starting `block1-demo` belong to the facilitator's live demo. You can ignore them.

Block 2 to 4 tags are added as those blocks are built.

## Stuck?

- Environment problem: `git switch -c block1-work-2 block1-setup-done`
- See the reference approach: `git log --oneline block1-setup-done..block1-solution`, then `git show <commit>`
- Compare your work with the reference: `git diff block1-work block1-solution -- csharp/`

The solution is one known-good answer, not the only one. AI output varies.

## Layout

```
prd/      base PRD and one feature PRD per block
docs/     language cards (C#, Python, JavaScript, Playwright)
csharp/   HaulRoad.sln, src/ (production code), tests/ (test projects)
```

## Your skills

Your team's skills are not in this repo. Clone the AI-Skills repo into a folder named `.agents` inside this project, from the repo root:

```
git clone git@github.com:Cascadia-Scientific-Inc/AI_Skills.git .agents
```

Your AI assistant reads the skills from `.agents/skills`. Using Gemini? Clone into `.gemini` instead. Using Claude? Use `.claude`.

`.agents` is a git repo of its own, nested here, and this repo ignores it. As you refine skills, commit and push from inside it (`cd .agents`, then the usual git commands). Do not run `git clean -x` in this repo: it deletes ignored folders, including uncommitted skill edits.
