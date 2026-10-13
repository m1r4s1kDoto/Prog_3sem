using Xunit;
using System;
using System.Collections.Generic;
using Lab1_Class;

namespace Lab1_Tests
{
    public class DeviceTests
    {
        [Fact]
        public void Test1_ValidObjectsAndPolymorphism()
        {
            //Проверка создания корректных объектов (массив и список)
            ComputerDevice[] devicesArray = new ComputerDevice[]
            {
                new ComputerDevice(),
                new ComputerDevice("Manufacturer", "Model"),
                new Keyboard("AULA", "F75", KeyboardType.Mechanical, true, 1023),
                new Lab1_Class.Monitor("XIAOMI", "G27QI", 27, 2560, 1440, 200, PanelType.IPS)
            };

            //Проверяем, что полиморфные методы возвращают непустые строки
            foreach (var device in devicesArray)
            {
                Assert.False(string.IsNullOrEmpty(device.GetName()));
                Assert.False(string.IsNullOrEmpty(device.ToString()));
            }

            List<ComputerDevice> deviceList = new List<ComputerDevice>
            {
                new Keyboard("Razer", "BlackWidow", KeyboardType.Mechanical, true, 1300),
                new Lab1_Class.Monitor("MSI", "Optix", 24.5, 1920, 1080, 240, PanelType.VA)
            };

            Assert.Equal(2, deviceList.Count);
        }

        [Fact]
        public void Test2_Validation_LongManufacturer_ThrowsException()
        {
            //Проверка: слишком длинный производитель вызывает исключение
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var badDevice = new ComputerDevice("ОченьДлинноеИмяПроизводителяКотороеПревышаетЛимитВТридцатьСимволов", "Model");
            });
        }

        [Fact]
        public void Test3_Validation_EmptyModel_ThrowsException()
        {
            //Проверка: пустая модель вызывает исключение
            Assert.Throws<ArgumentNullException>(() =>
            {
                var badDevice2 = new ComputerDevice("Brand", "");
            });
        }

        [Fact]
        public void Test4_Validation_KeyboardWeight_ThrowsException()
        {
            //Проверка: слишком маленький вес клавиатуры
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var badKeyboard = new Keyboard("BadBrand", "BadModel", KeyboardType.Membrane, false, 50);
            });
        }

        [Fact]
        public void Test5_Validation_MonitorDiagonal_ThrowsException()
        {
            //Проверка: слишком большая диагональ монитора
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var badMonitor = new Lab1_Class.Monitor("Brand", "Model", 75.0, 1920, 1080, 144, PanelType.IPS);
            });
        }

        [Fact]
        public void Test6_Validation_MonitorRefreshRate_ThrowsException()
        {
            // Проверка: слишком низкая частота обновления монитора
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var badMonitorRefresh = new Lab1_Class.Monitor("Brand", "Model", 24.0, 1920, 1080, 30, PanelType.IPS);
            });
        }
    }
}