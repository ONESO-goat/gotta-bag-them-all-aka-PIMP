// admin.cs - Contains admin control functionalities such as banning players, setting player levels, and managing game items

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class AdminControl
{

    private string adminKey = "admin123"; // This should be stored securely in a real application
    private bool isAdminLoggedIn = false;
    private string adminPassword = adminKey; 

    Database database = new Database();

    public void Login(string password)
    {
        if (password == adminPassword)
        {
            isAdminLoggedIn = true;
            Console.WriteLine("Admin login successful.");
        
        }
        else
        {
            Console.WriteLine("Invalid password.");
        }
    }

    public void Logout()
    {
        isAdminLoggedIn = false;
        Console.WriteLine("Admin logged out.");
    }

    public void AddItem(string itemName, int quantity)
    {
        if (!isAdminLoggedIn) { Console.WriteLine("Admin access required."); return; }
        Console.WriteLine($"Added {quantity}x {itemName}");
    }

    public void SetPlayerLevel(string playerName, int level)
    {
        if (!isAdminLoggedIn) { Console.WriteLine("Admin access required."); return; }
        
        database.GetPlayerByName(playerName)?.SetLevel(level);

        Console.WriteLine($"Set {playerName}'s level to {level}");
    }

    public void BanPlayer(Guid playerId)
    {
        if (!isAdminLoggedIn) { Console.WriteLine("Admin access required."); return; }
        
        database.BanPlayer(playerId);

        Console.WriteLine($"Player {playerId} has been banned.");
    }

    public void UnbanPlayer(Guid playerId)
    {
        if (!isAdminLoggedIn) { Console.WriteLine("Admin access required."); return; }
        
        database.UnbanPlayer(playerId);

        Console.WriteLine($"Player {playerId} has been unbanned.");
    }

    public bool IsAdminLoggedIn => isAdminLoggedIn;
}