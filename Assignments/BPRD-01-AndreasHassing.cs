namespace BPRD.Abhn {
    using System;
    using System.Collections.Generic; // Dictionary

    /// Exercise 1.4
    /// (i) Create the Expression data type in C#
    /// Part (ii), (iii) and (iv) are handled in the Main function
    public class Exercise1dot4 {
        public abstract class Expr {
            public abstract int Eval(Dictionary<string, int> env);
            public abstract Expr Simplify();
            public abstract override string ToString();
            public static Expr Zero = new CstI(0);
        }

        public class CstI : Expr {
            private readonly int i;
            public CstI(int i) { this.i = i; }
            public override int Eval(Dictionary<string, int> env) { return this.i; }
            public override Expr Simplify() { return this; }
            public override string ToString() { return "" + i; }
        }

        public class Var : Expr {
            private readonly string name;
            public Var(string name) { this.name = name; }
            public override int Eval(Dictionary<string, int> env) { return env[this.name]; }
            public override Expr Simplify() { return this; }
            public override string ToString() { return name; }
        }

        public abstract class Binop : Expr {
            internal readonly Expr e1, e2;
            public Binop(Expr e1, Expr e2) { this.e1 = e1; this.e2 = e2; }
            internal static bool IsCst(Expr e) {
                return e.GetType() == typeof(CstI);
            }
        }

        public class Add : Binop {
            public Add(Expr e1, Expr e2) : base(e1, e2) {}
            public override int Eval(Dictionary<string, int> env) { return e1.Eval(env) + e2.Eval(env); }

            public override Expr Simplify() {
                Expr s1 = e1.Simplify(), s2 = e2.Simplify();
                bool s1isCst = IsCst(s1), s2isCst = IsCst(s2);

                if (s1isCst && s1.Eval(null) == 0) {
                    return s2;
                } else if (s2isCst && s2.Eval(null) == 0) {
                    return s1;
                }

                return this;
            }

            public override string ToString() { return string.Format("Add({0}, {1})", e1, e2); }
        }

        public class Mul : Binop {
            public Mul(Expr e1, Expr e2) : base(e1, e2) {}
            public override int Eval(Dictionary<string, int> env) { return e1.Eval(env) * e2.Eval(env); }

            /// Helper for Simplify that takes care of the cases when
            /// Mul can be simplified.
            private Expr simp(int sv, Expr other) {
                switch (sv) {
                    case 0:
                        return Zero;
                    case 1:
                        return other;
                    default:
                        return this;
                }
            }

            private Expr simpBothCst(Expr e1, Expr e2) {
                var e1v = e1.Eval(null);
                var e2v = e2.Eval(null);

                if      (e1v == 1) return e2;
                else if (e2v == 1) return e1;
                else if (e1v == 0 || e2v == 0) return Zero;

                return this;
            }

            public override Expr Simplify() {
                Expr s1 = e1.Simplify(), s2 = e2.Simplify();
                bool s1isCst = IsCst(s1), s2isCst = IsCst(s2);

                if (s1isCst && s2isCst) {
                    return simpBothCst(s1, s2);
                }

                if (s1isCst) {
                    var s1v = s1.Eval(null);
                    return simp(s1v, s2);
                } else if (s2isCst) {
                    var s2v = s2.Eval(null);
                    return simp(s2v, s1);
                }

                return this;
            }

            public override string ToString() { return string.Format("Mul({0}, {1})", e1, e2); }
        }

        public class Sub : Binop {
            public Sub(Expr e1, Expr e2) : base(e1, e2) {}
            public override int Eval(Dictionary<string, int> env) { return e1.Eval(env) - e2.Eval(env); }

            public override Expr Simplify() {
                Expr s1 = e1.Simplify(), s2 = e2.Simplify();
                bool s1isCst = IsCst(s1), s2isCst = IsCst(s2);

                // x - 0 = x
                if (s2isCst && s2.Eval(null) == 0) return s1;
                // x - x = 0
                else if (s1isCst && s2isCst && s1.Eval(null) == s2.Eval(null)) return Zero;

                return this;
            }

            public override string ToString() { return string.Format("Sub({0}, {1})", e1, e2); }
        }

        public static void Main() {
            var e1 = new Add(new CstI(17), new Var("z")); // => Add(17, z)

            // WriteLine automatically calls .ToString on objects passed to it
            Console.WriteLine(e1);

            // (ii) Create three more expressions and print them
            //      Easter egg: I totally stole them from Intro2.fs
            var e2 = new CstI(17); // => 17
            var e3 = new Add(new CstI(3), new Var("a")); // => Add(3, a)
            var e4 = new Add(new Mul(new Var("b"), new CstI(9)), new Var("a"));
                    // => Add(Mul(b, 9), a)

            Console.WriteLine(string.Format("{0}\n{1}\n{2}", e2, e3, e4));

            // (iii) Add the eval function to each subclass of `Expr`
            var env = new Dictionary<string, int> {
                {"x", 5},
                {"y", 42}
            };
            var e5 = new Add(new Var("x"), new CstI(5));
            Console.WriteLine(e5.Eval(env)); // => 10

            // (iv) Add the simplify function to each subclass of `Expr`
            var e6 = new Add(new Var("x"), new CstI(0)); // => x
            var e7 = new Sub(new Var("x"), new CstI(0)); // => x
            var e8 = new Mul(new CstI(5), new CstI(0));  // => 0
            var e9 = new Mul(new Var("x"), new CstI(1)); // => x
            Console.WriteLine(string.Format("{0}\n{1}\n{2}\n{3}",
                e6.Simplify(), e7.Simplify(), e8.Simplify(), e9.Simplify()));
        }
    }
}
