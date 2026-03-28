using System;
using System.Collections.Generic;



public class Player{

    private readonly Guid id;

    public string SignUpName;

    public int modelChoice;

    public string playerName;

    private readonly string password;

    public string status;

    public bool onlineOrOffline;

    public List<Collectables> Pokemon = new List<Collectables>();

    public int saveNumber;

    private int hoursPlayed;

    public Player(string name, string nickname, string password, int model){



        this.SignUpName = name;

        this.playerName = nickname;

        this.password = password;

        this.modelChoice = model;

        hoursPlayed = 0;

        this.id = Guid.NewGuid();

        this.status = "new"; // new - causal - banned - blacklisted

        this.onlineOrOffline = true;

    }

    public string GetName(){
        return this.playerName;
    }

    public Guid GetId(){
        return this.id;
    }

    public int GetHoursPlayed(){
        return this.hoursPlayed;
    }



    public List<Collectables> GetRawCollection(){
        return this.Pokemon;
    }

    public void GetCollection(){
        // TODO: Add sorting and filtering options

        foreach (Collectables c in this.Pokemon){
            Console.WriteLine($"Name: {c.Name}, Nickname: {c.nickName}, Level: {c.level}, Element: {c.element}");
        }
    }

    public void AddHoursPlayed(int hours){
        if (hours < 0){
            Console.WriteLine("Hours played cannot be negative.");
            return;
        }

        this.hoursPlayed += hours;
    }

    private void increaseSave(){ this.saveNumber += 1; }

    public bool isBanned(){ return this.status == "banned"; }

    public void AddNewCollectable(Collectables chara){
        try{
            if (chara == null){
                Console.WriteLine("Collectable is null, cannot Add.");
                return;
            }

            chara.Holder = this.playerName;
            chara.HolderId = this.id;

            Pokemon.Add(chara);

        } catch ( Exception e){

            Console.WriteLine($"Error: {e}");
        }
    }

    public void RemoveCollectable(Collectables chara){
        try{
            if (chara == null){
                Console.WriteLine("Collectable is null, cannot Remove.");
                return;
            }

            Pokemon.Remove(chara);
        } catch (Exception e){
            Console.WriteLine($"Error: {e}");
        }
    }

    public void SetLevel(int level){
        if (level < 0){
            Console.WriteLine("Level cannot be negative.");
            return;
        }

        this.modelChoice = level;
    }

    public void changeNickname(string newName){
        if (newName.Length <= 0 || 
            string.IsNullOrWhiteSpace(newName)){ return; }

        Console.WriteLine($"You are changing your name to {newName}, confirm changes? (Y/N)");
        string choice = Console.ReadLine();
        if (choice.ToLower() == "y"){

            this.playerName = newName;

            Console.WriteLine("Changes successful");

            Console.WriteLine($"Hello, {newName}.)");

            return;
        } else {Console.WriteLine("Changes cancelled"); return;}
}

    public string GetPasswordHash(){
        return this.password;
    }

    public void showInfo(){
        Console.WriteLine($"Name: {this.playerName}");
        Console.WriteLine($"Model Choice: {this.modelChoice}");
        Console.WriteLine($"Status: {this.status}");
        Console.WriteLine($"Hours Played: {this.hoursPlayed}");
    }

    public void GetNameAndId(){
        Console.WriteLine($"Name: {this.playerName}, ID: {this.id}");
    }

    public string GetNickname(){
        return this.playerName;
    }
}
