using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BL
{
    public interface IBL
    {
        #region Tester

        void AddTester(Tester T); //Add Tester
        void DeleteTester(int TesterID); //Delete Tester
        void UpdateTester(Tester T); //Update Tester
        IEnumerable<Tester> GetAllTester(Func<Tester, bool> predicat = null);

        #endregion

        #region Trainee

        void AddTrainee(Trainee T); //Add Trainee
        void DeleteTrainee(int TraineeID); //Delete Trainee
        void UpdateTrainee(Trainee T);  //Update Trainee
        Trainee GetTrainee(int numT);
        IEnumerable<Trainee> GetAllTrainee(Func<Trainee, bool> predicate = null);

        #endregion

        #region Test

        void AddTest(Test T); //Add Test
        void DeleteTest(int TestID); //Delete Test
        void UpdateTest(Test T);// Update Test
        IEnumerable<Test> GetAllTests(Func<Test, bool> predicate = null);

        #endregion








    }
}
        

