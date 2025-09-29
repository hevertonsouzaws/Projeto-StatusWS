using Atlassian.Jira;

namespace StatusWS.Services
{
    internal class BasicAuthentication : JiraCache
    {
        private string email;
        private string token;

        public BasicAuthentication(string email, string token)
        {
            this.email = email;
            this.token = token;
        }
    }
}