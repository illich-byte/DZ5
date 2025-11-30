using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Interfaces;
using Core.Models.Location;
using Domain;
using Domain.Entities.Location;
using Microsoft.EntityFrameworkCore;

namespace Core.Services
{
    /// <summary>
    /// Сервіс для управління містами.
    /// </summary>
    public class CityService : ICityService
    {
        private readonly AppDbTransferContext _context;
        private readonly IMapper _mapper;

        public CityService(AppDbTransferContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Створює нове місто.
        /// </summary>
        public async Task<CityItemModel> CreateAsync(CityCreateModel model)
        {
            var entity = _mapper.Map<CityEntity>(model);

            _context.Cities.Add(entity);
            await _context.SaveChangesAsync();

            // Повертаємо створену модель з інформацією про країну
            var item = await _context.Cities
                .Where(c => c.Id == entity.Id) // <-- Тут ми використовуємо Id
                .ProjectTo<CityItemModel>(_mapper.ConfigurationProvider)
                .FirstAsync();

            return item;
        }

        /// <summary>
        /// Отримує список усіх міст.
        /// </summary>
        public async Task<List<CityItemModel>> GetListAsync()
        {
            var list = await _context.Cities
                .OrderBy(c => c.Name) // Сортування за назвою
                .ProjectTo<CityItemModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return list;
        }

        /// <summary>
        /// Отримує місто за його ID.
        /// </summary>
        public async Task<CityItemModel?> GetByIdAsync(int id)
        {
            return await _context.Cities
                .Where(c => c.Id == id)
                .ProjectTo<CityItemModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Оновлює існуюче місто.
        /// </summary>
        public async Task UpdateAsync(int id, CityCreateModel model)
        {
            var entity = await _context.Cities.FindAsync(id);

            if (entity == null)
            {
                // Замініть на більш відповідну помилку, наприклад, NotFoundException
                throw new Exception($"City with ID {id} not found.");
            }

            // Мапінг оновлених даних на сутність
            _mapper.Map(model, entity);

            // Зберігаємо зміни
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Видаляє місто за його ID.
        /// </summary>
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Cities.FindAsync(id);

            if (entity != null)
            {
                _context.Cities.Remove(entity);
                await _context.SaveChangesAsync();
            }
            // В іншому випадку, якщо entity == null, ми просто ігноруємо,
            // оскільки мета (видалення) вже досягнута, або кидаємо виняток.
        }
    }
}