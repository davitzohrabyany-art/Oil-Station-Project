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
        //     Nickname = "adminFather",
        //     Password = "admin123",
        //     OilType = "Synthetic"
        // });
        db.SaveChanges();
        testBot.Start();
        testBot.OnMessage += CommandLogic.OnMessage;
        Task.Delay(-1).Wait();
        
    }
}