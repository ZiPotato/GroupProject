# Basis for creating new DTO files

1. Create an internal static class "FirmDTO".  
2. Create a `public static Parcel ToParcel(string json)`   
The function of this method is to turn the json into the selected DTO.  
To do this we need to create a `internal sealed record Response` which will be used to extract the information given to us by the Json in the format it was intended. This will be done through:  
`[JsonProperty("The name of the variable being extracted currently")]`
3. When these variablehave been extracted, we modify them to make sure that they fit the *Parcel* in the intended way. I personally just created `DTOtoParcel()` method in which I set the Parcel information.
4. Finally go to the CompanyDTO and add the added company to the DTOHandle switch.

## Things you should do before adding a DTO

You should first find the documentation for the API you want to call and find out what the xml information will be.  

Then you should try to create for example a simple .txt file in which you try to match the given data with the Parcel.  
Make sure to write down the number codes and what they mean before you actually start writing them into the code, these will take more time than you'd think especially if you cannot just basically copy paste them into the file.

## Another method

If you provide copilot with the documentation it should be able to create the translator pretty easily, but then you risk not knowing how your DTO works.  
I would suggest segmented creation if you want to use copilot,  
For example I personally used copilot to create the number translation for the MatkahuoltoDTO,  
because it is just string information that doesn't matter that much yet. 
