module BPRD.Abhn.Exercise1

// the load statements might need to be changed for you
#load "../Intro/Intro2.fs";;
#load "../Intcomp/Intcomp1.fs";;

open Intro2

/// Exercise 1.1
/// (iv)  Extend the language with conditional expressions (`If`)
type expr =
    | CstI of int
    | Var of string
    | Prim of string * expr * expr
    | If of expr * expr * expr

/// (i)   Extend `eval` by adding `min`, `max` and `==`
/// (iii) Rewrite `eval` to evaluate arguments of a primitive
/// (v)   Extend `eval` by adding the `If` operation
let rec eval e (env : (string * int) list) : int =
    match e with
    | CstI i           -> i
    | Var x            -> lookup env x
    | If(e1, e2, e3)   -> if eval e1 env <> 0 then eval e2 env else eval e3 env
    | Prim(op, e1, e2) ->
        let i1, i2 = eval e1 env, eval e2 env
        match op with
        | "+"   -> i1 + i2
        | "*"   -> i1 * i2
        | "-"   -> i1 - i2
        | "max" -> max i1 i2
        | "min" -> min i1 i2
        | "=="  -> if i1 = i2 then 1 else 0
        | _     -> failwith "unknown primitive"

/// (ii) Write example expressions, using `min`, `max` and `==`
let eequality = Prim("==", Prim("*", Var "c", CstI 5), CstI 390)
let eequalityv =
    eval eequality env    // => 1 (true)

let emax = Prim("max", CstI 7, CstI 9)
let emaxv = eval emax env // => 9

let emin = Prim("min", Prim("+", CstI 5, Var "a"), CstI 6)
let eminv = eval emin env // => 6

let eif = If(Prim("==", CstI 5, CstI 5), CstI 42, CstI 3)
let eifv = eval eif env   // => 42

let eif2 = If(Var "a", CstI 17, CstI 5)
let eif2v = eval eif2 env // => 17


/// Exercise 1.2
/// (i) Declare `aexpr`
type aexpr =
    | CstI of int
    | Var of string
    | Add of aexpr * aexpr
    | Mul of aexpr * aexpr
    | Sub of aexpr * aexpr

/// (ii) Write expressions
let ae1 = Sub(Var "v", Add(Var "w", Var "z"))

let ae2 = Mul(CstI 2, Sub(Var "v", Add(Var "w", Var "z")))

/// (iii) Write `fmt` for `aexpr` (format)
let rec fmt = function
    | CstI i -> string i
    | Var v  -> v
    | Add(e1, e2) -> sprintf "(%s + %s)" (fmt e1) (fmt e2)
    | Mul(e1, e2) -> sprintf "(%s * %s)" (fmt e1) (fmt e2)
    | Sub(e1, e2) -> sprintf "(%s - %s)" (fmt e1) (fmt e2)

/// (iv) Write `simplify` for `aexpr` (format)
let rec simplify = function
    | CstI i -> CstI i
    | Var v  -> Var v
    | Add(e1, e2) ->
        let s1, s2 = simplify e1, simplify e2
        match (s1, s2) with
        | (CstI 0, e) | (e, CstI 0) -> e
        | _ -> Add(s1, s2)
    | Mul(e1, e2) ->
        let s1, s2 = simplify e1, simplify e2
        match (s1, s2) with
        | (CstI 1, e) | (e, CstI 1) -> e
        | (CstI 0, _) | (_, CstI 0) -> CstI 0
        | _ -> Mul(s1, s2)
    | Sub(e1, e2) ->
        let s1, s2 = simplify e1, simplify e2
        match (s1, s2) with
        | (e, CstI 0)       -> e
        | (a, b) when a = b -> CstI 0
        | _ -> Sub(s1, s2)

/// (v) Write a function to do symbolic differentiation on `aexpr`
///     with respect to a single variable
let rec diff var = function
    | Var v when v = var -> CstI 1
    | CstI _ | Var _     -> CstI 0
    | Add(e1, e2) -> Add(diff var e1, diff var e2)
    | Sub(e1, e2) -> Sub(diff var e1, diff var e2)
    | Mul(e1, e2) -> Add(Mul(diff var e1, e2), Mul(e1, diff var e2))


/// Exercise 1.4
/// See `BPRD-01-AndreasHassing.cs`


/// Exercise 2.1
/// Extend `eval` (here named `eval2`) with multiple
/// sequential let-bindings.
open Intcomp1

type expr2 =
    | CstI of int
    | Var of string
    | Let of (string * expr2) list * expr2
    | Prim of string * expr2 * expr2

let rec eval2 e (env : (string * int) list) : int =
    let envfolder envacc (vn, ve) =
        (vn, eval2 ve envacc) :: envacc
    match e with
    | CstI i            -> i
    | Var x             -> lookup env x
    | Let(bs, ebody)    -> let env = List.fold envfolder env bs
                           eval2 ebody env
    | Prim("+", e1, e2) -> eval2 e1 env + eval2 e2 env
    | Prim("*", e1, e2) -> eval2 e1 env * eval2 e2 env
    | Prim("-", e1, e2) -> eval2 e1 env - eval2 e2 env
    | Prim _            -> failwith "unknown primitive"

let intcompexp = Let ([("x1", Prim("+", CstI 5, CstI 7));
                       ("x2", Prim("*", Var "x1", CstI 2))],
                      Prim("+", Var "x1", Var "x2"))


/// Exercise 2.2
/// Revise `freevars` to work for the language as extended
/// in exercise 2.1.
let rec freevars e : string list =
    match e with
    | CstI i         -> []
    | Var x          -> [x]
    | Let([], ebody) -> freevars ebody
    | Let((x, erhs) :: bs, ebody) ->
        union ((freevars erhs), minus (freevars (Let(bs, ebody)), [x]))
    | Prim(ope, e1, e2) -> union (freevars e1, freevars e2)

