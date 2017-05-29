package com.testapp;

import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface repo extends CrudRepository<entityy, Integer> {
}
