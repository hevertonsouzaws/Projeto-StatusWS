using Atlassian.Jira;
using StatusWS.Dtos;
using StatusWS.Errors;

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
            _url = _configuration["Jira:Url"] ?? throw new ArgumentNullException("Jira:Url", "A URL do Jira não está configurada.");
            _email = _configuration["Jira:Email"] ?? throw new ArgumentNullException("Jira:Email", "O e-mail do Jira não está configurado.");
            _token = _configuration["Jira:ApiToken"] ?? throw new ArgumentNullException("Jira:ApiToken", "O token da API do Jira não está configurado.");
        }

        public bool IsJiraKeyFormat(string content)
        {
            return !string.IsNullOrWhiteSpace(content) && content.ToUpper().StartsWith(_prefix.ToUpper() + "-");
        }

        public async Task<JiraIssueDto> GetIssueDetailsAsync(string issueKey)
        {
            if (!IsJiraKeyFormat(issueKey))
            {
                throw new BadRequestException("Formato de chave Jira inválido.");
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
                throw new BadRequestException($"Erro ao consultar a API do Jira: {ex.Message}");
            }
        }

    }
}