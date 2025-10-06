using Atlassian.Jira;

namespace StatusWS.Services
{
    internal class BasicAuthentication : JiraCache
    {
        private readonly string _email;
        private readonly string _token;

        public BasicAuthentication(string email, string token)
        {
            _email = email;
            _token = token;
        }
    }
}