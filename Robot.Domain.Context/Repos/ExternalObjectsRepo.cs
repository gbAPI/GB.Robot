using Robot.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Robot.DAL.Repos
{
    public class ExternalObjectsRepo : IDisposable
    {
        private readonly RobotContext _dbContext;

        public ExternalObjectsRepo()
        {
            _dbContext = new RobotContext();
        }

        public List<ScannerField> GetAllScannerFields()
        {
            return _dbContext.Set<ScannerField>().ToList();
        }

        public List<Template> GetAllTemplates()
        {
            return _dbContext.Set<Template>().ToList();
        }


        public Template GetTemplateById(int id)
        {
            return _dbContext.Set<Template>().Find(id);
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
        
    
}
