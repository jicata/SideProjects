package com.testapp;

import org.junit.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.junit4.SpringJUnit4ClassRunner;

import static org.junit.Assert.assertEquals;

@RunWith(SpringJUnit4ClassRunner.class)
@SpringBootTest
public class tests {

    @Autowired
    repo repoo;

    @Test
    public void homeSaysHello() {
        repoo.save(new entityy());
        Assert.assertEquals(true,repoo.findAll().iterator().hasNext());
       // assertEquals(1,1);
    }
}
