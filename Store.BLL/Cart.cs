using System.Collections.Generic;
using System.Linq;
using Store.BLL.DTO;
using Store.DAL.Entities;

namespace Store.BLL
{
    public class Cart
    {
        private readonly List<OrderItemDTO> _lineCollection = new List<OrderItemDTO>();

        public IEnumerable<OrderItemDTO> Lines
        {
            get { return _lineCollection; }
        }

        public void AddItem(GoodDTO goodDto, int number)
        {
            var line = _lineCollection
                .FirstOrDefault(p => p.Good.Id == goodDto.Id);

            if (line == null)
            {
                _lineCollection.Add(new OrderItemDTO
                {
                    Good = goodDto,
                    Number = number
                });
            }
            else
            {
                line.Number += number;
            }
        }

        public void RemoveLine(GoodDTO goodDto)
        {
            _lineCollection.RemoveAll(l => l.Good.Id == goodDto.Id);
        }

        public decimal ComputeTotalValue()
        {
            return _lineCollection.Sum(e => e.Good.PriceSale*e.Number);
        }

        public void Clear()
        {
            _lineCollection.Clear();
        }
    }
}