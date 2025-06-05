package com.example.ticketsmanagement;

import java.util.ArrayList;
import java.util.List;

public class DataArray {

    // Класс для пользователя
    public static class User {
        public int Id;
        public String Login;
        public String Password;

        public User(int id, String login, String password) {
            this.Id = id;
            this.Login = login;
            this.Password = password;
        }

        @Override
        public String toString() {
            return "User{" +
                    "Id=" + Id +
                    ", Login='" + Login + '\'' +
                    ", Password='" + Password + '\'' +
                    '}';
        }
    }

    // Класс для заявки
    public static class Ticket {
        public int Id;
        public String CreationDate;
        public String Equipment;
        public String FaultType;
        public String Description;
        public int ClientId;
        public String Status;
        public String DateCompleted;

        public Ticket(int id, String creationDate, String equipment, String faultType, String description, int clientId, String status, String dateCompleted) {
            this.Id = id;
            this.CreationDate = creationDate;
            this.Equipment = equipment;
            this.FaultType = faultType;
            this.Description = description;
            this.ClientId = clientId;
            this.Status = status;
            this.DateCompleted = dateCompleted;
        }

        @Override
        public String toString() {
            return "Request{" +
                    "Id=" + Id +
                    ", CreationDate='" + CreationDate + '\'' +
                    ", Equipment='" + Equipment + '\'' +
                    ", FaultType='" + FaultType + '\'' +
                    ", Description='" + Description + '\'' +
                    ", ClientId=" + ClientId +
                    ", Status='" + Status + '\'' +
                    ", DateCompleted='" + DateCompleted + '\'' +
                    '}';
        }
    }


    // Массивы данных (замените на ваши данные)
    public static List<User> Users = new ArrayList<>();
    public static List<Ticket> Tickets = new ArrayList<>();

    static {
        // Пример заполнения пользователей
        Users.add(new User(1, "HologoDxx", "123321"));
        Users.add(new User(2, "Eugene", "123"));


        // Пример заполнения заявок
        Tickets.add(new Ticket(1, "2025-05-30", "Блок питания", "Сгорел", "Мой блок питания сгорел когда я попытался включить мой компьютер.", 1, "Выполнено", "2025-06-02"));
        Tickets.add(new Ticket(2, "2025-05-30", "Процессор", "Сильно греется", "Мой процессор сильно греется, даже когда нагрузки почти нет.", 2, "Выполнено", "2025-06-01"));
        Tickets.add(new Ticket(3, "2025-06-01", "Видеокарта", "Сильно греется", "Моя видеокарта перегревается.", 2, "В ожидании", "2025-06-02"));
    }
}