using Robot.DAL.Repos;
using Robot.DAL.Entities;

using System.Linq;
using System;
namespace Robot.Core
{
    public class RulesService : IRulesService
    {
        private readonly DecisionRepo _rulesRepo = new DecisionRepo();
        private readonly ExternalObjectsRepo _externalRepo = new ExternalObjectsRepo();

        public bool Add(BO_Rule rule)
        {
            rule.CreationDate = DateTime.Now;
            var resultID = _rulesRepo.Add(rule.To_DAL());


            if (resultID.Equals(-1))
                return false;
            else
                rule.ID = resultID;
                return true;
        }

        public bool Delete(BO_Rule rule)
        {
            return _rulesRepo.Delete(_rulesRepo.Get(rule.ID));
        }

        public BO_Rule[] GetAll()
        {
            var rules = _rulesRepo.GetAll().TO_BO();

            var templates = _externalRepo.GetAllTemplates();

            return rules.Join(
                    templates,
                    rule=>rule.Template.ID,
                    template=> template.ID,
                    (rule, template) =>
                    {
                        rule.Template.Name = template.Name;
                        return rule;
                    }
                )
                .ToArray();


        }

        public BO_Rule GetById(int Id)
        {
            var rule = _rulesRepo.Get(Id).To_BO();

            rule.Template.Name = _externalRepo.GetTemplateById(rule.Template.ID).Name;

            return rule;


        }

        public bool Update(BO_Rule rule)
        {
            var decision = _rulesRepo.Get(rule.ID);

            decision.Name = rule.Name;
            decision.OutputTemplateID = rule.Template.ID;
            decision.DocumentType = rule.DocumentType;
            decision.Description = rule.Description;
            decision.RequiredFields = rule.RequiredFields
                .Select(x => new Field
                {
                    FieldName = x.Name,
                    Description = x.Description
                })
                .ToList();

            return _rulesRepo.Save(decision);
        }
    }
}
