using Robot.Core.Integration;

namespace Robot.Core
{
    public interface IProcessingService
    {
        bool ProcessInputData(IMessageData inputData, out int templateId);
    }
}
