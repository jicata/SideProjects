#include <iostream>
#include "pesho.h"
#include "gosho.h"
using namespace std;
//TEST TEMPLATES

int main() {

    Pesho p = Pesho();
	Gosho g = Gosho();
    cout << p.wow() << endl;
	bool nadenica;
	cin >> boolalpha >> nadenica;
	cout << boolalpha << g.bob(nadenica) << endl;
}