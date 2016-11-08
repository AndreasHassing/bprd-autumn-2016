// beginning of method Selsort::SelectionSort
.method public static hidebysig
default void SelectionSort (int32[] arr)  cil managed
{
    // Method begins at RVA 0x20c8
    // Code size 72 (0x48)
    .maxstack 4             // for program analysis (http://jilc.sourceforge.net/ecma_p3_cil.shtml#_Must_Provide_Maxstack)

    // Initialize local variables
    .locals init (
        int32   V_0,        // initialize `i`,     local 0
        int32   V_1,        // initialize `least`, local 1
        int32   V_2,        // initialize `j`,     local 2
        int32   V_3)        // initialize `tmp`,   local 3

    // Assign 0 to `i`
    IL_0000:  ldc.i4.0      // push 0 onto stack as int32
    IL_0001:  stloc.0       // pop a value from stack into local variable 0 (i)
    IL_0002:  br IL_003e    // branch to IL_003e

    // Branch target of IL_0042 (outer for-loop body)
    IL_0007:  ldloc.0       // load local variable 0 (i) onto stack
    IL_0008:  stloc.1       // pop a value from stack into local variable 1 (least)
    // (inner for-loop initial assignment `j`)
    IL_0009:  ldloc.0       // load local variable 0 (i) onto stack
    IL_000a:  ldc.i4.1      // push 1 onto stack as int32
    IL_000b:  add           // pop two numbers from stack, add and push result to stack
    IL_000c:  stloc.2       // pop a value from stack into local variable 2 (j)
    IL_000d:  br IL_0023    // branch to IL_0023

    // Branch target of IL_0027 (inner for-loop body)
    // (if-statement)
    IL_0012:  ldarg.0       // load argument 0 (arr) onto stack
    IL_0013:  ldloc.2       // load local variable 2 (j) onto stack
    IL_0014:  ldelem.i4     // load array element onto stack as int32
    IL_0015:  ldarg.0       // load argument 0 (arr) onto stack
    IL_0016:  ldloc.1       // load local variable 1 (least) onto stack
    IL_0017:  ldelem.i4     // load array element onto stack as int32
    IL_0018:  bge IL_001f   // branch to IL_001f if greater than or equal to
    IL_001d:  ldloc.2       // .. otherwise load local variable 2 (j) onto stack
    IL_001e:  stloc.1       // pop a value from stack into local variable 1 (least)

    // Branch target of IL_0018 (inner for-loop increment)
    IL_001f:  ldloc.2       // load local variable 2 (j) onto stack
    IL_0020:  ldc.i4.1      // push 1 onto stack as int32
    IL_0021:  add           // add two top elements on stack (j+1)
    IL_0022:  stloc.2       // pop a value from stack into local variable 2 (j)

    // Branch target of IL_000d (inner for-loop condition)
    IL_0023:  ldloc.2       // load local variable 2 (j) onto stack
    IL_0024:  ldarg.0       // load argument 0 (arr) onto stack
    IL_0025:  ldlen         // push length of array onto stack
    IL_0026:  conv.i4       // convert to int32, and push result to stack
    IL_0027:  blt IL_0012   // branch to IL_0012 if less than

    // (after inner for-loop)
    IL_002c:  ldarg.0       // load argument 0 (arr) onto stack
    IL_002d:  ldloc.0       // load local variable 0 (i) onto stack
    IL_002e:  ldelem.i4     // load array element onto stack as int32
    IL_002f:  stloc.3       // pop a value from stack into local variable 3 (tmp)
    IL_0030:  ldarg.0       // load argument 0 (arr) onto stack
    IL_0031:  ldloc.0       // load local variable 0 (i) onto stack
    IL_0032:  ldarg.0       // load argument 0 (arr) onto stack
    IL_0033:  ldloc.1       // load local variable 1 (least) onto stack
    IL_0034:  ldelem.i4     // load array element onto stack as int32
    IL_0035:  stelem.i4     // pop a value from stack into array element
    IL_0036:  ldarg.0       // load argument 0 (arr) onto stack
    IL_0037:  ldloc.1       // load local variable 1 (least) onto stack
    IL_0038:  ldloc.3       // load local variable 3 (tmp) onto stack
    IL_0039:  stelem.i4     // pop a value from stack into array element
    // (outer for-loop increment)
    IL_003a:  ldloc.0       // load local variable 0 (i) onto stack
    IL_003b:  ldc.i4.1      // push 1 onto stack as int32
    IL_003c:  add           // add two top elements on stack
    IL_003d:  stloc.0       // pop a value from stack into local variable 0 (i)

    // Branch target of IL_0002 (outer for-loop condition)
    IL_003e:  ldloc.0       // load local variable 0 (i) onto stack
    IL_003f:  ldarg.0       // load argument 0 (arr) onto stack
    IL_0040:  ldlen         // push length of array onto stack
    IL_0041:  conv.i4       // convert to int32, and push result to stack
    IL_0042:  blt IL_0007   // branch to IL_0007 if less than

    IL_0047:  ret
} // end of method Selsort::SelectionSort
