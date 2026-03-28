using System;
using System.Collections.Generic;



class TradeLogic{
    privateDatabase db;

    public TradeLogic(){

        db = new Database();

        if (db == null){
            
            print("Unexpected error occured while grabbing database, please try again later.");
            return; 
            }
    }

    public bool tradePokemon(Collectables pokemonA,
                             Collectables pokemonB,
                             Guid oldHolderId, 
                             Guid newHolderId, 
                             Item item=null, 
                             string tradingType="double",
                             string tradeFor="personal"){

    // tradingType = double, both user"s are trading collectables to one another
    // tradingType = single, 1 user is trading a collectable to the other
    
    // tradeFor = personal, both user"s are trading collectables for personal stuff
    // tradeFor = evolution, user"s are trading to evolve pokemon, this makes the trade more strict 
    

    bool tradeActive = true;

    if (oldHolderId == null || newHolderId == null) {return false;}
    if (pokemonA == null){ print("Are we trading air?"); return false;}

    if (tradingType.ToLower() == "double" && pokemonB == null){
        print("Missing 2nd pokemon");
        return false;
    }

     Player player1 = db.GetPlayerById(oldHolderId);
     Player player2 = db.GetPlayerById(newHolderId);
    if (player1 == null || player2 == null){ return false; }

    if (canTrade(pokemonA, pokemonB) == false){
        print("One of the collectables is not tradable");
        return false;
    }

    bool tradeSuccessful = false;

    if (tradeFor.ToLower() == "personal"){
    try{
        player1.RemoveCollectable(pokemonA);
        player2.AddNewCollectable(pokemonA);
        if (tradingType.ToLower() == "double"){
            player2.RemoveCollectable(pokemonB);
            player1.AddNewCollectable(pokemonB);
        }
        tradeSuccessful = true;
        return tradeSuccessful;} 
    
    catch (Exception e){
            print($"Error occurred: {e}");
            tradeActive = false;
            rollbackTrade(pokemonA, pokemonB, player1, player2, tradingType);
            return false;
        
    }
    
    } else if (tradeFor.ToLower() == "evolution"){
    
        

        pokemonA.SetLevel(pokemonA.level + 1);
        if (tradingType.ToLower() == "double" && pokemonB.GetEvolutionType() == "trade"){
            pokemonB.SetLevel(pokemonB.level + 1);
        }
        Console.WriteLine("Trade went through, pokemon have evolved!");
        Console.WriteLine($"Returning {pokemonA.Name} to {player1.GetName()} and {pokemonB.Name} to {player2.GetName()}");
        bool wasSuccessful = rollbackTrade(pokemonA, pokemonB, player1, player2, tradingType);
        if (wasSuccessful){
            Console.WriteLine("Rollback successful, trade complete.");
            return true;
        } else {
            Console.WriteLine("Rollback failed, please check the database for inconsistencies.");
            return false;
        }

    }
                             



    }

    private void rollbackTrade(Collectables pokemonA, Collectables pokemonB, Player player1, Player player2, string tradingType){
        try{
            if (pokemonA == null){return false;} 

            player1.AddNewCollectable(pokemonA);
            player2.RemoveCollectable(pokemonA);
            
            if (tradingType.ToLower() == "double" && pokemonB != null){
                player2.AddNewCollectable(pokemonB);
                player1.RemoveCollectable(pokemonB);
            }
            return true;
        } catch (Exception e){
            print($"Error during rollback: {e}");
            return false;
        }
    }

    private bool canTrade(Collectables a, Collectables b=null){
        if (a.isTradable() == false || b?.isTradable() == false){ return false;}
        return true;
    }
}

class Ball{
    private readonly Guid id;
    public string nameOfBall;
    public Collectables pokemonInside;

    public Ball(string name){
        this.id = Guid.NewGuid();
        this.nameOfBall = name;
    }

    public void setPokemonInside(Collectables pokemon){
        if (pokemon == null){
            Utils.print("Pokemon is null, cannot set inside ball.");
            return;
        }

        this.pokemonInside = pokemon;
    }

    public Guid GetId(){
        return this.id;
    }

    public string GetName(){
        return this.nameOfBall;
    }

    public Collectables GetPokemonInside(){
        return this.pokemonInside;
    }
}

class Item{

    private readonly Guid id;
    public long itemId;
    public string nameOfItem;
    public string description;
    public Collectables currentPokemonHolding;

    public Item(string name, string description, long id){
        this.id = Guid.NewGuid();
        this.itemId = id;
        this.nameOfItem = name;
        this.description = description;
    }

    public void setHolder(Collectables pokemon){
        if (pokemon == null){
            Utils.print("Pokemon is null, cannot set holder.");
            return;
        }

        this.currentPokemonHolding = pokemon;
    }

    public void RemoveHolder(){
        this.currentPokemonHolding = null;
    }

    public Guid GetId(){
        return this.id;
    }

    public string GetDescription(){
        return this.description;
    }

    public string GetName(){
        return this.nameOfItem;
    }

    public Collectables GetCurrentHolder(){
        return this.currentPokemonHolding;
    }

    
}




class CatchSystem {

    private Random rng = new Random();

    public bool TryCatch(Player player, Collectables wildPokemon, Ball ball) {

        if (wildPokemon == null || player == null || ball == null)
            return false;

        // Example logic
        double baseCatchRate = 0.3; // 30%

        long? wildPokemonCatchRate = wildPokemon.GetCatchRate();
        if (wildPokemonCatchRate.HasValue)
        {
            baseCatchRate = wildPokemonCatchRate.Value / 100.0; // Convert percentage to decimal
        }

        // ball modifier
        double ballBonus = GetBallModifier(ball);

        // HP / status (you don't track HP yet, so placeholder)
        double finalChance = baseCatchRate * ballBonus;

        double roll = rng.NextDouble();

        if (roll <= finalChance) {
            player.AddNewCollectable(wildPokemon);
            wildPokemon.ballType = ball;
            wildPokemon.HolderId = player.GetId();
            return true;
        }

        return false;
    }

    private double GetBallModifier(Ball ball) {
        switch (ball.GetName().ToLower()) {
            case "ultra": return 2.0;
            case "great": return 1.5;
            case "master": return 255.0;
            case "admin": return 1.0; // Admin ball has no catch rate bonus, but can catch any pokemon
            default: return 1.0;
        }
    }
}