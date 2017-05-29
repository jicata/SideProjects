#include<iostream>

#include "BigInt.h"

using namespace std;

int main() {
    typedef BigInt Number;
    Number fibA = 0;
    Number fibB = 1;

    int firstFibN, lastFibN;
    cin >> firstFibN >> lastFibN;

    Number fibSum = 0;
    for (int i = 2; i <= lastFibN; i++) {
        Number current = fibA + fibB;

        if (i >= firstFibN) {
            fibSum += current;
        }

        fibA = fibB;
        fibB = current;
    }

    cout << fibSum << endl;

    return 0;
}
