using System;
using System.Collections.Generic;
using AutoMapper;
using Store.BLL.DTO;
using Store.BLL.Interfaces;
using Store.DAL.Entities;
using Store.DAL.Interfaces;

namespace Store.BLL.Logic
{
    public class GoodLogic : IGoodLogic
    {
        private readonly IGoodRepository _repository;

        public GoodLogic(IGoodRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<GoodDTO> GetAll()
        {
            var goods = _repository.GetAll();
            var goodsDto = Mapper.Map<IEnumerable<Good>, IEnumerable<GoodDTO>>(goods);

            return goodsDto;
        }

        public GoodDTO Get(int? id)
        {
            if (id == null)
            {
                throw new ArgumentException("id null");
            }

            var good = _repository.Get(id.Value);
            var goodDto = Mapper.Map<Good, GoodDTO>(good);

            return goodDto;
        }

        public void Add(GoodDTO goodDto)
        {
            var good = Mapper.Map<GoodDTO, Good>(goodDto);

            _repository.Add(good);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public void Edit(GoodDTO goodDto)
        {
            var good = Mapper.Map<GoodDTO, Good>(goodDto);
            _repository.Edit(good);
        }

        public IEnumerable<GoodDTO> Search(string search, FilterModelDTO filterDto)
        {
            var filter = Mapper.Map<FilterModelDTO, FilterModel>(filterDto);
            var goods = _repository.Search(search, filter);
            var goodsDto = Mapper.Map<IEnumerable<Good>, IEnumerable<GoodDTO>>(goods);
            return goodsDto;
        }
    }
}