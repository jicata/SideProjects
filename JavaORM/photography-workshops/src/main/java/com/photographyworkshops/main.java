package com.photographyworkshops;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.PropertySource;

@SpringBootApplication
@PropertySource("application.properties")
public class Main {
    public static void main(String[] args) {
        //new SpringApplicationBuilder(Main.class).web(false).run(args);
        SpringApplication.run(Main.class,args);

    }
}
