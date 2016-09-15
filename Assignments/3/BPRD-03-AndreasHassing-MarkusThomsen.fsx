//module BPRD.AbhnMatho.Exercise3

/// Exercise 4.1
// A. Load micro-ML evaluator (Abstract syntax only)
#load "Absyn.fs";;
#load "Fun.fs";;
open Absyn;;
open Fun;;

let res = run (Prim("+", CstI 5, CstI 7));; // => 12

// B. Generate and compile lexer and parser, and loading them
// Please refer to the Runtime path on your system!
#r "../../../FsLexYacc.Runtime.dll";;
#load "FunPar.fs";;
#load "FunLex.fs";;
#load "Parse.fs";;
open Parse;;

let e1 = fromString "5+7";;
    // => Prim ("+",CstI 5,CstI 7)
let e2 = fromString "let y = 7 in y + 2 end";;
    // => Let ("y",CstI 7,Prim ("+",Var "y",CstI 2))
let e3 = fromString "let f x = x + 7 in f 2 end";;
    // => Letfun ("f","x",Prim ("+",Var "x",CstI 7),Call (Var "f",CstI 2))

// C. Using the lexer, parser and first-order evaluator together
#load "ParseAndRun.fs";;
open ParseAndRun;;

run (fromString "5+7");;                        // => 12
run (fromString "let y = 7 in y + 2 end");;     // => 9
run (fromString "let f x = x + 7 in f 2 end");; // => 9


/// Exercise 4.2
let computesumprg = @"let sum n = if n = 0 then 0 else n + sum (n-1) in sum 1000 end";;
run (fromString computesumprg);;      // => 500,500

let computepowerprg = @"let power n = if n = 0 then 1 else 3 * power (n-1) in power 8 end";;
run (fromString computepowerprg);;    // => 6,561

let computepowersumprg =
    @"let power n = if n = 0 then 1 else 3 * power (n-1)
      in let sum x = if x = 11 then (power 11) else ((power x) + sum (x+1))
        in sum 0
        end
      end";;
run (fromString computepowersumprg);; // => 265,720

let computeeighthpowersumprg =
    @"let eighthpower n = n * n * n * n * n * n * n * n (* sorry for cheating :D *)
      in let sum x = if x = 10 then eighthpower 10 else eighthpower x + sum (x+1)
        in sum 1
        end
      end";;
run (fromString computeeighthpowersumprg);; // => 167,731,333


/// Exercise 4.3 and 4.4
let powerprg = @"let power x y = if y = 0 then 1 else x * power x (y-1) in power 2 6 end";;
run (fromString powerprg);; // => 64


/// Exercise 4.5
let andorprg = @"let inRange x = x < 5 && x > 0 in inRange 3 end";;
run (fromString andorprg);; // => 1 (true)
