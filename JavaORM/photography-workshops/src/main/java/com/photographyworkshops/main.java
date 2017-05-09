package com.photographyworkshops;

        import org.springframework.boot.SpringApplication;
        import org.springframework.boot.autoconfigure.SpringBootApplication;
        import org.springframework.context.annotation.PropertySource;


@SpringBootApplication


@PropertySource("classpath:config.properties")
@PropertySource("classpath:db.properties")
public class main {
    public static void main(String[] args) {
        SpringApplication.run(main.class,args);
    }
}
