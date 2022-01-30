using System.Linq;
using System.Collections.Generic;
using Robot.DAL.Repos;

namespace Robot.Core
{
    public class ExternalObjectsService : IExternalObjectsService
    {
        private readonly ExternalObjectsRepo _extobjRepo = new ExternalObjectsRepo();

        public ICollection<string> GetAllDocumentTypes()
        {
            return _extobjRepo.GetAllDocumentTypes();
        }

        public BO_Field[] GetScannerFields()
        {
            return _extobjRepo.GetAllScannerFields()
                .Select(fd => new BO_Field { 
                    Name = fd.Name
                })
                .ToArray();
        }

        public BO_Template[] GetTAllTemplates()
        {
            return _extobjRepo.GetAllTemplates()
                .Select(tmpl => new BO_Template
                {
                    Name = tmpl.Name,
                    ID = tmpl.ID
                })
                .ToArray();
        }
    }
}
