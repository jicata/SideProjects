package com.social;

import com.social.controllers.BikeController;
import com.social.exception.BikeNotFoundException;
import com.social.models.viewModels.BikeViewModel;
import com.social.services.BikeService;
import org.junit.Before;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.mockito.ArgumentCaptor;
import org.mockito.Captor;
import org.mockito.Mock;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.WebMvcTest;
import org.springframework.boot.test.mock.mockito.MockBean;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageImpl;
import org.springframework.data.domain.Pageable;
import org.springframework.data.web.config.EnableSpringDataWebSupport;
import org.springframework.test.context.ActiveProfiles;
import org.springframework.test.context.junit4.SpringRunner;
import org.springframework.test.web.servlet.MockMvc;

import static org.hamcrest.CoreMatchers.is;
import static org.mockito.Mockito.*;
import static org.springframework.security.test.web.servlet.response.SecurityMockMvcResultMatchers.authenticated;
import static org.springframework.security.test.web.servlet.response.SecurityMockMvcResultMatchers.unauthenticated;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.*;

import java.util.Arrays;

import static org.mockito.Mockito.when;

@RunWith(SpringRunner.class)
@WebMvcTest(BikeController.class)
@ActiveProfiles("test")
@EnableSpringDataWebSupport
public class BikeControllerTest {

    public static final long INVALID_BIKE_ID = -1;

    public static final long BIKE_ID = 1;

    public static final String MODEL = "BMX";

    public static final int EXPECTED_PAGE_SIZE = 2;

    @Autowired
    private MockMvc mvc;

    @MockBean
    private BikeService bikeService;

    @Mock
    private Pageable pageable;

    @Captor
    private ArgumentCaptor<Pageable> captor;

    @Before
    public void setUp() throws Exception {
        //Arrange
        BikeViewModel bikeViewModel = new BikeViewModel();
        bikeViewModel.setId(BIKE_ID);
        bikeViewModel.setModel(MODEL);
        Page<BikeViewModel> bikePage = new PageImpl(Arrays.asList(bikeViewModel, bikeViewModel));
        when(this.bikeService.findById(BIKE_ID)).thenReturn(bikeViewModel);
        when(this.bikeService.findById(INVALID_BIKE_ID)).thenThrow(new BikeNotFoundException());
        when(this.bikeService.findAll(captor.capture())).thenReturn(bikePage);
    }


    @Test
    public void showRestBike() throws Exception {
        this.mvc
                .perform(get("/bikes/rest/1"))
                .andExpect(status().isOk())
                .andExpect(jsonPath("$.id", is((int)BIKE_ID)))
                .andExpect(jsonPath("$.model", is(MODEL)));
    }
}