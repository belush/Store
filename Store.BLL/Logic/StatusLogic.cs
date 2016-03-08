using System;
using System.Collections.Generic;
using AutoMapper;
using Store.BLL.DTO;
using Store.BLL.Interfaces;
using Store.DAL.Entities;
using Store.DAL.Interfaces;

namespace Store.BLL.Logic
{
    public class StatusLogic : IStatusLogic
    {
        private readonly IRepository<Status> _repository;

        public StatusLogic(IRepository<Status> repository)
        {
            _repository = repository;
        }

        public IEnumerable<StatusDTO> GetAll()
        {
            var statuses = _repository.GetAll();
            var statusesDto = Mapper.Map<IEnumerable<Status>, IEnumerable<StatusDTO>>(statuses);

            return statusesDto;
        }

        public void Edit(StatusDTO statusDto)
        {
            var status = _repository.Get(statusDto.Id);
            status.Name = statusDto.Name;
            _repository.Edit(status);
        }

        public void Add(StatusDTO statusDto)
        {
           
            var status = Mapper.Map<StatusDTO, Status>(statusDto);
            _repository.Add(status);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public StatusDTO Get(int? id)
        {
            if (id == null)
            {
                throw new ArgumentException("id null");
            }

            var status = _repository.Get(id.Value);
            var statusDto = Mapper.Map<Status, StatusDTO>(status);
            return statusDto;
        }
    }
}