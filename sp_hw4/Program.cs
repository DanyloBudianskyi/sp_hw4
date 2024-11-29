using sp_hw4.Models;
using System.Linq;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace sp_hw4
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var db = new AchievementManager();
            var service = new Service(db);

            var newUser = new User
            {
                Name = "John Doe",
            };

            await service.CreateUserAsync(newUser);
            Console.WriteLine("User added");

            var users = await service.ReadUsers();
            Console.WriteLine("All users:");
            foreach (var user in users)
            {
                Console.WriteLine(user);
            }

            var achievement = new Achievement
            {
                Title = "Test",
                Description = "Test"
            };

            var userId = users.First().Id;
            await service.AddAchievementToUser(userId, achievement);
            Console.WriteLine("Achievement added");

            var userToUpdate = new User
            {
                Name = "John Smith",
                Achievements = new List<Achievement>()
            };
            await service.UpdateUserAsync(userId, userToUpdate);
            Console.WriteLine("User was updated");

            await service.DeleteUserByIdAsync(userId);
            Console.WriteLine("User was deleted");

            Console.WriteLine("--------------------------------------------");

            users = await service.ReadUsers();
            Console.WriteLine("All users:");
            foreach (var user in users)
            {
                Console.WriteLine(user);
            }
            Console.ReadKey();
        }
    }
}
