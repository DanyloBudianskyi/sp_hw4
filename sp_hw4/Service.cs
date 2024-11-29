using sp_hw4.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace sp_hw4
{
    public class Service
    {
        public AchievementManager _db;
        private readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);
        private int Counter = 0;
        public Service(AchievementManager db)
        {
            _db = db;
        }
        private void IncrementCounter()
        {
            Interlocked.Increment(ref Counter);
        }
        public async Task CreateUserAsync(User user)
        {
            await semaphore.WaitAsync();
            try
            {
                _db.Users.Add(user);
                await _db.SaveChangesAsync();
                IncrementCounter();
            }
            finally
            {
                semaphore.Release();
            }
        }
        public async Task DeleteUserByIdAsync(int id)
        {
            await semaphore.WaitAsync();
            try
            {
                var user = await _db.Users.FindAsync(id);
                if (user != null)
                {
                    _db.Users.Remove(user);
                    await _db.SaveChangesAsync();
                }
            }
            finally
            {
                semaphore.Release();
            }
        }
        public async Task UpdateUserAsync(int id, User user)
        {
            await semaphore.WaitAsync();
            try
            {
                var userToUpdate = await _db.Users.FindAsync(id);
                if (userToUpdate != null)
                {
                    if (user.Achievements != null)
                    {
                        user.Achievements.Clear();
                        await _db.SaveChangesAsync();
                    }
                    userToUpdate.Name = user.Name;
                    foreach (var achievement in user.Achievements)
                    {
                        var existingAchievement = await _db.Achievements.FindAsync(achievement.Id);
                        if (existingAchievement != null)
                        {
                            userToUpdate.Achievements.Add(existingAchievement);
                        }
                        else
                        {
                            userToUpdate.Achievements.Add(achievement);
                        }
                    }
                    await _db.SaveChangesAsync();
                }
            }
            finally
            {
                semaphore.Release();
            }
        }
        public async Task<List<User>> ReadUsers()
        {
            return await _db.Users.ToListAsync();
        }
        public async Task AddAchievementToUser(int id, Achievement achievement)
        {
            await semaphore.WaitAsync();
            try
            {
                var user = await _db.Users.FindAsync(id);
                if (user != null)
                {
                    if(user.Achievements == null) user.Achievements = new List<Achievement>();
                    user.Achievements.Add(achievement);
                    await _db.SaveChangesAsync();
                }
            }
            finally
            {
                semaphore.Release();
            }
        }
    }
}
