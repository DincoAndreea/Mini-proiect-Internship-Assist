# I.	Overview 
MyCookbooks is a recipe management system that allows users to create, modify, and delete cookbooks and recipes. Users can add images, nutrition facts, instructions, ingredients, and cooking time details. Recipes can be viewed for each cookbook or all cookbooks.

# II.	Getting Started
First, clone the repository for this app or download the source code zip file.
To run this app, SQL Server and Entity Framework are needed. 
To install the tools needed for Entity Framework, type in the following command line in the Package Manager Console : **dotnet tool install --global dotnet-ef**.
After configuring SQL Server on your machine, update the connection string in the appsettings.json file of the Server project to match your server name and user credentials.
To create the database, navigate to the Server directory using this command in the Package Manager Console: **cd Server**. Then update the database using this command: **dotnet ef database update**.
Run the app using this command: **dotnet watch run**.

More details about the project can be found in the **MyCookbooksDocumentation.pdf** file.
