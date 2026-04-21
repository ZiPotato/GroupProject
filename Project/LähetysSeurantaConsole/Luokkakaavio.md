## Model
- Customer
	 - Customer :
		Will handle the customer information
- Package
	- Package : 
		Will handle the "Seurantatunnus" given by the presenter and turn it into an usable object with provided information

## View
- UI : 
   Nothing to worry about yet, all we need to do is actually decide what information and in what form will we gather from the user.

## Presenter
- Each Presenter class will handle the given information so the Model doesn't need to worry about altering / parsing the data.
	- CustomerHandling : 
	    Handles user given customer information and make sure that it has a chance of being correct data without actually checking the database yet. Finally delivers the correct format data to the customer class for examplr : 
		
		1. The first and last name are capitalized
		2. The Email has @ and the .com / whatever
	
	- PackageIDHandling : 
	    Will handle the package ID and make sure that the format is atleast close to correct and deliver the string set to Upper to the model

## Interfaces
- ICustomer
	- Customer information.
- IPackage
	- Package ID
	- Way to call the method to get a parcel.
- IView
  	- Way to get the ID from the user
  	- Way to display the parcel to the user
