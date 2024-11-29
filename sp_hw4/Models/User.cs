using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sp_hw4.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Achievement> Achievements { get; set; }
        public override string ToString()
        {
            string result = $"Id: {Id}, Name: {Name}\nAchievements:\n";
            if (Achievements != null) {
                foreach (var achievement in Achievements) result += "\t" + achievement.ToString() + "\n"; 
            }
            else
            {
                result += "\tEmpty\n";
            }
            return result;
        }
    }
}
