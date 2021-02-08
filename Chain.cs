using System;
using System.Collections.Generic;

public class Chain
{
    public IList<Block> DataBase { get; set; }

    public Chain()
    {
        InitializeChain();
        AddGenesisBlock();
    }

    public void InitializeChain()
    {
        DataBase = new List<Block>();
    }

    public void AddGenesisBlock()
    {
        DataBase.Add(CreateGenesisBlock());
    }

    public Block CreateGenesisBlock()
    {
        return new Block(DateTime.UtcNow, null, "{}");
    }

    public void AddBlock(Block block)
    {
        Block latestBlock = GetLatestBlock();
        block.Key = latestBlock.Key + 1;
        block.PreviousHash = latestBlock.Hash;
        block.Hash = block.CalculateHash();
        DataBase.Add(block);
    }

    public Block GetLatestBlock()
    {
        return DataBase[DataBase.Count - 1];
    }

    public bool IsChainValid()
    {
        for(int i = 1; i < DataBase.Count; i++)
        {
            Block currentBlock = DataBase[i];
            Block previousBlock = DataBase[i - 1];

            if (
                currentBlock.Hash != currentBlock.CalculateHash() || 
                currentBlock.PreviousHash != previousBlock.Hash
                )
            {
                return false;
            }
        }
        return true;
    }
}