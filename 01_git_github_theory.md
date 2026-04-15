# Git and GitHub: The Foundation of Teamwork

Git and GitHub are at the core of this course. This document explains how you'll work together and manage shared code. The same process applies to both Path A (E-Bike Repair Requests) and Path B (your own project).

## Why GitHub?

- **Shared history:** Every change is recorded with who made it and why
- **Safety:** Nobody accidentally overwrites someone else's work
- **Quality:** Pull requests let the team review changes before they're merged
- **Organization:** GitHub Projects keeps work organized and transparent

## Important Concepts

### Repositories

A repository is your project's home on GitHub. It contains all your code and its complete history.

### Branches

A branch is a separate line of work. Your main branch should stay stable and usable. New features are built on their own branches and merged into main when ready.

Common branch names follow this pattern:
- `feature/add-repair-request` for new features
- `bugfix/null-reference` for bug fixes
- `docs/readme-update` for documentation

### Commits

A commit is a snapshot of your work at a specific moment. Each commit should represent one logical change (e.g., "add RepairRequest class" not "random changes").

Good commit messages:
- Start with a verb: "Add", "Fix", "Update", "Remove"
- Be specific: "Add RepairRequest class with Status and CreatedAt" (good) vs "stuff" (bad)
- Keep it under 50 characters for the main message

### Pull Requests (PRs)

A PR is a request to merge your branch into main. It gives your teammates a chance to review the code, ask questions, and suggest improvements before the merge.

### Code Review

When reviewing a teammate's PR:
- Read the code carefully
- Check it compiles and runs
- Look for bugs, unclear logic, or style issues
- Be constructive: "This could be clearer" not "This is stupid"
- Approve only when you'd be comfortable using this code yourself

## The Feature Branch Workflow

This is how we work:

1. **Create a feature branch** from main: `git checkout -b feature/your-feature-name`
2. **Make your changes** and commit: `git add .` then `git commit -m "Your message"`
3. **Push to GitHub**: `git push origin feature/your-feature-name`
4. **Open a pull request** on GitHub
5. **Get reviewed** by a teammate
6. **Make changes** if requested, push again, and re-request review
7. **Merge** into main when approved
8. **Delete** the feature branch

## GitHub Projects

Your team will use a GitHub Projects board to organize work. It typically has these columns:

- **Backlog:** All planned tasks
- **In Progress:** Tasks being worked on right now
- **Review:** Pull requests waiting for approval
- **Done:** Completed and merged work

Move your issues through the board as you work. This keeps everyone informed.

## Issues and Tasks

Break down the work into small, testable issues. Each issue should:
- Describe a specific problem or feature
- Include acceptance criteria (how do we know it's done?)
- Be assigned to one person
- Include an estimate of effort if possible

Example issue:
```
## Create RepairRequest class

Create a RepairRequest class to represent a single repair request.

Acceptance Criteria:
- Class has IssueTitle (string), Status (enum), CreatedDate (DateTime)
- All properties are accessible but immutable except Status
- Code is documented with XML comments
- Merged via PR with review approval
```

## Handling Merge Conflicts

Conflicts happen when two people edit the same code. Here's how to handle them:

1. **Understand why:** Check what each person changed
2. **Decide which version is right:** Keep one, the other, or combine both
3. **Clean up:** Git will show conflict markers (`<<<<<<<`, `=======`, `>>>>>>>`). Edit the file to remove these and keep the right code
4. **Test:** Verify the code still works
5. **Commit and push:** `git add .`, `git commit -m "Resolve merge conflict in RepairRequestService"`, `git push`

Visual Studio 2022 has excellent tools to help. When you see a conflict, it offers visual conflict resolution on the file.

## Important GitHub Practices

- Always create a branch for new work; never commit directly to main
- Write descriptive PR titles and descriptions
- Request at least one review before merging
- Delete branches after merging to keep the repo clean
- Pull the latest main before creating a new branch: `git fetch origin` then `git checkout -b feature/name`

## Your First Day

1. Clone the repository
2. Create a `.gitignore` file (have Visual Studio ignore `bin/`, `obj/`, `.vs/` folders)
3. Create a GitHub Projects board
4. Break work into issues
5. Assign issues to team members
6. Create your first feature branches and open a PR
7. Review each other's code

This process may feel a bit slow at first, but it helps the team stay organized and reduces mistakes.
