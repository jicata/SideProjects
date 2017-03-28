#pragma once
#include <iostream>
using namespace std;

template <typename T>
inline T isLower(T const& a, T const& b) {
	return a < b;
}

template <class Type>
class calculator {
public:
	Type add(Type const& a, Type const& b);
	Type subtract(Type const& a, Type const& b);
};

template <class Type>
Type calculator<Type>::add(Type const& a, Type const& b) {
	return a + b;
}

template <class Type>
Type calculator<Type>::subtract(Type const& a, Type const& b) {
	return a - b;
}
