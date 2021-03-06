﻿/*
 C# What are Anonymous Types ?

 Anonymous types provide a convenient way to encapsulate a set of read-only properties
 into a single object without having to explicitly define a type first. 
 The type name is generated by the compiler and is not available at the source code level. 
 The type of each property is inferred by the compiler.
 Anonymous types typically are used in the select clause of a query expression to 
 return a subset of the properties from each object in the source sequence.

 C# select clause 

 The following example shows all the different forms that a select clause may take. 
 In each query, note the relationship between the 
 select clause and the type of the query variable 
 studentQuery1, studentQuery2.

*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace CA
{
    class Sample
    {      
        public class Student
        {
            public string First { get; set; }
            public string Last { get; set; }
            public int ID { get; set; }
            public List<int> Scores;
            public ContactInfo GetContactInfo(Sample app, int id)
            {
                ContactInfo cInfo =
                    (from ci in app.contactList
                     where ci.ID == id
                     select ci)
                    .FirstOrDefault();

                return cInfo;
            }
            public override string ToString()
            {
                return First + " " + Last + ":" + ID;
            }
        }
        public class ContactInfo
        {
            public int ID { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public override string ToString() { return Email + "," + Phone; }
        }
        public class ScoreInfo
        {
            public double Average { get; set; }
            public int ID { get; set; }
        }
        List<Student> students = new List<Student>()
        {
             new Student {First="Aaron", Last="Garcia", ID=111, Scores= new List<int>() {97, 92, 81, 60}},
             new Student {First="Fab", Last="O'Donnell", ID=112, Scores= new List<int>() {75, 84, 91, 39}},
             new Student {First="Calvin ", Last="Mortensen", ID=113, Scores= new List<int>() {88, 94, 65, 91}},
             new Student {First="Denis", Last="Rafi", ID=114, Scores= new List<int>() {97, 89, 85, 82}},
        };
        List<ContactInfo> contactList = new List<ContactInfo>()
        {
            new ContactInfo {ID=111, Email="Aaron@gmail.com", Phone="456-745-4612"},
            new ContactInfo {ID=112, Email="FabO@gmail.com", Phone="563-561-8461"},
            new ContactInfo {ID=113, Email="Calvin@gmail.com", Phone="216-451-5921"},
            new ContactInfo {ID=114, Email="Denis@gmail.com", Phone="206-555-0521"}
        };
        static void Main()
        {
            Sample app = new Sample();

            IEnumerable<Student> studentQuery1 =
                from student in app.students
                where student.ID > 111
                select student;

            Console.WriteLine("Query1: select range_variable");
            foreach (Student s in studentQuery1)
            {
                Console.WriteLine(s.ToString());
            }
            IEnumerable<String> studentQuery2 =
                from student in app.students
                where student.ID > 111
                select student.Last;

            Console.WriteLine("\r\n studentQuery2: select range_variable.Property");
            foreach (string s in studentQuery2)
            {
                Console.WriteLine(s);
            }
            IEnumerable<ContactInfo> studentQuery3 =
                from student in app.students
                where student.ID > 111
                select student.GetContactInfo(app, student.ID);

            Console.WriteLine("\r\n studentQuery3: select range_variable.Method");
            foreach (ContactInfo ci in studentQuery3)
            {
                Console.WriteLine(ci.ToString());
            }
            IEnumerable<int> studentQuery4 =
                from student in app.students
                where student.ID > 111
                select student.Scores[0];

            Console.WriteLine("\r\n studentQuery4: select range_variable[index]");
            foreach (int i in studentQuery4)
            {
                Console.WriteLine("First score = {0}", i);
            }
            IEnumerable<double> studentQuery5 =
                from student in app.students
                where student.ID > 111
                select student.Scores[0] * 1.1;

            Console.WriteLine("\r\n studentQuery5: select expression");
            foreach (double d in studentQuery5)
            {
                Console.WriteLine("Adjusted first score = {0}", d);
            }
            IEnumerable<double> studentQuery6 =
                from student in app.students
                where student.ID > 111
                select student.Scores.Average();

            Console.WriteLine("\r\n studentQuery6: select expression2");
            foreach (double d in studentQuery6)
            {
                Console.WriteLine("Average = {0}", d);
            }
            var studentQuery7 =
                from student in app.students
                where student.ID > 111
                select new { student.First, student.Last };

            Console.WriteLine("\r\n studentQuery7: select new anonymous type");
            foreach (var item in studentQuery7)
            {
                Console.WriteLine("{0}, {1}", item.Last, item.First);
            }
            IEnumerable<ScoreInfo> studentQuery8 =
                from student in app.students
                where student.ID > 111
                select new ScoreInfo
                {
                    Average = student.Scores.Average(),
                    ID = student.ID
                };
            Console.WriteLine("\r\n studentQuery8: select new named type");
            foreach (ScoreInfo si in studentQuery8)
            {
                Console.WriteLine("ID = {0}, Average = {1}", si.ID, si.Average);
            }
            IEnumerable<ContactInfo> studentQuery9 =
                from student in app.students
                where student.Scores.Average() > 85
                join ci in app.contactList on student.ID equals ci.ID
                select ci;

            Console.WriteLine("\r\n studentQuery9: select result of join clause");
            foreach (ContactInfo ci in studentQuery9)
            {
                Console.WriteLine("ID = {0}, Email = {1}", ci.ID, ci.Email);
            }
            Console.ReadKey();
        }
    }   
}

