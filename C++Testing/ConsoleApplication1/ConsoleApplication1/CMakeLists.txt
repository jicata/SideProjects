cmake_minimum_required(VERSION 3.6)
project(Templating)

set(CMAKE_CXX_STANDARD 11)

set(SOURCE_FILES main.cpp pesho.cpp pesho.h)
add_executable(Templating ${SOURCE_FILES})