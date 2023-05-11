using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ProjectTest
{
    [TestClass]
    public class UnitTest1
    {
        private List<ReservationModel> _reservations;
        private ReservationLogic _reservationLogic;

        [TestInitialize]
        public void TestInitialize()
        {
            _reservations = new List<ReservationModel>
            {
                new ReservationModel { TableId = 1, Date = new DateTime(2023, 1, 1) },
                new ReservationModel { TableId = 2, Date = new DateTime(2023, 1, 2) }
            };

            _reservationLogic = new ReservationLogic(_reservations);
        }

        [TestMethod]
        public void CheckReservation_WhenReservationExists_ReturnsTrue()
        {
            var result = _reservationLogic.CheckReservation(1, new DateTime(2023, 1, 1));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CheckReservation_WhenReservationDoesNotExist_ReturnsFalse()
        {
            var result = _reservationLogic.CheckReservation(3, new DateTime(2023, 1, 3));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetById_WhenIdExists_ReturnsReservationModel()
        {
            var result = _reservationLogic.GetById(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.TableId);
        }

        [TestMethod]
        public void GetById_WhenIdDoesNotExist_ReturnsNull()
        {
            var result = _reservationLogic.GetById(3);

            Assert.IsNull(result);
        }
    }
}
