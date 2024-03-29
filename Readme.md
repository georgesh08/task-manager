## Вступительно задание в [ITMO kysect](https://github.com/kysect)
___
 ## Условие:



## Сущности

>Основная задача - реализовать консольную программу для управления задачами. 

- Задача
  - Соостоит из идентификатора и информации в виде строки
  - Может быть отмечена как выполненная
  - Может содержать дату выполнения (дедлайн)
  - Содержит список подзадач

- Группа задач
  - Состоит из идентификатора и названия
  - Содержит список задач

- Подзадача
  - Соостоит из идентификатора и информации в виде строки
  - Может быть отмечена как выполненная

## Функционал

- Задача
  - Создание новой задачи
  - Просмотр всех созданных задачи
  - Удаление задач
  - Возможность отметить задачу как выполненную
  - Получение всех выполненных задач
  - Возможность указать дату выполнения задачи (дедлайн)
  - Возможность получить задачи, которые нужно сделать сегодня

- Группа задач
  - Создание группы для задач
  - Удаление группы задач
  - Добавление задачи в группу
  - Удаление задачи из группы
  - Получение списка групп с добавленными задачами

- Подзадача
  - Добавление подзадачи к задаче
  - Возможность отметиить подзадачу выполненной
  - Отображение подзадач при получении задач

- Импорт/экспорт
  - Сохранение в файл задачи (включая группы и подзадачи)
  - Загрузка из файла задач (включая группы и подзадачи)

## Требования

Консольное приложение, которое принимает команды с аргументами, реализующие указанный функционал, и возвращает результат выполнения.

Пример команд:

Команда: `/create-taks task-info`
Аргументы: `task-info` - описание создаваемой задачи
Результат: `Task created. Id: task-id`

Команда: `/list-taks`
Результат: 
```
[ ] {task-id} task-info 
  - [ ] {sub-task-id} sub-task-info 
  - [x] {sub-task-id} sub-task-info 
[ ] (task-deadline) {task-id} task-info 
[x] {task-id} task-info
```

Следует сделать акцент на объектно-ориентированном решение. Отделить логику выполнения от логики работы с консолью (парсинг комманд, форматирование вывода и т. д.).

Для импорта и экспорта можно воспользоваться nuget-библиотеками. Например, сериализовать в Json с помощью System.Text.Json.