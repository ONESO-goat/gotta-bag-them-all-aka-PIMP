using System;
using System.Collections.Generic;



public class Ball{
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