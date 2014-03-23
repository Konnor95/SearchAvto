namespace SearchAvto.Models.DataModels
{
    public class ProcessResult
    {
        public int Id { get; private set; }
        public bool Succeeded { get; private set; }
        public string Message { get; private set; }
        public int? AffectedObjectId { get; set; }

        public ProcessResult(int id, bool succeeded, string message, int? affectedObjectId = null)
        {
            Id = id;
            Succeeded = succeeded;
            Message = message;
            AffectedObjectId = affectedObjectId;
        }

        public bool IsEmpty
        {
            get { return Succeeded == false && Message == null; }
        }
    }

    public class ProcessResults
    {
        static readonly ProcessResult[] Results =
        {
            new ProcessResult(0,false, "Название не может быть пустым"),
            new ProcessResult(1,false, "Такой бренд уже существует"),
            new ProcessResult(2,false, "Такого бренда не существует"),
            new ProcessResult(3,false, "Неверный формат изображения"),
            new ProcessResult(4,true, "Бренд успешно добавлен"),
            new ProcessResult(5,true, "Бренд успешно изменен"),
            new ProcessResult(6,true, "Бренд успешно удален"),
            new ProcessResult(7,true, "Модельная линия успешно добавлена"),
            new ProcessResult(8,true, "Модельная линия успешно изменена"),
            new ProcessResult(9,true, "Модельная линия успешно удалена"),
            new ProcessResult(10,false, "Такая модельная линия уже существует"),
            new ProcessResult(11,false, "Такой модельной линии не существует"),
            new ProcessResult(12,true, "Модель успешно добавлена"),
            new ProcessResult(13,true, "Модель успешно изменена"),
            new ProcessResult(14,true, "Модель успешно удалена"),
            new ProcessResult(15,false, "Такая модель уже существует"),
            new ProcessResult(16,false, "Такой модели не существует"),
            new ProcessResult(17,true, "Тип кузова успешно добавлен"),
            new ProcessResult(18,true, "Тип кузова успешно изменен"),
            new ProcessResult(19,true, "Тип кузова успешно удален"),
            new ProcessResult(20,false, "Такой тип кузова уже существует"),
            new ProcessResult(21,false, "Такого типа кузова не существует"),
            new ProcessResult(22,true, "Модификация успешно добавлена"),
            new ProcessResult(23,true, "Модификация успешно изменена"),
            new ProcessResult(24,true, "Модификация успешно удалена"),
            new ProcessResult(25,false, "Такая модификация уже существует"),
            new ProcessResult(26,false, "Такой модификации не существует"),
            new ProcessResult(27,false, "Текст не может быть пустым"),
            new ProcessResult(28,true, "Новость успешно добавлена"),
            new ProcessResult(29,true, "Новость успешно изменена"),
            new ProcessResult(30,true, "Новость успешно удалена"),
            new ProcessResult(31,false, "Такая новость уже существует"),
            new ProcessResult(32,false, "Такой новости не существует"),
            new ProcessResult(33,false, "Комметарий не может быть пустым"),
            new ProcessResult(34,true, "Комментарий успешно добалвен")
        };

        public static ProcessResult GetById(int id = -1)
        {
            if (id < 0 || id > Results.Length) return null;
            return Results[id];
        }
        public static ProcessResult TitleCannotBeEmpty
        {
            get { return Results[0]; }
        }

        public static ProcessResult SuchBrandExists
        {
            get { return Results[1]; }
        }

        public static ProcessResult NoSuchBrand
        {
            get { return Results[2]; }
        }

        public static ProcessResult InvalidImageFormat
        {
            get { return Results[3]; }
        }

        public static ProcessResult BrandAdded
        {
            get { return Results[4]; }
        }

        public static ProcessResult BrandChanged
        {
            get { return Results[5]; }
        }

        public static ProcessResult BrandDeleted
        {
            get { return Results[6]; }
        }

        public static ProcessResult ModelLineAdded
        {
            get { return Results[7]; }
        }

        public static ProcessResult ModelLineChanged
        {
            get { return Results[8]; }
        }

        public static ProcessResult ModelLineDeleted
        {
            get { return Results[9]; }
        }

        public static ProcessResult SuchModelLineExists
        {
            get { return Results[10]; }
        }

        public static ProcessResult NoSuchModelLine
        {
            get { return Results[11]; }
        }

        public static ProcessResult ModelAdded
        {
            get { return Results[12]; }
        }

        public static ProcessResult ModelChanged
        {
            get { return Results[13]; }
        }

        public static ProcessResult ModelDeleted
        {
            get { return Results[14]; }
        }

        public static ProcessResult SuchModelExists
        {
            get { return Results[15]; }
        }

        public static ProcessResult NoSuchModel
        {
            get { return Results[16]; }
        }

        public static ProcessResult BodyTypeAdded
        {
            get { return Results[17]; }
        }

        public static ProcessResult BodyTypeChanged
        {
            get { return Results[18]; }
        }

        public static ProcessResult BodyTypeDeleted
        {
            get { return Results[19]; }
        }

        public static ProcessResult SuchBodyTypeExists
        {
            get { return Results[20]; }
        }

        public static ProcessResult NoSuchBodyType
        {
            get { return Results[21]; }
        }

        public static ProcessResult ModificationAdded
        {
            get { return Results[22]; }
        }

        public static ProcessResult ModificationChanged
        {
            get { return Results[23]; }
        }

        public static ProcessResult ModificationDeleted
        {
            get { return Results[24]; }
        }

        public static ProcessResult SuchModificationExists
        {
            get { return Results[25]; }
        }

        public static ProcessResult NoSuchModification
        {
            get { return Results[26]; }
        }
        public static ProcessResult TextCannotBeEmpty
        {
            get { return Results[27]; }
        }

        public static ProcessResult NewsAdded
        {
            get { return Results[28]; }
        }

        public static ProcessResult NewsChanged
        {
            get { return Results[29]; }
        }

        public static ProcessResult NewsDeleted
        {
            get { return Results[30]; }
        }

        public static ProcessResult SuchNewsExists
        {
            get { return Results[31]; }
        }

        public static ProcessResult NoSuchNews
        {
            get { return Results[32]; }
        }

        public static ProcessResult CommentCannotBeEmpty
        {
            get { return Results[33]; }
        }

        public static ProcessResult CommentAddedSuccessfully
        {
            get { return Results[34]; }
        }
    }
}