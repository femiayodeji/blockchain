using System;
using System.Security.Cryptography;
using System.Text;

public class Block
{
    public int Key { get; set; } = 0;
    public DateTime TimeStamp { get; set; }
    public string PreviousHash { get; set; }
    public string Hash { get; set; }
    public string Data { get; set; }
    private long _nonce = 0;
    
    public Block(DateTime timeStamp, string previousHash, string data)
    {
        TimeStamp = timeStamp;
        PreviousHash = previousHash;
        Data = data;
        Hash = CalculateHash();
    }

    public string CalculateHash()
    {
        SHA256 algorithm = SHA256.Create();
        string rawData = $"{TimeStamp}-{PreviousHash ?? ""}-{Data}-{_nonce}";
        byte[] inputBytes = Encoding.ASCII.GetBytes(rawData);
        byte[] outputBytes = algorithm.ComputeHash(inputBytes);
        return Convert.ToBase64String(outputBytes);
    }

    public void Mine(int difficulty)
    {
        string leadingZeros = new string('0', difficulty);
        while(this.Hash == null || this.Hash.Substring(0, difficulty) != leadingZeros)
        {
            _nonce++;
            this.Hash = CalculateHash();
        }
    }
}

