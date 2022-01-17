using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scheduler.Models;
using Scheduler.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private ApplicationDbContext _dbContext;

        public DbInitializer(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext dbContext)

        {
            _roleManager = roleManager;
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public void Initialize()
        {
            //migrations if not applied
            try
            {
                if (_dbContext.Database.GetPendingMigrations().Count() > 0)
                {
                    _dbContext.Database.Migrate();
                }
            }
            catch
            {

            }
            //create roles if it does not exists

            if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Scheduler)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Physician)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_NP)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_PA)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_MA)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_RN)).GetAwaiter().GetResult();

                _dbContext.Locations.AddAsync(new Location 
                { Name = "Main Medical Center",
                  Street = "123 E. Main Ave.",
                  City = "Chicago",
                  State = "IL",
                  Zip = "60616",
                  CreatedDateTime = DateTime.Now,
                  IsDeleted = false
                                  
                }).GetAwaiter().GetResult();
                _dbContext.Locations.AddAsync(new Location
                {
                    Name = "South Medical Center",
                    Street = "123 South Ave.",
                    City = "Chicago",
                    State = "IL",
                    Zip = "60616",
                    CreatedDateTime = DateTime.Now,
                    IsDeleted = false

                }).GetAwaiter().GetResult();
                _dbContext.Locations.AddAsync(new Location
                {
                    Name = "West Medical Center",
                    Street = "123 West Ave.",
                    City = "Chicago",
                    State = "IL",
                    Zip = "60616",
                    CreatedDateTime = DateTime.Now,
                    IsDeleted = false

                }).GetAwaiter().GetResult();
                _dbContext.SaveChanges();


                var mainLoc = _dbContext.Locations.FirstOrDefault(l => l.Name =="Main Medical Center");
                var southLoc = _dbContext.Locations.FirstOrDefault(l => l.Name == "South Medical Center");
                var westLoc = _dbContext.Locations.FirstOrDefault(l => l.Name == "West Medical Center");


                //if roles does not exists create admin user
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "administrator@scheduler.com",
                    Email = "administrator@scheduler.com",
                    FirstName = "Admin",
                    LastName = "Admin",
                    EmailConfirmed = true,
                    LocationId = mainLoc.Id

                }, "Admin123$").GetAwaiter().GetResult();

                ApplicationUser user = _dbContext.ApplicationUsers.FirstOrDefault(u => u.Email == "administrator@scheduler.com");
                _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();

                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "ma@scheduler.com",
                    Email = "ma@scheduler.com",
                    FirstName = "Medical",
                    LastName = "Assistant",
                    EmailConfirmed = true,
                    LocationId = westLoc.Id

                }, "User123$").GetAwaiter().GetResult();

                user = _dbContext.ApplicationUsers.FirstOrDefault(u => u.Email == "ma@scheduler.com");
                _userManager.AddToRoleAsync(user, SD.Role_MA).GetAwaiter().GetResult();

                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "np@scheduler.com",
                    Email = "np@scheduler.com",
                    FirstName = "Nurse",
                    LastName = "Practitioner",
                    EmailConfirmed = true,
                    LocationId = mainLoc.Id

                }, "User123$").GetAwaiter().GetResult();

                user = _dbContext.ApplicationUsers.FirstOrDefault(u => u.Email == "np@scheduler.com");
                _userManager.AddToRoleAsync(user, SD.Role_NP).GetAwaiter().GetResult();

                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "providermain@scheduler.com",
                    Email = "providermain@scheduler.com",
                    FirstName = "Physician",
                    LastName = "Main",
                    EmailConfirmed = true,
                    LocationId = mainLoc.Id

                }, "User123$").GetAwaiter().GetResult();

                user = _dbContext.ApplicationUsers.FirstOrDefault(u => u.Email == "providermain@scheduler.com");
                _userManager.AddToRoleAsync(user, SD.Role_Physician).GetAwaiter().GetResult();

                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "providerwest@scheduler.com",
                    Email = "providerwest@scheduler.com",
                    FirstName = "Physician",
                    LastName = "West",
                    EmailConfirmed = true,
                    LocationId = westLoc.Id

                }, "User123$").GetAwaiter().GetResult();

                user = _dbContext.ApplicationUsers.FirstOrDefault(u => u.Email == "providerwest@scheduler.com");
                _userManager.AddToRoleAsync(user, SD.Role_Physician).GetAwaiter().GetResult();

                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "providersouth@scheduler.com",
                    Email = "providersouth@scheduler.com",
                    FirstName = "Physician",
                    LastName = "South",
                    EmailConfirmed = true,
                    LocationId = southLoc.Id

                }, "User123$").GetAwaiter().GetResult();

                user = _dbContext.ApplicationUsers.FirstOrDefault(u => u.Email == "providersouth@scheduler.com");
                _userManager.AddToRoleAsync(user, SD.Role_Physician).GetAwaiter().GetResult();

                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "rn@scheduler.com",
                    Email = "rn@scheduler.com",
                    FirstName = "Registered",
                    LastName = "Nurese",
                    EmailConfirmed = true,
                    LocationId = southLoc.Id

                }, "User123$").GetAwaiter().GetResult();

                user = _dbContext.ApplicationUsers.FirstOrDefault(u => u.Email == "rn@scheduler.com");
                _userManager.AddToRoleAsync(user, SD.Role_RN).GetAwaiter().GetResult();

                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "registration@scheduler.com",
                    Email = "registration@scheduler.com",
                    FirstName = "Registration",
                    LastName = "User",
                    EmailConfirmed = true,
                    LocationId = mainLoc.Id

                }, "User123$").GetAwaiter().GetResult();

                user = _dbContext.ApplicationUsers.FirstOrDefault(u => u.Email == "registration@scheduler.com");
                _userManager.AddToRoleAsync(user, SD.Role_Scheduler).GetAwaiter().GetResult();

                _dbContext.CodeSets.AddAsync(new CodeSet { Name = "Appointment Type", Description = "Appointment Type" }).GetAwaiter().GetResult();
                _dbContext.CodeSets.AddAsync(new CodeSet { Name = "Order Type", Description = "Order Type" }).GetAwaiter().GetResult();
                _dbContext.CodeSets.AddAsync(new CodeSet { Name = "Schedule Type", Description = "Schedule Type" }).GetAwaiter().GetResult();
                _dbContext.CodeSets.AddAsync(new CodeSet { Name = "Appointment Status", Description = "Appointment Status" }).GetAwaiter().GetResult();
                _dbContext.CodeSets.AddAsync(new CodeSet { Name = "Location Type", Description = "Location Type" }).GetAwaiter().GetResult();
                _dbContext.CodeSets.AddAsync(new CodeSet { Name = "Document Status", Description = "Document Status" }).GetAwaiter().GetResult();
                _dbContext.CodeSets.AddAsync(new CodeSet { Name = "Document Type", Description = "Document Type" }).GetAwaiter().GetResult();
                _dbContext.CodeSets.AddAsync(new CodeSet { Name = "Administration Route", Description = "Administration Route" }).GetAwaiter().GetResult();
                _dbContext.CodeSets.AddAsync(new CodeSet { Name = "Administration Frequency", Description = "Administration Frequency" }).GetAwaiter().GetResult();
                _dbContext.CodeSets.AddAsync(new CodeSet { Name = "Administration Time", Description = "Administration Time" }).GetAwaiter().GetResult();
                _dbContext.CodeSets.AddAsync(new CodeSet { Name = "Order Status", Description = "Order Status" }).GetAwaiter().GetResult();
                _dbContext.CodeSets.AddAsync(new CodeSet { Name = "Gender", Description = "Gender" }).GetAwaiter().GetResult();
                _dbContext.CodeSets.AddAsync(new CodeSet { Name = "Ethnicity", Description = "Ethnicity" }).GetAwaiter().GetResult();
                _dbContext.CodeSets.AddAsync(new CodeSet { Name = "Race", Description = "Race" }).GetAwaiter().GetResult();

                _dbContext.SaveChanges();

                CodeSet cv = _dbContext.CodeSets.FirstOrDefault(c => c.Name == "Administration Frequency");
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "PRN (As Needed)", Description = "As Needed", CodeSetId = cv.Id }).GetAwaiter().GetResult();
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "Q2H (Every 2 Hours)", Description = "Q2H (Every 2 Hours)", CodeSetId = cv.Id }).GetAwaiter().GetResult();
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "QD (Everyday)", Description = "QD (Everyday)", CodeSetId = cv.Id }).GetAwaiter().GetResult();
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "QH (Every Hour)", Description = "QH (Every Hour)", CodeSetId = cv.Id }).GetAwaiter().GetResult();
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "QID (4 Times a Day)", Description = "QID (4 Times a Day)", CodeSetId = cv.Id }).GetAwaiter().GetResult();
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "TID (Three Times a Day)", Description = "TID (Three Times a Day)", CodeSetId = cv.Id }).GetAwaiter().GetResult();
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "BID (Twice a Day)", Description = "BID (Twice a Day)", CodeSetId = cv.Id }).GetAwaiter().GetResult();

                cv = _dbContext.CodeSets.FirstOrDefault(c => c.Name == "Administration Route");
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "CAP (Capsule)", Description = "CAP (Capsule)", CodeSetId = cv.Id }).GetAwaiter().GetResult();
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "GT (Drop)", Description = "GT (Drop)", CodeSetId = cv.Id }).GetAwaiter().GetResult();
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "OD (Right Eye)", Description = "OD (Right Eye)", CodeSetId = cv.Id }).GetAwaiter().GetResult();
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "OS (Left Eye)", Description = "OS (Left Eye)", CodeSetId = cv.Id }).GetAwaiter().GetResult();
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "PO (By Mouth)", Description = "PO (By Mouth)", CodeSetId = cv.Id }).GetAwaiter().GetResult();
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "PIL (Pill)", Description = "PIL (Pill)", CodeSetId = cv.Id }).GetAwaiter().GetResult();
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "TAB (Tablet)", Description = "TAB (Tablet)", CodeSetId = cv.Id }).GetAwaiter().GetResult();

                cv = _dbContext.CodeSets.FirstOrDefault(c => c.Name == "Administration Time");
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "HS (At Bedtime)", Description = "HS (At Bedtime)", CodeSetId = cv.Id }).GetAwaiter().GetResult();
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "AC (Before Meals)", Description = "AC (Before Meals)", CodeSetId = cv.Id }).GetAwaiter().GetResult();
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "PC (After Meals)", Description = "PC (After Meals)", CodeSetId = cv.Id }).GetAwaiter().GetResult();

                cv = _dbContext.CodeSets.FirstOrDefault(c => c.Name == "Appointment Status");
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "Admitted", Description = "Admitted", CodeSetId = cv.Id }).GetAwaiter().GetResult();
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "Confirmed", Description = "Confirmed", CodeSetId = cv.Id }).GetAwaiter().GetResult();
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "Cancelled (Appt)", Description = "Cancelled (Appt)", CodeSetId = cv.Id }).GetAwaiter().GetResult();
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "Discharged", Description = "Discharged", CodeSetId = cv.Id }).GetAwaiter().GetResult();

                cv = _dbContext.CodeSets.FirstOrDefault(c => c.Name == "Appointment Type");
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "Initial Visit", Description = "Initial Visit", CodeSetId = cv.Id }).GetAwaiter().GetResult();
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "Follow-Up Visit", Description = "Follow-Up Visit", CodeSetId = cv.Id }).GetAwaiter().GetResult();

                cv = _dbContext.CodeSets.FirstOrDefault(c => c.Name == "Document Status");
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "In-Progress", Description = "In-Progress", CodeSetId = cv.Id }).GetAwaiter().GetResult();
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "Final (Authorized)", Description = "Final (Authorized)", CodeSetId = cv.Id }).GetAwaiter().GetResult();

                cv = _dbContext.CodeSets.FirstOrDefault(c => c.Name == "Document Type");
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "Initial Assessment", Description = "Initial Assessment", CodeSetId = cv.Id }).GetAwaiter().GetResult();
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "Follow up Notes", Description = "Follow up Notes", CodeSetId = cv.Id }).GetAwaiter().GetResult();

                cv = _dbContext.CodeSets.FirstOrDefault(c => c.Name == "Ethnicity");
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "Hispanic or Latino", Description = "Hispanic or Latino", CodeSetId = cv.Id }).GetAwaiter().GetResult();
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "Mixed Ethnicity", Description = "Mixed Ethnicity", CodeSetId = cv.Id }).GetAwaiter().GetResult();
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "Non-Hispanic", Description = "Non-Hispanic", CodeSetId = cv.Id }).GetAwaiter().GetResult();
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "Not Specified", Description = "Not Specified", CodeSetId = cv.Id }).GetAwaiter().GetResult();

                cv = _dbContext.CodeSets.FirstOrDefault(c => c.Name == "Gender");
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "Unknown", Description = "Unknown", CodeSetId = cv.Id }).GetAwaiter().GetResult();
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "Male", Description = "Male", CodeSetId = cv.Id }).GetAwaiter().GetResult();
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "Female", Description = "Female", CodeSetId = cv.Id }).GetAwaiter().GetResult();

                cv = _dbContext.CodeSets.FirstOrDefault(c => c.Name == "Order Status");
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "Ordered", Description = "Ordered", CodeSetId = cv.Id }).GetAwaiter().GetResult();
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "On-hold", Description = "On-hold", CodeSetId = cv.Id }).GetAwaiter().GetResult();
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "Completed", Description = "Completed", CodeSetId = cv.Id }).GetAwaiter().GetResult();
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "Cancelled (Order)", Description = "Cancelled (Order)", CodeSetId = cv.Id }).GetAwaiter().GetResult();

                cv = _dbContext.CodeSets.FirstOrDefault(c => c.Name == "Order Type");
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "Medication", Description = "Pharmacy Orders", CodeSetId = cv.Id }).GetAwaiter().GetResult();
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "Laboratory", Description = "Laboratory Orders", CodeSetId = cv.Id }).GetAwaiter().GetResult();
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "Diagnostics", Description = "Diagnostic Orders", CodeSetId = cv.Id }).GetAwaiter().GetResult();
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "Nutrition Services", Description = "Nutrition Services", CodeSetId = cv.Id }).GetAwaiter().GetResult();
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "Consults", Description = "Consults", CodeSetId = cv.Id }).GetAwaiter().GetResult();
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "Therapy Services", Description = "Therapy Services", CodeSetId = cv.Id }).GetAwaiter().GetResult();
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "Charges", Description = "Charges", CodeSetId = cv.Id }).GetAwaiter().GetResult();
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "Medical Equipment", Description = "Medical Equipment", CodeSetId = cv.Id }).GetAwaiter().GetResult();

                cv = _dbContext.CodeSets.FirstOrDefault(c => c.Name == "Race");
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "White", Description = "White", CodeSetId = cv.Id }).GetAwaiter().GetResult();
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "Black or African American", Description = "Black or African American", CodeSetId = cv.Id }).GetAwaiter().GetResult();
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "Asian", Description = "Asian", CodeSetId = cv.Id }).GetAwaiter().GetResult();
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "Native Hawaiian/Pacific Islander", Description = "Native Hawaiian/Pacific Islander", CodeSetId = cv.Id }).GetAwaiter().GetResult();
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "Not Specified", Description = "Not Specified", CodeSetId = cv.Id }).GetAwaiter().GetResult();
                _dbContext.CodeValues.AddAsync(new CodeValue { Name = "American Indian/Alaska Native", Description = "American Indian/Alaska Native", CodeSetId = cv.Id }).GetAwaiter().GetResult();


                _dbContext.SaveChanges();


            }

            return;
        }
    }
}
