(* File MicroC/ParseAndContcomp.fs *)
module ParseAndContComp

let fromString = Parse.fromString

let fromFile = Parse.fromFile

let contCompileToFile = Contcomp.contCompileToFile
