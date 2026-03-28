
using System;
using System.Collections.Generic;


public class Utils{
    public static void print(string message){
        Console.WriteLine(message);
    }

    public static string input(){
        return Console.ReadLine();
    }

    public static void debug(string message){
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[DEBUG] {message}");
        Console.ResetColor();
    }


    public static void Error(string exceptionType)
    {
        print($"Unexpected [{exceptionType}] error while using user keys, would you like to report this or restart?");


        var userChoice = input();


        var c = userChoice.ToLower();

        if (c == "cancel")
        {
            return;
        } else if (c == "restart")
        { // TODO: Find a way to read who ever clicked restart.
            Database db = new Database();
            Player player = db.FindPlayerById();
        }
    }
}