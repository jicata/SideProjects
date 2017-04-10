#ifndef BIG_INT_H
#define BIG_INT_H

#include<string>
#include<sstream>
#include<cmath>

class BigInt {
	std::string digits;
public:
	BigInt(std::string digits) :
		digits(digits) {
	}

	BigInt(int number) :
		digits(intToString(number)) {
	}

	BigInt& operator+=(const BigInt & other);

	BigInt operator+(const BigInt & other) const;

	friend std::ostream& operator<<(std::ostream& stream, const BigInt &number);

private:
	static std::string intToString(int number);

	static std::string getPaddedWithZeroes(std::string s, int widthNeeded);
};

inline std::ostream& operator<<(std::ostream& stream, const BigInt &number);

#endif // BIG_INT_H
