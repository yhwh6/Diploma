using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Diploma.Models;


public class TelegramBotService
{
    private readonly TelegramBotClient botClient;
    private readonly DiplomaContext dbContext;

    public TelegramBotService(string botToken, DiplomaContext dbContext)
    {
        botClient = new TelegramBotClient(botToken);
        this.dbContext = dbContext;

/*        botClient.OnMessage += BotClient_OnMessage;
        botClient.StartReceiving();*/
    }

    private async void BotClient_OnMessage(object sender, Update update)
    {
        Message message = update.Message;

        if (message.Type == MessageType.Text)
        {
            string responseMessage = string.Empty;

            switch (message.Text.ToLower())
            {
                case "/contacts":
                    responseMessage = GetContactInfo();
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
        }
    }

    private string GetContactInfo()
    {
        return @"Контактная информация:
            Адрес: Швейца́рская Конфедера́ция, Бёрн
            Телефон: + 41 111 44 11
            Email: swiss@test.com";
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
