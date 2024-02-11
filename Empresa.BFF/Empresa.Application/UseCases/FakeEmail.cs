using Empresa.Application.UseCases.Interfaces;
using Empresa.Domain.Adapters;
using System.IO;
using System.Threading.Tasks;

namespace Empresa.Application.UseCases
{
    public class FakeEmail : IEmailService
    {
        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var emailContent = $"To: {to}\nSubject: {subject}\n\n{body}\n\n";

            await File.AppendAllTextAsync("email.txt", emailContent);
        }
    }
}
