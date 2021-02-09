# blockchain
A simple Blockchain implementation

Blockchain is a data structure similar to a linked list. This data structure is composed of blocks and every block has the following properties.
* Key - The block index
* Timestamp - The datetime the block was generated
* Previous Hash - The hash of the previous block
* Hash - The block crypted information
* Data - The 
* Nonce - This is a private property a block needs to meet the constraint.

A change in any block invalidates every block after it, which makes the data secure and difficult to manipulate. It uses cryptography to ensure data integrity.

The investor of Blockchain invented a constraint to ensure the hash calculation is difficulty. A significant computing time is required to generate a new block. This constraint says that the hash of each block must begin with X number of zeros. This is known as Proof of Work and the generated information must be simple and verifiable so that it can be easily verified by any nodes in the Peer-to-Peer network.