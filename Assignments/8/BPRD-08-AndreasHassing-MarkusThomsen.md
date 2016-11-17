# BPRD Exercise 8, Fall 2016
> By Andreas Bjørn Hassing Nielsen and Markus Thomsen

## Exercise 10.1
### (i) How the abstract machine executes ...
* The `ADD` instruction sets the value of the result in stack pointer position `sp-1`.
  The result is calculated by grabbing the values of the elements at stack pointer position
  `sp-1` and stack pointer position `sp`.
  The first and second values on the stack get untagged, and then the result gets tagged.
  Then the stack pointer is decremented.
* The `CSTI i` instruction sets the value of `i` in stack pointer position `sp+1`.
  The value of `i` is found by incrementing the program counter by 1, and getting that value, this value then gets tagged.
  Then the stack pointer is incremented.
* The `NIL` instruction is different from `CSTI 0`, in that it does not increment the program
  counter, and thus does not tag the `0`.
* The `IFZERO` instruction gets the integer value at stack pointer position `sp` and decrements the stack pointer.
  Then it checks if the value is tagged (with `IsInt`), if it is, it is untagged, and in either case checked for equality against `0`. If it is equal to `0`, the program jumps to the specified program counter at `pc`, otherwise it increments the program counter.
  The reason for the `IsInt` check is to allow `nil` values for equality as well.
* The `CONS` instruction allocates a cons cell on the heap.
  In that cell, two values `v1` and `v2` are inserted, namely the ones found at stack positions `sp-1` and `sp` respectively.
  Then the address of the cons cell (`p`) is inserted at stack position `sp-1`, and stack pointer is decremented.
* The `CAR` instruction gets the cons cell found at the address pointed to by the stack pointer, then gets the first value of that cons cell.
  The value is then inserted at the stack pointer.
* The `SETCAR` instruction takes the value `v` found at the stack pointer `sp`, then decrements the stack pointer.
  Then the stack pointer will point to a cons cell reference, which is used to set the value of the first cell to the value of `v`. 


### (ii) The result of applying `Length`, `Color` and `Paint` to a header
* Applying `Length`:
  Bitshifts twice to the right (this removes the color bits `gg`), then uses the `&` operator to deduct `2^22` from the number, thus getting the length of the block.
* Applying `Color`:
  Uses the `&` operator to get the last two bits. We would postulate, that it would've been more clear if something like `((hdr)&0b0011)` had been used, which would read: "keep the last two bits".
* Applying `Paint`:
  Flips the bits of `0b0011`, then uses the `&` operator to get the colors and then sets those colors with an `|` operator and the values of the specified colors.


### (iii) When is `allocate(...)` called?
Whenever a `CONS` is found in the program, `allocate(...)` is called.

The `Tag` and `Untag` functions are also used to help the garbage collector, but not directly.


### (iv) When will `collect(...)` be called?
`collect(...)` is called when there is no free memory. In the case that it could not free any memory in the next attempt in `allocate`, the program will print `Out of memory` and exit.
