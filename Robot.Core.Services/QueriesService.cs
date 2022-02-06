using Robot.DAL.Entities;
using Robot.DAL.Repos;
using System.Linq;

namespace Robot.Core
{
    public class QueriesService : IQueriesService
    {

        QueryRepo _queriesRepo = new QueryRepo();
        ExternalObjectsRepo _extObjRepo = new ExternalObjectsRepo();

        public BO_Query[] GetAll()
        {

            var result = _queriesRepo.GetAll()
                .Select(qr => new BO_Query
                {
                    ID = qr.ID,
                    DocumentType = qr.InputDocumentType,
                    ErrorDescription = qr.ErrorDescription,
                    OutputTemplate = new BO_Template { Name = qr.OutputTemplate },
                    MessageDate = qr.MessageDate
                }
                )
                .ToArray();

            return result;
        }

        public void Add(BO_Query query)
        {
            var templates = _extObjRepo.GetAllTemplates();

            var tmpl = templates.FirstOrDefault(t => t.ID.Equals(query.OutputTemplate.ID));

            var dal_query = new Query
            {
                MessageDate = query.MessageDate,
                InputDocumentType = query.DocumentType,
                OutputTemplate = tmpl?.Name,
                ErrorDescription = query.ErrorDescription,
            };

            _queriesRepo.Add(dal_query);

            query.ID = dal_query.ID;
        }
    }
}
