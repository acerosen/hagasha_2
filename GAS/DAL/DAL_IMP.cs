using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BE;
using DS;
using System.IO;
using System.ComponentModel;

namespace DAL
{
    public class DAL_IMP : Idal
    {
        XElement TesterRoot;
        string TesterPath = @"TesterXml.xml";

        XElement TraineeRoot;
        string TraineePath = @"TraineeXml.xml";

        XElement TestRoot;
        const string TestPath = @"Test.XML"; //orderPath

        public DAL_IMP()
        {
            try
            {
                if (!File.Exists(TesterPath))
                {
                    TesterRoot = new XElement("Testers");
                    TesterRoot.Save(TesterPath);
                }
                else TesterRoot = XElement.Load(TesterPath);
                if (!File.Exists(TraineePath))//branch
                {
                    TraineeRoot = new XElement("Trainees");
                    TraineeRoot.Save(TraineePath);
                }
                else TraineeRoot = XElement.Load(TraineePath);

                if (!File.Exists(TestPath))
                {
                    TestRoot = new XElement("Test");
                    TestRoot.Save(TestPath);
                }
                else TestRoot = XElement.Load(TestPath);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }

        #region Test
        public bool FindTest(Test T)
        {
            XElement orderElement;
            orderElement = (from p in TestRoot.Elements()
                            where Convert.ToInt32(p.Element("TestID").Value) == T.ID
                            select p).FirstOrDefault();
            if (orderElement == null)
                return false;
            return true;
        }
        public void AddTest(Test T)
        {
            XElement TestID = new XElement("TestID", T.ID);
            XElement TesterID = new XElement("TesterID", T.TesterID);
            XElement TraineeID = new XElement("TraineeID", T.TraineeID);
            XElement Date = new XElement("Date", T.Date);
            XElement Hour = new XElement("Hour", T.Hour);
            XElement Address = new XElement("Address", T.Address);
            XElement Grade = new XElement("Grade", T.Grade);
            XElement Comment = new XElement("Comment", T.Comment);
            TesterRoot.Add(new XElement("Test", TestID, TesterID, TraineeID, Date, Hour, Address, Grade, Comment));
            TestRoot.Save(TestPath);

        }
        public void DeleteTest(int TestID)
        {

            
            try
            {
                IEnumerable<XElement> TestElements;
                XElement TestElement;
                TestElement = (from p in TestRoot.Elements()
                                where p.Element("TestID").Value == TestID.ToString();
                                select p.FirstOrDefault();

               TestElements = from p in TestRoot.Elements()
                                    where Convert.ToInt32(p.Element("TestID").Value) == T.ID
                                    select p;
                foreach (var item in TestElements)
                    item.Remove();
                TestRoot.Save(TestPath);
                TestElement.Remove();
                TestRoot.Save(TestPath);
            }
            catch
            {
                throw new Exception("The order dos'nt exsist in the system");
            }
        }
        public void UpdateTest(Test T)
        {
            TestRoot = XElement.Load(TestPath);
            XElement Test = (from item in TestRoot.Elements()
                              where item.Element("TestID").Value == T.ID.ToString()
                              select item).FirstOrDefault();
            Test.Element("TestID").Value = T.ID.ToString();
            Test.Element("TesterID").Value = T.TesterID.ToString();
            Test.Element("TraineeID").Value = T.TraineeID.ToString();
            Test.Element("Date").Value = T.Date.ToString();
            Test.Element("Hour").Value = T.Hour.ToString();
            Test.Element("Address").Value = T.Address.ToString();
            Test.Element("Grade").Value = T.Grade.ToString();
            Test.Element("Comment").Value = T.Comment.ToString();
            TestRoot.Save(TestPath);
        }

        public List<Test> GetListTests()
        {
            TestRoot = XElement.Load(TestPath);
            List<Test> orders = new List<Test>();
            try
            {
                List<Test> Test = (from item in TestRoot.Elements()
                                   select new Test()
                                   {
                                       ID = Convert.ToInt32(item.Element("TestID").Value),
                                       TesterID = Convert.ToInt32(item.Element("Tester").Value),
                                       TraineeID = Convert.ToInt32(item.Element("TraineeID").Value),
                                       Date = DateTime.Parse(item.Element("Date").Value.to),
                                       Hour = Convert.ToInt32(item.Element("Hour").Value),
                                       Address = item.Element("Address").Value,
                                       Grade = Convert.ToBoolean("Grade"),
                                       Comment = item.Element("Comment").Value,
                                   }).ToList();
            }
            catch
            {
                orders = null;
            }

            return orders;
        }

        public IEnumerable<Test> GetAllTests(Func<Test, bool> predicat = null)
        {
            IEnumerable<Test> Test;
            try
            {

                Test = (from item in TestRoot.Elements()
                         select new Test()
                         {
                             ID = Convert.ToInt32(item.Element("TestID").Value),
                             TesterID = Convert.ToInt32(item.Element("TesterID").Value),
                             TraineeID = Convert.ToInt32(item.Element("TraineeID").Value),
                             Date = DateTime.Parse(item.Element("Date").Value),
                             Hour = Convert.ToInt32(item.Element("Hour").Value),
                             Address = item.Element("Address").Value,
                             Grade = Convert.ToBoolean("Grade"),
                             Comment = item.Element("Comment").Value,
                         }).ToList();
                if (predicat != null)
                {
                    Test = Test.Where(predicat);

                }
            }
            catch
            {
                Test = null;
            }
            return Test;
        }
        #endregion

        #region Tester

        public bool FindTester(int id)
        {
            XElement TesterElement;
            TesterElement = (from p in TesterRoot.Elements()
                           where Convert.ToInt32(p.Element("TesterID").Value) == id
                           select p).FirstOrDefault();
            if (TesterElement == null)
                return false;
            return true;
        }

        public void AddTester(Tester T)
        {
            if (FindTester(T.ID))
                throw new Exception("The dish is already exsist in the system");
            XElement TesterID = new XElement("TesterID", T.ID);
            XElement FamilyName = new XElement("FamilyName", T.FamilyName);
            XElement FirstName = new XElement("FirstName", T.FirstName);
            XElement Birthday = new XElement("Birthday", T.Birthday);
            XElement Gender = new XElement("Gender", T.Gender);
            XElement PhoneNum = new XElement("PhoneNum", T.PhoneNum);
            XElement Address = new XElement("Address", T.Address);
            XElement Experience = new XElement("Experience", T.Experience);
            XElement MaxTestAmt = new XElement("MaxTestAmt", T.MaxTestAmt);
            XElement Model = new XElement("Model", T.Model);
            XElement Hours = new XElement("Hours", T.Hours);
            XElement MaxDist = new XElement("MaxDist", T.Hours);
            TesterRoot.Add(new XElement("Tester", TesterID, FamilyName, FirstName, Birthday, Gender, PhoneNum, Address, Experience, MaxTestAmt, Model, Hours, MaxDist));
            TesterRoot.Save(TesterPath);
        }
        public void DeleteTester(int TesterID)  
        {
            XElement TesterElement;
            TesterElement = (from p in TesterRoot.Elements()
                           where Convert.ToInt32(p.Element("TesterID").Value) == TesterID
                           select p).FirstOrDefault();
            if (TesterElement == null)
                throw new Exception("The dish dos'nt exsist in the system");
            TesterElement.Remove();
            TesterRoot.Save(TesterPath);

        }
        public IEnumerable<Tester> GetAllTesters(Func<Tester, bool> predicat = null)
        {
            IEnumerable<Tester> Testers;
            try
            {

                Testers = from p in TesterRoot.Elements()
                          select new Tester()
                          {
                              ID = Convert.ToInt32(p.Element("TesterID").Value),
                              FamilyName = (p.Element("FamilyName").Value).ToString(),
                              FirstName = (p.Element("FirstName").Value).ToString(),
                              Birthday = (p.Element("Birthday").Value).ToString(),
                              Gender = (p.Element("Gender").Value).ToString(),
                              PhoneNum = Convert.ToInt32(p.Element("PhoneNum").Value),
                              Address = (p.Element("Address").Value).ToString(),
                              Experience = Convert.ToInt32(p.Element("Address").Value),
                              MaxTestAmt = Convert.ToInt32(p.Element("MaxTestAmt").Value),
                              Model = (p.Element("Model").Value).ToString(),
                              Hours = (p.Element("Hours").Value).ToString(),
                              MaxDist = Convert.ToInt32(p.Element("MaxDist").Value),
                         };
                if (predicat != null)
                {
                    Testers = Testers.Where(predicat);

                }
            }
            catch
            {
                Testers = null;
            }
            return Testers;
        }


        public void UpdateTester(Tester T)
        {
            XElement TesterElement = (from p in TesterRoot.Elements()
                                    where Convert.ToInt32(p.Element("TesterID").Value) == T.ID
                                    select p).FirstOrDefault();
            TesterElement.Element("FamilyName").Value = T.FamilyName.ToString();
            TesterElement.Element("FirstName").Value = T.FirstName.ToString();
            TesterElement.Element("Birthday").Value = T.Birthday.ToString();
            TesterElement.Element("Gender").Value = T.Gender.ToString();
            TesterElement.Element("PhoneNum").Value = T.PhoneNum.ToString();
            TesterElement.Element("Address").Value = T.Address.ToString();
            TesterRoot.Save(TesterPath);
        }

        public Tester GetTester(int TesterID)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Trainee
        public bool FindTrainee(int TraineeID)
        {
            XElement TraineeElement;
            TraineeElement = (from p in TraineeRoot.Elements()
                            where Convert.ToInt32(p.Element("TraineeID").Value) == TraineeID
                            select p).FirstOrDefault();
            if (TraineeElement == null)
                return false;
            return true;
        }

        public void AddTrainee(Trainee T)
        {
            if (FindTrainee(T.ID))
                throw new Exception("Trainee already exists in the system");
            XElement TraineeID = new XElement("TraineeID", T.ID);
            XElement FirstName = new XElement("FirstName", T.FirstName);
            XElement FamilyName = new XElement("FamilyName", T.FamilyName);
            XElement Gender = new XElement("Gender", T.Gender);
            XElement PhoneNum = new XElement("PhoneNum", T.PhoneNum);
            XElement Address = new XElement("Address", T.Address);
            XElement Birthday = new XElement("Birthday", T.Birthday);
            XElement Model = new XElement("Model", T.Model);
            XElement GearType = new XElement("Gear", T.GearType);
            XElement School = new XElement("School", T.School);
            XElement TeacherName = new XElement("TeacherName", T.TeacherName);
            XElement LessonAMT = new XElement("LessonAMT", T.LessonAMT);
            TraineeRoot.Add(new XElement("trainee", TraineeID, FirstName, FamilyName, Gender, PhoneNum, Address, Birthday, Model, GearType, School, TeacherName, LessonAMT));
            TraineeRoot.Save(TraineePath);
        }

        public void DeleteTrainee(int TraineeID)
        {
            XElement TraineeElement;
            TraineeElement = (from p in TraineeRoot.Elements()
                             where Convert.ToInt32(p.Element("TraineeID").Value) == TraineeID
                             select p).FirstOrDefault();
            if (TraineeElement == null)
                throw new Exception("Trainee doesn't exist in system");
            TraineeElement.Remove();
            TraineeRoot.Save(TraineePath);
        }

        public void UpdateTrainee(Trainee T)
        {

            XElement branchElement = (from p in TraineeRoot.Elements()
                                      where Convert.ToInt32(p.Element("TraineeID").Value) == T.ID
                                      select p).FirstOrDefault();
            branchElement.Element("FirstName").Value = T.FirstName;
            branchElement.Element("FamilyName").Value = T.FamilyName;
            branchElement.Element("Gender").Value = T.Gender;
            branchElement.Element("PhoneNum").Value = (T.PhoneNum).ToString();
            branchElement.Element("Address").Value = T.Address.ToString();
            branchElement.Element("Birthday").Value = T.Birthday.ToString();
            branchElement.Element("Model").Value = T.Model;
            branchElement.Element("GearType").Value = T.GearType;
            branchElement.Element("School").Value = T.School;
            branchElement.Element("TeacherName").Value = T.School;
            branchElement.Element("LessonAMT").Value = (T.LessonAMT).ToString();
            TraineeRoot.Save(TraineePath);
        }

        public Trainee GetTrainee(int TraineeID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Trainee> GetAllTrainees(Func<Trainee, bool> predicat = null)
        {
            IEnumerable<Trainee> Trainees;
            try
            {
                Trainees = from p in TraineeRoot.Elements()
                           select new Trainee()
                           {
                               ID = Convert.ToInt32(p.Element("TraineeID").Value),
                               FirstName = p.Element("FamilyName").Value,
                               FamilyName = p.Element("FamilyName").Value,
                               Gender = p.Element("Gender").Value,
                               PhoneNum = Convert.ToInt32(p.Element("PhoneNum").Value),
                               Address = p.Element("Address").Value,
                               Birthday = p.Element("Birthday").Value,
                               Model = p.Element("Model").Value,
                               GearType = p.Element("Geartype").Value,
                               School = p.Element("School").Value,
                               TeacherName = p.Element("TeacherName").Value,
                               LessonAMT = Convert.ToInt32(p.Element("LessonAmt").Value),
                           };
                if (predicat != null)
                {
                    Trainees = Trainees.Where(predicat);

                }
            }
            catch
            {
                Trainees = null;
            }
            return Trainees;
        }
        #endregion

        #region Lists

        public List<Tester> GetListTesters()
        {
            throw new NotImplementedException();
        }

        public List<Trainee> GetListTrainees()
        {
            throw new NotImplementedException();
        }

        public List<Test> GetListTest()
        {
            throw new NotImplementedException();
        }

        #endregion


    }
}
