using System;
using System.Collections.Generic;
using LibrarySystem;

// These lines must NOT compile:
// Person person = new Person(\"P1\", \"Ali\", \"123\");
// Member member = new Member(...);
// Staff staff = new Staff(...);
// LibraryItem item = new LibraryItem(...);
// student.FullName = \"New Name\";
// student.Loans.Add(loan);
// book.IsOnLoan = true;

Console.WriteLine($"=== Invalid Borrowing Attempts ===");

PremiumMember premium = new PremiumMember( "P1", "Ali", "111", 20);

StudentMember student = new StudentMember( "p2", "Ahmad", "222");

Librarian librarian = new Librarian(
    "S1", "Sara", "333",
    new DateTime(2024, 1, 1), 3000);

HeadLibrarian head = new HeadLibrarian(
    "S2", "Omar", "444",
    new DateTime(2023, 1, 1), 5000);

Shelver shelver = new Shelver(
    "S3", "Maya", "555",
    new DateTime(2024, 1, 1), 2500, "A");

Book book1 = new Book("B1", "C# Basics", 10);
DVD dvd1 = new DVD("D1", "Learning C#", 10);
Magazine magazine1 = new Magazine("M1", "Tech Today", 10);

head.WithdrawItem(book1);

try
{
    student.BorrowItem(book1, DateTime.Today);
    student.BorrowItem(dvd1, DateTime.Today);
    student.BorrowItem(dvd1, DateTime.Today);
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}


Book book2 = new Book("B2", "OOP", 10);
Book book3 = new Book("B3", "C# Advanced", 10);
Book book4 = new Book("B4", "Design Patterns", 10);

try
{

    student.BorrowItem(book2, DateTime.Today);
    student.BorrowItem(book3, DateTime.Today);
    student.BorrowItem(book4, DateTime.Today);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
Console.WriteLine($"\n=== Staff Monthly Pay ===");

List<Staff> staffMembers = new List<Staff>
{
    librarian,
    head,
    shelver
};

foreach (Staff staffMember in staffMembers)
{
    Console.WriteLine(
        $"{staffMember.FullName}: {staffMember.GetMonthlyPay()}");
}

Console.WriteLine("\n=== Library Items ===");

List<LibraryItem> items = new List<LibraryItem>
{
    book1,
    dvd1,
    magazine1
};

foreach (LibraryItem item in items)
{
    Console.WriteLine(
        $"{item.Title}: {item.LoanPeriodDays} days," +
        $"Daily late fee: {item.GetDailyLateFee()}");
}

Console.WriteLine($"\n=== Premium Member Return ===");

DVD premiumDvd = new DVD("D2", "Advanced C#", 10);

Loan premiumLoan = premium.BorrowItem( premiumDvd, DateTime.Today);

DateTime returnDate = premiumLoan.DueDate.AddDays(5);

librarian.Return(premiumLoan, returnDate);

Console.WriteLine($"Due Date: {premiumLoan.DueDate}");
Console.WriteLine($"Late Fee: {premiumLoan.LateFee}");
Console.WriteLine($"Reading Points: {premium.ReadingPoints}");

Console.WriteLine($"\n=== Invalid Loan Status Changes ===");

try
{
    librarian.Return(premiumLoan, returnDate.AddDays(1));
    librarian.MarkAsLost(premiumLoan);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

