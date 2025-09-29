using Atlassian.Jira;
using StatusWS.Dtos;

namespace StatusWS.Services
{
    public class JiraService : IJiraService
    {
        private readonly IConfiguration _configuration;
        private readonly string _prefix;
        private readonly string _url;
        private readonly string _email;
        private readonly string _token;

        public JiraService(IConfiguration configuration)
        {
            _configuration = configuration;
            _prefix = _configuration["Jira:ProjectPrefix"] ?? "PROJETO";
            _url = _configuration["Jira:Url"];
            _email = _configuration["Jira:Email"];
            _token = _configuration["Jira:ApiToken"];
        }

        public bool IsJiraKeyFormat(string content)
        {
            return !string.IsNullOrWhiteSpace(content) && content.ToUpper().StartsWith(_prefix.ToUpper() + "-");
        }

        public async Task<JiraIssueDto> GetIssueDetailsAsync(string issueKey)
        {
            if (!IsJiraKeyFormat(issueKey))
            {
                return new JiraIssueDto { ErrorMessage = "Formato de chave Jira inválido." };
            }

            try
            {
                var jiraClient = Jira.CreateRestClient(_url, _email, _token);
                var issue = await jiraClient.Issues.GetIssueAsync(issueKey);

                return new JiraIssueDto
                {
                    Key = issue.Key.Value,
                    Summary = issue.Summary,
                    Description = issue.Description,
                    Status = issue.Status.Name
                };
            }
            catch (Exception ex)
            {
                return new JiraIssueDto
                {
                    ErrorMessage = ex.Message.Split('\n')[0]
                };
            }
        }

    }
}