package com.photographyworkshops;

import com.photographyworkshops.repository.LenRepository;

import org.junit.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;


import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;

import org.springframework.context.annotation.PropertySource;
import org.springframework.test.context.junit4.SpringJUnit4ClassRunner;


@RunWith(SpringJUnit4ClassRunner.class)
@SpringBootTest
@PropertySource("application.properties")
public class Test1 {

    @Autowired
    LenRepository lenRepository;

    @Test
    public void test() throws Exception {

    Assert.assertEquals(true,lenRepository.findAll().iterator().hasNext());
    }

}
