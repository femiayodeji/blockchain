using System;
using System.Collections.Generic;
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
        Console.WriteLine("All tests passed");
    }

    public static void Seed()
    {
        DateTime startTime = DateTime.Now;

        Chain testCoin = new Chain();  
        testCoin.AddBlock(new Block(DateTime.Now, null, TestTransactions()[0]));  
        testCoin.AddBlock(new Block(DateTime.Now, null, TestTransactions()[1]));  
        testCoin.AddBlock(new Block(DateTime.Now, null, TestTransactions()[2]));  

        DateTime endTime = DateTime.Now;

        Console.WriteLine($"Time taken: {endTime - startTime}");

        Debug.Assert(testCoin.DataBase[0].PreviousHash == null, "Genesis Block Test Failed", $"The previous block hash should be null");
        Console.WriteLine("Genesis Block Test - Passed");

        Debug.Assert(testCoin.DataBase.Count > 1, "New Block Test Failed", $"The chain has no new block. Expected {4} new blocks");
        Console.WriteLine("New Block Test - Passed");
        
        Console.WriteLine(JsonConvert.SerializeObject(testCoin, Formatting.Indented));
    }

    public static void DataSecurity()
    {
        bool blockValidation;
        Chain testCoin = new Chain();  
        testCoin.AddBlock(new Block(DateTime.Now, null, TestTransactions()[0]));  
        testCoin.AddBlock(new Block(DateTime.Now, null, TestTransactions()[2]));  
        
        blockValidation = testCoin.IsChainValid();
        Debug.Assert(blockValidation == true, "Data Security Test Failed", $"The chain is valid. Expected {blockValidation}");
        Console.WriteLine("Data Security Test (Valid Blockchain) - Passed");

        testCoin.DataBase[1].Data = $"";

        blockValidation = testCoin.IsChainValid();
        Debug.Assert(blockValidation == false, "Data Security Test Failed", $"The chain is invalid. Expected {blockValidation}");
        Console.WriteLine("Data Security Test (Invalid Blockchain) - Passed");
    }

    public static void ProofOfWork()
    {
        string expectedLeadingZeros = new string('0', Config.Difficulty);

        Chain testCoin = new Chain();  
        testCoin.AddBlock(new Block(DateTime.Now, null, TestTransactions()[1]));  
        testCoin.AddBlock(new Block(DateTime.Now, null, TestTransactions()[2]));  

        string leadingZeros = GetLeadingZero(testCoin.DataBase[1].Hash);
        
        Debug.Assert(leadingZeros == expectedLeadingZeros, "Proof of Work Test Failed", $"Leading zeros is expected to be {expectedLeadingZeros}");
        Console.WriteLine("Proof of Work Test - Passed");
    }

    private static string[] TestTransactions()
    {
        return new string[] {
            "{sender:Femi,receiver:Adetayo,amount:10}",
            "{sender:Adetayo,receiver:Femi,amount:5}",
            "{sender:Adetayo,receiver:Femi,amount:2}",
        };
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

