# Package Tracking System - Project Presentation

## What Does the application do?

Our application is a **package tracking system** that enables users to fetch shipping information directly from courier companies. The system allows customers to track their package deliveries by searching courier websites and retrieving real-time status updates.

### What problem gets solved with our application

The application solves the fragmentation problem of package tracking—instead of customers visiting multiple courier websites individually, our system provides a unified interface to search and monitor packages from different logistics providers.


## Technology Stack

The project is built using modern C# technologies:
- **Backend**: C# (about 62% of codebase)
- **Frontend**: Blazor (web UI framework)
- **Styling**: CSS (about 21%)
- **Structure**: Clean Architecture pattern

---

## Work Division & GitHub Collaboration

Our team utilized **feature branch workflow** with pull requests for all major work:

**Key Contributors:**
- **Vesa**: Architectural design, console development, UI styling, testing
- **Aapo**: Blazor UI components, file handling, PostiDTO
- **Aapeli**: Blazor components, notifications, UI styling

**Workflow Practices:**
- Create feature branches for each major feature
- Use pull requests to review and merge code (85+ PRs created)
- Follow clean architecture principles with separation of concerns
- Regularly merge completed features to main branch

**Major Development Phases:**
1. Console application version (MVP foundation)
2. Package testing and validation framework
3. Blazor web UI implementation
4. Component styling and UX improvements
5. File handling and data management
6. Architectural refactoring and enhancements

---

## Testing Approach

The project incorporates multiple testing strategies:

The codebase includes dedicated test infrastructure alongside production code, with testing embedded throughout the development lifecycle.

1. **Unit Testing**: Test projects for package-related functionality
2. **Integration Testing**: Package tracking flow validation
3. **Manual Testing**: UI component testing through Blazor components
4. **Continuous Validation**: Regular commits and pull request reviews ensure code quality


