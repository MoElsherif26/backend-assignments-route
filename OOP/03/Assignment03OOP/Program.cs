using System;
using System.Collections.Generic;

namespace Assignment03OOP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Part 01 - MCQs

            Console.WriteLine("Part 01 - Interface MCQs:");

            Console.WriteLine("1- b) To define a blueprint for a class");
            Console.WriteLine("2- a) private");
            Console.WriteLine("3- b) No");
            Console.WriteLine("4- b) Yes, interfaces can inherit from multiple interfaces");
            Console.WriteLine("5- d) implements");
            Console.WriteLine("6- a) Yes");
            Console.WriteLine("7- b) No, all members are implicitly public");
            Console.WriteLine("8- a) To hide the interface members from outside access");
            Console.WriteLine("9- b) No, interfaces cannot have constructors");
            Console.WriteLine("10- c) By separating interface names with commas");

            Console.WriteLine();

            #endregion

            #region Part 02 - Q1: IShape, ICircle, IRectangle

            Console.WriteLine("Part 02 - Q1: Shape Info\n");
            IShape circle = new Circle(5);
            circle.DisplayShapeInfo();

            IShape rectangle = new Rectangle(4, 6);
            rectangle.DisplayShapeInfo();

            #endregion

            #region Part 02 - Q2: IAuthenticationService

            Console.WriteLine("\nPart 02 - Q2: Authentication Service\n");
            IAuthenticationService authService = new BasicAuthenticationService();

            bool isAuthenticated = authService.AuthenticateUser("admin", "1234");
            Console.WriteLine($"Authenticated: {isAuthenticated}");

            bool isAuthorized = authService.AuthorizeUser("admin", "admin");
            Console.WriteLine($"Authorized: {isAuthorized}");

            #endregion

            #region Part 02 - Q3: Notification Services

            Console.WriteLine("\nPart 02 - Q3: Notification Services\n");

            INotificationService emailService = new EmailNotificationService();
            emailService.SendNotification("m@example.com", "Welcome!");

            INotificationService smsService = new SmsNotificationService();
            smsService.SendNotification("0100000000", "Your code is 1234");

            INotificationService pushService = new PushNotificationService();
            pushService.SendNotification("User123", "New message received");

            #endregion
        }
    }

    #region Q1 - Interfaces and Shapes

    interface IShape
    {
        double Area { get; }
        void DisplayShapeInfo();
    }

    interface ICircle : IShape { }

    interface IRectangle : IShape { }

    class Circle : ICircle
    {
        public double Radius { get; set; }
        public double Area => Math.PI * Radius * Radius;

        public Circle(double radius) => Radius = radius;

        public void DisplayShapeInfo()
        {
            Console.WriteLine($"Circle with Radius = {Radius}, Area = {Area:F2}");
        }
    }

    class Rectangle : IRectangle
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public double Area => Width * Height;

        public Rectangle(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public void DisplayShapeInfo()
        {
            Console.WriteLine($"Rectangle with Width = {Width}, Height = {Height}, Area = {Area:F2}");
        }
    }

    #endregion

    #region Q2 - Authentication Service

    interface IAuthenticationService
    {
        bool AuthenticateUser(string username, string password);
        bool AuthorizeUser(string username, string role);
    }

    class BasicAuthenticationService : IAuthenticationService
    {
        public bool AuthenticateUser(string username, string password)
        {
            return username == "admin" && password == "1234";
        }

        public bool AuthorizeUser(string username, string role)
        {
            return username == "admin" && role == "admin";
        }
    }

    #endregion

    #region Q3 - Notification Services

    interface INotificationService
    {
        void SendNotification(string recipient, string message);
    }

    class EmailNotificationService : INotificationService
    {
        public void SendNotification(string recipient, string message)
        {
            Console.WriteLine($"Sending EMAIL to {recipient}: {message}");
        }
    }

    class SmsNotificationService : INotificationService
    {
        public void SendNotification(string recipient, string message)
        {
            Console.WriteLine($"Sending SMS to {recipient}: {message}");
        }
    }

    class PushNotificationService : INotificationService
    {
        public void SendNotification(string recipient, string message)
        {
            Console.WriteLine($"Sending PUSH Notification to {recipient}: {message}");
        }
    }

    #endregion
}
