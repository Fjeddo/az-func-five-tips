using TuplesPatternMatchingFunction.Domain.Models;

namespace TuplesPatternMatchingFunction.Domain.Processes
{
    public class UpdateUserWorkProcess : IProcess<User>
    {
        public UpdateUserWorkProcess(string ssn, string work) { }
        public (bool success, User model, int status) Run() => (true, default, 0);
    }
}
