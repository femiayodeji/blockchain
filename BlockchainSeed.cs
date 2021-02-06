using System;
using Newtonsoft.Json;

public static class BlockchainTest
{
    public static void Run()
    {
        Chain testCoin = new Chain();  
        testCoin.AddBlock(new Block(DateTime.Now, null, "{sender:Femi,receiver:Adetayo,amount:10}"));  
        testCoin.AddBlock(new Block(DateTime.Now, null, "{sender:Adetayo,receiver:Femi,amount:5}"));  
        testCoin.AddBlock(new Block(DateTime.Now, null, "{sender:Adetayo,receiver:Femi,amount:5}"));  
        
        Console.WriteLine(JsonConvert.SerializeObject(testCoin, Formatting.Indented));
    }
}

