#include <sstream>
#include "Matrix.h"

Matrix::Matrix(unsigned int rows, unsigned int cols) :
    rows(rows),
    columns(cols),
    data(initRowsCols(rows, cols)) {
}

Matrix::Matrix(const Matrix& other) :
    rows(other.rows),
    columns(other.columns),
    data(initRowsCols(other.rows, other.columns)) {

    copyData(other.data, other.rows, other.columns,
             // NOTE: columns and rows now match between this and other, but writing it out for consistency:
             this->data, this->rows, this->columns);
}

unsigned int Matrix::getRows() const {
    return this->rows;
}

unsigned int Matrix::getColumns() const {
    return this->columns;
}

void Matrix::changeSize(unsigned int rows, unsigned int cols) {
    RowsCols newRowsCols = initRowsCols(rows, cols);

    copyData(this->data, this->rows, this->columns, newRowsCols, rows, cols);

    freeRowsCols(this->data, this->rows);

    this->rows = rows;
    this->columns = cols;
    this->data = newRowsCols;
}

DataType Matrix::get(unsigned int row, unsigned int col) const {
    ensureInBounds(row, col);

    return this->data[row][col];
}

void Matrix::set(unsigned int row, unsigned int col, DataType value) {
    ensureInBounds(row, col);

    this->data[row][col] = value;
}

std::string Matrix::toString() const {
    std::ostringstream stream;

    for (unsigned int row = 0; row < this->rows; row++) {
        for (unsigned int col = 0; col < this->columns; col++) {
            stream << this->data[row][col];

            if (col < this->columns - 1) {
                stream << " ";
            }
        }

        stream << std::endl;
    }

    return stream.str();
}

void Matrix::ensureInBounds(unsigned int row, unsigned int col) const {
    if (row < 0 || row >= this->rows
        || col < 0 || col >= this->columns) {
        throw std::range_error("Out of bounds");
    }
}

RowsCols Matrix::initRowsCols(unsigned int rows, unsigned int cols) {
    RowsCols data;
    data = new Row[rows];
    for (unsigned int row = 0; row < rows; row++) {
        data[row] = new DataType[cols]{};
    }

    return data;
}

void Matrix::freeRowsCols(RowsCols data, unsigned int rows) {
    for (unsigned int row = 0; row < rows; row++) {
        delete[] data[row];
    }

    delete[] data;
}

void Matrix::copyData(RowsCols source, unsigned int sourceRows, unsigned int sourceCols, RowsCols dest, unsigned int destRows, unsigned int destCols) {
    const unsigned int rows = sourceRows < destRows ? sourceRows : destRows;
    const unsigned int cols = sourceCols < destCols ? sourceCols : destCols;

    for (unsigned int row = 0; row < rows; row++) {
        for (unsigned int col = 0; col < cols; col++) {
            dest[row][col] = source[row][col];
        }
    }
}

Matrix& Matrix::operator=(const Matrix& other) {
    if (this != &other) {
        freeRowsCols(this->data, this->rows);

        this->rows = other.rows;
        this->columns = other.columns;

        this->data = initRowsCols(other.rows, other.columns);

        copyData(other.data, other.rows, other.columns,
                 this->data, this->rows, this->columns);
    }

    return *this;
}

Matrix::~Matrix() {
    freeRowsCols(this->data, this->rows);
}


