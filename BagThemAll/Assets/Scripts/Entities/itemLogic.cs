using System;
using System.Collections.Generic;




public class Item{

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
            Console.WriteLine("Pokemon is null, cannot set holder.");
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


