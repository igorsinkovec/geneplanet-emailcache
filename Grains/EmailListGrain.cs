using System.Collections.Specialized;
using System.Threading.Tasks;

namespace EmailCache
{
    public class EmailListGrain : IEmailList
    {
        public string Domain { get; set; }

        private StringCollection _emails;

        public EmailListGrain()
        {
            _emails = new StringCollection();
        }

        Task IEmailList.Add(string email)
        {
            if (!_emails.Contains(email)) {
                _emails.Add(email);
            }
            return Task.CompletedTask;
        }

        Task IEmailList.Remove(string email)
        {
            int i = _emails.IndexOf(email);
            while (i > -1) {
                _emails.RemoveAt(i);
                i = _emails.IndexOf(email);
            }
            return Task.CompletedTask;
        }

        Task<bool> IEmailList.Contains(string email)
        {
            return Task.FromResult(_emails.Contains(email));
        }

        Task<int> IEmailList.Count()
        {
            return Task.FromResult(_emails.Count);
        }
    }
}
