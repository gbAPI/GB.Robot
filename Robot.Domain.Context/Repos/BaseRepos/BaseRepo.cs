﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Robot.DAL.Repos.Interfaces;

namespace Robot.DAL.Repos
{
    public class BaseRepo<T> : IDisposable, IRepo<T> where T : BaseEntities.BaseEntity
    {
        protected readonly DbSet<T> _table;
        protected readonly RobotContext _dbContext;


        public BaseRepo()
        {
            _dbContext = new RobotContext();
            _table = _dbContext.Set<T>();
        }

        public int Add(T entity)
        {
            _table.Add(entity);
            if (SaveChanges())
                return entity.ID;
            else
                return -1;
        }

        public bool Delete(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
            return SaveChanges();
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }

        public virtual T Get(int id)
        {
            return _table.Find(id);
        }
        
        public virtual List<T> GetAll()
        {
            return _table.ToList();
        }

        public bool Save(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            
            return SaveChanges();
        }


        private bool SaveChanges()
        {
            try
            {
                SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private async void SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
