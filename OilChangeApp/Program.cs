using System;
using System.IO;
using MySqlConnector;
using OilChangeApp;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OilChangeApp.Aplications;
using OilChangeApp.Domain.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;
using User = OilChangeApp.User;

class Program
{
    static void Main()
    {
        Host testBot = new Host("8040948589:AAGyIxpxOcm6r0pZwo8VHyoUXmUTi-i18x8");
        using var db = new DbContext();

        db.Admins.Add(new Admin
        {
            Nickname = "adminFather",
            Password = "admin123",
        });
        // db.BanedUsers.Add(new BanedUsers
        // {
        //     BanedTgId = 1066092660,
        //     ExpiredDate = DateTime.Now + TimeSpan.FromMinutes(5),
        // });
        db.SaveChanges();
        testBot.Start();
        testBot.OnMessage += CommandLogic.OnMessage;
        Task.Delay(-1).Wait();
        
    }
    //Todo addMethode to rememberAdmin
    //Todo delete OilType from Admin table
    //Todo Add ExceptionHandling for carCreating (for liters only number to give error if it is not number), (for visite date give error if format is wrong)
    //Todo /connectToCar (add car already connected)
    //Todo if car_num and car_name is same for service admin send car is created and dont create
    //Todo visite_date passed notify user
    //
}