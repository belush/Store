using System;
using System.Collections.Generic;
using AutoMapper;
using Store.BLL.DTO;
using Store.BLL.Interfaces;
using Store.DAL.Entities;
using Store.DAL.Interfaces;

namespace Store.BLL.Logic
{
    public class CategoryLogic : ICategoryLogic
    {
        private readonly IRepository<Category> _repository;

        public CategoryLogic(IRepository<Category> repository)
        {
            _repository = repository;
        }

        public IEnumerable<CategoryDTO> GetAll()
        {
            var categories = _repository.GetAll();
            var categoriesDto = Mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(categories);
            return categoriesDto;
        }

        public CategoryDTO Get(int? id)
        {
            if (id == null)
            {
                throw new ArgumentException("id null");
            }
            var category = _repository.Get(id.Value);
            var categoryDto = CategoryToCategoryDto(category);
            return categoryDto;
        }

        public void Add(CategoryDTO categoryDto)
        {
            var category = CategoryDtoToCategory(categoryDto);
            _repository.Add(category);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public void Edit(CategoryDTO categoryDto)
        {
            var category = CategoryDtoToCategory(categoryDto);
            _repository.Edit(category);
        }

        public Category CategoryDtoToCategory(CategoryDTO categoryDto)
        {
            return Mapper.Map<CategoryDTO, Category>(categoryDto);
        }

        public CategoryDTO CategoryToCategoryDto(Category category)
        {
            return Mapper.Map<Category, CategoryDTO>(category);
        }
    }
}