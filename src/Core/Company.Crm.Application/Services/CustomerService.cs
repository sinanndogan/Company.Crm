using AutoMapper;
using Company.Crm.Application.Dtos;
using Company.Crm.Application.Services.Abstracts;
using Company.Crm.Domain.Entities;
using Company.Crm.Domain.Repositories;
using Company.Framework.Dtos;
using Company.Framework.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Company.Crm.Application.Services;

// Concrete-Abstract
public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IGenderRepository _genderRepository;
    private readonly IMapper _mapper;

    public CustomerService(IMapper mapper, ICustomerRepository customerRepository, IGenderRepository genderRepository)
    {
        _mapper = mapper;
        _customerRepository = customerRepository;
        _genderRepository = genderRepository;
    }

    public ServiceResponse<List<CustomerDto>> GetAll()
    {
        var entityList = _customerRepository.GetAll()
            .Include(e => e.UserFk)
            .Include(e => e.TitleFk);

        #region Old

        //var dtoList = new List<CustomerDto>();
        //foreach (var item in list)
        //{
        //    dtoList.Add(new CustomerDto
        //    {
        //        Id = item.Id,
        //        TitleName = "Developer",
        //        CompanyName = item.CompanyName,
        //        GenderId = item.GenderId,
        //        TitleId = item.TitleId
        //    });
        //}

        #endregion

        var dtoList = _mapper.Map<List<CustomerDto>>(entityList);

        return new ServiceResponse<List<CustomerDto>>(dtoList);
    }

    public ServicePaginationResponse<List<CustomerDto>> GetPaged(PaginationRequest req)
    {
        var entityQuery = _customerRepository.GetAll()
            .Include(e => e.StatusTypeFk)
            .Include(e => e.GenderFk)
            .Include(e => e.UserFk)
            .Include(e => e.TitleFk)
            .OrderByDescending(c => c.Id);

        var totalEntity = entityQuery.Count();
        var pagedList = entityQuery.ToPagedList(req);
        var dtoList = _mapper.Map<List<CustomerDto>>(pagedList);

        return new ServicePaginationResponse<List<CustomerDto>>(dtoList, totalEntity, req);
    }

    public ServiceResponse<CustomerDto?> GetById(int id)
    {
        var entity = _customerRepository.GetById(id);
        if (entity == null) return new ServiceResponse<CustomerDto?>("Not Found!");

        var dto = _mapper.Map<CustomerDto>(entity);

        if (entity.GenderId.HasValue)
        {
            var gender = _genderRepository.GetById(entity.GenderId.Value);
            if (gender != null) dto.GenderName = gender.Name;
        }

        return new ServiceResponse<CustomerDto?>(dto);
    }

    public ServiceResponse<CreateOrUpdateCustomerDto?> GetForEditById(int id)
    {
        var entity = _customerRepository.GetById(id);
        var dto = _mapper.Map<CreateOrUpdateCustomerDto>(entity);
        return new ServiceResponse<CreateOrUpdateCustomerDto?>(dto);
    }

    public ServiceResponse<bool> Insert(CreateOrUpdateCustomerDto dto)
    {
        var customer = _mapper.Map<Customer>(dto);
        return new ServiceResponse<bool>(_customerRepository.Insert(customer));
    }

    public ServiceResponse<bool> Update(CreateOrUpdateCustomerDto dto)
    {
        var customer = _mapper.Map<Customer>(dto);
        return new ServiceResponse<bool>(_customerRepository.Update(customer));
    }

    public ServiceResponse<bool> Delete(CustomerDto dto)
    {
        var customer = _mapper.Map<Customer>(dto);
        return new ServiceResponse<bool>(_customerRepository.Delete(customer));
    }

    public ServiceResponse<bool> DeleteById(int id)
    {
        return new ServiceResponse<bool>(_customerRepository.DeleteById(id));
    }

    public ServiceResponse<List<CustomerDto>> GetAllByRegionId(int regionId)
    {
        var entityList = _customerRepository.GetAllByRegionId(regionId);
        var dtoList = _mapper.Map<List<CustomerDto>>(entityList.ToList());
        return new ServiceResponse<List<CustomerDto>>(dtoList);
    }
}