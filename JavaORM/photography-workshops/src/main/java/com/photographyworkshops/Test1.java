package com.photographyworkshops;

import com.photographyworkshops.terminal.Terminal;
import org.junit.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;


import org.mockito.InjectMocks;
import org.mockito.runners.MockitoJUnitRunner;
import org.springframework.boot.test.context.SpringBootTest;


import org.springframework.context.annotation.PropertySource;


@RunWith(MockitoJUnitRunner.class)
@PropertySource("classpath:application.properties")
public class Test1 {

    @InjectMocks
    private Terminal app = new Terminal();

    @Test
    public void test() throws Exception {

        app.run("arg");

        Assert.assertTrue(app.lenRepo.count()==285);
    }

}
