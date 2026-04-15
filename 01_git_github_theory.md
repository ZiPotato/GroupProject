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

## From User Stories to MVP

Before you start coding, build a large backlog first. In the first planning session, your goal is quantity, not perfection.

1. Write as many user stories as possible in 15-20 minutes.
2. Use the format: `As a [user], I want [goal], so that [benefit]`.
3. Avoid technical discussion at first. Focus on user needs and outcomes.
4. Merge duplicates and rewrite unclear stories.
5. Mark the stories needed for the first usable version as **MVP**.
6. Leave nice-to-have ideas for later with a stretch label.

Example user stories:

- `As a student, I want to add a task, so that I do not forget my homework.`
- `As a student, I want to mark a task completed, so that I can track progress.`
- `As a student, I want to filter tasks by status, so that I can focus on unfinished work.`

### Where to Find Labels in GitHub

- Open your repository on GitHub.
- Go to **Issues**.
- Click **Labels** near the top of the Issues page.
- Create labels such as `mvp`, `stretch`, `bug`, `docs`, and `technical task`.
- When you open or edit an issue, labels are available in the right sidebar under **Labels**.

Recommended label usage:

- `mvp` = required for the first working version
- `stretch` = useful, but only after MVP works
- `bug` = defect to fix
- `docs` = documentation work
- `technical task` = internal implementation or refactoring work

## Turning a User Story into Issues

One user story can become one issue or several smaller issues.

- If the story is small and clear, create one issue.
- If the story needs UI, logic, persistence, or testing work, split it into several issues.
- Link the smaller issues back to the original user story.

Practical workflow:

1. Create the user story issue first.
2. Add the `mvp` label if the story belongs to the first usable version.
3. Create 1-3 implementation issues from that story.
4. Move the implementation issues to **Backlog**.
5. Start work only when the issue has clear acceptance criteria.

## Issues and Tasks

Break down the work into small, testable issues. Each issue should:
- Describe a specific problem or feature
- Include acceptance criteria (how do we know it's done?)
- Be assigned to one person
- Include an estimate of effort if possible

Example user story issue:
```
## User Story: View All Order Data in One Place

As a user, I want to see all my order data in one form, so that I do not need to access email to view it all the time.

Why this matters:
- Users should be able to view their order information directly in the app instead of searching old emails.

Acceptance Criteria:
- User can open one view that shows their order data
- The view shows the key order details clearly
- Loading state is shown while data is being fetched
- Empty state is shown if no orders exist
- Error state is shown if loading fails

Labels:
- mvp

Possible child issues:
- Fetch order data
- Build order overview screen
- Handle loading, empty, and error states
- Add tests for order overview
```

Example implementation issue:
```
## Task: Build Order Overview Screen

Related user story: View All Order Data in One Place

Build the screen or form that lets the user view all order data in one place.

Acceptance Criteria:
- App has a dedicated order overview screen
- Screen shows the key order information clearly
- User can reach the screen from the main navigation
- Layout is readable on the target device size
- Merged via PR with review approval

Estimated effort: 2-4 hours
```

## Where to Find the Templates

This repository includes ready-made GitHub issue templates in:

- `.github/ISSUE_TEMPLATE/user-story.md`
- `.github/ISSUE_TEMPLATE/task-from-user-story.md`

In GitHub, these appear automatically when you click **New issue**. If you browse the repository files directly, open the same files from the `.github/ISSUE_TEMPLATE` folder.

If you later add a pull request template, GitHub looks for it in `.github/pull_request_template.md`.

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
