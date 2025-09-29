using StatusWS.Dtos;
using System.Threading.Tasks;

namespace StatusWS.Services
{
    public interface IJiraService
    {
        Task<JiraIssueDto> GetIssueDetailsAsync(string issueKey);
        bool IsJiraKeyFormat(string content);
    }
}