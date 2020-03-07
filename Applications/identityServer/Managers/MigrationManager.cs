using System;
using System.Linq;
using System.Security.Claims;
// using ASPNETIdentityServer;
// using ASPNETIdentityServer.Contfigurations;
// using ASPNETIdentityServer.Data;
// using ASPNETIdentityServer.Data.SeedData;
// using IdentityService.Core.Model;
using IdentityModel;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
// using IdentityService.Core.Domain;

public static class MigrationManager
{
    public static IHost MigrateDatabase(this IHost webHost)
    {
        using (var scope = webHost.Services.CreateScope())
        {
            //identity server
            using (var persDBContext = scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>())
            {
                try
                {
                    persDBContext.Database.Migrate();
                    
                    using (var confDBContext = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>())
                    {
                        confDBContext.Database.Migrate();
                        // if (!confDBContext.Clients.Any())
                        // {
                        //     foreach (var client in IdentityServerConfig.Clients)
                        //     {
                        //         confDBContext.Clients.Add(client.ToEntity());
                        //     }
                        //     confDBContext.SaveChanges();
                        // }

                        // if (!confDBContext.IdentityResources.Any())
                        // {
                        //     foreach (var resource in IdentityServerConfig.Ids)
                        //     {
                              
                        //         confDBContext.IdentityResources.Add(resource.ToEntity());
                        //     }
                        //     confDBContext.SaveChanges();
                        // }

                        // if (!confDBContext.ApiResources.Any())
                        // {
                        //     foreach (var resource in IdentityServerConfig.Apis)
                        //     {
                        //         confDBContext.ApiResources.Add(resource.ToEntity());
                        //     }
                        //     confDBContext.SaveChanges();
                        // }
                    }
            
                }
                catch (Exception)
                {
                    //Log errors or do anything you think it's needed
                    throw;
                }
            }

            // //asp net identity 
            // using (var aspnetIdentityDBContext =  scope.ServiceProvider.GetService<ApplicationDbContext>())
            // {
            //         aspnetIdentityDBContext.Database.Migrate();

            //         var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>(); 

            //         var admin=SeedUser.Users.FirstOrDefault();
            //         if (admin==null)
            //         {
            //            Log.Debug("no seed user data");
            //            return webHost;
            //         }

            //         foreach (var SampleUser in SeedUser.Users)
            //         {
            //             var existSampleUser = userMgr.FindByNameAsync(SampleUser.UserName).Result;
            //             if (existSampleUser==null){
                 
            //                 var newUser=new ApplicationUser{
            //                     UserName=SampleUser.UserName,
            //                     Email=SampleUser.Email,
            //                     EmailConfirmed=true                  
            //                 };

            //                 var UserCreateResult = userMgr.CreateAsync(newUser, SampleUser.Password).Result;
            //                 if (!UserCreateResult.Succeeded)
            //                 {
            //                    Log.Debug(newUser.UserName+" create fail");
            //                     throw new Exception(UserCreateResult.Errors.First().Description);
            //                 }

            //                 Log.Debug(newUser.UserName+" created");
            //             }else{                       
            //                 Log.Debug(existSampleUser.UserName+" already exists");
            //             }
            //         }     
                    
            //         // var UserClaimsCreateResult = userMgr.AddClaimsAsync(newUser, new Claim[]{
            //         // new Claim(JwtClaimTypes.Name, "Identity Admin"),
            //         // new Claim(JwtClaimTypes.GivenName, "Admin"),
            //         // new Claim(JwtClaimTypes.FamilyName, "Identity"),
            //         // new Claim(JwtClaimTypes.Email, "identityadmin@webim.com.tw"),
            //         // new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean)})
            //         // .Result;

            //         // if (!UserClaimsCreateResult.Succeeded)
            //         // {
            //         //     throw new Exception(UserClaimsCreateResult.Errors.First().Description);
            //         // }
            //         // Log.Debug("identityadmin claims created");
            // }
        }
 
        return webHost;
    }
}