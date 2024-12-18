How to run the project

Development Mode
1. Go to ICMS folder under project directory
2. Run "npm install"
3. Run "npm audit" to review if any dependent packages have security vulnerabilities
4. Run "ng build" for one time build. Run "ng build --watch" if you want continous build on each change.
5. Open ICMS.sln file in Visual Studio
6. Build/ Run project

Production Mode
1. Go to ICMS folder under project directory
2. Run "npm install"
3. Run "ng build --prod --output-hashing none"
4. Open ICMS.sln file in Visual Studio
5. Right click on scripts/libs folder and select "Exclude from project" in ICMS project
6. Refresh all files from solution explore refresh icon (to see libs files compiles in production mode)
7. RIght click on scripts/libs folder and select "Include in project" in ICMS project
8. Run project