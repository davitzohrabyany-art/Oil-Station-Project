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

        // db.Admins.Add(new Admin
        // {
        //     Nickname = "admin2",
        //     Password = "12345",
        //     OilType = "Synthetic"
        // });
        // db.Users.Add(new User
        // {
        //     TelegramId = 720153725,
        //     PhoneNumber = "99999999"
        // });
        // db.Car.Add(new Car
        // {
        //     car_name = "Toyota",
        //     car_num = "01oo001",
        //     password = "12345",
        //     oil_type = "Lukoil"
        // });
        // db.Service_visit.Add(new Service_visit
        // {
        //     Service_id = 1,
        //     Car_id = 1,
        //     Visit_date = new DateTime(2024, 3, 16)
        // });
        // db.Oil_change.Add(new Oil_change
        // {
        //     Service_id = 1,
        //     Oil_id = 1,
        //     Oil_name = "Lukoil",
        //     Oil_location = "Motor",
        //     Next_change_km = 56,
        //     Next_change_date = new DateTime(2026, 4, 20)
        // });


        db.SaveChanges();
        testBot.Start();
        testBot.OnMessage += CommandLogic.OnMessage;
        Task.Delay(-1).Wait();
        
    }
}