package com.photographyworkshops;

import com.photographyworkshops.domain.entities.lens.Lens;
import com.photographyworkshops.repository.LenRepository;


import org.junit.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;


import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;

import org.springframework.test.context.junit4.SpringJUnit4ClassRunner;

import java.util.ArrayList;


@RunWith(SpringJUnit4ClassRunner.class)
@SpringBootTest
public class Test2 {

    @Autowired
    LenRepository lenRepository;

    @Test(expected = IllegalArgumentException.class)
    public void test() throws Exception {
        lenRepository.save(new Lens());
        ArrayList<String> strings = new ArrayList<String>();
        strings.remove(200);
    }

}
