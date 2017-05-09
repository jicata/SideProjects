package com.social.repositories;

import com.social.entities.Bike;
import org.junit.Before;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.orm.jpa.DataJpaTest;
import org.springframework.boot.test.autoconfigure.orm.jpa.TestEntityManager;
import org.springframework.test.annotation.Rollback;
import org.springframework.test.context.ActiveProfiles;
import org.springframework.test.context.junit4.SpringRunner;

import javax.persistence.Query;
import javax.transaction.Transactional;
import java.util.List;

import static org.junit.Assert.assertEquals;

@RunWith(SpringRunner.class)
@DataJpaTest
@ActiveProfiles("test")
public class BikeRepositoryTest {

    public static final String MODEL = "BMX";

    @Autowired
    private TestEntityManager em;

    @Autowired
    private BikeRepository bikeRepository;

    @Before
    public void setUp() throws Exception {
        //Arrange
        Bike bike = new Bike();
        bike.setModel(MODEL);
        bike.setGears(11);
        this.em.persist(bike);
    }

    @Test
    public void findBikesWithMoreThan10GearsGivenValidBike_ShouldReturnBike() throws Exception {
        //Act
        List<Bike> bikes = this.bikeRepository.findBikesWithMoreThan10Gears();

        //Assert
        assertEquals(1, bikes.size());

        //Assert
        Bike bike = bikes.get(0);
        assertEquals(MODEL, bike.getModel());
    }

    @Test
    @Transactional
    public void testPersist() throws Exception {

        //Act
        Bike bike = new Bike();
        this.bikeRepository.save(bike);
        long id = bike.getId();

        Query q = this.em.getEntityManager().createQuery("SELECT b FROM Bike AS b " +
                "WHERE b.id = :id").setParameter("id", id);

        List<Bike> bikes = q.getResultList();

        assertEquals(id, bikes.get(0).getId());
    }
}