using Telegram.Bot;
using Telegram.Bot.Types;
using Diploma.Models;
using Telegram.Bot.Polling;

public class TelegramBotService
{
    private readonly ITelegramBotClient bot;
    private readonly DiplomaContext dbContext;

    public TelegramBotService(DiplomaContext dbContext)
    {
        string token = System.IO.File.ReadAllText(@"token.txt");
        bot = new TelegramBotClient(token);
        this.dbContext = dbContext;

        var cts = new CancellationTokenSource();
        var cancellationToken = cts.Token;
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = { },
        };
        bot.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            receiverOptions,
            cancellationToken
        );
    }

    public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        await botClient.SendTextMessageAsync(exception.Message, "Error", cancellationToken: cancellationToken);
    }

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var message = update.Message;

        switch (message.Type)
        {
            case Telegram.Bot.Types.Enums.MessageType.Text:
                {
                    string responseMessage = string.Empty;

                    switch (message.Text.ToLower())
                    {
                        case "/contacts":
                            responseMessage = GetContactInfo();
                            break;

                        case "/services":
                            responseMessage = GetServiceList();
                            break;

                        case "/projects":
                            responseMessage = GetProjectList();
                            break;

                        case "/blogs":
                            responseMessage = GetBlogList();
                            break;

                        case "/apply":
                            responseMessage = "Please provide your name, email, and message in the format '/apply name email message'.";
                            break;

                        default:
                            if (message.Text.ToLower().StartsWith("/apply"))
                            {
                                responseMessage = ProcessApplication(message.Text);
                            }
                            break;
                    }

                    if (!string.IsNullOrEmpty(responseMessage))
                    {
                        await botClient.SendTextMessageAsync(message.Chat.Id, responseMessage);
                    }

                    break;
                }

            default:
                await botClient.SendTextMessageAsync(message.Chat, "Меня ещё не научили работать с таким типом данных *sadface*", cancellationToken: cancellationToken);
                break;
        }
    }

    private string GetContactInfo()
    {
        return @"Контактная информация:
            Адрес: Швейца́рская Конфедера́ция, Бёрн
            Телефон: + 41 111 44 11
            Email: swiss@test.com";
    }
    private string GetServiceList()
    {
        var services = dbContext.Services.ToList();

        if (services.Any())
        {
            string response = "Список услуг:\n";

            foreach (var service in services)
            {
                response += $"- {service.Title}\n";
            }

            return response;
        }

        return "Нет доступных услуг.";
    }

    private string GetProjectList()
    {
        var projects = dbContext.Projects.ToList();

        if (projects.Any())
        {
            string response = "Список проектов:\n";

            foreach (var project in projects)
            {
                response += $"- {project.Title}\n";
            }

            return response;
        }

        return "Нет доступных проектов.";
    }

    private string GetBlogList()
    {
        var blogs = dbContext.Blogs.ToList();

        if (blogs.Any())
        {
            string response = "Список блогов:\n";

            foreach (var blog in blogs)
            {
                response += $"- {blog.Title}\n";
            }

            return response;
        }

        return "Нет доступных блогов.";
    }

    private string ProcessApplication(string messageText)
    {
        string[] parts = messageText.Split(' ');

        if (parts.Length >= 4)
        {
            string name = parts[1];
            string email = parts[2];
            string applicationMessage = string.Join(" ", parts.Skip(3));

            Application application = new Application
            {
                Name = name,
                Email = email,
                Message = applicationMessage,
                Status = ApplicationStatus.Received
            };

            dbContext.Applications.Add(application);
            dbContext.SaveChanges();

            return "Заявка успешно отправлена.";
        }

        return "Неверный формат заявки. Пожалуйста, укажите имя, email и сообщение в формате '/apply имя email сообщение'.";
    }
}
