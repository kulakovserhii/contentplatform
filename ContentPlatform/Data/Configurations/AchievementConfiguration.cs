using ContentPlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ContentPlatform.Enums;

namespace ContentPlatform.Data.Configurations
{
    public class AchievementConfiguration : IEntityTypeConfiguration<Achievement>
    {
        public void Configure(EntityTypeBuilder<Achievement> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Name).IsRequired().HasMaxLength(70);
            builder.Property(a => a.Description).IsRequired().HasMaxLength(300);
            builder.Property(a => a.BadgeTitle).HasMaxLength(50);

            builder.Property(a => a.Category).IsRequired();

            builder.HasData(
                new Achievement
                {
                    Id = 1,
                    Name = "Дебют",
                    Description = "Залишено перший відгук",
                    Category = AchievementCategory.Social,
                    RequirementValue = 1
                },
                new Achievement
                {
                    Id = 2,
                    Name = "Перша вподобайка",
                    Description = "Ваш відгук отриав перший лайк",
                    Category = AchievementCategory.Social,
                    RequirementValue = 1,
                },
                new Achievement
                {
                    Id = 3,
                    Name = "Критик",
                    Description = "Залишено 15 відгуків сумарно",
                    Category = AchievementCategory.Social,
                    RequirementValue = 15,
                },
                new Achievement
                {
                    Id = 4,
                    Name = "Зірка",
                    Description = "Ваші відгуки зібрали 100 лайків",
                    Category = AchievementCategory.Social,
                    RequirementValue = 100
                },
                new Achievement
                {
                    Id = 5,
                    Name = "Ласкаво просимо",
                    Description = "За реєстрацію в системі",
                    Category = AchievementCategory.Social,
                    RequirementValue = 0,
                },
                new Achievement
                {
                    Id = 6,
                    Name = "Кіноман",
                    Description = "5 відгуків на фільми",
                    Category = AchievementCategory.Film,
                    RequirementValue = 5
                },
                new Achievement
                {
                    Id = 7,
                    Name = "Серіаломан",
                    Description = "5 відгуків на серіали",
                    Category = AchievementCategory.TvShow,
                    RequirementValue = 5,
                },
                new Achievement
                {
                    Id = 8,
                    Name = "Меломан",
                    Description = "5 відгуків на музику",
                    Category = AchievementCategory.Music,
                    RequirementValue = 5
                },
                new Achievement
                {
                    Id = 9,
                    Name = "Геймер",
                    Description = "5 відгуків на ігри",
                    Category = AchievementCategory.Game,
                    RequirementValue = 5,
                },
                new Achievement
                {
                    Id = 10,
                    Name = "Книголюб",
                    Description = "5 відгуків на книги",
                    Category = AchievementCategory.Book,
                    RequirementValue = 5
                },
                new Achievement
                {
                     Id = 11,
                     Name = "Активний виборець",
                     Description = "Ви поставили 50 лайків іншим",
                     Category = AchievementCategory.Social,
                     RequirementValue = 50
                },
                new Achievement
                {
                    Id = 12,
                    Name = "Думка громади",
                    Description = "Один відгук набрав 30 лайків",
                    Category = AchievementCategory.Social,
                    RequirementValue = 30,
                },
                new Achievement
                {
                    Id = 13,
                    Name = "Популярний автор",
                    Description = "Сумарно отримано 500 лайків",
                    Category = AchievementCategory.Social,
                    RequirementValue = 500
                },
                new Achievement
                {
                    Id = 14,
                    Name = "Перфекціоніст",
                    Description = "Поставлено 5 оцінок 10/10",
                    Category = AchievementCategory.Social,
                    RequirementValue = 5,
                },
                new Achievement
                {
                    Id = 15,
                    Name = "Скептик",
                    Description = "Поставлено 5 оцінок 3/10 або нижче",
                    Category = AchievementCategory.Social,
                    RequirementValue = 5
                }
            );
        }
    }
}
