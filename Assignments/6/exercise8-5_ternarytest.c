void main(int n) {
    int x;
    x =   5 > 4  ? 100 : -1;
    print x;

    print 5 == 4 ?  50 : -1;

    10 == 10 ? otherFunc() : 0; // calls otherFunc()
    10 == 5  ? otherFunc() : 0; // 0, and nothing happens
}

int otherFunc() {
    print 42;
    return 1;
}
