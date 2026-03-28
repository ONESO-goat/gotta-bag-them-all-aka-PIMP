// gameLogic.cs - Contains core game logic, player and collectable management, and trade mechanics

using System;
using System.Collections.Generic;
using Guid;

Utils Utils = new Utils();


public class Database{

    private List<Player> thePlayers = new List<Player>();

    public List<Player> GetPlayers(){
        return this.thePlayers;
    } 
    public Player GetPlayerByName(string name){
        
        Player tarGet = null;

        foreach (Player player in thePlayers){
            if (player.GetName() == name){
                tarGet = player;
                break;
            } else { continue; }
        }

        if (tarGet == null){
            Utils.print("User was not found.");
            return null;
        }
        return tarGet;
    }

    public Player GetPlayerById(Guid id){
        
        Player tarGet = null;

        foreach (Player player in thePlayers){
            if (player.GetId() == id){
                tarGet = player;
                break;
            } else { continue; }
        }

        if (tarGet == null){
            Utils.print("User was not found.");
            return null;
        }
        return tarGet;
    }

    public void AddPlayer(Player player){
        if (player == null){
            Utils.print("Player is null, cannot Add to database.");
            return;
        }

        thePlayers.Add(player);
    }

    public void RemovePlayer(Guid id){
        Player tarGet = GetPlayerById(id);
        if (tarGet == null){
            Utils.print("Player was not found, cannot Remove from database.");
            return;
        }

        thePlayers.Remove(tarGet);
    }

    public void UpdatePlayer(Player UpdatedPlayer){
        if (UpdatedPlayer == null){
            Utils.print("Updated player is null, cannot Update database.");
            return;
        }

        Player tarGet = GetPlayerById(UpdatedPlayer.GetId());
        if (tarGet == null){
            Utils.print("Player was not found, cannot Update database.");
            return;
        }

        int index = thePlayers.IndexOf(tarGet);
        thePlayers[index] = UpdatedPlayer;
    }

    public void showAllPlayers(){
        if (thePlayers.Count == 0){
            Utils.print("No players in database.");
            return;
        }

        foreach (Player player in thePlayers){
            Utils.print($"Player: {player.GetName()}, ID: {player.GetId()}");
        }
    }

    public void BanPlayer(Guid id){
        Player tarGet = GetPlayerById(id);
        if (tarGet == null){
            Utils.print("Player was not found, cannot ban.");
            return;
        }

        tarGet.status = "banned";
    }

    public void UnbanPlayer(Guid id){
        Player tarGet = GetPlayerById(id);
        if (tarGet == null){
            Utils.print("Player was not found, cannot unban.");
            return;
        }

        tarGet.status = "causal";
    }
}

class Player{

    private readonly Guid id;

    public string SignUpName { get; set; }

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
            Utils.print($"Name: {c.Name}, Nickname: {c.nickName}, Level: {c.level}, Element: {c.element}");
        }
    }

    public void AddHoursPlayed(int hours){
        if (hours < 0){
            Utils.print("Hours played cannot be negative.");
            return;
        }

        this.hoursPlayed += hours;
    }

    private void increaseSave(){ this.saveNumber += 1; }

    public bool isBanned(){ return this.status == "banned"; }

    public void AddNewCollectable(Collectables chara){
        try{
            if (chara == null){
                Utils.print("Collectable is null, cannot Add.");
                return;
            }

            chara.Holder = this.playerName;
            chara.HolderId = this.id;

            Pokemon.Add(chara);

        } catch ( Exception e){

            Utils.print($"Error: {e}");
        }
    }

    public void RemoveCollectable(Collectables chara){
        try{
            if (chara == null){
                Utils.print("Collectable is null, cannot Remove.");
                return;
            }

            Pokemon.Remove(chara);
        } catch (Exception e){
            print();
        }
    }

    public void SetLevel(int level){
        if (level < 0){
            Utils.print("Level cannot be negative.");
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

            Utils.print("Changes successful");

            Utils.print($"Hello, {newName}.)");

            return;
        } else {Utils.print("Changes cancelled"); return;}
}

    public void GetPasswordHash(){
        return this.password;
    }

    public void showInfo(){
        Utils.print($"Name: {this.playerName}");
        Utils.print($"Model Choice: {this.modelChoice}");
        Utils.print($"Status: {this.status}");
        Utils.print($"Hours Played: {this.hoursPlayed}");
    }

    public void GetNameAndId(){
        Utils.print($"Name: {this.playerName}, ID: {this.id}");
    }

    public void GetNickname(){
        return this.playerName;
    }
}

class Collectables{

    // ==================== POKEMON LOGIC ====================

    private readonly Guid id;

    public string Name { get; private set; }

    public long collectableId { get; set; }

    public int level { get; set; }

    public string nickName { get; set; }

    public string element { get; set; }

    // ==================== HOLDER ====================

    public string Holder { get; set; }

    public Guid HolderId { get; set; }

    // ==================== SHINY LOGIC ====================

    public bool isShiny { get; set; }

    // ==================== ORIGINAL OWNER ====================

    public string ownerId { get; set; }

    public  string orginalOwner { get; set; }


    // ==================== BALL ====================

    public Ball ballType { get; set; }

    // ==================== BATTLE LOGIC ====================

    private int amountOfBattlesTaken;

    private bool inBattle;

    private bool isCatchable;

    private bool isFainted;

    // ==================== CATCH LOGIC ====================

    public bool isCatchable;

    public long catchRate;

    // ==================== EVOLUTION LOGIC ====================
    private string evolutionType;

    // ==================== TRADE LOGIC ====================

    public bool traded { get; set; }

    private bool tradable { get; set; }

    public string tradeHistory { get; set; }


    // ==================== FRIEND SHIPS ====================

    private int friendShipLevel;

    // ==================== ITEMS ====================

    public Items itemBeingHeld { get; set; }

    public long itemBeingHeldId { get; set; }

    // ==================== DATES ====================

    public  DateTime dateCatched { get; set; }






    public Collectables(string name, 
                        string whosHolding,
                        Ball caughtIn, 
                        Guid ownerId,
                        string originalOwnerName,
                        bool wasTraded,
                        Item itemBeingHeld,
                        long collectableId,
                        string element,
                        string evolutionType,
                        bool tradable,
                        long catchRate){

    if (string.IsNullOrWhiteSpace(name)){ return; }

    this.Name = name;

    this.ballType = caughtIn;

    this.Holder = whosHolding;

    this.HolderId = ownerId;

    this.element = element;

    this.evolutionType = evolutionType;

    this.tradable = tradable;

    this.catchRate = catchRate;

    this.traded = wasTraded;

    this.itemBeingHeld = itemBeingHeld;

    this.orginalOwner = originalOwnerName;

    this.collectableId = collectableId;

    dateCatched = DateTime.Now;

    this.friendShipLevel = 0;

    }

    public void setNewItem(Item item, long id){
        if (item == null || id <= 0){ return; }

        try{

            itemBeingHeld = item;
            itemBeingHeldId = id;

        } catch (Exception e){
            print($"Error: {e}");
        }
    }

    public void changeNickname(string newName){
        if (newName.Length <= 0 || 
            string.IsNullOrWhiteSpace(newName)){ return; }

        print($"You are changing your collectables name to {newName}, confirm changes? (Y/N)");
         string choice = Console.ReadLine();
        if (choice.ToLower() == "y"){
            this.nickName = newName;
            print("Changes successful");
            print($"Hello, {newName} :)");
            return;
        } else {print("Changes cancelled"); return;}


        
    }

    public bool isTradable(){ return this.tradable;}

    public string GetEvolutionType(){ return this.evolutionType; }

    public void SetLevel(int level){
        if (level < 0){
            Utils.print("Level cannot be negative.");
            return;
        }

        this.level = level;
        }

    public long? GetCatchRate(){
        return this.catchRate ?? null;
    }

    public void showInfo(){
        print($"Name: {this.Name}");
        print($"Nickname: {this.nickName}");
        print($"Level: {this.level}");
        print($"Element: {this.element}");
        print($"Held Item: {this.itemBeingHeld?.GetName() ?? "None"}");
    }

  

}

