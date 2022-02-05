using Robot.DAL.Entities;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System;

namespace Robot.Core
{
    public static class Extensions
    {
        public static Decision To_DAL(this BO_Rule rule)
        {


            var result = new Decision()
            {
                ID = rule.ID,
                Name = rule.Name,
                Description = rule.Description,
                DocumentType = rule.DocumentType,
                OutputTemplateID = rule.Template.ID,
                CreationDate = rule.CreationDate,

                RequiredFields = rule.RequiredFields.Select((fd) => new Field
                {
                    FieldName = fd.Name,
                    Description = fd.Description
                })
                .ToArray()
            };

            result.HashBytesOfFields = result.GetHash();


            return result;
        }
      
        public static string GetHash(this Decision rule)
        {
            var sb = new StringBuilder(rule.DocumentType + ";");

            var fieldsNameList = rule.RequiredFields
                .Select(fd => fd.FieldName)
                .ToList();

            fieldsNameList.Sort();

            sb.AppendJoin(';', fieldsNameList);

            var md5 = MD5.Create();
            var hashString = md5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString()));

            return Convert.ToBase64String(hashString);
        }

        public static BO_Rule To_BO(this Decision decision)
        {
            return new BO_Rule()
            {
                ID = decision.ID,
                Name = decision.Name,
                Description = decision.Description,
                DocumentType = decision.DocumentType,
                Template = new BO_Template { ID = decision.OutputTemplateID },
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
