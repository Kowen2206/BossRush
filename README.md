# Pixel Maya Project Conventions

### CaseStyles:
-------------
- ##### PascalCase:
	Use uppercase for every first letter of every word of the variable name.

- ##### camelCase:
	Use lowercase for the first letter of the first word and upper case for the first letter of every other word.
- ##### SNAKE_CASE: 
	Use all caps with an underscore in between every word.

### Scripting:
-------------
   - ##### Public Variables:
     camelCase
	
   

	public int numberVariable;

   - ##### Private Variables: 
     _camelCase with an underscore at the start.
 

	private int _numberVariable;

- ##### Constants:
    SNAKE_CASE
   

		const int NUMBER_CONSTANT;

- ##### Methods:
    PascalCase.


	    private void MyMethod(  )
		{
		 //code
	    }

- ##### Classes:
    PascalCase.
    


	    public class MyClass : MonoBehaviour
		{
		  //code
	    }

- ##### Events:
    PascalCase, name starting with On(NameOfTheEvent)Event.
   

         public UnityEvent OnJumpEvent;

### Github:
-------------
#### Branchs:

- ##### Branch "Main"
	This branch will have the last finished features, and only will be merged once the development code is ready and the developers team is shure their code wont cause any 	conflict or bug.

- ##### Branch "Develpment"
	The whole team will work under this branch
	This branch will be used to proof that the new features won't cause any kind of conflict with the rest of code when you merge it.


- ##### Feature branchs:
	Every feature will have their own branch using the next nomenclature, this branchs will be merge just by a pull request
 

    	FeatureFeatureName