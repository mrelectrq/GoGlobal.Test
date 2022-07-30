# Requirements : 
# 1. dotnet 6 version and EF tool.
# 2. NodeJS stable version and npm CLI 

#Run steps : 

1 Open terminal Go to {Project path}/GoGlobal.Test.Backend/GoGlobal.Test.Data 
      and run command : dotnet ef database update
      
2 Got to {Project path}/GoGlobal.Test.Backend/GoGlobal.Test.Backend
      and run following command: dotnet run  
      Application should listen http://localhost:5100
      
3 Go to {Project path}/GoGlobal.Test.Backend/GoGlobal.Test.FrontEnd
     run command : npm update and then npm start

Using page http://localhost:4200 u can login as user using credentials: 
        username: simpleuser
        password: Test1234$
