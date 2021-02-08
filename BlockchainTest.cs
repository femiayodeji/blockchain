using System;
using System.Diagnostics;
using Newtonsoft.Json;

public static class BlockchainTest
{
    public static void Run()
    {
        Seed();
        DataSecurity();
    }

    public static void Seed()
    {
        Chain testCoin = new Chain();  
        testCoin.AddBlock(new Block(DateTime.Now, null, "{sender:Femi,receiver:Adetayo,amount:10}"));  
        testCoin.AddBlock(new Block(DateTime.Now, null, "{sender:Adetayo,receiver:Femi,amount:5}"));  
        testCoin.AddBlock(new Block(DateTime.Now, null, "{sender:Adetayo,receiver:Femi,amount:5}"));  
        
        Console.WriteLine(JsonConvert.SerializeObject(testCoin, Formatting.Indented));
    }

    public static void DataSecurity()
    {
        Chain testCoin = new Chain();  
        testCoin.AddBlock(new Block(DateTime.Now, null, "{sender:Femi,receiver:Adetayo,amount:10}"));  
        testCoin.AddBlock(new Block(DateTime.Now, null, "{sender:Adetayo,receiver:Femi,amount:5}"));  
        testCoin.AddBlock(new Block(DateTime.Now, null, "{sender:Adetayo,receiver:Femi,amount:5}"));  
        
        Debug.Assert(testCoin.IsChainValid() == true, "Data Security Test Failed", "The chain is valid. Expected true");

        testCoin.DataBase[1].Data = $"";

        Debug.Assert(testCoin.IsChainValid() == false, "Data Security Test Failed", "The chain is invalid. Expected false");
    }
}

