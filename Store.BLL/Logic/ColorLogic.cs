using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Store.BLL.DTO;
using Store.BLL.Interfaces;
using Store.DAL.Entities;
using Store.DAL.Interfaces;

namespace Store.BLL.Logic
{
    public class ColorLogic : IColorLogic
    {
        private readonly IRepository<Color> _repository;

        public ColorLogic(IRepository<Color> repository)
        {
            _repository = repository;
        }

        public IEnumerable<ColorDTO> GetAll()
        {
            var colors = _repository.GetAll().ToList();
            var colorsDto = Mapper.Map<IEnumerable<Color>, IEnumerable<ColorDTO>>(colors);
            return colorsDto;
        }

        public ColorDTO Get(int? id)
        {
            if (id == null)
            {
                throw new ArgumentException("id null");
            }

            var color = _repository.Get(id.Value);
            var colorDto = Mapper.Map<Color, ColorDTO>(color);

            return colorDto;
        }

        public void Add(ColorDTO colorDto)
        {
            var color = Mapper.Map<ColorDTO, Color>(colorDto);
            _repository.Add(color);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public void Edit(ColorDTO colorDto)
        {
            var color = Mapper.Map<ColorDTO, Color>(colorDto);
            _repository.Edit(color);
        }
    }
}