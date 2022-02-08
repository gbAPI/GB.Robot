using Robot.DAL.Repos;
using Robot.Core.Integration;
using System.Text;
using System.Linq;
using System.Security.Cryptography;
using System;

namespace Robot.Core.Services
{
    public class ProcessingService : IProcessingService
    {
        DecisionRepo _decisionRepo = new DecisionRepo();

        public bool ProcessInputData(IMessageData inputData,out int templateId)
        {
            templateId = -1;

            var sb = new StringBuilder(inputData.DocumentType + ";");

            var fieldsList = inputData.InputData.Keys.ToList();

            fieldsList.Sort();

            sb.AppendJoin(';', fieldsList);

            var hashBytes = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(sb.ToString()));

            string searchHash = Convert.ToBase64String(hashBytes);

            var decision = _decisionRepo.GetByHash(searchHash);

            if (decision is null)
                return false;
            else
                templateId = decision.OutputTemplateID;

            return true;
        }
    }
}
