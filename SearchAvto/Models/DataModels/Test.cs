using System.Collections.Generic;

namespace SearchAvto.Models.DataModels
{
    public class Answer
    {
        public byte Id { get; private set; }
        public string Text { get; private set; }

        public Answer(byte id, string text)
        {
            Id = id;
            Text = text;
        }
    }

    public class Question
    {
        public string Id { get; private set; }
        public string Text { get; private set; }
        public string Image1 { get; private set; }
        public string Image2 { get; private set; }
        public IEnumerable<Answer> Answers { get; private set; }

        public Question(string id, string text, IEnumerable<Answer> answers, string image1 = null, string image2 = null)
        {
            Id = id;
            Text = text;
            Answers = answers;
            Image1 = image1;
            Image2 = image2;
        }

    }
    public class Test
    {
        private static readonly Question[] Questions =
        {
            new Question("TMainFeature","Что для Вас главное в автомобиле?", new[]
            {
                new Answer(1,"Комфорт"),
                new Answer(2,"Скорость"),
                new Answer(3, "Надежность"),
                new Answer(4, "Престижность"),
                new Answer(5, "Простота"),
                new Answer(6, "Безопасность"),
                new Answer(7, "Элегантность"),
                new Answer(8, "Эстетичность"),
                new Answer(9, "Мощь"),
                new Answer(0, "Затрудняюсь ответить")
            }), 
            new Question("TColor", "Какой у Вас любимый цвет?", new[]
            {
                new Answer(1,"Черный"),
                new Answer(2,"Белый"),
                new Answer(3,"Желтый"), 
                new Answer(4, "Красный"),
                new Answer(5, "Серый (стальной)"), 
                new Answer(6, "Оранжевый"),
                new Answer(7, "Синий"),
                new Answer(8, "Зеленый"), 
                new Answer(9, "Все цвета"), 
                new Answer(0,"У меня нет любимого цвета")
            }),
            new Question("TPositiveTrait","Какая положительная черта характера Вам присуща больше всего?", new[]
            {
                new Answer(1,"Благоразумие"),
                new Answer(2,"Дружелюбие"), 
                new Answer(3, "Решительность"),
                new Answer(4, "Доброта"), 
                new Answer(5, "Любовь к людям"), 
                new Answer(6,"Верность"), 
                new Answer(7,"Скромность"), 
                new Answer(8, "Щедрость"), 
                new Answer(9, "Целеустремленность"), 
                new Answer(0, "У меня нет положительных черт характера")
            }), 
            new Question("TNegativeTrait","Какая отрицательная черта характера Вам присуща больше всего?", new[]
            {
                new Answer(1,"Жестокость"),
                new Answer(2,"Ветренность"), 
                new Answer(3, "Боязливость"),
                new Answer(4, "Лживость"), 
                new Answer(5, "Лень"), 
                new Answer(6,"Развясность"), 
                new Answer(7,"Эгоизм"), 
                new Answer(8, "Скрытность"), 
                new Answer(9, "Раздражительность"), 
                new Answer(0, "У меня нет отрицательных черт характера")
            }), 
            new Question("TAge", "Какой автомобиль вам больше нравится?",new[]
            {
                new Answer(1,"Первый"), 
                new Answer(2,"Второй")
            },
            "Images/Test/Age1.jpg",
            "Images/Test/Age2.jpg"), 
            new Question("TSpeed", "Какой автомобиль вам больше нравится?",new[]
            {
                new Answer(1,"Первый"), 
                new Answer(2,"Второй")
            },
            "Images/Test/Speed1.jpg",
            "Images/Test/Speed2.jpg"), 
            new Question("TBodyType", "Какой автомобиль вам больше нравится?",new[]
            {
                new Answer(1,"Первый"), 
                new Answer(2,"Второй")
            },
            "Images/Test/BodyType1.jpg",
            "Images/Test/BodyType2.jpg"), 
            new Question("TElectric","Как вы относитесь к электрокарам?",new[]
            {
                new Answer(1,"Положительно. Если бы ездили на электрокарах, воздух стал бы намного чище"),
                new Answer(2, "Отрицательно. Маломощные бесполезные автомобили"), 
                new Answer(0, "Нейтрально")
            }), 
            new Question("TPassengers","С кем Вы ездите чаще всего?",new[]
            {
                new Answer(1,"С семьей"),
                new Answer(2, "С друзьми"), 
                new Answer(0, "Я езжу один")
            }), 
            new Question("TDistance","Какое расстояние Вы проезжаете в сутки?",new[]
            {
                new Answer(1,"До 20 км"),
                new Answer(2, "До 100 км"), 
                new Answer(3, "Свыше 100 км")
            }),
            new Question("TOutLand","Часто ли Вам приходится ездить по внедорожью?", new[]
            {
                new Answer(1,"Вообще не приходится"),
                new Answer(2, "Довольно редко"), 
                new Answer(3, "Когда как (50:50)"),
                new Answer(4, "Довольно часто"),
                new Answer(5, "Очень часто")
            }), 
            new Question("TPrice","Какие автомобили Вы предпочитаете?",new[]
            {
                new Answer(1,"Простые"),
                new Answer(2, "Среднего класса"), 
                new Answer(3, "Выского класса"),
                new Answer(4, "Престижные")
            })
        };

        public static IEnumerable<Question> GetAllQuestions()
        {
            return Questions;
        } 
        public static Question GetQuestion(byte id)
        {
            return id < Questions.Length ? Questions[id] : null;
        }

        public static byte Length
        {
            get { return (byte)Questions.Length; }
        }
    }
}