# The gameplan for the creation of our Blazor front.

While creating the blazor we should start by being aware of what we need to do.
1. We will need the home page.
2. We will need a way for customer to login and receive the previous packages etc. 
3. We will need to present the packages in a pleasant form.
4. We will need to make sure we don't make too many calls to any potential API we will use.
## Pages

As for the pages, we want to keep them to an minimum and we hope that the customer doesn't need to hassle too much.  
Ideally we will be able to do all of this in a singular page, but likely we will have to create a login page. We might be able to avoid this by using devices browser to store the cookie etc, but then the customer will lose the ability to swap between devices.

## Pleasant display

This is most likely plausible via creating a component that displays the data in a desired form.  

By doing this we need to figure out how will we create the components in the page without bringing the parcel list into the front view. This should be a fun challenge to think of, unless I am just too mentally tired to figure out the simple answer for it right now.

## Calls to the API

This would be easy to do with an authenticating cookie, since we could add timestamps to it and limit the times the refresh will be made. This can also be done with a thread that could be attached to the customer, but I can see this causing us a lot of problems long term.
