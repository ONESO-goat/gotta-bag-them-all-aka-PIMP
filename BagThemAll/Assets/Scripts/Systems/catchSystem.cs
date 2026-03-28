
using System;
using System.Collections.Generic;



public class CatchSystem {

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