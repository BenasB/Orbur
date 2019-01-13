# Orbur

A game about controlling a robot by creating sequences of "commands"

Inspired by Seven Billion Humans

---

## Blocker

Blocker is the system which manages and executes commands, constructs and displays the UI for the blocks and sequences.

All of blockers components are located in `Scripts/Blocker` but the parameter prefabs (UI prefabs which are used to construct the block's parameters) are located in `Prefabs/Blocker`

![Blocker explanation](https://i.imgur.com/4GCqkYU.png)

* The Selection Block list is populated upon Startup
* When one of the Selection Blocks is pressed, a new block is constructed and added to the block sequence
* When a block is being constructed, needed parameters are instantiated from parameter prefabs and added to the block
* On sequence execution, the Sequencer iterates through the Block Sequence, calls the methods with their corresponding parameters and waits `executionDelay` seconds before moving onto the next block

### Demo
![Screnshot](https://i.redd.it/i2jwcxhwb8s11.gif)

### To do
* ~~Reset the robot's position after the player restarts the sequence~~
* Implement block rearrangement within the sequence
* Implement callbacks that cause the sequencer to wait for the command to finish before moving onto the next block
* ~~Ability to remove blocks from sequence~~ (Can remove all of them for now)