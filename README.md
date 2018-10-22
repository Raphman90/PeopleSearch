# PeopleSearch
Health Catalyst people search
The People Search Application

Business Requirements

The application accepts search input in a text box and then displays in a pleasing style a list of people where any part of their first or last name matches what was typed in the search box (displaying at least name, address, age, interests, and a picture). 
Solution should either seed data or provide a way to enter new users or both
Simulate search being slow and have the UI gracefully handle the delay
Technical Requirements

A Web Application using WebAPI and a front-end JavaScript framework (e.g., Angular, AngularJS, React, Aurelia, etc.) 
Use an ORM framework to talk to the database
Unit Tests for appropriate parts of the application


In order to run the application, you must use version 15.7.3 or newer of Visual studio 2017. set it up for .net development with c#

you will also need to down load the .Net Core 2.1 SDK if you have not already, found here: https://www.microsoft.com/net/download

NOTE: you may want to use a VM for this test as to not alter your Visual Studio environment.

after those two steps, you should open up the solution (PeopleSearch.sln) and press F5. The project should build and you should see the website. you may see an SSL certificate warning, but go ahead and accept/install the certificate.

NOTE: running the project for the first time may give you a 500 error. I found that deleting the project and redownloading it solved that problem.

on the page you will see a text box to enter your search criteria as well as a number of fields that are used to Add your own people to the database.

The search button will initiate the search, and the slow search button will imitate a longer load time for the search.

There are Three people seeded for the search for your convienince: Raphael Rosa, Arnold Schwarzenegger, and Jane Doe.

after completing a search, you are free to edit or delete people from the database. NOTE: if everyone is deleted from the database, the seed function will be called, replacing the original three entries.

I use the Entity Framework to act as an intermediary for the database, and Jquery to handle front end ajax calls.

in order to see the unit tests run you should be able to open up the PersonControllerTests in the PeopleSearchTest project, then selecting the test menu option, then run all tests.

Thank you for looking at my app, and I look forward to hearing feedback!

