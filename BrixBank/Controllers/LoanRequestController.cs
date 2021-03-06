﻿using AutoMapper;
using BrixBank.Services.Interfaces;
using BrixBank.Services.Models;
using BrixBank.webApi.DTO;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BrixBank.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoanRequestController : ControllerBase
    {
        private readonly ILoanRequestServies _service;
        private readonly IMapper _mapper;
        public LoanRequestController(ILoanRequestServies service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public void LoanRequestFor([FromBody] LoanRequestDTO loanRequestDTO)
        {
            try
            {
                _service.Reqest(_mapper.Map<LoanRequestModel>(loanRequestDTO));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
