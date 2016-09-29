//BPRD-04-AndreasHassing-MarkusThomsen.fsx

// The next line only exists to show that the
// FsLexYacc runtime is required to run these
// samples, so make sure to run fsi with
// `-r /your/path/to/FsLexYacc.Runtime.dll`.
//#r "FsLexYacc.Runtime.dll";;
#load "Absyn.fs";;
#load "FunPar.fs";;
#load "FunLex.fs";;
#load "Parse.fs";;
#load "HigherFun.fs";;
#load "ParseAndRunHigher.fs";;

open ParseAndRunHigher;;

/// Exercise 6.1
run (fromString @"let add x = let f y = x+y in f end
in add 2 5 end");; // => Int 7

run (fromString @"let add x = let f y = x+y in f end
in let addtwo = add 2
    in addtwo 5 end
end");; // => Int 7

// "the third one"
// yes the output is expected, as the `x` defined
// in the closure will be captured in the scope of the
// closure, and the new `x` that equals 77 is never used.
run (fromString @"let add x = let f y = x+y in f end
in let addtwo = add 2
    in let x = 77 in addtwo 5 end
    end
end");; // => Int 7

// "the last one"
// in this one we get a closure back, that encapsulates
// another closure. The outer closure is one containing
// the value of `x` to be 2 and the other closure. The
// other closure can be called, and will add the value of
// `x` (which is 2), to the argument that the closure is
// called with (which will be put into the variable `y`).
run (fromString @"let add x = let f y = x+y in f end
in add 2 end");; (* =>
    HigherFun.value =
        Closure
            ("f","y",Prim ("+",Var "x",Var "y"),
            [("x", Int 2);
            ("add",
            Closure
                ("add","x",Letfun ("f","y",Prim ("+",Var "x",Var "y"),Var "f"),[]))]) *)


/// Exercise 6.2
// See:
//   Absyn.fs:     a definition of the `Fun` expression
//   FunLex.fsl:   LAMBDA token `->`, and the `fun` keyword
//   FunPar.fsy:   parsing of lambda functions
//   HigherFun.fs: evaluation of lambda functions


/// Exercise 6.3
// We did this in exercise 6.2, as we thought the
// functionality was needed all around to begin with.
// Following are some working examples of the functionality.
run (fromString @"let add x = fun y -> x+y in add 2 5 end");;
    // => Int 7

run (fromString @"let y = 22 in fun z -> z+y end");;
    // => Clos ("z",Prim ("+",Var "z",Var "y"),[("y", Int 22)])


/// Exercise 6.4
// (i) Type rule tree for the following ML-Micro program,
//     and why should should the type of `f` be polymorphic?
@"let f x = 1
  in f f end";;
// I can't find anything in PLC about creating type rule trees :(

// I'm assuming you mean that `x` should be polymorphic, in which
// case I can explain the polymorphism:
// `f` can be called with any type for `x`, because `x` is unused,
// thus `x` should be polymorphic, as we can't derive its type.

// (ii) Same as for (i), except: why should the type of `f`
//      NOT be polymorphic?
@"let f x = if x<10 then 42 else f(x+1)
  in f 20 end"

// Again, I have not been able to find anything about type rule trees.

// For `x` in this `f`, it must not be polymorphic, as `x` is compared
// against an integer, an integer is added to `x`, and `x` is set to be
// 20 when running the function. `x` is monomorphic, and is, more
// specifically, an integer.


/// Exercise 6.5
// For your copy-pasting convenience :)
#load "Absyn.fs";;
#load "FunPar.fs";;
#load "FunLex.fs";;
#load "Parse.fs";
#load "TypeInference.fs";;
#load "ParseAndType.fs";;

open ParseAndType;;

inferType (fromString "let f x = 1 in f 7 + f false end");; // => "int"

// (i) Use the type inference on the micro-ML programs shown below,
//     and report what type the program has. Some of the type inferences
//     will fail because the programs are not typable in micro-ML;
//     in those cases, explain why the program is not typable:
inferType (fromString @"let f x = 1
                        in f f end");; // => "int"

inferType (fromString @"let f g = g g
                        in f end");;
    // => System.Exception: type error: circularity
    // `g` must be a function with types: `α -> α`,
    // and when its called with itself as argument,
    // we get a circular type inference chain
    // (which is illegal).

inferType (fromString @"let f x =
                            let g y = y
                            in g false end
                        in f 42 end");; // => "bool"

inferType (fromString @"let f x =
                            let g y = if true then y else x
                            in g false end");;
    // => System.Exception: parse error near line 3, column 42
    // The `Letfun` expression is defined as follows:
    // LET NAME NAME EQ Expr IN Expr END { Letfun($2, $3, $5, $7) }
    // in the parser. So we get a parser error, as the first let
    // function is missing its Letbody.

inferType (fromString @"in f 42 end");;
    // => System.Exception: parse error near line 1, column 2
    // This is invalid Micro-ML syntax. No program expression
    // can start with the `in` keyword.

inferType (fromString @"let f x =
                            let g y = if true then y else x
                            in g false end
                        in f true end");; // => "bool"

// (ii) Write micro-ML programs for which the micro-ML type inference
//      report the following types:

// bool -> bool
inferType (fromString @"let falsifyer x = x=false in falsifyer end");;

// int -> int
inferType (fromString @"let increment x = x+1 in increment end");;

// int -> int -> int
inferType (fromString @"let add x = let add2 y = x+y in add2 end in add end");;

// ’a -> ’b -> ’a
inferType (fromString @"let do x = let id y = x in id end in do end");;

// ’a -> ’b -> ’b
inferType (fromString @"let do x = let id y = y in id end in do end");;

// (’a -> ’b) -> (’b -> ’c) -> (’a -> ’c)


// ’a -> ’b
// We can't seem to find a way to make a program with this type inference.

// ’a
// Not this one either :( .
