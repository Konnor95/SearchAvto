using System.Collections.Generic;
using System.Linq;

namespace SearchAvto.Models.DataModels
{
    public class Answer
    {
        public byte Id { get; private set; }
        public string Text { get; private set; }
        public string Meaning { get; private set; }

        public Answer(byte id, string text, string meaning = null)
        {
            Id = id;
            Text = text;
            Meaning = meaning ?? text;
        }
    }

    public class Question
    {
        public string Id { get; private set; }
        public string Text { get; private set; }
        public string Meaning { get; private set; }
        public List<string> Images { get; private set; }
        public List<Answer> Answers { get; private set; }

        public Question(string id, string text, string meaning, IEnumerable<Answer> answers, IEnumerable<string> images = null)
        {
            Id = id;
            Text = text;
            Meaning = meaning ?? text;
            Answers = new List<Answer>(answers);
            Images = images == null ? null : new List<string>(images);
        }

        public Answer GetAnswer(byte index)
        {
            return Answers.FirstOrDefault(x => x.Id == index);
        }
    }
    public class Test
    {
        private static readonly Question[] Questions =
        {
            new Question("TMainFeature","Что для Вас главное в автомобиле?", "Главное качество",new[]
            {
                new Answer(0,"Комфорт"),
                new Answer(1,"Скорость"),
                new Answer(2, "Надежность"),
                new Answer(3, "Престижность"),
                new Answer(4, "Простота"),
                new Answer(5, "Безопасность"),
                new Answer(6, "Эстетичность (красота)"),
                new Answer(7, "Мощь")
            }), 
            new Question("TColor", "Какой у Вас любимый цвет?", "Ассоциируемый цвет", new[]
            {
                new Answer(0,"Черный"),
                new Answer(1,"Белый"),
                new Answer(2,"Желтый"), 
                new Answer(3, "Красный / бардовый"),
                new Answer(4, "Серый / стальной"), 
                new Answer(5, "Оранжевый / бурый"),
                new Answer(6, "Синий"),
                new Answer(7, "Зеленый")
            }),
            new Question("TAge", "Какой Chevrolet Camaro вам больше нравится?","Временная характеристика",
                new[]
                {
                    new Answer(0,"Первый (2013 года)","Новый (От 2005 года)"), 
                    new Answer(1,"Второй (2002 года)","Средний (От 1990 до 2005 года)"),
                    new Answer(2,"Третий (1967 года)","Старый (До 1990 года)")
                },
                new[]
                {
                    "Images/Test/Age1.jpg",
                    "Images/Test/Age2.jpg",
                    "Images/Test/Age3.jpg"
                }
            ), 
            new Question("TSpeed", "Какой автомобиль вам больше нравится?","Скоростная характеристика",
                new[]
                {
                    new Answer(0,"Первый","Очень быстрый (свыше 300 км/ч)"), 
                    new Answer(1,"Второй","Быстрый (От 200 до 300 км/ч)"),
                    new Answer(2,"Третий","Не очень быстрый (до 200 км/ч)")
                },
                new[]
                {
                    "Images/Test/Speed1.jpg",
                    "Images/Test/Speed2.jpg",
                    "Images/Test/Speed3.jpg"
                }
            ), 
            new Question("TSize", "Какой автомобиль вам больше нравится?","Относительный размер",
                new[]
                {
                    new Answer(0,"Первый","Большой"), 
                    new Answer(1,"Второй", "Средний"),
                    new Answer(2,"Третий","Маленький")
                },
                new[]
                {
                    "Images/Test/Size1.jpg",
                    "Images/Test/Size2.jpg",
                    "Images/Test/Size3.jpg"
                }
            ), 
            new Question("TEngine","Какие автомобили Вы предпочитаете?","Тип двигателя",new[]
            {
                new Answer(0,"С двигателем внутреннего сгорания","Внутреннего сгорания"),
                new Answer(1, "Электрокары", "Электрический"), 
                new Answer(2, "Гибриды", "Гибрид")
            }), 
            new Question("TPassengers","С кем Вы ездите чаще всего?","Основной тип пассажиров",new[]
            {
                new Answer(0,"С семьей","Семья"),
                new Answer(1, "С друзьми","Друзья"), 
                new Answer(2, "Я езжу один","Нет пассажиров")
            }), 
            new Question("TDistance","Часто ли Вы ездите на большие расстояния?","Подходит ли для езды на расстояния",new[]
            {
                new Answer(0,"Часто","Большие"),
                new Answer(1, "Иногда","Средние"), 
                new Answer(2, "Никогда", "Небольшие")
            }),
            new Question("TOutLand","Часто ли Вам приходится ездить по внедорожью?","Подходит ли для внедорожья", new[]
            {
                new Answer(0, "Довольно редко","Слабо подходит"), 
                new Answer(1, "Когда как (50:50)", "Подходит"),
                new Answer(2, "Довольно часто","Очень подходит")
            }), 
            new Question("TPrice","Какие автомобили Вы предпочитаете?","Ценовая категория",new[]
            {
                new Answer(0,"Простые"),
                new Answer(1, "Среднего класса"), 
                new Answer(2, "Высокого класса"),
                new Answer(3, "Престижные")
            })
        };

        public static Question MainFeature { get { return Questions[0]; } }
        public static Question Color { get { return Questions[1]; } }
        public static Question PositiveTrait { get { return Questions[2]; } }
        public static Question NegativeTrait { get { return Questions[3]; } }
        public static Question Age { get { return Questions[4]; } }
        public static Question Speed { get { return Questions[5]; } }
        public static Question BodyType { get { return Questions[6]; } }
        public static Question Electric { get { return Questions[7]; } }
        public static Question Passengers { get { return Questions[8]; } }
        public static Question Distance { get { return Questions[9]; } }
        public static Question OutLand { get { return Questions[10]; } }
        public static Question Price { get { return Questions[11]; } }

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