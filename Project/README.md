# About this project.

## What is this?

This project is a student group project that we created with the simple idea of wanting to fetch packages from API:s without going to the carrier sites individually.  
Currently instead of using the actual API:s, we simulate the Json data that we would get from them locally. Using the Json data we create Parcel records that we use to display the current state of the package.

## How to use the program?

Program can simply be run from the published folder by using the OrderTrackingBlazor.exe (Once we actually merge the published version). After this it should automatically open your browser to the program, but if it doesn't for one reason or another the default ip is "127.0.0.1:5161". This is the simple "localhost" without the local host approach and can be changed naturally in the blazor projects program.cs.  
Here you can simply put in your trackingID given to you by the carrier company usually through an email. If the carrier company is supported by our program you should be able to get a widget with its current information.

## Model

This is the backend of our program.

### Model responsibility

Use `Model` for:

- DTOs
- API response models
- request models
- domain data containers

### Maintenance

When adding a new model:

1. place it in this folder
2. give it a name that describes the data it represents
3. keep it independent from UI or infrastructure concerns
4. document any non-obvious JSON field mappings
5. update this README if the folder structure changes significantly

### Summary

The `Model` folder defines the application's data structures and provides a clean boundary between raw data and application behavior.


## Presenter

This is the part of our program that handles how data is shown in our console application.

### Presenter responsibility

Use `Presenter` for:

- formatting output for the user
- presenting model data in a readable way
- separating display logic from data logic
- preparing information before printing it

### Maintenance

When adding a new presenter:

1. place it in this folder
2. give it a name that describes what it presents
3. keep it focused on output and formatting
4. avoid putting data-fetching logic here
5. update this README if the folder structure changes significantly

### Summary

The `Presenter` folder defines how application data is displayed and provides a clean boundary between data and presentation.

## Blazor / View 

This part of the program is everything the user sees. It collects input, handles UI logic and shows information.
The blazor part of this program is its own project that depends on LähetysSeurantaConsole's classes (models).
Right now the Home page file is both the presenter and view in blazor.

### Blazor resposibility

Use `Blazor` for:

- Displaying information
- Collecting input
- All graphical components
- UI logic

### Maintenance

When adding a new blazor component:

1. place it in the blazor project's Components folder and from there, the components type folder (Pages, Widget)
2. name it appropriately
3. keep it independent of presenter or model
4. update this README if the folder structure changes significantly

### Summary

The `Blazor` project acts as a presenter aswell a view and uses LähetysSeurantaConsole's classes (models).



# TLDR

- Model: models data
- Presenter: presents data
- View: shows data

[Arkkitehtuurista](https://github.com/gmagana/clean-architecture-example-csharp?tab=readme-ov-file)
