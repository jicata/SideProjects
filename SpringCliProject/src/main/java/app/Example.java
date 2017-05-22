package app;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.boot.builder.SpringApplicationBuilder;
import org.springframework.context.annotation.PropertySource;
import org.springframework.web.bind.annotation.*;


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