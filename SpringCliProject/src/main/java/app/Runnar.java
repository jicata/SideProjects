package app;

import org.springframework.boot.CommandLineRunner;
import org.springframework.stereotype.Component;

@Component
public class Runnar implements CommandLineRunner {
    @Override
    public void run(String... strings) throws Exception {
        System.out.println("kur");

    }
}
