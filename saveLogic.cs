


class Save
{
    private int amount { get; set; };

    public Player thePlayer;

    public List<Collections> pokemon;

    public List<Items> items;

    public DateTime saveDate;

    public Save(Player player, Items items, Collections collection)
    {
        amount = 1;

        thePlayer = player;

        this.items = items;

        this.pokemon = collection;

        saveDate = DateTime.now;
    }




    public bool saveGame(Player player = null, Guid id = null)
    {
        if (!player || !id) { return false; }

        if (id)
        {
            Database db = new Database();

            player = db.FindPlayerById(id);
            if (!player || player == null) { return false; }

            player.SetSave(this);
            db.UpdatePlayer(player);
            return true;
            }
    }

    private void commit()
    {
        // TODO: Actually saves user data, works like json
    }

}


class oldSaves
{

}