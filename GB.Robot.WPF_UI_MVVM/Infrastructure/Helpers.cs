using Robot.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GB.Robot.WPF_UI_MVVM.Infrastructure
{
    public static class Helpers
    {
        public static BO_Rule Clone(this BO_Rule rule)
        {
            BO_Rule result = new BO_Rule();
            result.CreationDate = rule.CreationDate;
            result.Description = rule.Description;
            result.DocumentType = rule.DocumentType;
            result.ID = rule.ID;
            result.Name = rule.Name;
            result.Template = rule.Template;
            result.RequiredFields = new(rule.RequiredFields);

            return result;
        }
        
    }
}
