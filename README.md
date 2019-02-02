# SimpleDB
soft2019spring excercise 1 - simple database

1) Install dotnet core 2.2 on your computer 
https://dotnet.microsoft.com/download/dotnet-core/2.2

2) clone SimpleDB repo

3) navigate to ...\SimpleDB\SetData folder

4) run command 'dotnet restore' (this will fix depedencies)

5) run command 'dotnet build' (this will build the binaries)

6) run command 'dotnet run' (this will run the binaries)
   this command will execute the program that inserts data into the simple.bin fil.
   executing this program will also print an offset map after data has been inserted

7) navigate to ...\SimpleDB\GetData folder

8) run command 'dotnet restore' (this will fix depedencies)

9) run command 'dotnet build' (this will build the binaries)

10) run command 'dotnet run' (this will run the binaries)
   this command will execute the program that retrives data from the simple.bin fil   

-------------------------------------------------------------------------------
- This will(should) run on both linux and windows OS

- The source for SetData and GetData methods is located in ...\classlib\SimpleDB.cs

- The simpledb.bin file is located in ...\SimpleDB folder after SetData command is run.

- Both .dll files for SetData and GetData are included and are located ...\SimpleDB\(GetData or SetData)\bin\Debug\netcoreapp2.2

- The .dll files can be run directly with the command 'dotnet run FILENAME.dll' - this will require dotnet core runtime only
