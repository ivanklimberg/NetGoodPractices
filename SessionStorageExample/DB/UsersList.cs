using SessionStorageExample.Models;

namespace SessionStorageExample.DB
{
    public static class UsersList
    {
        public static List<User> Users = new List<User>
        {
            new User
            {
                Id = Guid.Parse("0437bfe7-afcd-4556-9db4-fe66f24fafb6"),
                Email = "test_user_1@gmail.com"
            },
            new User
            {
                Id = Guid.Parse("a26e659c-3884-4ca0-aa9c-5f8d9ff15017"),
                Email = "test_user_2@gmail.com"
            },
            new User
            {
                Id = Guid.Parse("d2a12623-c8df-4fef-ac88-609f265604e4"),
                Email = "test_user_3@gmail.com"
            },
            new User
            {
                Id = Guid.Parse("fc2edc5e-844a-49a3-bf8a-d7542397d52e"),
                Email = "test_user_4@gmail.com"
            },
            new User
            {
                Id = Guid.Parse("73ac0c44-ed82-4e32-87e4-8883df18281b"),
                Email = "test_user_5@gmail.com"
            },
        };
    }
}
