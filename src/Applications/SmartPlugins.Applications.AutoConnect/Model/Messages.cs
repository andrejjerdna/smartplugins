using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxCoDesign.Applications.AutoConnect.Model
{
    public struct MLMessages
    {
        public static readonly string StatusProgress = "ОБРАБОТАНО";


        public static readonly string ModelDatasDone = "ДАННЫЕ ИЗ МОДЕЛИ ПОЛУЧЕНЫ!";

        public static readonly string ModelTrainingDone = "МОДЕЛЬ ОБУЧЕНА!";
        public static readonly string ModelTrainingError = "ОШИБКА ПРИ ОБУЧЕНИИ МОДЕЛИ!";

        public static readonly string ResultsVisibleDone = "РЕЗУЛЬТАТЫ ОБОЗНАЧЕНЫ В МОДЕЛИ!";
        public static readonly string ResultsVisibleError = "ОШИБКА ОБОЗНАЧЕНИЯ РЕЗУЛЬТАТОВ!";

        public static readonly string ConnectTrainDone = "ОБУЧЕНИЕ РАССТАНОВКЕ УЗЛОВ УСПЕШНО ЗАВЕРШЕНО!";
        public static readonly string ConnectTrainError = "ОБУЧЕНИЕ РАССТАНОВКЕ ЗАВЕРШЕНО С ОШИБКОЙ!";

        public static readonly string EditError = "ОШИБКА РЕДАКТИРОВАНИЯ!";

        public static readonly string InsertConnecrionsDone = "УЗЛЫ ВСТАВЛЕНЫ В МОДЕЛЬ!";
        public static readonly string InsertConnecrionsError = "ВСТАВКА УЗЛОВ ПРОИЗОШЛА С ОШИБКОЙ!";

        public static readonly string GetObjectsWithView = "ПОЛУЧЕНИЕ ОБЪЕКТОВ ВИДА...";

        public static readonly string ViewSelectError = "НЕ ВЫБРАН ВИД!";

        public static readonly string ConnectDeleteDone = "УЗЛЫ УДАЛЕНЫ ИЗ МОДЕЛИ!";
    }
}
