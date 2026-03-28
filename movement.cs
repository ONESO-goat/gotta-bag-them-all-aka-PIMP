class Movement
{

    public int Speed {get; set;}

    private readonly long Coordinates;

    private Utils u;

    private readonly bool AdminControl;

    public bool isSprinting;

    

    private readonly bool KeysAreWorking;

    public bool isMoving;
    public string currentKey;

    public Movement()
    {

        this.currentKey = "idle";

        

        this.isSprinting = false;

        this.isMoving = false;

        var check = KeyCheck();

        if (!check){KeysAreWorking = false;}

        KeysAreWorking = true;

        u = new Utils();
        if (u == null){u = null;}
        

    }


    // Read keys, if User presses key, runs based on that key.
    public void ReadKeys(bool sprint)
    {
        if (!KeyCheck()){return;}

        ConsoleKeyInfo keyInfo = Console.Readkey(true);

        try {if (keyInfo){
            isMoving = true;
            
            while (isMoving){
                if (keyInfo.Key == ConsoleKey.W || keyInfo.Key == ConsoleKey.UpArrow)
                {
                    Console.WriteLine("moving up");
                    
                }

                else if (keyInfo.Key == ConsoleKey.A || keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    Console.WriteLine("moving left");
                    
                }
                else if (keyInfo.Key == ConsoleKey.S || keyInfo.Key == ConsoleKey.SouthArrow)
                
                {
                    Console.WriteLine("moving south");
                    
                }

                else if (keyInfo.Key == ConsoleKey.D || keyInfo.Key == ConsoleKey.RightArrow)
                {
                    Console.WriteLine("moving right");
                    
                }

                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    
                    Console.WriteLine("exiting");
                    
                }
            }
        }} catch (exception ex)
        {
            movementException e = new movementException();
            if (u!=null){u.Error(exceptionType=ex);}
            else{Console.Writeline($"Unexplained error occured: " + ex);}

            
        }

    }

    public int getSpeed(){return this.Speed;}

    private void SetSpeed(int amount)
    {
        if (!AdminControl){Console.Writeline("Cant change speed due to invalid access"); return;}

        Speed = amount;
        Console.WriteLine("speed successfully changed");
    } 


   
    public bool KeyCheck(){
        if (!Console.KeyAvailable || Console.KeyAvailable == null)
        {
            Console.WriteLine("Keys seems to not be available, please double check keys access.");
            return false;
            
        }
        
        return true;
        }



}