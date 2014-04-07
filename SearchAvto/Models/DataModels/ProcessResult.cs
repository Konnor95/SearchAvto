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
            new ProcessResult(34,true, "Комментарий успешно добавлен"),
            new ProcessResult(35,true, "Тип батареи успешно добавлен"),
            new ProcessResult(36,true, "Тип батареи успешно изменен"),
            new ProcessResult(37,true, "Тип батареи успешно удален"),
            new ProcessResult(38,false, "Такой тип батареи уже существует"),
            new ProcessResult(39,false, "Такого типа батареи не существует"),
            new ProcessResult(40,true, "Тип топлива успешно добавлен"),
            new ProcessResult(41,true, "Тип топлива успешно изменен"),
            new ProcessResult(42,true, "Тип топлива успешно удален"),
            new ProcessResult(43,false, "Такой тип топлива уже существует"),
            new ProcessResult(44,false, "Такого типа топлива не существует"),
            new ProcessResult(45,true, "Тип каркасса шины успешно добавлен"),
            new ProcessResult(46,true, "Тип каркасса шины успешно изменен"),
            new ProcessResult(47,true, "Тип каркасса шины успешно удален"),
            new ProcessResult(48,false, "Такой тип каркасса шины уже существует"),
            new ProcessResult(49,false, "Такого типа каркасса шины не существует"),
            new ProcessResult(50,true, "Тип трансмиссии успешно добавлен"),
            new ProcessResult(51,true, "Тип трансмиссии успешно изменен"),
            new ProcessResult(52,true, "Тип трансмиссии успешно удален"),
            new ProcessResult(53,false, "Такой тип трансмиссии уже существует"),
            new ProcessResult(54,false, "Такого типа трансмиссии не существует"),
            new ProcessResult(55,true, "Тип расположения двигателя успешно добавлен"),
            new ProcessResult(56,true, "Тип расположения двигателя успешно изменен"),
            new ProcessResult(57,true, "Тип расположения двигателя успешно удален"),
            new ProcessResult(58,false, "Такой тип расположения двигателя уже существует"),
            new ProcessResult(59,false, "Такого типа расположения двигателя не существует"),
            new ProcessResult(60,true, "Тип компоновки цилиндров успешно добавлен"),
            new ProcessResult(61,true, "Тип компоновки цилиндров успешно изменен"),
            new ProcessResult(62,true, "Тип компоновки цилиндров успешно удален"),
            new ProcessResult(63,false, "Такой тип компоновки цилиндров уже существует"),
            new ProcessResult(64,false, "Такого типа компоновки цилиндров не существует"),
            new ProcessResult(65, false, "Такого комменатрия не существует"),
            new ProcessResult(66, true, "Комментарий успешно удален") ,
            new ProcessResult(67, false, "Пользователь с таким адресом электронной почты уже существует"),
            new ProcessResult(68, false, "Пользователь с таким именем уже существует"),
            new ProcessResult(69, false, "Ошибка при регистрации"),
            new ProcessResult(70, true, "Пользователь зарегестрирован"),
            new ProcessResult(71, false, "Такого пользователя не существует"),
            new ProcessResult(72, true, "Статус пользователя успешно изменен"),
            new ProcessResult(73, false, "Неверный пароль"),
            new ProcessResult(74, false, "Неверный логин или email"),
            new ProcessResult(75, true, "Пользователь вошел в систему")
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

        public static ProcessResult BatteryTypeAdded
        {
            get { return Results[35]; }
        }

        public static ProcessResult BatteryTypeChanged
        {
            get { return Results[36]; }
        }

        public static ProcessResult BatteryTypeDeleted
        {
            get { return Results[37]; }
        }

        public static ProcessResult SuchBatteryTypeExists
        {
            get { return Results[38]; }
        }

        public static ProcessResult NoSuchBatteryType
        {
            get { return Results[39]; }
        }

        public static ProcessResult FuelTypeAdded
        {
            get { return Results[40]; }
        }

        public static ProcessResult FuelTypeChanged
        {
            get { return Results[41]; }
        }

        public static ProcessResult FuelTypeDeleted
        {
            get { return Results[42]; }
        }

        public static ProcessResult SuchFuelTypeExists
        {
            get { return Results[43]; }
        }

        public static ProcessResult NoSuchFuelType
        {
            get { return Results[44]; }
        }

        public static ProcessResult TireCarcassTypeAdded
        {
            get { return Results[45]; }
        }

        public static ProcessResult TireCarcassTypeChanged
        {
            get { return Results[46]; }
        }

        public static ProcessResult TireCarcassTypeDeleted
        {
            get { return Results[47]; }
        }

        public static ProcessResult SuchTireCarcassTypeExists
        {
            get { return Results[48]; }
        }

        public static ProcessResult NoSuchTireCarcassType
        {
            get { return Results[49]; }
        }

        public static ProcessResult TransmissionTypeAdded
        {
            get { return Results[50]; }
        }

        public static ProcessResult TransmissionTypeChanged
        {
            get { return Results[51]; }
        }

        public static ProcessResult TransmissionTypeDeleted
        {
            get { return Results[52]; }
        }

        public static ProcessResult SuchTransmissionTypeExists
        {
            get { return Results[53]; }
        }

        public static ProcessResult NoSuchTransmissionType
        {
            get { return Results[54]; }
        }


        public static ProcessResult EngineLocationAdded
        {
            get { return Results[55]; }
        }

        public static ProcessResult EngineLocationChanged
        {
            get { return Results[56]; }
        }

        public static ProcessResult EngineLocationDeleted
        {
            get { return Results[57]; }
        }

        public static ProcessResult SuchEngineLocationExists
        {
            get { return Results[58]; }
        }

        public static ProcessResult NoSuchEngineLocation
        {
            get { return Results[59]; }
        }

        public static ProcessResult ValvesArrangementAdded
        {
            get { return Results[60]; }
        }

        public static ProcessResult ValvesArrangementChanged
        {
            get { return Results[61]; }
        }

        public static ProcessResult ValvesArrangementDeleted
        {
            get { return Results[62]; }
        }

        public static ProcessResult SuchValvesArrangementExists
        {
            get { return Results[63]; }
        }

        public static ProcessResult NoSuchValvesArrangement
        {
            get { return Results[64]; }
        }

        public static ProcessResult NoSuchComment
        {
            get { return Results[65]; }
        }

        public static ProcessResult CommentDeleted
        {
            get { return Results[66]; }
        }

        public static ProcessResult UserWithSuchEmailExists
        {
            get { return Results[67]; }
        }

        public static ProcessResult UserWithSuchNameExists
        {
            get { return Results[68]; }
        }

        public static ProcessResult RegistrationError
        {
            get { return Results[69]; }
        }

        public static ProcessResult UserRegistered
        {
            get { return Results[70]; }
        }

        public static ProcessResult NoSuchUser
        {
            get { return Results[71]; }
        }

        public static ProcessResult UserStatusChanged
        {
            get { return Results[72]; }
        }

        public static ProcessResult InvalidPassword
        {
            get { return Results[73]; }
        }
        public static ProcessResult InvalidLoginOrEmail
        {
            get { return Results[74]; }
        }
        public static ProcessResult UserLogedIn
        {
            get { return Results[75]; }
        }
    }
}