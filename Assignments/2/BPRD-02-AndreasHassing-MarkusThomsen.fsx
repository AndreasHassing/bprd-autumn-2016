module BPRD.AbhnMatho.Exercise2

open System.Text.RegularExpressions

/// Exercise 3.2
/// Write a regular expression that recognizes all
/// sequences consisting of `a` and `b`, where two
/// `a`'s are always separated by at least one `b`.
///
/// Then construct the corresponding NFA, and attempt
/// to find a DFA corresponding to the NFA.
let abrgx = Regex @"^([^a]a|b)+$" // this regex is wrong! the string `a` won't work.
// regex reads: from the start of the line, find an `a`
// (but not two `a`'s in a row) or a `b`, one or more times
// until the end of the line

// Refer to the image files: `NFA.png` and `NDFA.png` for the
// remainder of this exercise.


/// Exercise 3.3
let exprstr = "let z = (17) in z + 2 * 3 end EOF"

//    Main
// A: Expr EOF
// F: NAME EQ Expr IN Expr END EOF
// G: NAME EQ Expr IN Expr * Expr END EOF
// C: NAME EQ Expr IN Expr * 3 END EOF
// H: NAME EQ Expr IN Expr + Expr * 3 END EOF
// C: NAME EQ Expr IN Expr + 2 * 3 END EOF
// B: NAME EQ Expr IN z + 2 * 3 END EOF
// E: NAME EQ (Expr) IN z + 2 * 3 END EOF
// C: NAME EQ (17) IN z + 2 * 3 END EOF
// B: z EQ (17) IN z + 2 * 3 END EOF


/// Exercise 3.4
// Refer to the image file: `Tree.png`


/// Exercise 3.5
// Here we totally pretend that we did that exercise and have successfully
// configured everything according to the specs.


/// Exercise 3.6
/// Define the function `compString`.
// the load statements might need to be changed for you
#load "../../Expr/Expr.fs";;
#load "../../Expr/Parse.fs";;

open Expr
open Parse

let compString s =
    scomp (fromString s) []


/// Exercise 3.7
// See the modified files `Absyn.fs`, `ExprLex.fsl` and `ExprPar.fsy`.
