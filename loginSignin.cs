// loginSignin.cs - Handles user login and Signup functionality


using System;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;


class Login{
    
    DotEnv.Load();
    public static void Login(string username, string password){
        // Implement login logic here
        Database? database = new Database();

        if (username == null || password == null){
            Console.WriteLine("Username and password cannot be empty.");
            return;
        }

        if (username == "admin" && password == "admin123"){ // change password to a secure one in production
            AdminControl adminControl = new AdminControl();
            adminControl.Login(password);
            Console.WriteLine("Admin login successful.");
            return;
        }


        var hasher = new PasswordHasher();
        var hashCheck = hasher.HashPassword(password); // Use a secure hashing algorithm in production
        if (hashCheck == null){ 
            Console.WriteLine("Invalid username or password.");
            return;
        }
        
        Player player = database.GetPlayerByName(username); // Default model choice is 0, can be changed later
    
        if (player == null){
            Console.WriteLine("User not found.");
            return;
        }

        if (!hasher.VerifyHashedPassword(player.GetPasswordHash(), password)){
            Console.WriteLine("Invalid username or password.");
            return;
        }

        if (player.status == "banned" || player.isBanned()){
            Console.WriteLine("Your account has been banned. Please contact support for more information.");
            return;
        }

        player.onlineOrOffline = true;
        Console.WriteLine($"Welcome back, {player.GetName()}!");
    }

    public static void SignUp(string username, string password){
        Database database = new Database();

        if (username == null || password == null){
            Console.WriteLine("Username and password cannot be empty.");
            return;
        }

        if (database.GetPlayerByName(username) != null){
            Console.WriteLine("Username already exists. Please choose a different username.");
            return;
        }

        if (password.Length < 6){
            Console.WriteLine("Password must be at least 6 characters long.");
            return;
        }

        if (username.Length < 3){
            Console.WriteLine("Username must be at least 3 characters long.");
            return;
        }
        var hasher = new PasswordHasher();
        var hash = hasher.HashPassword(password); // Use a secure hashing algorithm in production
        if (hash == null){
            Console.WriteLine("Error hashing password.");
            return;
        }

        try{
        

            Player newPlayer = new Player(username, hash);

            database.AddPlayer(newPlayer);

            newPlayer.onlineOrOffline = true;
            Console.WriteLine($"User {username} Signed up successfully.");

            Console.WriteLine($"Welcome, {username}!");}

        catch (Exception ex){

            Console.WriteLine($"Error Signing up: {ex.Message}");
        }

    }

    public static void Logout(Player player){
        if (player == null){
            Console.WriteLine("No user is currently logged in.");
            return;
        }
        player.onlineOrOffline = false;
        Console.WriteLine($"Goodbye, {player.GetName()}!");
    }

    
}




class PasswordHasher{
    public string HashPassword(string password){
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }

    public bool VerifyHashedPassword(string hash, string password){
        return hash == HashPassword(password);
    }
}