using Application.Dto;
using Application.Interfaces.Auth;
using Domain.Entity.Auth;
using Domain.Repostry;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MimeKit;


namespace Infrastructure.Services.Command.EmailServices
{
    public class EmailService(IUserService userQuery, UserManager<Appuser> userManager, IConfiguration configuration) : IEmailService
    {
        public async Task<ServiceResponse> SendEmailAsync(string email)
        {

            try
            {
                var user = await userQuery.GetUserByEmail(email);

                if (user == null)
                    return new ServiceResponse(true, "If the email exists, a reset link was sent");

                var token = await userManager.GeneratePasswordResetTokenAsync(user); // انشاء توكين

                var message = new MimeMessage();

                message.From.Add(
                    MailboxAddress.Parse(configuration["Email:MyEmail"]!)
                        );

                message.To.Add(
                    MailboxAddress.Parse(user.Email!)
                );

                message.Subject = "Reset Password";

                var bodybuilder = new BodyBuilder();

                bodybuilder.TextBody = $@"
                    Hello {user.UserName},

                    You requested to reset your password.

                     Click the link below:

                    https://localhost:7024/api/Authantication/checkuser?email={Uri.EscapeDataString(user.Email!)}&token={Uri.EscapeDataString(token)}

                         If you didn't request this, ignore this email.

                                   Job Board Team
";

                message.Body = bodybuilder.ToMessageBody();

                using var smtp = new SmtpClient();

                await smtp.ConnectAsync(
                            "smtp.gmail.com",
                            587,
                    MailKit.Security.SecureSocketOptions.StartTls

                );

                await smtp.AuthenticateAsync(
                    configuration["Email:MyEmail"],
                    configuration["Email:passowerd"]

                );

                await smtp.SendAsync(message);

                await smtp.DisconnectAsync(true);

                return new ServiceResponse(true, "the message is sent");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, "Something went wrong while sending email");
            }
        }
    }
    }
