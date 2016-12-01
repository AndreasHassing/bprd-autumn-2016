/// Exercise 12.1
// See Contcomp.fs: the `addIFZERO` and `addIFNZRO` auxiliary functions.
(* -- Output of ex16.c compilation --
  [LDARGS; CALL (1,"L1"); STOP; Label "L1"; GETBP; LDI; IFZERO "L3"; GOTO "L2";
   Label "L3"; CSTI 1111; PRINTI; INCSP -1; Label "L2"; CSTI 2222; PRINTI;
   RET 1]
*)


/// Exercise 12.2
// See Contcomp.fs: the `addCST` function.
(* -- Output of optimization_test.c compilation --
  [LDARGS; CALL (0,"L1"); STOP; Label "L1"; CSTI 33; PRINTI; INCSP -1;
   Label "L8"; CSTI 11; PRINTI; INCSP -1; Label "L7"; CSTI 12; PRINTI;
   INCSP -1; Label "L6"; CSTI 11; PRINTI; INCSP -1; Label "L5"; CSTI 33;
   PRINTI; INCSP -1; Label "L4"; CSTI 22; PRINTI; INCSP -1; GOTO "L2";
   Label "L3"; CSTI -1; PRINTI; INCSP -1; Label "L2"; CSTI 0; PRINTI; RET 0]
*)


/// Exercise 12.3
#load "Absyn.fs";;
#load "Contcomp.fs";;
open Absyn;;
open Contcomp;;

cExpr (Cond(CstI 1, CstI 1111, CstI 2222)) ([], 0) [] [];;
// => [CSTI 1111; GOTO "L0"; Label "L1"; CSTI 2222; Label "L0"]
// again, we skip the else block instead of removing it, as described
// in `optimization_test.c` at the final example.

cExpr (Cond(CstI 0, CstI 1111, CstI 2222)) ([], 0) [] [];;
// => [Label "L3"; CSTI 2222; Label "L2"]


/// Exercise 12.4
// Outputted code from using Cond and Andalso or Orelse are the same.
// The code used to generate the code is not. The Cond code generation
// is much simpler, and looks almost identical to the If-statement code.

(* -- Output of ex13.c compilation, using Cond --
  [LDARGS; CALL (1,"L1"); STOP; Label "L1"; INCSP 1; GETBP; CSTI 1; ADD;
   CSTI 1889; STI; INCSP -1; GOTO "L3"; Label "L2"; GETBP; CSTI 1; ADD; GETBP;
   CSTI 1; ADD; LDI; CSTI 1; ADD; STI; INCSP -1; GETBP; CSTI 1; ADD; LDI;
   CSTI 4; MOD; IFNZRO "L3"; GETBP; CSTI 1; ADD; LDI; CSTI 100; MOD;
   IFNZRO "L4"; GETBP; CSTI 1; ADD; LDI; CSTI 400; MOD; IFNZRO "L3";
   Label "L4"; GETBP; CSTI 1; ADD; LDI; PRINTI; INCSP -1; Label "L3"; GETBP;
   CSTI 1; ADD; LDI; GETBP; LDI; LT; IFNZRO "L2"; RET 1]
*)

(* -- Output of ex13.c compilation, using Andalso and Orelse --
  [LDARGS; CALL (1,"L1"); STOP; Label "L1"; INCSP 1; GETBP; CSTI 1; ADD;
   CSTI 1889; STI; INCSP -1; GOTO "L3"; Label "L2"; GETBP; CSTI 1; ADD; GETBP;
   CSTI 1; ADD; LDI; CSTI 1; ADD; STI; INCSP -1; GETBP; CSTI 1; ADD; LDI;
   CSTI 4; MOD; IFNZRO "L3"; GETBP; CSTI 1; ADD; LDI; CSTI 100; MOD;
   IFNZRO "L4"; GETBP; CSTI 1; ADD; LDI; CSTI 400; MOD; IFNZRO "L3";
   Label "L4"; GETBP; CSTI 1; ADD; LDI; PRINTI; INCSP -1; Label "L3"; GETBP;
   CSTI 1; ADD; LDI; GETBP; LDI; LT; IFNZRO "L2"; RET 1]
*)
