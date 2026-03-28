
using System;
using System.Collections.Generic;



public class TradeLogic{
    private Database db;

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