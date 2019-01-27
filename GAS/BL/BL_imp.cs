using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BL
{
    public class BL_imp : IBL
    {
        Idal dal = FactoryDalXml.getdal();
        static int maxPrice = 500;
        #region Tester
        public void AddTester(Tester T)
        {
            dal.AddTester(T);
        }
        public void DeleteTester(int TesterID)
        {
            dal.DeleteTester(TesterID);
        }
        public void UpdateTester(Tester T)
        {
            dal.UpdateTester(T);
        }
        public IEnumerable<Tester> GetAllTester(Func<Tester, bool> predicat = null)
        {
            return dal.GetAllTesters(predicat);
        }
        #endregion
        #region Trainee
        public void AddTrainee(Trainee T)
        {
            dal.AddTrainee(T);
        }
        public void DeleteTrainee(int TraineeID)
        {
            dal.DeleteTrainee(TraineeID);
        }
        public void UpdateTrainee(Trainee T)
        {
            dal.UpdateTrainee(T);
        }

        public Trainee GetTrainee(int TraineeID)
        {
            return dal.GetTrainee(TraineeID);
        }
        public IEnumerable<Trainee> GetAllTrainee(Func<Trainee, bool> predicate = null)
        {
            return dal.GetAllTrainees(predicate);
        }
        #endregion
        #region Test
        public void AddTest(Test T)
        {
            dal.AddTest(T);
        }
        public void DeleteTest(int TestID)
        {
            dal.DeleteTest(TestID);
        }
        public void UpdateTest(Test T)
        {
            Test Tst = dal.GetAllTests(T => T.ID == T.ID).FirstOrDefault();
            if (T == null)
                throw new Exception("This test doesn't exist");
            dal.UpdateTest(T);
        }
        public IEnumerable<Test> GetAllTests(Func<Test, bool> predicat = null)
        {
            return dal.GetAllTests(predicat);
        }
        #endregion
        #region GetLists
        public List<Test> GetListTest()
        {
            return dal.GetListTests();
        }
        public List<Tester> GetListTesters()
        {
            return dal.GetListTesters();
        }
        public List<Trainee> GetListTrainees()
        {
            return dal.GetListTrainees();
        }
        #endregion



    }
}