# Object-Oriented Programming: First Team Project

This is your first group project. You will work in a team of 2-3 students, use GitHub for collaboration, and build a small application together.

## Course Objectives

- Master Git/GitHub-based teamwork: feature branches, pull requests, code review, project management
- Learn shared code ownership: backlog management, issue breakdown, prioritization, version control
- Implement a working application together as a team, following your chosen path
- Develop conflict resolution skills, both technical (merge conflicts) and collaborative

## Course Structure

The course runs for 3-4 weeks and is organized as follows:

1. Week 1, Kickoff: Form teams, set up GitHub repo, approve project choice or select E-Bike Repair Requests path, plan issues
2. Weeks 2-3, Development: Develop features in sprints, participate in PR review cycles, checkpoint testing at midpoint
3. Week 4, Finalization: Final integration, quality assurance, demo, and retrospective

## Two Paths

### Path A: E-Bike Repair Requests (Beginner Level)

A small and clearly limited task that suits every team. It is a good option if you want to focus on teamwork and workflow.

- Minimum requirements: add repair requests, list requests, handle requests by status, and save or load from file
- Mandatory practices: feature branches, pull requests, at least one peer code review per team member, conflict resolution exercise
- Testing baseline: unit tests for core logic plus functional tests for main user flow
- Bonus challenges: JSON format, search/filter, priorities, estimated completion date, improved UI

See [02_ebike_repair_requests.md](02_ebike_repair_requests.md).

### Path B: Your Own Project (Challenge Level)

Your team proposes a smaller software project that the instructor approves. Perfect for those who want a bigger technical challenge and want to build their own idea.

- Approval criteria: clear problem statement and user, feasible within 3-4 weeks, meets technical and process requirements
- Project categories: study/productivity tools, games, utilities, collaborative apps, data processing, or any approved idea
- Mandatory practices: same as Path A, plus detailed issue planning, project board, and prioritization

See [03_own_project_approval.md](03_own_project_approval.md).

---

## Materials and Resources

- [01_git_github_theory.md](01_git_github_theory.md): Git and GitHub fundamentals plus team workflow process
- [02_ebike_repair_requests.md](02_ebike_repair_requests.md): Path A, e-bike repair requests assignment
- [03_own_project_approval.md](03_own_project_approval.md): Path B, own project, approval criteria, and project ideas
- [04_grading_rubric.md](04_grading_rubric.md): how your work will be evaluated

## C# Resources

C# course materials available at http://www.csharpcourse.com/

---

# Team Technical Setup Guide

## Creating SSH Keys for GitHub

GitHub changed its authentication requirements, and password-based login no longer works. We'll create SSH keys for secure authentication.

**Note:** If you can already log into GitHub normally, SSH keys are optional. Only follow these steps if GitHub won't let you in.

1. Open GitBash in your project folder (right-click in Windows Explorer, select "Open GitBash here")
2. Test your connection: `ssh -T git@github.com`. If you see "...You've successfully authenticated..." you're done.
3. Check for existing keys: `ls ~/.ssh`. If you see `id_rsa` and `id_rsa.pub`, skip to step 5.
4. Create new keys: `ssh-keygen -t rsa -b 2048`. Press Enter for each prompt (keep defaults).
5. List your keys: `ls ~/.ssh`. You should now see `id_rsa` and `id_rsa.pub`.
6. Display your public key: `cat ~/.ssh/id_rsa.pub`. Copy this entire key.
7. Go to GitHub SSH settings: https://github.com/settings/keys
8. Click the green "New SSH key" button.
9. Give your key a name in the "Title" field.
10. Paste your key into the "Key" field.
11. Click the green "Add SSH key" button.
12. Test again in GitBash: `ssh -T git@github.com`. You should see the authentication success message.

## Cloning Your Repository

You'll do this once at the start of the course. You received a link to GitHub Classroom, which created a repository for you. Select your name from the list.

1. Open GitBash in the folder where you want your project folder to live.
2. Clone your repository: `git clone [your-repo-url]` (copy the URL from the green "Code" button on GitHub, e.g., `git@github.com:YourOrganization/your-repo.git`)

## Fetching New Course Materials

Do this in GitBash in your project folder.

1. Check status: `git status`. Make sure you're on the main branch with no uncommitted changes (nothing red or green). If needed, save changes: `git add .` and `git commit -m "Save changes"`.
2. Add upstream (do this only once): `git remote add upstream https://github.com/course-organization/template-repo`
3. Fetch updates: `git pull upstream main --allow-unrelated-histories`
4. If merge conflicts occur, resolve them. Visual Studio 2022 has good conflict resolution tools.

## Submitting Your Work

Do this in GitBash in your project folder.

1. Check status: `git status`. You should see changes (shown in red).
2. Stage changes: `git add .`
3. Check status again: `git status`. Changes should now show in green.
4. Commit: `git commit -m "Submit assignment [name or number]"`. Include what you're submitting.
5. Push: `git push`

## Useful GitBash Commands

**cd** - change directory, e.g., `cd my-folder` moves into my-folder

**ls** - list files and folders in the current directory

**git status** - show current branch, which files changed (red), and which are staged (green)

**git log** - display the commit history of your current branch

**git log --graph --all --decorate** - visualize all branches and commits as a text graph

---

## Course License

This material is licensed under Creative Commons BY-NC-SA 4.0, so you're free to use and share it.
