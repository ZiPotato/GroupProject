# Order Tracking System - Project Presentation - Raw

## What Does the application do?

Our application is a **order tracking system** that enables users to fetch shipping information directly from courier companies. The system allows customers to track their package deliveries by searching courier websites and retrieving real-time status updates.

### What problem gets solved with our application

The application solves the fragmentation problem of package tracking—instead of customers visiting multiple courier websites individually, our system provides a unified interface to search and monitor packages from different logistics providers.


## Technology Stack

- **Backend** : C# (about 62% of codebase)
- **Frontend** : Blazor (web UI framework)
- **Styling** : CSS (about 23% of codebase)
- **Structure** : Clean Architecture pattern

---

## Work Division

Our team utilized **feature branch workflow** with pull requests for all major work.  
This means we basically generated a branch for every feature individually.

**Contributors :**
- **Aapeli** : Blazor components, notifications, UI styling
- **Aapo** : Blazor UI components, UI styling, file handling, PostiDTO
- **Vesa** : Architectural design, console development, UI styling, testing

**Workflow Practices we learned to use :**
- Create feature branches for each major feature
- Use pull requests to review and merge code
- Follow clean architecture principles with separation of concerns
- Regularly merge completed features to main branch

**Major development phases we went through :**
1. Console application version (MVP foundation)
2. Package testing and validation framework
3. Blazor web UI implementation
4. Component styling and UX improvements
5. Architectural refactoring and enhancements
6. File handling and data management

---

## Our approach to testing

The codebase includes dedicated test infrastructure alongside production code, with testing embedded throughout the development lifecycle.
The tests have changed and molded with the code to point they're unrecognizable from their original selves even though some of them share the same name.

And this is why the project incorporates multiple testing strategies :

1. **Unit Testing** : Test projects for package-related functionality
2. **Integration Testing** : Package tracking flow validation
3. **Manual Testing** : UI component testing through Blazor components

---

## What we learned

As a team we learned :

1. **Blazor development** : 
2. **Component Creation** : 
3. **UI Desing** : 
4. **Why terminology should be prediscussed** : For example still in this presentation we have been swapping between Parcel, Package and Order while describing the same thing. This could've been fixed early on with a simple meeting, but we did not do that.
