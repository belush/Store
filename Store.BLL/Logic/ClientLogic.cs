using System.Collections.Generic;
using AutoMapper;
using Store.BLL.DTO;
using Store.BLL.Interfaces;
using Store.DAL.Entities;
//using Store.DAL.Entities;
using Store.DAL.Interfaces;

namespace Store.BLL.Logic
{
    public class ClientLogic : IClientLogic
    {
        //private readonly IClientRepository _repository;

        //public ClientLogic(IClientRepository repository)
        //{
        //    _repository = repository;
        //}

        //public IEnumerable<ClientProfile> GetAll()
        //{
        //    return _repository.GetAll().ToList();
        //}

        //public ClientProfile Get(string id)
        //{
        //    return _repository.Get(id);
        //}

        //public IEnumerable<ClientProfile> Find(Func<ClientProfile, bool> predicate)
        //{
        //    return _repository.Find(predicate).ToList();
        //}

        //public void Add(ClientProfile client)
        //{
        //    _repository.Add(client);
        //}

        //public void Delete(int id)
        //{
        //    _repository.Delete(id);
        //}

        //public void Edit(ClientProfile client)
        //{
        //    _repository.Edit(client);
        //}

        private readonly IClientRepository _repository;

        public ClientLogic(IClientRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<UserDTO> GetAll()
        {
            var clients = _repository.GetAll();
            var users = Mapper.Map<IEnumerable<ClientProfile>, IEnumerable<UserDTO>>(clients);

            return users;
        }

        public UserDTO Get(string id)
        {
            var client = _repository.Get(id);
            var user = Mapper.Map<ClientProfile, UserDTO>(client);

            return user;
        }

        public void Add(UserDTO userDto)
        {
            var client = Mapper.Map<UserDTO, ClientProfile>(userDto);
            _repository.Add(client);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public void Edit(UserDTO userDto)
        {
            var client = Mapper.Map<UserDTO, ClientProfile>(userDto);
            _repository.Edit(client);
        }
    }
}