using System;
using System.Collections.Generic;


public class Collectables{

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

    private bool isCatchable {get; set;}

    private bool isFainted;

    // ==================== CATCH LOGIC ====================


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

    public Item itemBeingHeld { get; set; }

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

class Pokemon : Collectables{
    public Pokemon(string name, 
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
                    long catchRate) : base(name, whosHolding, caughtIn, ownerId, originalOwnerName, wasTraded, itemBeingHeld, collectableId, element, evolutionType, tradable, catchRate){

    }

    public void evolve(){
        if (this.GetEvolutionType() == "level"){
            this.SetLevel(this.level + 1);
        } else if (this.GetEvolutionType() == "trade"){
            Utils.print("This pokemon evolves through trading, please trade it to evolve.");
        } else {
            Utils.print("This pokemon does not evolve.");
        }
    }
}