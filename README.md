# SkiBookingAPI
API part of SkiBooking, backend to SkiBookingWeb
* .Net 7, runs on SQL server
* Dev environment is VS Studio 2022
* Default endpoint: https://localhost:7019/
* Default swagger endpoint (dev only): https://localhost:7019/swagger/index.html
* CORS is hardcoded in dev for the default SkiBookingWeb URL

# Getting Started
1. Ensure you have access to SQL Server
2. Run the file CreateDBSQL.txt on SQL Server, this will create the SkiBooking DB
3. Optional: run PopulateTestSQL.txt on the SkiBooking DB
4. Create a user for the SkiBooking DB
5. Update appsettings.json / ConnectionStrings / DefaultConnection with the above connection details

# Design descisions
* Separate API and website so they can be rolled out separately and that Vue and .Net code don't interfere with each other
* Basic endpoints
* See SkiBookingWeb for a detailed list of descisions

# Limitations / Further Enhancements
* No authentication / restrictions on API use, can definitely be added down the track
* There's only one basket - as we don't have a way of distinguish users / sessions, this hasn't been built
* Nothing happens after adding things to the basket
* Can definitely enhance many things here, such as:
  - Relationship between packages & the other things
  - Date range validity
  - A real pricing model
  - ... etc...
