#include "BigInt.h"
BigInt::BigInt(std::string digits) :
	digits(digits) {
}

BigInt::BigInt(int number) :
	digits(intToString(number)) {
}

BigInt& operator+=(const BigInt & other) {
	*this = *this + other;
	return *this;
}

BigInt operator+(const BigInt & other) const {
	int maxTotalDigits = 1 + std::max(this->digits.size(), other.digits.size());

	std::string thisDigits = getPaddedWithZeroes(this->digits, maxTotalDigits);
	std::string otherDigits = getPaddedWithZeroes(other.digits, maxTotalDigits);

	std::string resultDigits(maxTotalDigits, '0');

	int carry = 0;
	for (int i = resultDigits.size() - 1; i >= 0; i--) {
		int thisDigit = thisDigits[i] - '0';
		int otherDigit = otherDigits[i] - '0';

		int sum = thisDigit + otherDigit + carry;
		carry = sum / 10;
		int sumDigit = sum - carry * 10;

		resultDigits[i] = sumDigit + '0';
	}

	std::string resultDigitsTrimmed;
	for (int i = 0; i < resultDigits.size(); i++) {
		if (resultDigits[i] != '0') {
			resultDigitsTrimmed = resultDigits.substr(i);
			break;
		}
	}

	return BigInt(resultDigitsTrimmed);
}

friend std::ostream& operator<<(std::ostream& stream, const BigInt &number);