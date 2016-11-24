/// Exercise 11.1
// (i) write a CPS of the function `len`
let rec len xs =
    match xs with
    | []      -> 0
    | x :: xr -> 1 + len xr;;

let rec lenc xs c =
    match xs with
    | []      -> c 0
    | x :: xr -> lenc xr (fun v -> c(v + 1));;

lenc [2; 5; 7] id;; // => 3
lenc [2; 5; 7] (printf "The answer is `%d`\n");; // => "The answer is `3`"

// (ii) what happens when calling the following?
lenc [2; 5; 7] (fun v -> 2 * v);; // => 6
// Takes the final result of the length and multiplies it with 2.

// (iii) write a tail-recursive version using an accumulator
let rec leni xs acc =
    match xs with
    | []      -> acc
    | x :: xr -> leni xr (acc + 1)

leni [2; 5; 7] 0;; // => 3

// The relation between `lenc` and `leni` is that they are both tail recursive,
// but instead of putting a continuation closure on the heap, `leni`
// will increment an accumulator on the stack for each iteration.
// The accumulating version is not using heap space which is a benefit,
// and because it is tail recursive it will not blow up the stack for
// really large list lengths.
// Not all functions can be made tail recursive by using an accumulating value,
// thus it is good to also keep the CPS version in mind.


/// Exercise 11.2
// (i) write a CPS version of the list reversal function `rev`
let rec rev xs =
    match xs with
    | []      -> []
    | x :: xr -> rev xr @ [x];;

let rec revc xs c =
    match xs with
    | []      -> c []
    | x :: xr -> revc xr (fun l -> c(l @ [x]));;

revc [2; 5; 7] id;; // => [7; 5; 2]

// (ii) what happens if `revc xs (fun v -> v @ v)` is called?
// the output will be the reversed list times two
revc [2; 5; 7] (fun v -> v @ v);; // => [7; 5; 2; 7; 5; 2] // yay!

// (iii) write a tail recursive version `revi` that uses an accumulator
let rec revi xs acc =
    match xs with
    | []      -> acc
    | x :: xr -> revi xr (x :: acc);;

revi [2; 5; 7] [];; // => [7; 5; 2]


/// Exercise 11.3
// Write a CPS version of `prod`
let rec prod xs =
    match xs with
    | []      -> 1
    | x :: xr -> x * prod xr;;

let rec prodc xs c =
    match xs with
    | []      -> c 1
    | x :: xr -> prodc xr (fun v -> c(x * v));;

prod [1; 2; 3];;  // => 6
prodc [1; 2; 3];; // => 6


/// Exercise 11.4
// Optimize `prodc`.
let rec oprodc xs c =
    match xs with
    | []      -> c 1
    | 0 :: _  -> c 0
    | x :: xr -> prodc xr (fun v -> c(x * v));;

oprodc [2; 5; 7] id;; // => 70
oprodc [2; 5; 7] (printf "The answer is `%d`\n");; // => "The answer is `70`"
oprodc [2; 5; 7; 0] id;; // => 0

let rec prodi xs acc =
    match xs with
    | []             -> acc
    | _ when acc = 0 -> 0
    | 0 :: _         -> 0
    | x :: xr        -> prodi xr (x * acc);;

prodi [2; 5; 7] 1;; // => 70
prodi [2; 5; 7; 0] 1;; // => 0


/// Exercise 11.8
// Go nuts with Icon
#load "Icon.fs";;
open Icon;;
run (Every(Write(Prim("*", CstI 2, FromTo(1, 4)))));; // => 2 4 6 8

// (i) write an expression that prints: 3 5 7 9, and one that prints 21 22 31 32 41 42
run (Every(Write(Prim("+", CstI 1, Prim("*", CstI 2, FromTo(1, 4))))));; // => 3 5 7 9
run (Every(Write(Or(Or(FromTo(21, 22), FromTo(31, 32)), FromTo(41, 42)))));; // => 21 22 31 32 41 42

// (ii) using Icon, print the least multiple of 7 that is greater than 50
run (Write(Prim("<", CstI 50, Prim("*", CstI 7, FromTo(1, 10)))));;

// (iii) extend Icon.fs with unary primitive functions `sqr` and `even`
run (Every(Write(Prim1("sqr", FromTo(3, 6)))));; // => 9 16 25 36
run (Every(Write(Prim1("even", FromTo(1, 7)))));; // => 2 4 6

// (iv) extend Icon.fs with unary primitive function `multiples`
// this explodes in a horrible stack overflow, as it generates "infinitely" many results
//run (Every(Write(Seq(FromTo(1, 5), Prim1("multiples", CstI 5)))));;
