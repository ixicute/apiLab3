using SkolprojektLab3.Models;

namespace SkolprojektLab3.Data
{
    public class Seed
    {
        public static async void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                if (!context.Users.Any())
                {
                    await context.Users.AddRangeAsync(new List<User>
                    {
                        new User
                        {
                            FirstName = "Aldor",
                            LastName = "B",
                            PhoneNumber = "0731234565",
                        },
                        new User
                        {
                            FirstName = "Oskar",
                            LastName = "U",
                            PhoneNumber = "0736547887",
                        },
                        new User
                        {
                            FirstName = "Emma",
                            LastName = "H. W",
                            PhoneNumber = "0747893848",
                        },
                    });
                    await context.SaveChangesAsync();
                }

                if (!context.Interests.Any())
                {
                    await context.Interests.AddRangeAsync(new List<Interest>
                    {
                        new Interest
                        {
                            Title = "Fiske",
                            Description = "Fiska abborre är det bästa som finns!"
                        },
                        new Interest
                        {
                            Title = "Läsa böcker",
                            Description = "Pesten av Albert Camus är bäst!"
                        },
                        new Interest
                        {
                            Title = "koda!",
                            Description = "C# och .NET är bästa som finns!"
                        },
                        new Interest
                        {
                            Title = "Klättra",
                            Description = "Klättrar berg som en riktig karl!!"
                        },
                        new Interest
                        {
                            Title = "Cykla",
                            Description = "Det är ju viktigt för hälsan att man rör på sig!"
                        },
                        new Interest
                        {
                            Title = "Fiske",
                            Description = "ready or not gäddor, here i come!!"
                        },
                    });
                    await context.SaveChangesAsync();
                }

                if (!context.UserInterestsRelationshipTable.Any())
                {
                    await context.UserInterestsRelationshipTable.AddRangeAsync(new List<UserInterestRT>
                    {
                        new UserInterestRT
                        {
                            FK_UserId = 1,
                            FK_InterestId = 1
                        },
                        new UserInterestRT
                        {
                            FK_UserId = 1,
                            FK_InterestId = 2
                        },
                        new UserInterestRT
                        {
                            FK_UserId = 1,
                            FK_InterestId = 3
                        },
                        new UserInterestRT
                        {
                            FK_UserId = 2,
                            FK_InterestId = 4
                        },
                        new UserInterestRT
                        {
                            FK_UserId = 2,
                            FK_InterestId = 6
                        },
                        new UserInterestRT
                        {
                            FK_UserId = 3,
                            FK_InterestId = 5
                        },
                    });
                    await context.SaveChangesAsync();
                }

                if (!context.LinksRelationshipTable.Any())
                {
                    await context.AddRangeAsync(new List<LinkRT>
                    {
                        new LinkRT
                        {
                            FK_InterestUserId = 1,
                            Link = "www.fiske.se"
                        },
                        new LinkRT
                        {
                            FK_InterestUserId = 1,
                            Link = "www.sportfiske.se"
                        },
                        new LinkRT
                        {
                            FK_InterestUserId = 2,
                            Link = "www.biblioteket.se"
                        },
                        new LinkRT
                        {
                            FK_InterestUserId = 3,
                            Link = "www.edugrade.se"
                        },
                        new LinkRT
                        {
                            FK_InterestUserId = 4,
                            Link = "www.klättrahögt.se"
                        },
                        new LinkRT
                        {
                            FK_InterestUserId = 2,
                            Link = "www.adlibris.com"
                        },
                        new LinkRT
                        {
                            FK_InterestUserId = 5,
                            Link = "www.störrefiskar.se"
                        },
                        new LinkRT
                        {
                            FK_InterestUserId = 5,
                            Link = "www.gäddfiske.se"
                        },
                    });
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
