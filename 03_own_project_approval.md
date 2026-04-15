# Path B: Your Own Project and Approval Criteria

If you choose Path B, your team will propose a small software project for instructor approval. This path gives you more freedom, but it also requires realistic planning and a clear scope.

## Phase 1: Project Proposal

**Timeline:** Early in Week 1 (e.g., Monday-Tuesday)

**Format:** 10-minute pitch + 1-page written proposal

**Proposal Template:**

```
# Project Name

## Problem and Target User

- What problem does this solve?
- Who is the user?
- Example: "Team members want to share task lists without using email mailboxes"

## Scope (MVP: Minimum Viable Product)

- What are the 3-4 core features?
- What is explicitly NOT in scope?
- Example: "Features: add task, list tasks, delete task, mark complete. 
  Not in scope: notifications, user login, cloud sync"

## Technical Choices

- What technology will you use? (e.g., C# console, WinForms, WPF)
- Where will data persist? (file, database, memory)
- What libraries or frameworks do you need?

## Feasibility Assessment

- Estimate hours per team member
- Can this realistically be done in 3-4 weeks by 2-3 people?

## Team Structure

- Who will build which part?
- Who owns which feature area?
```

## Phase 2: Instructor Approval

The instructor reviews your proposal against these criteria:

### Approval Checklist (all must be checked)

| Criterion | Definition | Approved? |
|-----------|------------|-----------|
| Problem and user are clear | We understand what's being built and for whom | ☐ |
| Scope is appropriate | MVP fits into 3-4 weeks, not too big or small | ☐ |
| Technical choices are feasible | Team has skills or ability to learn | ☐ |
| At least 3 core features | Not just data entry; real functionality | ☐ |
| Data persistence | Data survives between sessions (file or database) | ☐ |
| Error handling | App doesn't crash on bad input | ☐ |
| Testing plan exists | Team defines unit tests for core logic and functional tests for key user flow | ☐ |

### Process Requirements (Path B same as Path A)

- GitHub repository and code shared via feature branches
- GitHub Projects board (Backlog, In Progress, Review, Done)
- Issues broken into concrete, testable tasks
- Pull request review (minimum 1 approval before merge)
- Main branch always deployable
- Every team member has visible contributions (commits, reviews, issues)
- Testing is required: include unit tests for core logic and functional tests for the main workflow

---

## Phase 3: Planning After Approval

Once the instructor approves your project, your team can:

1. **Create the GitHub repository** (linked to GitHub Classroom)
2. **Set up GitHub Projects board** with columns:
   - Backlog
   - In Progress
   - Review
   - Done
3. **Break MVP into issues** and add to Backlog
   - Each issue describes a concrete, testable task
   - Example: "Create ConsoleUI class with menu display" (not "build UI")
4. **Prioritize and plan sprints** (e.g., 1 week sprint)
5. **Assign roles:** who is Project Lead, who reviews code, who integrates. This can be difficult and teacher helps with the project lead, but it is advisable to rotate roles during the project.

---

## Project Ideas by Category

If you're unsure what to build, consider ideas from these categories:

### Study and Productivity Tools

- Study schedule planner with reminders
- Task list with priorities and categories
- Deadline calendar with notes
- File organizer with tagging system
- Grade calculator for classes
- Note-taking app with search

### Games and Simulations

- Text-based adventure game
- 20 Questions game
- Trivia game with scoring
- Card game simulator
- Dice game simulator
- Simple puzzle game

### Utilities

- Personal budget tracker
- Inventory manager (warehouse, bookshelf, pantry)
- Reservation system for equipment or rooms
- Event scheduler
- Contact manager
- Recipe collection with search

### Collaborative Apps

- Shared task board (no network, file-based)
- Feedback or idea suggestion box
- Club or group manager (members, dues, events)
- Message or comment system
- Voting or polling app

### Data Processing

- CSV file reader and analyzer
- JSON log parser with reports
- Sales or user data analyzer
- Email list manager
- Data transformation tool
- Report generator

---

## What If Your Project Gets Rejected?

If the instructor asks for changes:

1. **Refine and resubmit:** Narrow the scope, clarify the problem, or pick a simpler idea
2. **Switch to Path A:** E-Bike Repair Requests is always approved and ready to start
3. **Pick a different idea:** Use one of the suggestions above

This approval step is there to help your team avoid a project that is too large or too difficult.

---

## Grading

Your work is evaluated using the rubric in [04_grading_rubric.md](04_grading_rubric.md).

Path B is graded on the same rubric as Path A, but is more challenging. You're expected to handle more complex planning, broader scope, and deeper technical decisions. Bonus points reward innovation and extra features.

---

## Tips for Success

- **Start small:** You can always add features. Starting too big gets you stuck.
- **Define "done" upfront:** Write acceptance criteria for each issue before coding.
- **Communicate:** Check in daily. Misalignment is the enemy of teamwork.
- **Test early:** Don't wait until Week 4 to discover bugs.
- **Leave margin:** Plans never go perfectly. Build in buffer.
