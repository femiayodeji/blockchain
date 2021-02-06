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
        block.Hash = block.CreateHash();
        DataBase.Add(block);
    }

    public Block GetLatestBlock()
    {
        return DataBase[DataBase.Count - 1];
    }
}