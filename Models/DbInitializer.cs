namespace Diploma.Models
{
    public static class DbInitializer
    {
        public static void Seed(DiplomaContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Users.Any())
            {
                var user = new User
                {
                    ID = 1,
                    Username = "test",
                    Password = "test",
                    Role = "Administrator"
                };

                context.Users.Add(user);
                context.SaveChanges();
            }
        }
    }
}
