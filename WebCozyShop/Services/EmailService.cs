using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using WebCozyShop.Repositories.Interface;
using WebCozyShop.Services.Interface;

namespace WebCozyShop.Services
{
    public class SmtpSettings
    {
        public string Host { get; init; } = default!;
        public int Port { get; init; }
        public bool UseStartTls { get; init; }
        public string User { get; init; } = default!;
        public string Pass { get; init; } = default!;
        public string SenderEmail { get; init; } = default!;
        public string SenderName { get; init; } = default!;
    }

    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _smtp;

        public EmailService(IOptions<SmtpSettings> options) => _smtp = options.Value;

        public async Task SendEmailAsync(string toEmail, string resetLink)
        {
            var msg = new MimeMessage();
            msg.From.Add(new MailboxAddress(_smtp.SenderName, _smtp.SenderEmail));
            msg.To.Add(MailboxAddress.Parse(toEmail));
            msg.Subject = "Đặt lại mật khẩu • CozyShop";


            msg.Body = new BodyBuilder
            {
                HtmlBody = $@"
        <p>Xin chào,</p>

        <p>Bạn vừa yêu cầu đặt lại mật khẩu. Nhấn vào nút bên dưới 
           (hiệu lực 30 phút):</p>

        <p style=""text-align:center;"">
            <a href=""{resetLink}""
               style=""display:inline-block;
                      padding:12px 24px;
                      background-color:#0d6efd;
                      color:#ffffff;
                      text-decoration:none;
                      font-weight:bold;
                      border-radius:6px;"">
                Đặt lại mật khẩu
            </a>
        </p>

        <p>Nếu không phải bạn, vui lòng bỏ qua e‑mail này.</p>

        <hr/>
        <small>© 2025 CozyShop</small>"
            }.ToMessageBody();


            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(
                _smtp.Host,
                _smtp.Port,
                _smtp.UseStartTls ? SecureSocketOptions.StartTls : SecureSocketOptions.SslOnConnect);

            await smtp.AuthenticateAsync(_smtp.User, _smtp.Pass);
            await smtp.SendAsync(msg);
            await smtp.DisconnectAsync(true);
        }
    }
}
