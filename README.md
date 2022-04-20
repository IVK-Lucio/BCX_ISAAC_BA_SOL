# BCX_ISAAC_BA_SOL
Mini Job Portal using ASP.NET MVC


#Project guidelines and Specifications
General Specification
• Avoid static data for questions forms, questions, and answers
• Users submitting question forms should have their ‘user question form’ associations
tracked with a unique identifier i.e. email. This will allow us to see that piet@xyz.com
answer question 1, 2, and 3.
• The solution must be a question form, not a survey.
Question Form Area
• Use appropriate input controls for questions answers.
• Allow user to input their email before starting the question form
• Question that needs to be configured are the following:
o Question 1
▪ Where did you hear from us, and what do you think will make you a
great asset to the BCX Business Application Department?
▪ Respond text characters max length 1024
o Question 2
▪ How many software solutions did you write in your life?
▪ Respond with options
• 1 to 5
• 6 -25
• 26 -100
• 101 +
o Question 3
▪ Was it fun building a website for an interview?
▪ Response Yes/No
Question Form Area Bonus Criteria
• Add a timer that ends the user’s question form session after a [x] period.
• Render all website styles look and feel in a single CSS theme class.
Backend Portal Area
• Allow access to a separate backend admin portal area.
• No authentication required, just add a button anywhere to access this area
• Allow admin to review question form submissions
• Add button on each question form entry to export the question form with user answers
to JSON.
Backend Portal Area Bonus Criteria
• Add authentication for an admin user. i.e. username and password
• Statistics reports on how long the user takes to complete the survey.
• Allow admin to add more questions and answers to question form.
Framework Options
Option 1 - [ASP .NET MVC stack] [BCX PREFERENCE]
The solution must meet the criteria supplicated below. Each area has bonus criteria if you
want to impress us:
• The solution must use the ASP .NET MVC framework
• The solution must be written in C#
• The data must be stored in a MSSQL Database

