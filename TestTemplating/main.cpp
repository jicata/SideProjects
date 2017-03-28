#include <iostream>
#include "pesho.h"
#include "../C++Testing/ConsoleApplication1/ConsoleApplication1/gosho.h"

//TEST TEMPLATES

int main() {
	cout << isLower(13,2) << endl;
    cout << isLower(5.5,17.7) << endl;

    calculator<int> intCalc;
    cout << intCalc.add(13,2) << endl;
    cout << intCalc.subtract(3, 9) << endl;

    calculator<double> doubleCalc;
    cout << doubleCalc.add(13.1,2.17) << endl;
    cout << doubleCalc.subtract(3.33, 7.1) << endl;

    Pesho p = Pesho();
    cout << p.wow() << endl;
}