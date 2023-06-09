﻿using AutoMapper;
using Drones_Api.Data;
using Drones_Api.Models;
using Drones_Api.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Drones_Api.Repository
{
    public class DroneRepository : IDroneRepository
    {
        private readonly DronesDB _db;
        private readonly IMapper _mapper;

        public DroneRepository(DronesDB db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<DroneDTO> Get(Expression<Func<Drone, bool>> expression, List<string>? includes = null)
        {
            var query = _db.Drones.AsQueryable();
            if (includes != null)
            {
                foreach (var includeProp in includes)
                {
                    query = query.Include(includeProp);
                }
            }
            var result = await query.AsNoTracking().FirstOrDefaultAsync(expression);
            return _mapper.Map<DroneDTO>(result);
        }

        public async Task<IList<DroneDTO>> GetAll(
            Expression<Func<Drone, bool>>? expression = null,
            Func<IQueryable<Drone>, IOrderedQueryable<Drone>>? orderby = null,
            List<string>? includes = null
            )
        {
            var query = _db.Drones.AsQueryable();
            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (includes != null)
            {
                foreach (var includeProp in includes)
                {
                    query = query.Include(includeProp);
                }
            }

            if (orderby != null)
            {
                query = orderby(query);
            }
            var result = await query.AsNoTracking().ToListAsync();
            return _mapper.Map<IList<DroneDTO>>(result);
        }

        public async Task<DroneDTO> Insert(Drone entity)
        {
            try
            {
                await _db.Drones.AddAsync(entity);
                await _db.SaveChangesAsync();
                return _mapper.Map<DroneDTO>(entity);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<DroneDTO> Update(Drone entity)
        {
            try
            {
                _db.Entry(entity).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return _mapper.Map<DroneDTO>(entity);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
