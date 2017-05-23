package com.testapp;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;


@SpringBootApplication
//@PropertySource("application.properties")
public class Example {
//
//    @GetMapping("/")
//    String home() {
//        return "Hello World!";
//    }

    public static void main(String[] args) throws Exception {

        SpringApplication.run(Example.class,args);
    }

}