using System;
using System.Security.Cryptography;
using System.Text;

public class Block
{
    public int Key { get; set; }
    public DateTime TimeStamp { get; set; }
    public string PreviousHash { get; set; }
    public string Hash { get; set; }
    public string Data { get; set; }

    Block(
        DateTime timeStamp, 
        string previousHash, 
        string data)
    {
        Key = 0;
        TimeStamp = timeStamp;
        PreviousHash = previousHash;
        Data = Data;
        Hash = CreateHash();
    }

    public string CreateHash()
    {
        SHA256 algorithm = SHA256.Create();
        string rawData = $"{TimeStamp}-{PreviousHash ?? ""}-{Data}";
        byte[] inputBytes = Encoding.ASCII.GetBytes(rawData);
        byte[] outputBytes = algorithm.ComputeHash(inputBytes);
        return Convert.ToBase64String(outputBytes);
    }
}