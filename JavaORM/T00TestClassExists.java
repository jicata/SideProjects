package com.photographyworkshops;

import com.photographyworkshops.domain.dto.lens.LenImportJSONDto;
import com.photographyworkshops.parser.interfaces.ModelParser;
import com.photographyworkshops.repository.LenRepository;
import com.photographyworkshops.serviceImpl.LenServiceImpl;

import org.junit.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;

import org.springframework.test.context.junit4.SpringRunner;

import java.util.HashMap;
import java.util.Map;

@RunWith(SpringRunner.class)
@SpringBootTest
public class Test1 {

    // Error messages
    private static final String CLASS_NOT_PRESENT_ERROR_MESSAGE = "whatever error bro";
    public static Map<String, Class> allClasses = new HashMap<>();
    // Class names
    @Autowired
    private LenRepository lenRepository;

    @Autowired
    private ModelParser modelParser;

    @Autowired
    private LenServiceImpl service;


    @Test
    public void test() throws IllegalAccessException, InstantiationException, ClassNotFoundException {

        LenImportJSONDto jsonDto = new LenImportJSONDto();
        service.lenRepository = lenRepository;
        service.modelParser = modelParser;
        service.create(jsonDto);
        Assert.assertTrue(service.lenRepository.count() == 1);
    }
//    private Class getClass(String className) {
//        Assert.assertTrue(String.format(CLASS_NOT_PRESENT_ERROR_MESSAGE, className),
//                Classes.allClasses.containsKey(className));
//        return Classes.allClasses.get(className);
//    }
}
