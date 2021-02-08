using System;
using System.Diagnostics;
using Newtonsoft.Json;

public static class BlockchainTest
{
    public static void Run()
    {
        Console.WriteLine("Running tests..");
        Seed();
        DataSecurity();
        ProofOfWork();
    }

    public static void Seed()
    {
        Chain testCoin = new Chain();  

        Debug.Assert(testCoin.DataBase.Count == 1, "Genesis Block Test Failed", $"The chain is empty. Expected {1} block");

        testCoin.AddBlock(new Block(DateTime.Now, null, "{sender:Femi,receiver:Adetayo,amount:10}"));  
        testCoin.AddBlock(new Block(DateTime.Now, null, "{sender:Adetayo,receiver:Femi,amount:5}"));  
        testCoin.AddBlock(new Block(DateTime.Now, null, "{sender:Adetayo,receiver:Femi,amount:5}"));  

        Debug.Assert(testCoin.DataBase.Count > 1, "New Block Test Failed", $"The chain has no new block. Expected {4} new blocks");
        
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

    public static void ProofOfWork()
    {
        string expectedLeadingZeros = new string('0', Config.Difficulty);

        Chain testCoin = new Chain();  
        testCoin.AddBlock(new Block(DateTime.Now, null, "{sender:Femi,receiver:Adetayo,amount:10}"));  
        testCoin.AddBlock(new Block(DateTime.Now, null, "{sender:Adetayo,receiver:Femi,amount:5}"));  
        testCoin.AddBlock(new Block(DateTime.Now, null, "{sender:Adetayo,receiver:Femi,amount:5}"));          

        string leadingZeros = GetLeadingZero(testCoin.DataBase[1].Hash);
        
        Debug.Assert(leadingZeros == expectedLeadingZeros, "Proof of Work Test Failed", $"Leading zeros is expected to be {expectedLeadingZeros}");
    }

    private static string GetLeadingZero(string str)
    {
        string result = "";
        foreach(char c in str)
        {
            if(c != '0')
                break;
            result += c;            
        }
        return result;
    }
}

