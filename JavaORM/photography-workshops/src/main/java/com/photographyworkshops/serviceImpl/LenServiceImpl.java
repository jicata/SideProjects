package com.photographyworkshops.serviceImpl;

import com.photographyworkshops.domain.dto.lens.LenImportJSONDto;
import com.photographyworkshops.domain.entities.lens.Lens;
import com.photographyworkshops.parser.interfaces.ModelParser;
import com.photographyworkshops.repository.LenRepository;
import com.photographyworkshops.service.LenService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import javax.transaction.Transactional;

@Service
@Transactional
public class LenServiceImpl implements LenService {

    @Autowired
    public ModelParser modelParser;

    @Autowired
    public LenRepository lenRepository;

    @Override
    public void create(LenImportJSONDto lenDto) {
        Lens len = this.modelParser.convert(lenDto, Lens.class);
        this.lenRepository.save(len);
    }
}
