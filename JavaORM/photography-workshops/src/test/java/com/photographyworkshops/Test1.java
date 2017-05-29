package com.photographyworkshops;

import com.photographyworkshops.domain.entities.lens.Lens;
import com.photographyworkshops.repository.LenRepository;


import org.junit.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;


import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;

import org.springframework.test.context.junit4.SpringJUnit4ClassRunner;


@RunWith(SpringJUnit4ClassRunner.class)
@SpringBootTest
public class Test1 {

    @Autowired
    LenRepository lenRepository;

    @Test
    public void test() throws Exception {
     lenRepository.save(new Lens());
    Assert.assertEquals(false,lenRepository.findAll().iterator().hasNext());
    }

}
