using System;
using System.Collections.Generic;



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
            Console.WriteLine("User was not found.");
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
            Console.WriteLine("User was not found.");
            return null;
        }
        return tarGet;
    }

    public void AddPlayer(Player player){
        if (player == null){
            Console.WriteLine("Player is null, cannot Add to database.");
            return;
        }

        thePlayers.Add(player);
    }

    public void RemovePlayer(Guid id){
        Player tarGet = GetPlayerById(id);
        if (tarGet == null){
            Console.WriteLine("Player was not found, cannot Remove from database.");
            return;
        }

        thePlayers.Remove(tarGet);
    }

    public void UpdatePlayer(Player UpdatedPlayer){
        if (UpdatedPlayer == null){
            Console.WriteLine("Updated player is null, cannot Update database.");
            return;
        }

        Player tarGet = GetPlayerById(UpdatedPlayer.GetId());
        if (tarGet == null){
            Console.WriteLine("Player was not found, cannot Update database.");
            return;
        }

        int index = thePlayers.IndexOf(tarGet);
        thePlayers[index] = UpdatedPlayer;
    }

    public void showAllPlayers(){
        if (thePlayers.Count == 0){
            Console.WriteLine("No players in database.");
            return;
        }

        var index = 1;
        foreach (Player player in thePlayers){
            Console.WriteLine($"\nPlayer {index}: {player.GetName()}, ID: {player.GetId()}\n");
        
            index++;}
    }

    public void BanPlayer(Guid id){
        Player tarGet = GetPlayerById(id);
        if (tarGet == null){
            Console.WriteLine("Player was not found, cannot ban.");
            return;
        }

        tarGet.status = "banned";
    }

    public void UnbanPlayer(Guid id){
        Player tarGet = GetPlayerById(id);
        if (tarGet == null){
            Console.WriteLine("Player was not found, cannot unban.");
            return;
        }

        tarGet.status = "causal";
    }
}