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
