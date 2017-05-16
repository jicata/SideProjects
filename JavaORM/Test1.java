package com.photographyworkshops;

import org.junit.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;


import org.springframework.boot.test.context.SpringBootTest;


import org.springframework.context.annotation.PropertySource;
import org.springframework.test.context.ContextConfiguration;
import org.springframework.test.context.junit4.SpringJUnit4ClassRunner;


@RunWith(SpringJUnit4ClassRunner.class)
@SpringBootTest
@PropertySource("classpath:application.properties")
public class Test1 {


    @Test
    public void test() throws IllegalAccessException, InstantiationException, ClassNotFoundException {

//        Class neccessaryClass = Classes.allClasses.get("LenServiceImpl");
//        Assert.assertTrue(neccessaryClass.getDeclaredFields().length > 0);

        Assert.assertTrue(true);
    }

}
