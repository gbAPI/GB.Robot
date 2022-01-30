using Robot.DAL.Entities;
using System.Linq;
using System.Collections.Generic;

namespace Robot.Core
{
    public static class Extensions
    {
        public static Decision To_DAL(this BO_Rule rule)
        {
            return new Decision()
            {
                ID = rule.ID,
                Name = rule.Name,
                Description = rule.Description,
                DocumentType = rule.DocumentType,
                OutputTemplateID = rule.Template.ID,
                CreationDate = rule.CreationDate,

                RequiredFields = rule.RequiredFields.Select((fd) => new Field {
                    FieldName = fd.Name,
                    Description = fd.Description
                })
                .ToArray()

            };
        }

        public static BO_Rule To_BO(this Decision decision)
        {
            return new BO_Rule()
            {
                ID = decision.ID,
                Name = decision.Name,
                Description = decision.Description,
                DocumentType = decision.DocumentType,
                Template = new BO_Template { ID = decision.ID },
                CreationDate = decision.CreationDate,

                RequiredFields = decision.RequiredFields.Select((fd) => new BO_Field
                {
                    Name = fd.FieldName,
                    Description = fd.Description
                })
                .ToList()
                

            };
        }
    
        public static ICollection<BO_Rule> TO_BO(this ICollection<Decision> decisions)
        {
            return decisions.Select(dc => dc.To_BO()).ToList();
        }
    
    }
}
