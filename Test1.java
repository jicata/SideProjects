import org.junit.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;


import org.springframework.boot.test.context.SpringBootTest;


import org.springframework.context.annotation.PropertySource;
import org.springframework.test.context.ContextConfiguration;
import org.springframework.test.context.junit4.SpringRunner;

@RunWith(SpringRunner.class)
@SpringBootTest
@PropertySource("classpath:application.properties")
@ContextConfiguration
public class Test1 {


    @Test
    public void test() throws IllegalAccessException, InstantiationException, ClassNotFoundException {

        Class neccessaryClass = Classes.allClasses.get("LenServiceImpl");
        Assert.assertTrue(neccessaryClass.getDeclaredFields().length > 0);

        Assert.assertTrue(true);
    }

}
